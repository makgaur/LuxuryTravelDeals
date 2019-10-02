// <copyright file="MasterController.cs" company="Luxury Travel Deals">
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
    /// UploadBannersController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class MasterController : AdminController
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterController" /> class.
        /// </summary>
        /// <param name="configuration">Config</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="vendorService">Vendor Service</param>
        /// <param name="masterService">Master Service</param>
        public MasterController(IConfiguration configuration, IMapper mapper, IVendorService vendorService, IMasterService masterService)
           : base(mapper, configuration)
        {
            this.masterService = masterService;
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <returns>viewmodel</returns>
        [HttpGet]
        public IActionResult Salutation()
        {
            return this.PartialView("_Salutation");
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <returns>viewmodel</returns>
        [HttpGet]
        public IActionResult SalutationMaster()
        {
            return this.View();
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <returns>viewmodel</returns>
        /// <param name="name">The Name.</param>
        /// <param name="mode">The Mode.</param>
        [HttpPost]
        public async Task<IActionResult> Salutation(string name, string mode)
        {
            await this.masterService.AddSalutation(new SalutationModel { Name = name });
            if (mode == "master")
            {
                this.ShowMessage("Saved");
                return this.RedirectToAction("Index", "Dashboard", new { @area = "Admin" });
            }

            return this.Json("success");
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <returns>viewmodel</returns>
        [HttpGet]
        public IActionResult Designation()
        {
            return this.PartialView("_Designation");
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <returns>viewmodel</returns>
        [HttpGet]
        public IActionResult DesignationMaster()
        {
            return this.View();
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <returns>viewmodel</returns>
        /// <param name="name">The Name.</param>
        /// <param name="mode">The Mode.</param>
        [HttpPost]
        public async Task<IActionResult> Designation(string name, string mode)
        {
            await this.masterService.AddDesignation(new DesignationModel { Name = name });
            if (mode == "master")
            {
                this.ShowMessage("Saved");
                return this.RedirectToAction("Index", "Dashboard", new { @area = "Admin" });
            }

            return this.Json("success");
        }
    }
}