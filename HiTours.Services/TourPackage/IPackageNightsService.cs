// <copyright file="IPackageNightsService.cs" company="Luxury Travel Deals">
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
    public interface IPackageNightsService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The tourpackage night.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(TourPackageNightModel tourpackageNight);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The tourpackage night.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(TourPackageNightModel tourpackageNight);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<TourPackageNightModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The tour package.</param>
        /// <returns>
        /// Delete
        /// </returns>
        Task<int> DeleteAsync(TourPackageNightModel tourPackage);
    }
}
