// <copyright file="CompanySettingService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CompanySettingService
    /// </summary>
    /// <seealso cref="HiTours.Services.ICompanySettingService" />
    public class CompanySettingService : ICompanySettingService
    {
        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<CompanySettingModel> companysettingRepository;

        private readonly IRepository<Dropdown> dropdownRespository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanySettingService" /> class.
        /// </summary>
        /// <param name="companysettingRepository">The CompanySetting repository.</param>
        /// <param name="dropdownRepository">The dropdown repository.</param>
        public CompanySettingService(IRepository<CompanySettingModel> companysettingRepository, IRepository<Dropdown> dropdownRepository)
        {
            this.companysettingRepository = companysettingRepository;
            this.dropdownRespository = dropdownRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="companysetting">The model.</param>
        /// <returns>Insert Company Setting</returns>
        public async Task<int> InsertAsync(CompanySettingModel companysetting)
        {
            try
            {
                return await this.companysettingRepository.InsertAsync(companysetting);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetCompanyMarkup
        /// </returns>
        public async Task<CompanySettingModel> GetCompanyMarkup()
        {
            return await this.companysettingRepository.Table.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="companysetting">The category.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(CompanySettingModel companysetting)
        {
            if (companysetting == null)
            {
                throw new ArgumentNullException("companysetting");
            }

            return await this.companysettingRepository.UpdateAsync(companysetting);
        }
    }
}
