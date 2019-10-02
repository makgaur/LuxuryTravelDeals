// <copyright file="AreaService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CityService
    /// </summary>
    /// <seealso cref="IAreaService" />
    public class AreaService : IAreaService
    {
        private readonly IRepository<PackageAreaModel> areaRepository;
        private readonly IRepository<PackageAreaViewModel> areaVMRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaService"/> class.
        /// </summary>
        /// <param name="areaRepository">The package area repository.</param>
        /// <param name="areaVMRepository">Area View Model</param>
        public AreaService(IRepository<PackageAreaModel> areaRepository, IRepository<PackageAreaViewModel> areaVMRepository)
        {
            this.areaVMRepository = areaVMRepository;
            this.areaRepository = areaRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="areaModel">The category.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageAreaModel areaModel)
        {
            if (areaModel == null)
            {
                throw new ArgumentNullException("category");
            }

            try
            {
                return await this.areaRepository.InsertAsync(areaModel);
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="area">The category.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageAreaModel area)
        {
            if (area == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.areaRepository.UpdateAsync(area);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="areaId">The state identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageAreaModel> GetByIdAsync(int areaId)
        {
            if (areaId == 0)
            {
                return null;
            }

            try
            {
                return await this.areaRepository.Table.Where(m => m.Id == areaId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new PackageAreaModel { IsActive = true };
            }
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
                var query = this.areaRepository.Table.Select(x => new PackageAreaViewModel
                            {
                                CityName = x.PackageCityModel.Name,
                                Name = x.Name,
                                Id = x.Id,
                                IsActive = x.IsActive
                            });
                var list = await this.areaVMRepository.ToPagedListAsync(query, model);
                return list;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        /////// <summary>
        /////// Determines whether [is duplicate asyc] [the specified name].
        /////// </summary>
        /////// <param name="name">The name.</param>
        /////// <param name="cityId">The city identifier.</param>
        /////// <returns>
        /////// GetDuplicateAsync
        /////// </returns>
        ////public async Task<bool> IsDuplicateAsync(string name, int cityId)
        ////{
        ////    var city = await this.cityRepository.Table.FirstOrDefaultAsync(x => x.Id != cityId && x.Name.ToLower().Trim() == name.ToLower().Trim());
        ////    return city == null;
        ////}

        /////// <summary>
        /////// Determines whether [is duplicate asyc] [the specified name].
        /////// </summary>
        /////// <param name="code">The code.</param>
        /////// <param name="cityId">The city identifier.</param>
        /////// <returns>
        /////// GetDuplicateAsync
        /////// </returns>
        ////public async Task<bool> IsDuplicateCityCodeAsync(string code, int cityId)
        ////{
        ////    var city = await this.cityRepository.Table.FirstOrDefaultAsync(x => x.Id != cityId && x.Code.ToLower().Trim() == code.ToLower().Trim());
        ////    return city == null;
        ////}

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="areaModel">The area.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageAreaModel areaModel)
        {
            if (areaModel == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.areaRepository.DeleteAsync(areaModel);
        }
    }
}