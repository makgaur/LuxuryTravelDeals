// <copyright file="ICompanySettingService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// ICompanySetting
    /// </summary>
    public interface ICompanySettingService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="companysetting">The category.</param>
        /// <returns>InsertAsync</returns>
        Task<int> InsertAsync(CompanySettingModel companysetting);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="companysetting">The category.</param>
        /// <returns>UpdateAsync</returns>
        Task<int> UpdateAsync(CompanySettingModel companysetting);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>GetCompanyMarkup</returns>
        Task<CompanySettingModel> GetCompanyMarkup();
    }
}
