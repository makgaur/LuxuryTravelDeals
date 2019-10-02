// <copyright file="IHomePageService.cs" company="Luxury Travel Deals">
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
    /// IVendorService
    /// </summary>
    public interface IHomePageService
    {
        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<DealsPackageModel> GetDealPackageByIdAsync(int dealId);

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="model">Data Table</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<DataTableResult> GetAllSpecialDealsAsync(DataTableParameter model);

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<List<LocationDealsCurationViewModel>> GetActiveLocations();

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<LocationDealModel> GetLocationDealByIdAsync(int id);

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="data">Identifier</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<int> AddLocationDeal(LocationDealModel data);

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="data">Identifier</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<int> UpdateLocationDeal(LocationDealModel data);

        /// <summary>
        /// Get Top 6 Tagged Travel Styles
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        List<PackageTravelStyleModel> GetTop6TaggedTravelStyles();

        /// <summary>
        /// Get Top 6 Country Destinations
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        List<PackageCountryModel> GetTop6CountryDestinations();
    }
}