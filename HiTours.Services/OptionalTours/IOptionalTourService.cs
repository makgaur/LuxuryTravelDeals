// <copyright file="IOptionalTourService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.ViewModels;

    /// <summary>
    /// IVendorService
    /// </summary>
    public interface IOptionalTourService
    {
        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <returns>
        /////// Get All List
        /////// </returns>
        /////// <param name="model">Model.</param>
        ////Task<DataTableResult> GetAllOptionalTourMasterAsync(DataTableParameter model);

        /// <summary>
        /// Gets By Id asynchronous.
        /// </summary>
        /// <returns>
        /// Get By ID First Or Default
        /// </returns>
        /// <param name="id">Identifier</param>
        Task<OptionalTourAndItemsAddViewModel> GetOptionalToursAndItemsAsyncByOTID(int id);

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="record">Identifier</param>
        Task<int> UpdateOptionalTourMaster(OptionalToursMasterModel record);

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="records">Identifier</param>
        /// <param name="optionalTourId">Optional Tour Id</param>
        Task<int> AddOTItemByOptionalTourId(List<OptionalToursItemsMasterModel> records, int optionalTourId);

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="record">Identifier</param>
        Task<int> DeleteOptionalTourItem(OptionalToursItemsMasterModel record);

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="record">Identifier</param>
        Task<int> AddOptionalTourMaster(OptionalToursMasterModel record);

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="id">Identifier</param>
        Task<OptionalToursMasterModel> GetOptionalTourMasterByIdAsyn(int id);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> AddPackageOptionalTourInfo(TourPackageOptionalTourInfoModel record);

        /// <summary>
        /// Get Package Visa View Model Record.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns>
        /// Get Package Visa view model Record.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<PackageOptionalTourViewModel> GetPackageOptionalTourAsync(Guid id);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> DeletePackageOptionalTour(Guid id);
    }
}