// <copyright file="PromotionController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Promotion Controller
    /// </summary>
    /// <seealso cref="HiTours.Web.BaseController" />
    public class PromotionController : BaseController
    {
        private readonly IMasterService masterService;

        private readonly IPromotionService promotionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionController"/> class.
        /// </summary>
        /// <param name="promotionService">Promotion Services</param>
        /// <param name="masterService">The master service.</param>
        public PromotionController(IPromotionService promotionService, IMasterService masterService)
        {
            this.masterService = masterService;
            this.promotionService = promotionService;
        }

        /////// <summary>
        /////// Indexes this instance.
        /////// </summary>
        /////// <param name="type">The Promotion type.</param>
        /////// <param name="promoUrl">The Promotion URL.</param>
        /////// <returns>
        /////// view
        /////// </returns>
        ////[Route("promotion/{type}/{promoUrl}")]
        ////public async Task<IActionResult> Index(string type, string promoUrl)
        ////{
        ////    PromotionLandingPageViewModel model = await this.promotionService.GetPromotionAsync(type, promoUrl);
        ////    return this.View(model);
        ////}
    }
}