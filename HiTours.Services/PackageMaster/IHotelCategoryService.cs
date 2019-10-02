// <copyright file="IHotelCategoryService.cs" company="Luxury Travel Deals">
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

    /// <summary>
    /// IRegionService
    /// </summary>
    public interface IHotelCategoryService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="hotelcategory">The hotelcategory.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(PackageHotelCategoryModel hotelcategory);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="hotelcategory">The hotelcategory.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(PackageHotelCategoryModel hotelcategory);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<PackageHotelCategoryModel> GetByIdAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="hotelcategory">The hotelcategory.</param>
        /// <returns>Delete</returns>
        Task<int> DeleteAsync(PackageHotelCategoryModel hotelcategory);

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int categoryId);
    }
}
