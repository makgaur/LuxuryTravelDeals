// <copyright file="IInventoryService.cs" company="Luxury Travel Deals">
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
    public interface IInventoryService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="packageId">The Package Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetPackageInventoryAsync(DataTableParameter model, Guid packageId);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Room Configuration Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> AddAsync(RoomInventoryModel model);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Room Configuration Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Room Configuration</exception>
        Task<int> UpdateAsync(RoomInventoryModel model);

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
        /// <param name="id">The Room Configuration Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> DeleteInventoryAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomInventoryId">Room Configuration ID</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<RoomInventoryModel> GetByIdAsync(int roomInventoryId);
    }
}