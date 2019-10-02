// <copyright file="IHotelService.cs" company="Luxury Travel Deals">
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
    public interface IHotelService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="hotel">The hotel.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(PackageHotelModel hotel);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="hotel">The hotel.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(PackageHotelModel hotel);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<PackageHotelModel> GetByIdAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>int </returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveToContextHotelRoomType(PackageHotelRoomTypeDescModel entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(PackageHotelModel category);

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="area">The area.</param>
        /// <param name="cityId">The cityId.</param>
        /// <param name="hotelid">The hotelid.</param>
        /// <returns>
        /// IsDuplicateAsync
        /// </returns>
        Task<bool> IsDuplicateAsync(string name, string area, int cityId, int hotelid);
    }
}
