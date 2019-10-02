// <copyright file="SearchPageController.cs" company="Luxury Travel Deals">
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
    public class SearchPageController : AdminController
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHomePageService homePageService;
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPageController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        public SearchPageController(IConfiguration configuration, IMasterService masterService, IHomePageService homePageService, IHostingEnvironment hostingEnvironment, IMapper mapper)
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
        public IActionResult Recommendations([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                ////var result = await this.homePageService.GetAllSpecialDealsAsync(model);
                return this.Json(new object { });
            }

            return this.View();
        }
    }
}
