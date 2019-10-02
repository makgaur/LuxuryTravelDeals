// <copyright file="CityService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CityService
    /// </summary>
    /// <seealso cref="ICityService" />
    public class CityService : ICityService
    {
        private readonly IRepository<PackageCityModel> cityRepository;
        private readonly IRepository<DealsPackageModel> packageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityService"/> class.
        /// </summary>
        /// <param name="cityRepository">The package image repository.</param>
        /// <param name="packageRepository">The package repository.</param>
        public CityService(IRepository<PackageCityModel> cityRepository, IRepository<DealsPackageModel> packageRepository)
        {
            this.cityRepository = cityRepository;
            this.packageRepository = packageRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="city">The category.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageCityModel city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("category");
            }

            return await this.cityRepository.InsertAsync(city);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="city">The category.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageCityModel city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.cityRepository.UpdateAsync(city);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="cityId">The state identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageCityModel> GetByIdAsync(int cityId)
        {
            if (cityId == 0)
            {
                return null;
            }

            return await this.cityRepository.Table.FirstOrDefaultAsync(m => m.Id == cityId);
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
            var query = from ct in this.cityRepository.Table
                        select ct;
                var list = await this.cityRepository.ToPagedListAsync(query, model);
               return list;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<Tuple<PackageCityModel, int>> GetCityWiseDealsCount()
        {
            var dealQuery = this.packageRepository.Table.Include(x => x.DealsDestinationModels).Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2));
            var dealsCities = dealQuery.SelectMany(x => x.DealsDestinationModels.Where(y => y.Country == 61).Select(y => y.City)).Distinct().ToList();
            List<Tuple<PackageCityModel, int>> result = new List<Tuple<PackageCityModel, int>>();
            if (dealsCities.Count > 0)
            {
                foreach (var item in dealsCities)
                {
                    result.Add(new Tuple<PackageCityModel, int>(this.cityRepository.Table.Where(x => x.Id == item).FirstOrDefault(), dealQuery.Where(x => x.DealsDestinationModels.Select(y => y.City).Contains(item)).Count()));
                }
            }

            return result.OrderByDescending(x => x.Item2).ToList();
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateAsync(string name, int cityId)
        {
            var city = await this.cityRepository.Table.FirstOrDefaultAsync(x => x.Id != cityId && x.Name.ToLower().Trim() == name.ToLower().Trim());
            return city == null;
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateCityCodeAsync(string code, int cityId)
        {
            var city = await this.cityRepository.Table.FirstOrDefaultAsync(x => x.Id != cityId && x.Code.ToLower().Trim() == code.ToLower().Trim());
            return city == null;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageCityModel city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.cityRepository.DeleteAsync(city);
        }
    }
}