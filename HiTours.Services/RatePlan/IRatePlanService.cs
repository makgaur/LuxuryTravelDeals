// <copyright file="IRatePlanService.cs" company="Luxury Travel Deals">
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
    public interface IRatePlanService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="packageId">The Package Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetRatePlanAsync(DataTableParameter model, Guid packageId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="ratePlanId">Rate Plan ID</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<RatePlanModel> GetByIdAsync(int ratePlanId);

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomId">The Room Id</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetRoomConfigurationListAsync(string search, short page, int? roomId, Guid packageId);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Rate Plan Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Room Configuration</exception>
        Task<int> UpdateAsync(RatePlanModel model);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Rate Plan Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> AddAsync(RatePlanModel model);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The Room Configuration Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> DeleteRatePlanAsync(int id);

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetieId">The Ameneties Id</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetAmenitiesListAsync(string search, short page, int? amenetieId);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <param name="amenetieId">Ameneties Id</param>
        /// <returns>
        /// Add Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> AddRatePlanAmenetiesAsync(int ratePlanId, int amenetieId);
    }
}