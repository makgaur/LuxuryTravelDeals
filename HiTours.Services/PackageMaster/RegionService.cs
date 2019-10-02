// <copyright file="RegionService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="HiTours.Services.IRegionService" />
    public class RegionService : IRegionService
    {
        private readonly IRepository<PackageRegionModel> packageRegion;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionService" /> class.
        /// </summary>
        /// <param name="packageRegion">The package region.</param>
        public RegionService(IRepository<PackageRegionModel> packageRegion)
        {
            this.packageRegion = packageRegion;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="regionModel">The region model.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageRegionModel regionModel)
        {
            if (regionModel == null)
            {
                throw new ArgumentNullException("region");
            }

            return await this.packageRegion.InsertAsync(regionModel);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="regionModel">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageRegionModel regionModel)
        {
            if (regionModel == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.packageRegion.UpdateAsync(regionModel);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageRegionModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await this.packageRegion.Table.FirstOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageRegionModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            return await this.packageRegion.DeleteAsync(category);
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
            var query = this.packageRegion.Table;

            return await this.packageRegion.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryid">The categoryid.</param>
        /// <returns>GetDuplicateAsync</returns>
        public async Task<bool> IsDuplicateAsync(string name, int categoryid)
        {
            var category =
              await this.packageRegion.Table.FirstOrDefaultAsync(x => x.Id != categoryid && x.Name.ToLower().Trim() == name.ToLower().Trim());
            return category == null;
        }
    }
}
