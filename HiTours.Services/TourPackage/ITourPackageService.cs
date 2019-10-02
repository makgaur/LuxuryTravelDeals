// <copyright file="ITourPackageService.cs" company="Luxury Travel Deals">
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
    /// IRegionService
    /// </summary>
    public interface ITourPackageService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The tour package.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(TourPackageModel tourPackage);

        /// <summary>
        /// Saves the package nights.
        /// </summary>
        /// <param name="tourPackageNight">The tour package night.</param>
        /// <returns>SavePackageNights</returns>
        Task<int> SavePackageNights(TourPackageNightModel tourPackageNight);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The tour package.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(TourPackageModel tourPackage);

        /// <summary>
        /// Updates the tour package night asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The tourpackage night.</param>
        /// <returns>UpdateTourPackageNightAsync</returns>
        Task<int> UpdateTourPackageNightAsync(TourPackageNightModel tourpackageNight);

        /// <summary>
        /// Updates the tour package night vilidity asynchronous.
        /// </summary>
        /// <param name="tourpackageNightvalidity">The tourpackage night.</param>
        /// <returns>UpdateTourPackageNightAsync</returns>
        Task<int> UpdateTourPackageNightValidityAsync(TourPackageNightsValidityModel tourpackageNightvalidity);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<TourPackageModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets the tour package night by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>GetTourPackageNightByIdAsync</returns>
        Task<TourPackageNightModel> GetTourPackageNightByIdAsync(Guid id);

        /// <summary>
        /// Gets the tour package night bytou package identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>GetTourPackageNightBytouPackageIdAsync</returns>
        Task<TourPackageNightModel> GetTourPackageNightBytouPackageIdAsync(Guid id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Get all Hotel List
        /// </summary>
        /// <param name="model">param</param>
        /// <returns>Hotel list</returns>
        Task<DataTableResult> GetAllHotelListAsync(DataTableParameter model);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The tour package.</param>
        /// <returns>
        /// Delete
        /// </returns>
        Task<int> DeleteAsync(TourPackageModel tourPackage);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>list of dropdown</returns>
        Task<IList<Dropdown>> GetDropDownListAsync(string search, short page, params object[] param);

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveTourpackageCityEntity(TourPackageCityModel entity);

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveTourPackageBookDateModelEntity(TourPackageBookDateModel entity);

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveTourPackageNightModelEntity(TourPackageNightModel entity);

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveTourPackageImageEntity(TourPackageImageModel entity);

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveTourPackageNightsValidityModelEntity(TourPackageNightsValidityModel entity);

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveTourPackageTravelStyleModelEntity(TourPackageTravelStyleModel entity);

        /// <summary>
        /// Gets all package night asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllPackageNightAsync
        /// </returns>
        Task<List<TourPackageNightModel>> GetAllPackageNightAsync();

        /// <summary>
        /// Gets the nights by package asynchronous.
        /// </summary>
        /// <param name="packageid">The packageid.</param>
        /// <returns>GetNightsByPackageAsync</returns>
        Task<List<TourPackageNightModel>> GetNightsByPackageAsync(Guid packageid);

        /// <summary>
        /// GetTourPackageImageByIdAsync
        /// </summary>
        /// <param name="id">sdfs</param>
        /// <returns>sdf</returns>
        Task<TourPackageImageModel> GetTourPackageImageByIdAsync(Guid id);

        /// <summary>
        /// DeleteTourPackageImageAsync
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Delete</returns>
        Task<int> DeleteTourPackageImageAsync(TourPackageImageModel entity);

        /// <summary>
        /// DeleteTourPackageImageAsync
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Delete</returns>
        Task<int> TourPackageImageUpdateAsync(TourPackageImageModel entity);

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified no of nights].
        /// </summary>
        /// <param name="noOfNights">The no of nights.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(int noOfNights, Guid id);

        /// <summary>
        /// Determines whether [is duplicate URL] [the specified urltitle].
        /// </summary>
        /// <param name="urltitle">The urltitle.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>IsDuplicateUrl</returns>
        Task<bool> IsDuplicateUrl(string urltitle, Guid id);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="tourPackagenightsvalidity">Tour Package nights validity.</param>
        /// <returns>
        /// InsertTourPackagesnightsValidityAsync
        /// </returns>
        Task<int> InsertTourPackagesnightsValidityAsync(TourPackageNightsValidityModel tourPackagenightsvalidity);

        /// <summary>
        /// Get All List the asynchronous.
        /// </summary>
        /// <param name="packagenightsvalidityid">Tour Package nights validity identifier.</param>
        /// <returns>
        /// GetTourPackagesNightsValidityListByTourpackagesnightsIDAsync
        /// </returns>
        Task<List<TourPackageNightsValidityModel>> GetTourPackagesNightsValidityListByTourpackagesnightsIDAsync(Guid packagenightsvalidityid);

        /// <summary>
        /// Gets the tour packages nights validity by tourpackagesnights identifier asynchronous.
        /// </summary>
        /// <param name="packagenightsvalidityid">The packagenightsvalidityid.</param>
        /// <returns>GetTourPackagesNightsValidityByTourpackagesnightsIDAsync</returns>
        Task<TourPackageNightsValidityModel> GetTourPackagesNightsValidityByTourpackagesnightsIDAsync(Guid packagenightsvalidityid);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The tour package.</param>
        /// <returns>
        /// Delete
        /// </returns>
        Task<int> TourPackageValidityDeleteAsync(TourPackageNightsValidityModel tourPackage);

        /// <summary>
        /// Get All List the asynchronous.
        /// </summary>
        /// <param name="id">Tour Package nights validity identifier.</param>
        /// <param name="from">Tour Package nights validity from.</param>
        /// <param name="to">Tour Package nights validity to.</param>
        /// <returns>
        /// GetTourPackagesNightsValidityListByAsync
        /// </returns>
        Task<List<TourPackageNightsValidityModel>> GetTourPackagesNightsValidityListByAsync(Guid id, DateTime from, DateTime to);

        /// <summary>
        /// Get All List the asynchronous.
        /// </summary>
        /// <param name="id">Tour Package nights validity identifier.</param>
        /// <returns>
        /// GetTourPackageByIdAsyn
        /// </returns>
        Task<TourPackageModel> GetTourPackageByIdAsyn(Guid id);

        /// <summary>
        /// Gets the city by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>GetCityByIDAsync</returns>
        Task<PackageCityModel> GetCityByIDAsync(int id);

        /// <summary>
        /// Updates the tour package night rates asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The tourpackage night.</param>
        /// <returns>UpdateTourPackageNightRatesAsync</returns>
        Task<int> UpdateTourPackageNightRatesAsync(TourPackageNightModel tourpackageNight);

        /// <summary>
        /// Gets the maximum deal code asynchronous.
        /// </summary>
        /// <returns>Get Max Number of Deal</returns>
        Task<int> GetMaxDealCodeAsync();

        /// <summary>
        /// Gets the nights by package night identifier asynchronous.
        /// </summary>
        /// <param name="packagenightid">The packagenightid.</param>
        /// <returns>GetNightsByPackageNightIDAsync</returns>
        Task<TourPackageNightModel> GetNightsByPackageNightIDAsync(Guid packagenightid);

        /// <summary>
        /// Gets all tour package nights bytour package identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nights">The nights.</param>
        /// <returns>
        /// GetAllTourPackageNightsBytourPackageIdAsync
        /// </returns>
        Task<List<TourPackageNightModel>> GetAllTourPackageNightsBytourPackageIdAsync(Guid id, byte nights);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageId">Package Id</param>
        /// <param name="isHotelOnly">Is Hotel Only Flag</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<BlockBookingViewModel> GetPackageBlockedDatesAsync(Guid packageId, bool isHotelOnly);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="model">Block Booking Model</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<int> AddPackageBlockedDatesAsync(BlockBookingModel model);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="model">The Model</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<int> DeletePackageBlockedDatesAsync(BlockBookingModel model);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageId">Package Id</param>
        /// <param name="date">Date</param>
        /// <param name="isHotel">Is Hotel Fag</param>
        /// <returns>
        /// Get
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        Task<BlockBookingModel> GetBlockBookingDateEntryAsync(Guid packageId, DateTime date, bool isHotel);
    }
}