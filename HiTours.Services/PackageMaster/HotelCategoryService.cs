// <copyright file="HotelCategoryService.cs" company="Luxury Travel Deals">
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
    public class HotelCategoryService : IHotelCategoryService
    {
        private readonly IRepository<PackageHotelCategoryModel> hotelCategory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelCategoryService" /> class.
        /// </summary>
        /// <param name="hotelCategory">The hotel category.</param>
        public HotelCategoryService(IRepository<PackageHotelCategoryModel> hotelCategory)
        {
            this.hotelCategory = hotelCategory;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="category">The region model.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageHotelCategoryModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("region");
            }

            return await this.hotelCategory.InsertAsync(category);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="category">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageHotelCategoryModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.hotelCategory.UpdateAsync(category);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageHotelCategoryModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await this.hotelCategory.Table.FirstOrDefaultAsync(m => m.Id == id);
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
            var query = this.hotelCategory.Table;

            return await this.hotelCategory.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="hotelcategory">The category.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageHotelCategoryModel hotelcategory)
        {
            if (hotelcategory == null)
            {
                throw new ArgumentNullException("hotelcategory");
            }

            return await this.hotelCategory.DeleteAsync(hotelcategory);
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateAsync(string name, int categoryId)
        {
            var city = await this.hotelCategory.Table.FirstOrDefaultAsync(x => x.Id != categoryId && x.Name.ToLower().Trim() == name.ToLower().Trim());
            return city == null;
        }
    }
}
