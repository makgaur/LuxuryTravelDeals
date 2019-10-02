// <copyright file="PromotionController.cs" company="Luxury Travel Deals">
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
    public class PromotionController : AdminController
    {
        private static Random random = new Random();
        private readonly IHotelierService hotelierServices;
        private readonly IMasterService masterService;
        private readonly IVendorService vendorService;
        private readonly IPromotionService promotionService;
        private readonly ICancellationService cancellationService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionController" /> class.
        /// </summary>
        /// <param name="configuration">Config</param>
        /// <param name="cancellationService">Cancellation Service</param>
        /// <param name="promotionService">Promotion Service</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="hotel">Type of the hoteroom.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="vendorService">Vendor Service</param>
        public PromotionController(IConfiguration configuration, ICancellationService cancellationService, IPromotionService promotionService, IHostingEnvironment hostingEnvironment, IMapper mapper, IHotelierService hotel, IMasterService masterService, IVendorService vendorService)
            : base(mapper, configuration)
        {
            this.cancellationService = cancellationService;
            this.promotionService = promotionService;
            this.hostingEnvironment = hostingEnvironment;
            this.vendorService = vendorService;
            this.hotelierServices = hotel;
            this.masterService = masterService;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="model">Model Binder</param>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.promotionService.GetAllPromotionAsync(model);
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="promotionId">Promotion Id</param>
        public async Task<IActionResult> Add(int promotionId)
        {
            PromotionViewModel model = new PromotionViewModel
            {
                Id = 0,
                IsActive = true,
                IsDeleted = false
            };
            if (promotionId > 0)
            {
                model = this.Mapper.Map<PromotionViewModel>(await this.promotionService.GetPromotionById(promotionId));
            }

            model.PromotionTypeItems = (await this.promotionService.GetPromotionTypeItems()).ToSelectList();
            model.MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList();
            return this.View(model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> AddBulkPromition()
        {
            BulkPromotionViewModel model = new BulkPromotionViewModel
            {
                PromotionTypeItems = (await this.promotionService.GetPromotionTypeItems()).ToSelectList(),
                MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList()
            };

            return this.View(model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="model">Promotion Id</param>
        [HttpPost]
        public async Task<IActionResult> AddBulkPromition(List<PromotionViewModel> model)
        {
            if (model.Count > 0)
            {
                foreach (var item in model)
                {
                    var record = this.Mapper.Map<PromotionModel>(item);
                    record.IsActive = true;
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.promotionService.AddPromotion(record);
                }

                this.ShowMessage("Saved Successfully");
                return this.View("Index");
            }

            return this.View("Index");
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="model">Promotion Id</param>
        [HttpPost]
        public async Task<IActionResult> GenerateBulkCoupons(BulkPromotionViewModel model)
        {
            string charSets = string.Empty;
            if (model.Characters)
            {
                charSets = charSets + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }

            if (model.Numbers)
            {
                charSets = charSets + "0123456789";
            }

            if (!model.Characters && !model.Numbers)
            {
                charSets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            }

            List<PromotionViewModel> models = new List<PromotionViewModel>();
            if (model.NoOfCoupons > 0)
            {
                var promoItem = (await this.cancellationService.GetMarginTypeItems()).ToSelectList().Where(x => x.Value == model.DiscountType.ToString()).FirstOrDefault();
                for (int i = 0; i < model.NoOfCoupons; i++)
                {
                    string couponCode = model.Preffix.ToUpper() + "-" + this.RandomString(4, charSets) + "-" + model.Postfix.ToString();
                    PromotionViewModel subModel = new PromotionViewModel
                    {
                        Id = 0,
                        CouponCode = couponCode,
                        DiscountType = model.DiscountType,
                        DiscountValue = model.DiscountValue,
                        IsActive = true,
                        IsDeleted = false,
                        MaxCount = model.MaxCount,
                        MaxDiscountFlat = model.MaxDiscountFlat,
                        Remark = "Bulk Coupon Generated on " + DateTime.Now.Date.ToString("dd/MM/yyyy"),
                        ValidityStart = model.ValidityStart,
                        ValidityEnd = model.ValidityEnd
                    };
                    models.Add(subModel);
                }
            }

            return this.PartialView(models);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="model">Promotion Id</param>
        [HttpPost]
        public async Task<IActionResult> Add(PromotionViewModel model)
        {
            var record = this.Mapper.Map<PromotionModel>(model);
            if (model.Id > 0)
            {
                ////Update
                record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.promotionService.UpdatePromotion(record);
                this.ShowMessage("Updated Successfully");
            }
            else
            {
                record.IsActive = true;
                record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.promotionService.AddPromotion(record);
                this.ShowMessage("Saved Successfully");
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="id">Promotion Id</param>
        public async Task<IActionResult> ChangeActiveStatus(int id)
        {
            var record = await this.promotionService.GetPromotionById(id);
            record.IsActive = !record.IsActive;
            await this.promotionService.UpdatePromotion(record);
            return this.Json(new { Status = true });
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="id">Promotion Id</param>
        public async Task<IActionResult> Delete(int id)
        {
            var record = await this.promotionService.GetPromotionById(id);
            record.IsDeleted = !record.IsDeleted;
            await this.promotionService.UpdatePromotion(record);
            return this.Json("success");
        }

        private string RandomString(int length, string charSet)
        {
            return new string(Enumerable.Repeat(charSet, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}