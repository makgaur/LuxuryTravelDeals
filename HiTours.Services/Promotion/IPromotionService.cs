// <copyright file="IPromotionService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;

    /// <summary>
    /// IPackageService
    /// </summary>
    public interface IPromotionService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="code">The Coupon Code.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<PromotionModel> GetPromotionByCode(string code);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllPromotionAsync(DataTableParameter model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="promotionId">The Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<PromotionModel> GetPromotionById(int promotionId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        Task<IList<Dropdown>> GetPromotionTypeItems();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<int?> UpdatePromotion(PromotionModel model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<int?> AddPromotion(PromotionModel model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealId">The Deal Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<DealsPromotionViewModel>> GetAllDealPromotionsById(int dealId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealPromotionId">The Deal Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DealsPromotion_RoomType> GetDealRoomPromotionByDealPromotionId(int dealPromotionId);
    }
}