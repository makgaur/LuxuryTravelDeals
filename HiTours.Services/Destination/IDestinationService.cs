// <copyright file="IDestinationService.cs" company="Luxury Travel Deals">
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
    /// IDestinationService
    /// </summary>
    public interface IDestinationService
    {
        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <param name="model">The model.</param>
        /////// <param name="packageId">The Package Id</param>
        /////// <returns>
        /////// GetAllAsync
        /////// </returns>
        ////Task<DataTableResult> GetPackageDestinationAsync(DataTableParameter model, Guid packageId);

        /// <summary>
        /// Add Destination asynchronous.
        /// </summary>
        /// <param name="model">The Destination Record.</param>
        /// <returns>
        /// Add Destination Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Destination</exception>
        Task<int> AddDestinationAsync(DestinationModel model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="packageId">The Package Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetPackageDestinationValidityAsync(DataTableParameter model, Guid packageId);

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="destinationId">The Destination Id</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetDestinationForValidityListAsync(string search, short page, int? destinationId, Guid packageId);

        /// <summary>
        /// Add Destination asynchronous.
        /// </summary>
        /// <param name="model">The Destination Validity Record.</param>
        /// <returns>
        /// Add Destination Validity Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Destination</exception>
        Task<int> AddDestinationValidityAsync(DestinationValidityModel model);
    }
}