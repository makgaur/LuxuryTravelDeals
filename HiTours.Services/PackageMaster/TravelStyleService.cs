// <copyright file="TravelStyleService.cs" company="Luxury Travel Deals">
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
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// TravelStyleService
    /// </summary>
    /// <seealso cref="HiTours.Services.ITravelStyleService" />
    public class TravelStyleService : ITravelStyleService
    {
        private readonly IRepository<PackageTravelStyleModel> travelSttyleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TravelStyleService" /> class.
        /// </summary>
        /// <param name="travelSttyleRepository">The travel sttyle repository.</param>
        public TravelStyleService(IRepository<PackageTravelStyleModel> travelSttyleRepository)
        {
            this.travelSttyleRepository = travelSttyleRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="stylemodel">The stylemodel.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageTravelStyleModel stylemodel)
        {
            if (stylemodel == null)
            {
                throw new ArgumentNullException("category");
            }

            return await this.travelSttyleRepository.InsertAsync(stylemodel);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="stylemodel">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageTravelStyleModel stylemodel)
        {
            if (stylemodel == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.travelSttyleRepository.UpdateAsync(stylemodel);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageTravelStyleModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await this.travelSttyleRepository.Table.FirstOrDefaultAsync(m => m.Id == id);
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
            var query = this.travelSttyleRepository.Table;

            return await this.travelSttyleRepository.ToPagedListAsync(query, model);
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
            var city = await this.travelSttyleRepository.Table.FirstOrDefaultAsync(x => x.Id != tarvelstyleId && x.Name.ToLower().Trim() == name.ToLower().Trim());
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
        public async Task<int> DeleteAsync(PackageTravelStyleModel travelstyle)
        {
            if (travelstyle == null)
            {
                throw new ArgumentNullException("category");
            }

            return await this.travelSttyleRepository.DeleteAsync(travelstyle);
        }
    }
}
