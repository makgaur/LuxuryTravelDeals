// <copyright file="CompanySettingController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CompanySettingController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class CompanySettingController : AdminController
    {
        /// <summary>
        /// The category service
        /// </summary>
        private readonly ICompanySettingService companysettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanySettingController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="companysettingService">The category service.</param>
        public CompanySettingController(IConfiguration configuration, IMapper mapper, ICompanySettingService companysettingService)
            : base(mapper, configuration)
        {
            this.companysettingService = companysettingService;
        }

        /// <summary>
        /// Get FlightMarkup When it is there in the table
        /// </summary>
        /// <returns>CompanyView Model</returns>
        public async Task<ActionResult> Index()
        {
            CompanySettingViewModel viewModel = new CompanySettingViewModel();
            var record = await this.companysettingService.GetCompanyMarkup();
            if (record != null)
            {
                viewModel = this.Mapper.Map<CompanySettingViewModel>(record);
            }

            return this.View(viewModel);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Index(CompanySettingViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<CompanySettingModel>(model);
                if (model.Id == 0)
                {
                    record.CreatedDate = DateTime.Now;
                    record.UpdatedDate = DateTime.Now;
                    record.CreatedBy = 1;
                    record.UpdatedBy = 1;
                    await this.companysettingService.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    record.UpdatedDate = DateTime.Now;
                    await this.companysettingService.UpdateAsync(record);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "CompanySetting", action = "index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }
    }
}