// <copyright file="IVisaService.cs" company="Luxury Travel Deals">
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
    public interface IVisaService
    {
        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <returns>
        /////// Get All List
        /////// </returns>
        /////// <param name="model">Model.</param>
        ////Task<DataTableResult> GetAllVisaMasterAsync(DataTableParameter model);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> AddVisaMasterAsync(VisaModel record);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> UpdateVisaMasterAsync(VisaModel record);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<VisaModel> GetVisaMasterByIdAsyn(int id);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> DeleteMasterAsync(int id);

        /// <summary>
        /// Get Package Visa View Model Record.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns>
        /// Get Package Visa view model Record.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<PackageVisaViewModel> GetPackageVisaAsync(Guid id);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> DeletePackageVisa(Guid id);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> AddPackageVisaInfo(TourPackageVisaInfoModel record);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="model">Model.</param>
        Task<DataTableResult> GetAllVisaMasterAsync(DataTableParameter model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetVendorVisaDropDownListAsync(string search, short page, int? id);
    }
}