// <copyright file="CountryController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// PackageCountryController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class CountryController : AdminController
    {
        private readonly ICountryService country;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly IHostingEnvironment hostingEnviorment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryController" /> class.
        /// </summary>
        /// <param name="configuration">Config</param>
        /// <param name="hostingEnviorment">Hosting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="country">The country.</param>
        /// <param name="masterService">The master service.</param>
        public CountryController(IConfiguration configuration, IHostingEnvironment hostingEnviorment, IMapper mapper, ICountryService country, IMasterService masterService)
            : base(mapper, configuration)
        {
            this.hostingEnviorment = hostingEnviorment;
            this.country = country;
            this.masterService = masterService;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.country.GetAllAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Manage(int id)
        {
            var model = new PackageCountryViewModel();
            if (id > 0)
            {
                var record = await this.country.GetByIdAsync(id);
                //// model.States = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, model.CountryId, model.CountryId.ToString())).ToSelectList();
                model = this.Mapper.Map<PackageCountryViewModel>(record);
                model.RegionList = (await this.masterService.GetPackageRegionListAsync(string.Empty, 1, record.RegionId)).ToSelectList();
                model.CountryList = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, record.Id)).ToSelectList();
            }

            return this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageCountryViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    if (model.ImageFile != null)
                    {
                        model = await this.UploadOnly("images/Country", model);
                    }

                    var record = this.Mapper.Map<PackageCountryModel>(model);
                    record.SetAuditInfo(0);
                    await this.country.UpdateAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "country", action = "Index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }

        /// <summary>
        /// Changes the active status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChangeActiveStatus</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            var country = await this.country.GetByIdAsync(id);
            if (country == null)
            {
                return this.NotFound();
            }

            country.IsActive = !country.IsActive;
            await this.country.UpdateAsync(country);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Country", action = "index", area = Constants.AreaAdmin });
            }
        }

        private async Task<PackageCountryViewModel> UploadOnly(string folder, PackageCountryViewModel model)
        {
            model.Image = await this.UploadImageBlobStorage(folder, model.ImageFile);
            return model;
        }
    }
}
