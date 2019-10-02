// <copyright file="ICityService.cs" company="Luxury Travel Deals">
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
    public interface ICityService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(PackageCityModel city);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(PackageCityModel city);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<PackageCityModel> GetByIdAsync(int cityId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<Tuple<PackageCityModel, int>> GetCityWiseDealsCount();

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
        /// <param name="cityId">The city identifier.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int cityId);

        /// <summary>
        /// Determines whether [is duplicate city code asynchronous] [the specified code].
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>IsDuplicateCityCodeAsync</returns>
        Task<bool> IsDuplicateCityCodeAsync(string code, int cityId);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(PackageCityModel city);
    }
}
