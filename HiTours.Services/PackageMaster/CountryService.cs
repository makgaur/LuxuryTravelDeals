// <copyright file="CountryService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CountryService
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly IRepository<PackageCountryModel> countryRepository;

        private readonly IRepository<PackageCountryViewModel> packagecountryRepository;
        private readonly IRepository<DealsPackageModel> packageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService" /> class.
        /// </summary>
        /// <param name="packageRepository">The Deal package Repository</param>
        /// <param name="countryRepository">The package image repository.</param>
        /// <param name="packagecountryRepository">The packagecountry repository.</param>
        public CountryService(IRepository<DealsPackageModel> packageRepository, IRepository<PackageCountryModel> countryRepository, IRepository<PackageCountryViewModel> packagecountryRepository)
        {
            this.packageRepository = packageRepository;
            this.countryRepository = countryRepository;
            this.packagecountryRepository = packagecountryRepository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllAsync(DataTableParameter model)
        {
            try
            {
                var query = this.countryRepository.Table.Include(r => r.PackageRegion).Select(x => new
                PackageCountryViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    RegionName = x.PackageRegion.Name,
                    Name = x.Name,
                    PhoneCode = x.PhoneCode,
                    IsActive = x.IsActive
                });
                var list = await this.packagecountryRepository.ToPagedListAsync(query, model);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<Tuple<PackageCountryModel, int>> GetCountryWiseDealsCount()
        {
            var dealQuery = this.packageRepository.Table.Include(x => x.DealsDestinationModels).Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2));
            var dealsCountries = dealQuery.SelectMany(x => x.DealsDestinationModels.Where(y => y.Country != Convert.ToInt16(61)).Select(y => y.Country)).Distinct().ToList();
            List<Tuple<PackageCountryModel, int>> result = new List<Tuple<PackageCountryModel, int>>();
            var countryRepo = this.countryRepository.Table;
            if (dealsCountries.Count > 0)
            {
                foreach (var item in dealsCountries)
                {
                    result.Add(new Tuple<PackageCountryModel, int>(countryRepo.Where(x => x.Id == item).FirstOrDefault(), dealQuery.Where(x => x.DealsDestinationModels.Select(y => y.Country).Contains(item)).Count()));
                }
            }

            return result.OrderByDescending(x => x.Item2).ToList();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="countryId">The state identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageCountryModel> GetByIdAsync(int countryId)
        {
            if (countryId == 0)
            {
                return null;
            }

            return await this.countryRepository.Table.FirstOrDefaultAsync(m => m.Id == countryId);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="country">The package image.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> UpdateAsync(PackageCountryModel country)
        {
            try
            {
                if (country == null)
                {
                    throw new ArgumentNullException("packageImage");
                }

                var record = await this.countryRepository.Table.FirstOrDefaultAsync(x => x.Id == country.Id);
                record.Description = country.Description;
                record.Id = record.Id;
                record.RegionId = country.RegionId;
                record.Name = country.Name;
                record.PhoneCode = country.PhoneCode;
                record.UpdatedDate = DateTime.Now;
                record.Image = country.Image;
                return await this.countryRepository.UpdateAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
