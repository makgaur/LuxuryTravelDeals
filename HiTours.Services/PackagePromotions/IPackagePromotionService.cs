// <copyright file="IPackagePromotionService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;

    /// <summary>
    /// IApplicationUserService
    /// </summary>
    public interface IPackagePromotionService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="promotionId">The Cancellation Policy identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        Task<PackagePromotionsModel> GetByIdAsync(int promotionId);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Currency Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Package Currency</exception>
        Task<int> UpdateAsync(PackagePromotionsModel model);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Vendor Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> AddAsync(PackagePromotionsModel model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">TThe Package Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllPackagePromotionsListAsync(DataTableParameter model, Guid id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">TThe Package Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<int>> GetAllPackagePromotionsAsync(Guid id);

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="promotionId">The currency identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetDropdownPromotionListAsync(string search, short page, List<int> promotionId);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Vendor Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> AddPackagePromotionAsync(PackagePromotions_PackageModel model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="packageId">Package Id</param>
        /// <param name="promotionId">Promotion Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        bool CheckDuplicatePackagePromotion(Guid packageId, int promotionId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">TThe Package Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<int> DeletePackagePromotionRelRecord(int id);
    }
}
