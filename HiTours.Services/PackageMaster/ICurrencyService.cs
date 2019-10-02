// <copyright file="ICurrencyService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels;

    /// <summary>
    /// ICountryService
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="currency">The Currency Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Package Currency</exception>
        Task<int> UpdateAsync(CurrencyModel currency);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAllAsync</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="currencyId">The currency identifier.</param>
        /// <returns>GetByIdAsync</returns>
        Task<CurrencyModel> GetByIdAsync(int currencyId);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="currency">The Currency Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Package Currency</exception>
        Task<int> AddAsync(CurrencyModel currency);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="currency">The category.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<int> DeleteAsync(CurrencyModel currency);
    }
}
