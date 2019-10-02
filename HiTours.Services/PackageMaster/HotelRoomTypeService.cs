// <copyright file="HotelRoomTypeService.cs" company="Luxury Travel Deals">
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
    public class HotelRoomTypeService : IHotelRoomTypeService
    {
        private readonly IRepository<PackageHotelRoomTypeModel> hotelroomType;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelRoomTypeService" /> class.
        /// </summary>
        /// <param name="hotelroomType">Type of the hotelroom.</param>
        public HotelRoomTypeService(IRepository<PackageHotelRoomTypeModel> hotelroomType)
        {
            this.hotelroomType = hotelroomType;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="hotelroomType">Type of the hotelroom.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageHotelRoomTypeModel hotelroomType)
        {
            if (hotelroomType == null)
            {
                throw new ArgumentNullException("hotelroomType");
            }

            return await this.hotelroomType.InsertAsync(hotelroomType);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="hotelroomType">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageHotelRoomTypeModel hotelroomType)
        {
            if (hotelroomType == null)
            {
                throw new ArgumentNullException("city");
            }

            return await this.hotelroomType.UpdateAsync(hotelroomType);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageHotelRoomTypeModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await this.hotelroomType.Table.FirstOrDefaultAsync(m => m.Id == id);
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
            var query = this.hotelroomType.Table;

            return await this.hotelroomType.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="roomtypeId">The roomtype identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateAsync(string name, int roomtypeId)
        {
            var category =
              await this.hotelroomType.Table.FirstOrDefaultAsync(x => x.Id != roomtypeId && x.Name.ToLower().Trim() == name.ToLower().Trim());
            return category == null;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="roomtype">The travelstyle.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageHotelRoomTypeModel roomtype)
        {
            if (roomtype == null)
            {
                throw new ArgumentNullException("roomtype");
            }

            return await this.hotelroomType.DeleteAsync(roomtype);
        }
    }
}
