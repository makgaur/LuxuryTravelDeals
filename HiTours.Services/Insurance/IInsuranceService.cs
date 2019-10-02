// <copyright file="IInsuranceService.cs" company="Luxury Travel Deals">
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
    public interface IInsuranceService
    {
        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <returns>
        /////// Get All List
        /////// </returns>
        /////// <param name="model">Model.</param>
        ////Task<DataTableResult> GetAllInsuranceMasterAsync(DataTableParameter model);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> AddInsuranceMasterAsync(InsuranceModel record);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> UpdateInsuranceMasterAsync(InsuranceModel record);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<InsuranceModel> GetInsuranceMasterByIdAsyn(int id);

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
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> AddPackageInsuranceInfo(TourPackageInsuranceInfoModel record);

        /// <summary>
        /// Get Package Visa View Model Record.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns>
        /// Get Package Visa view model Record.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<PackageInsuranceViewModel> GetPackageInsuranceAsync(Guid id);

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int?> DeletePackageInsurance(Guid id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="model">Model.</param>
        Task<DataTableResult> GetAllInsuranceMasterAsync(DataTableParameter model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetVendorInsuranceDropDownListAsync(string search, short page, int? id);
    }
}