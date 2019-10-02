// <copyright file="IHotelRoomTypeService.cs" company="Luxury Travel Deals">
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
    public interface IHotelRoomTypeService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="hotelroomType">Type of the hotelroom.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(PackageHotelRoomTypeModel hotelroomType);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="hotelroomType">Type of the hotelroom.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(PackageHotelRoomTypeModel hotelroomType);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<PackageHotelRoomTypeModel> GetByIdAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="roomtypeId">The roomtype identifier.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int roomtypeId);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="roomtype">The roomtype.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(PackageHotelRoomTypeModel roomtype);
    }
}
