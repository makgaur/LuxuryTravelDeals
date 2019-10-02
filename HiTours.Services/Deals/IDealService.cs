// <copyright file="IDealService.cs" company="Luxury Travel Deals">
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
    using HiTours.ViewModels.Deals;

    /// <summary>
    /// IVendorService
    /// </summary>
    public interface IDealService
    {
        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsDepartureDatesModel>> GetAllDealDepartureRecordsByNightIdAsync(int nightId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="countryId">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<List<DealVisaModel>> GetAllVisaFromActiveDealsFromCountryId(short? countryId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dealType">Deal Type</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetDealsAsync(DataTableParameter model, int dealType);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="packageId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<DealsReviewModel>> GetDealReviewsByPackageId(int packageId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetDealsPromotionsAsync(DataTableParameter model, int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="ratePlanId">The Rate Plan Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="days">Days of Week</param>
        /// <returns>InformationModel</returns>
        Task<List<DealInventoryModel>> GetFilteredInventoryByRatePlanId(int ratePlanId, DateTime? startDate, DateTime? endDate, int[] days);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="dealId">Deal indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsRatePlanModel>> GetRatePlansByDealIdAsync(int dealId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="dealId">The Deal Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealsRatePlanModel>> GetRatePlansByDealIdForTourAsync(int dealId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inventoryRecord">Inventory Record</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealInventoryRecordAsync(DealInventoryModel inventoryRecord);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<DealsPackageModel> GetDealPackageAsync(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inventoryRecord">Inventory Record</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealInventoryRecordAsync(DealInventoryModel inventoryRecord);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="ratePlanId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealInventoryModel>> GetInventoryByRatePlanId(int ratePlanId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package ID</param>
        /// <returns>Deal package model</returns>
        Task<DealsNightModel> GetDealNightHotelByPackageId(int packageId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> UpdateDealNight(DealsNightModel model);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> AddDealsNightAsync(DealsNightModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealPromotionRoomTypeRecord(DealsPromotion_RoomType model);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="promotionId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<DealsPromotion_RoomType> GetDealPromotionRoomTypeRecord(int promotionId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealPromotionRoomTypeRecord(DealsPromotion_RoomType model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<int> DeletePromotionRoomTypeRecord(DealsPromotion_RoomType model);

        /////// <summary>
        /////// Get package information asynchromusly
        /////// </summary>
        /////// <param name="packageId">Package indentifer</param>
        /////// <returns>Deal package model</returns>
        ////Task<List<DealsNightModel>> GetDealPackageNightsByPackageIdAsync(int packageId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="promotionId">Promotion indentifer</param>
        /// <returns>Deal package model</returns>
        Task<DealsPromotionModel> GetDealPromotionById(int promotionId);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="nightId">Airport/City Code</param>
        /// <returns>Deal package model</returns>
        Task<DealsNightModel> GetNightByNightId(int nightId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsFlightViewModel> GetFlightsFromInclusion(int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetAirportsCodesDropdownAsync(string search, short page, string id);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="dealFlightId">Deals Flight ID</param>
        /// <returns>Deal package model</returns>
        Task<DealsFlightModel> GetDealFlightAsync(int dealFlightId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Flight Model</param>
        /// <returns>Deal package model</returns>
        Task<int> UpdateDealFlightAsync(DealsFlightModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealVisaViewModel>> GetVisaItemsByPackageId(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> MoveVisaItemsByPackageDestinations(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="visaId">The Visa ID</param>
        /// <returns>InformationModel</returns>
        Task<int> DeletePackageVisaById(int visaId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealPackageVisa(DealVisaModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateMinPriceForPackage(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="countryId">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<VisaModel> GetVisaByCountryId(short? countryId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealPackageVisa(DealVisaModel model);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> AddDealsItenaryAsync(DealsItineraryModel model);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> AddDealsInclusionAsync(DealsInclusionModel model);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsNightModel>> GetNightsAsync(int packageId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package ID</param>
        /// <returns>Deal package model</returns>
        Task<DealsItineraryModel> GetDealItenaryHotelByNightId(int nightId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="itenaryId">Itenary ID</param>
        /// <returns>Deal package model</returns>
        Task<DealsInclusionModel> GetDealInclusionHotelByItenaryId(int itenaryId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> UpdateDealsItenaryAsync(DealsItineraryModel model);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> UpdateDealsInclusionAsync(DealsInclusionModel model);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<int?> GetDealHotelierFromNightId(int nightId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> AddDealsFlightAsync(DealsFlightModel model);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="code">Airport/City Code</param>
        /// <returns>Deal package model</returns>
        Task<FlightDestination> GetAirportDetailsByCode(string code);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsHotelInfoViewModel> GetDealPackageHotelInfoByPackageIdAsync(int id);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsDestinationModel>> GetDestinationsAsync(int packageId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsBookingValidityModel>> GetBookingValiditiesAsync(int packageId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<DealsBookingValidityModel> GetLastBookingValidityAsync(int packageId);

         // Flight Booking

        /// <summary>
        /// get Flight Details by Inclusion Id
        /// </summary>
        /// <param name="inclusionId">Inclusion Id</param>param>
        /// <returns>Flight model</returns>
        Task<DealsFlightModel> GetFlightDetailsByInclusionId(int inclusionId);

        /// <summary>
        /// get city name by city code
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>flight destination model</returns>
        Task<FlightDestination> GetCityByCityCode(string code);

        // Flight Booking

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsItineraryModel>> GetItinerariesAsync(int nightId);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="itenaryID">Itenary Id id</param>
        /// <returns>Deal package model</returns>
        Task<DealsItineraryModel> GetItineraryByIdAsync(int itenaryID);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="fromNightId">from night id </param>
        /// <param name="toNightId">to night id </param>
        /// <param name="packageId">pacakge Id id</param>
        /// <returns>1- Success, 2- Data Already Filled, 3 - Internal Server Error</returns>
        Task<int> CopyItinerary(int fromNightId, int toNightId, int packageId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsPaxCombinationModel>> GetPaxCombinationsAsync(int packageId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<DealsRatePlanModel>> GetratePlansAsync(int nightId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="pacakgeId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllDealsReviewsByPackageId(DataTableParameter model, int pacakgeId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="pacakgeId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsReviewModel> GetDealsReviewsById(int pacakgeId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        DealsInclusionModel GetInclusionRecordForHotelFromPackageId(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealsReviewAsync(DealsReviewModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealsReviewAsync(DealsReviewModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteDealsReviewAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inclusionId">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteFlightInclusion(int inclusionId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealsImageModel>> GetDealsImagesByPackageId(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealsImageAsync(DealsImageModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealsImageAsync(DealsImageModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsContentViewModel> GetDealsContentByPackageIdAsync(int packageId);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="hotelId">Hotel indentifer</param>
        /// <returns>Deal package model</returns>
        Task<HotelierContentModel> GetHotelierContentByHotelIdAsync(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="highLightId">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteDealHighlightByIdAsync(int highLightId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealHighlightAsync(DealsHighlightModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealsHighlightViewModel>> GetAllDealHighlightsFromPackageIdAsync(int packageId);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="hotelId">Hotel indentifer</param>
        /// <returns>Deal package model</returns>
        Task<List<HotelierImageModel>> GetHotelierImagesByHotelIdAsync(int hotelId);

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="imageRecordId">Hotel indentifer</param>
        /// <returns>Deal package model</returns>
        Task<int?> DeletePackageImageById(int imageRecordId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealHighlight(DealsHighlightModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealsContent(DealsContentModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealsContent(DealsContentModel model);

        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <param name="model">The model.</param>
        /////// <param name="packageId">Hotel Id</param>
        /////// <returns>
        /////// GetAllAsync
        /////// </returns>
        ////Task<DataTableResult> GetAllAddOnsByPackageId(DataTableParameter model, int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inclusionId">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsAddOnViewModel> GetDealActivitiesByInclusionId(int inclusionId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="dealAddOnId">Deals Add On ID</param>
        /// <returns>Deal package model</returns>
        Task<DealsAddOnModel> GetDealAddOnAsync(int dealAddOnId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> UpdateDealAddOnAsync(DealsAddOnModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsAddOnModel> AddDealsAddOnByIdAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealsAddOnAsync(DealsAddOnModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealsAddOnAsync(DealsAddOnModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteAddOnAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealPackageInfoAsync(DealsPackageModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealPackageInfoAsync(DealsPackageModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteAllPackageDestinationByPackageId(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealPackageDestinationAsync(DealsDestinationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteAllPackagePaxCombinationByPackageId(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealPackagePaxCombinationAsync(DealsPaxCombinationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteAllPackageBookingValidtyByPackageId(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="validityId">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeletePackageBookingValidityById(int validityId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealBookingValidty(DealsBookingValidityModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="paxId">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeletePackagePaxCombinationById(int paxId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="dealId">The Deal Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealsDestinationModel>> GetDealDestinationByDealIdAsync(int dealId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealPaxCombination(DealsPaxCombinationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="destinationId">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteDealsDestinationById(int destinationId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Deal Destination Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealDestination(DealsDestinationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealPackageBookingValidityAsync(DealsBookingValidityModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealsPromotionAsync(DealsPromotionModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealsPromotionAsync(DealsPromotionModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealPackageHotelRoomConfiguration(DealRoomConfigurationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealRoomConfigViewModel>> GetDealPackageRoomConfigByPackageIdAsync(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">The Deal.RoomConfig Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DealDeletePackageRoomConfig(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal.RoomConfig Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealPackageRoomConfig(DealRoomConfigurationModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Deal.RoomConfig Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealsHotelRatePlanViewModel>> GetRoomConfigsForHotelRatePlans(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealRatePlan(DealsRatePlanModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="roomConfigId">Room Config Id</param>
        /// <returns>InformationModel</returns>
        Task<List<DealsRatePlanViewModel>> GetDealRoomConfigAllRatePlans(int roomConfigId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="ratePlanId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteRatePlanAsync(int ratePlanId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsRatePlanModel> GetDealRatePlanById(int ratePlanId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealRatePlan(DealsRatePlanModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetRoomConfigDropDownListForRatePlanAsync(string search, short page, int? id, int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Itineary Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteItineraryAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inclusionId">inclusionId</param>
        /// <returns>InformationModel</returns>
        Task<DealsInclusionModel> GetDealsInclusion(int inclusionId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteHotelInclusion(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="roomTypeId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        Task<int> DeleteRoomTypeInclusion(int roomTypeId);

        /// <summary>
        /// Delete Deal Image
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>Model</returns>
        Task<int> DeleteDealTourImageAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<DealsSeoDetail> GetSeoDetail(int packageId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> UpdateDealSeo(DealsSeoDetail model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        Task<int> AddDealSeo(DealsSeoDetail model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelierId">The Hotel Id</param>
        /// <returns>InformationModel</returns>
        Task<List<HotelierReviewModel>> GetAllHotelReviews(int hotelierId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageIds">The Package Id</param>
        /// <returns>InformationModel</returns>
        List<short> GetAllRoomTypeIdsFromPackageId(int packageIds);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<DealsDepartureDateViewModel> GetDealDepartureByNightIdAsync(int nightId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        Task<int> AddDealsDepartureAsync(DealsDepartureDatesModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="search">Search</param>
        /// <param name="page">Page</param>
        /// <param name="nightId">The Package Id</param>
        /// <param name="inclusionIds">The Inclusion Ids</param>
        /// <returns>Get Inclusion Hoteliers From NightId</returns>
        Task<IList<Dropdown>> GetInclusionHoteliersFromNightId(string search, short page, int nightId, int[] inclusionIds);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">THe Night Id</param>
        /// <returns>Deal package model</returns>
        Task<int> DeleteAllDealsDeparture(int nightId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inclusionId">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        DealsFlightModel GetHotelFlightRecordFromInclusionId(int inclusionId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="dealid">Deal Id</param>
        /// <returns>Deal package model</returns>
        Task<SendLeadViewModel> GetSendDealinfo(int dealid);

        /// <summary>
        /// Return Deal Night Record
        /// </summary>
        /// <param name="nightValue">Night Value</param>
        /// <param name="dealId">Deal Id</param>
        /// <returns>Deal Night Record</returns>
        Task<DealsNightModel> GetNightRecordByValueAndDealId(int nightValue, int dealId);

        /// <summary>
        /// Task to Delete Deal Night Record and All Record Associated with it.
        /// </summary>
        /// <param name="nightModel">Night Model</param>
        /// <returns>Deal Night Record</returns>
        Task<int> DeleteDealNightPackageTask(DealsNightModel nightModel);
    }
}