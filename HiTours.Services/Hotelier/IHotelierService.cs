// <copyright file="IHotelierService.cs" company="Luxury Travel Deals">
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

    /// <summary>
    /// Interface Hotelier Service
    /// </summary>
    /// <seealso cref="IDealService" />
    public interface IHotelierService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetHotels
        /// </returns>
        Task<DataTableResult> GetHotelsAsync(DataTableParameter model);

        /// <summary>
        /// Gets hotel information
        /// </summary>
        /// <param name="id">hotel identifier</param>
        /// <returns>hotel Info View Model</returns>
        Task<HotelierInformationModel> GetHotelierInfoAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierInformationAsync(HotelierInformationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateHotelierInformationAsync(HotelierInformationModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetHotelierPropertyTypeDropDownListAsync(string search, short page, int? id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>InformationModel</returns>
        Task<HotelierRoomConfigurationModel> GetHotelierRoomConfigByIdAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>InformationModel</returns>
        Task<List<HotelierRoomConfigurationModel>> GetAllHotelierRoomConfigByHotelIdAsync(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierRoomConfigAsync(HotelierRoomConfigurationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateHotelierRoomConfigAsync(HotelierRoomConfigurationModel model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetHotelierRoomCofigGrid(DataTableParameter model, int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<HotelierContentViewModel> GetHotelierContentByHotelIdAsync(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteHotelierRoomConfigAsync(int id);

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetieId">The Ameneties Id</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetAmenitiesListAsync(string search, short page, int[] amenetieId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="roomConfigId">Room Configuration Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteAllHotelierRoomAmetiesByRoomConfigId(int roomConfigId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierRoomAmenity(HotelierRoomAmenetiesModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<HotelierContentModel> GetHotelierContentByIdAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierContent(HotelierContentModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateHotelierContent(HotelierContentModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<List<HotelierImageModel>> GetHotelierImageByHotelId(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<List<HotelierRoomImageModel>> GetHotelierRoomImagesByHotelId(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Room Configuration Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteAllHotelierAmetiesByHotelId(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierAmenity(HotelierAmenitiesModel model);

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetieIds">The Ameneties Id</param>
        /// <param name="key">Key</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetHotelierAmenitiesListAsync(string search, short page, int[] amenetieIds, string key);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <param name="roomTypeId">Room Type Id</param>
        /// <returns>InformationModel</returns>
        Task<HotelierRoomConfigurationModel> GetHotelierRoomRecordByHotelIdAndRoomTypeId(int hotelId, int roomTypeId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="roomConfigId">Room Config Id</param>
        /// <returns>InformationModel</returns>
        Task<List<HotelierRoomImageModel>> GetRoomImageFromRoomConfigId(int roomConfigId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllHotelReviewsByHotelId(DataTableParameter model, int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<HotelierReviewModel> GetHotelReviewsById(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateHotelierReviewAsync(HotelierReviewModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierReviewAsync(HotelierReviewModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteHotelierReviewAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierRoomImageAsync(HotelierRoomImageModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateHotelierRoomImageAsync(HotelierRoomImageModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierImageAsync(HotelierImageModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateHotelierImageAsync(HotelierImageModel model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllHotelierPromotionAsync(DataTableParameter model, int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<HotelierPromotionModel> GetHotelPromotionById(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddHotelierPromotionAsync(HotelierPromotionModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateHotelierPromotionAsync(HotelierPromotionModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetActiveHoteliersForDeals(string search, short page, int? id);

        /// <summary>
        /// Delete Hotel Image
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>Model</returns>
        Task<int> DeleteHotelierImageAsync(int id);

        /// <summary>
        /// Delete Hotel Room Image
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>Model</returns>
        Task<int> DeleteHotelierRoomImageAsync(int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <param name="cities">Cities</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetActiveHoteliersInCities(string search, short page, int? id, List<int> cities);
    }
}
