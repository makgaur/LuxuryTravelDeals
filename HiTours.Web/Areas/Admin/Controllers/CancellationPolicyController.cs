// <copyright file="CancellationPolicyController.cs" company="Luxury Travel Deals">
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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CityController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class CancellationPolicyController : AdminController
    {
        private readonly IHotelierService hotelierServices;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly IVendorService vendorService;
        private readonly ICancellationService cancellationService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancellationPolicyController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="cancellationService">Cancellation Service</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="hotel">Type of the hoteroom.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="vendorService">Vendor Service</param>
        public CancellationPolicyController(IConfiguration configuration, ICancellationService cancellationService, IHostingEnvironment hostingEnvironment, IMapper mapper, IHotelierService hotel, IMasterService masterService, IVendorService vendorService)
            : base(mapper, configuration)
        {
            this.cancellationService = cancellationService;
            this.hostingEnvironment = hostingEnvironment;
            this.vendorService = vendorService;
            this.hotelierServices = hotel;
            this.masterService = masterService;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="dealType">Deal Type</param>
        public IActionResult Index(int? dealType)
        {
            CancellationPolicyViewModel model = new CancellationPolicyViewModel
            {
                Id = 0,
                DealType = dealType != null && dealType != 0 ? dealType : 0
            };
            return this.View(model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="dealType">Deal Type</param>
        public async Task<IActionResult> GetCancellationPolicyByDealType(int dealType)
        {
            List<CancellationPolicyViewModel> model = await this.cancellationService.GetCancellationPolicyByDealType(dealType);
            foreach (var item in model)
            {
                item.MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList();
            }

            this.ViewBag.DealType = dealType;
            return this.PartialView("_CancellationPolicy", model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="dealType">Deal Type</param>
        public async Task<IActionResult> AddCancellationPolicyRow(int dealType)
        {
            CancellationPolicyViewModel model = new CancellationPolicyViewModel
            {
                Id = 0,
                IsDeleted = false,
                DealType = dealType,
                MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList()
            };
            this.ViewBag.DealType = dealType;
            return this.PartialView("_CancellationPolicyRow", model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="model">Model</param>
        public async Task<IActionResult> AddCancellationPolicies(List<CancellationPolicyViewModel> model)
        {
            if (this.ModelState.IsValid)
            {
                if (model.Count > 0)
                {
                    int dealTypeId = Convert.ToInt32(model[0].DealType);
                    foreach (var item in model)
                    {
                        if (item.IsDeleted)
                        {
                            if (item.Id > 0)
                            {
                                var record = await this.cancellationService.GetCancellationPolicyById(item.Id);
                                record.IsDeleted = true;
                                await this.cancellationService.UpdateCancellationPolicy(record);
                            }
                        }
                        else if (item.Id > 0)
                        {
                            ////Update
                            var record = this.Mapper.Map<CancellationPolicyModel>(item);
                            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.cancellationService.UpdateCancellationPolicy(record);
                        }
                        else
                        {
                            var record = this.Mapper.Map<CancellationPolicyModel>(item);
                            record.IsActive = true;
                            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.cancellationService.AddCancellationPolicy(record);
                        }
                    }

                    this.ShowMessage("Successfully Updated", Enums.MessageType.Success);
                    return this.RedirectToAction("Index", new { @dealType= dealTypeId });
                }
            }

            return this.RedirectToAction("Index");
        }
    }
}