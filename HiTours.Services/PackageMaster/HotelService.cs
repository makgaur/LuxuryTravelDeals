// <copyright file="HotelService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// TravelStyleService
    /// </summary>
    /// <seealso cref="HiTours.Services.IRegionService" />
    public class HotelService : IHotelService
    {
        private readonly IRepository<PackageHotelModel> hotel;
        private readonly IRepository<PackageHotelRoomTypeDescModel> hotelRoomType;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelService" /> class.
        /// </summary>
        /// <param name="hotel">Type of the hotelroom.</param>
        /// <param name="hotelRoomType">Type of the hotel room.</param>
        public HotelService(IRepository<PackageHotelModel> hotel, IRepository<PackageHotelRoomTypeDescModel> hotelRoomType)
        {
            this.hotel = hotel;
            this.hotelRoomType = hotelRoomType;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="hotel">The hotel.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageHotelModel hotel)
        {
            try
            {
                if (hotel == null)
                {
                    throw new ArgumentNullException("hotel");
                }

                return await this.hotel.InsertAsync(hotel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="hotel">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageHotelModel hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException("city");
            }

            this.hotel.UpdateCompleteGraph(hotel, hotel.Id);
            return await this.hotel.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageHotelModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await this.hotel.Table.Include(x => x.HotelRoomTypeDesc).FirstOrDefaultAsync(m => m.Id == id);
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
            var query = this.hotel.Table.Where(x => !x.IsDeleted);

            return await this.hotel.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>fff</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.hotel.SaveChangesAsync();
        }

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void RemoveToContextHotelRoomType(PackageHotelRoomTypeDescModel entity)
        {
            this.hotelRoomType.RemoveToContext(entity);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="hotel">The hotel.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageHotelModel hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException("category");
            }

            hotel.IsDeleted = !hotel.IsDeleted;
            this.hotel.UpdateCompleteGraph(hotel, hotel.Id);
            return await this.hotel.SaveChangesAsync();
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="area">The area.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="hotelid">The roomtype identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateAsync(string name, string area, int cityId, int hotelid)
        {
            var hotel =
              await this.hotel.Table.FirstOrDefaultAsync(x => x.Id != hotelid && x.Name.ToLower().Trim() == name.ToLower().Trim() && x.CityId == cityId && x.Area.ToLower().Trim() == area.ToLower().Trim());
            return hotel == null;
        }
    }
}