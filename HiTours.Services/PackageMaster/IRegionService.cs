// <copyright file="IRegionService.cs" company="Luxury Travel Deals">
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
    /// IRegionService
    /// </summary>
    public interface IRegionService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(PackageRegionModel region);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(PackageRegionModel region);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<PackageRegionModel> GetByIdAsync(int id);

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
        /// <param name="categoryid">The categoryid.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int categoryid);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(PackageRegionModel category);
    }
}
