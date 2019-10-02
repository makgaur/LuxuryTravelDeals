// <copyright file="HolidayMenuService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// TravelStyleService
    /// </summary>
    /// <seealso cref="HiTours.Services.ITravelStyleService" />
    public class HolidayMenuService : IHolidayMenuService
    {
        private readonly IRepository<PackageHolidayMenuModel> holidayRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayMenuService" /> class.
        /// </summary>
        /// <param name="holidayRepository">The holiday repository.</param>
        public HolidayMenuService(IRepository<PackageHolidayMenuModel> holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="stylemodel">The stylemodel.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageHolidayMenuModel stylemodel)
        {
            if (stylemodel == null)
            {
                throw new ArgumentNullException("holidaymenu");
            }

            return await this.holidayRepository.InsertAsync(stylemodel);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="holidaymenu">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageHolidayMenuModel holidaymenu)
        {
            if (holidaymenu == null)
            {
                throw new ArgumentNullException("holidaymenu");
            }

            return await this.holidayRepository.UpdateAsync(holidaymenu);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageHolidayMenuModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await this.holidayRepository.Table.FirstOrDefaultAsync(m => m.Id == id);
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
            var query = this.holidayRepository.Table;

            return await this.holidayRepository.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tarvelstyleId">The tarvelstyle identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateAsync(string name, int tarvelstyleId)
        {
            var city = await this.holidayRepository.Table.FirstOrDefaultAsync(x => x.Id != tarvelstyleId && x.Name.ToLower().Trim() == name.ToLower().Trim());
            return city == null;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="travelstyle">The travelstyle.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageHolidayMenuModel travelstyle)
        {
            if (travelstyle == null)
            {
                throw new ArgumentNullException("holidaymenu");
            }

            return await this.holidayRepository.DeleteAsync(travelstyle);
        }
    }
}
