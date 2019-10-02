// <copyright file="HomePageController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// StateController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class HomePageController : AdminController
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHomePageService homePageService;
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        public HomePageController(IConfiguration configuration, IMasterService masterService, IHomePageService homePageService, IHostingEnvironment hostingEnvironment, IMapper mapper)
            : base(mapper, configuration)
        {
            this.masterService = masterService;
            this.homePageService = homePageService;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">Model Data Table</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Location([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.homePageService.GetAllSpecialDealsAsync(model);
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="id">Visa Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> ManageLocation(int id)
        {
            LocationDealsAddViewModel model = new LocationDealsAddViewModel();

            if (id > 0)
            {
                model = this.Mapper.Map<LocationDealsAddViewModel>(await this.homePageService.GetLocationDealByIdAsync(id));
                model.CityItems = model.City != null && model.City != 0 ? (await this.masterService.GetTourPackageCityByCounryIdorStateIdAsync(string.Empty, 1, model.Country, 0, (short)model.City)).ToSelectList() : new List<SelectListItem>();
                model.CountryItems = model.Country == 0 ? new List<SelectListItem>() : (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, model.Country)).ToSelectList();
            }
            else
            {
                model.Id = 0;
                model.IsActive = true;
            }

            return this.View(model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">Visa Id</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<IActionResult> ManageLocation(LocationDealsAddViewModel model)
        {
            var data = this.Mapper.Map<LocationDealModel>(model);
            if (data.Id > 0)
            {
                await this.homePageService.UpdateLocationDeal(data);
                this.ShowMessage("Updated Successfully");
            }
            else
            {
                await this.homePageService.AddLocationDeal(data);
                this.ShowMessage("Inserted Successfully");
            }

            return this.RedirectToAction("Location");
        }
    }
}
