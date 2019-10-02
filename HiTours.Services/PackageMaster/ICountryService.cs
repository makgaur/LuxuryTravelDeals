// <copyright file="ICountryService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// ICountryService
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns>UpdateAsync</returns>
        Task<int> UpdateAsync(PackageCountryModel country);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAllAsync</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>GetByIdAsync</returns>
        Task<PackageCountryModel> GetByIdAsync(int countryId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<Tuple<PackageCountryModel, int>> GetCountryWiseDealsCount();
    }
}
