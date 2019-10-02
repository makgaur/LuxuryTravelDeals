// <copyright file="IProductService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using HiTours.ViewModels.Deals.Product.Hotel;
    using HiTours.ViewModels.Deals.Product.Tour;

    /// <summary>
    /// IDestinationService
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="address">Address</param> <param name="key">key</param>
        /// <returns>
        /// Latitude  and Longitude
        /// </returns>
        Tuple<decimal, decimal> GetLatLong(int id, string address, string key);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealId">Deal Id.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<int?> UpdateDealCounter(int dealId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="url">The model.</param><param name="key">The map api key.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<HotelProductViewModel> GetHotelDealByUrl(string url, string key);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="url">The model.</param><param name="key">The map api key.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<TourProductViewModel> GetTourDealByUrl(string url, string key);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <param name="nightId">Night Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<DealTourHotelInfoViewModel>> GetTourRatePlans(int dealId, int nightId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <param name="nightId">Night Id</param>
        /// <param name="inclusionId">Inclusion Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<DealRoomConfigurationModel>> GetHotelRoomConfiguration(int dealId, int nightId, int inclusionId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomConfigId">RoomConfig Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<DealsRatePlanModel>> GetDealRatePlanByRoomConfig(int roomConfigId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomTypeId">Room Type Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<PackageHotelRoomTypeModel> GetRoomTypeRecord(int roomTypeId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomTypeId">Room Type Id</param>
        /// <param name="inclusionId">Inclusion Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<string>> GetRoomAmeneties(int roomTypeId, int inclusionId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="nightId">Night Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<DealsRatePlanViewModel>> GetDealRatePlanByNightId(int nightId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="nightId">Night Id</param>
        /// <param name="bufferDays">Buffer Dayys</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<DealFixedDepartureDateViewModel>> GetDealFixedDepartureDates(int nightId, int bufferDays);
    }
}