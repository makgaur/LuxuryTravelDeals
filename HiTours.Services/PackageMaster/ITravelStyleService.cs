// <copyright file="ITravelStyleService.cs" company="Luxury Travel Deals">
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

    /// <summary>
    /// ITravelStyleService
    /// </summary>
    public interface ITravelStyleService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="travelstyle">The travelstyle.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(PackageTravelStyleModel travelstyle);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="travelstyle">The travelstyle.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(PackageTravelStyleModel travelstyle);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<PackageTravelStyleModel> GetByIdAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tarvelstyleId">The tarvelstyle identifier.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int tarvelstyleId);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="travelstyle">The travelstyle.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(PackageTravelStyleModel travelstyle);
    }
}
