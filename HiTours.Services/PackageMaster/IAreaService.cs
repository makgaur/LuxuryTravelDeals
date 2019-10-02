// <copyright file="IAreaService.cs" company="Luxury Travel Deals">
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
    /// ICityService
    /// </summary>
    public interface IAreaService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="areaModel">The category.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<int> InsertAsync(PackageAreaModel areaModel);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="area">The category.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<int> UpdateAsync(PackageAreaModel area);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="areaId">The state identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        Task<PackageAreaModel> GetByIdAsync(int areaId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="areaModel">The area.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<int> DeleteAsync(PackageAreaModel areaModel);
    }
}
