// <copyright file="IVendorService.cs" company="Luxury Travel Deals">
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
    public interface IVendorService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllVendorsAsync(DataTableParameter model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetVendorGroupDropDownListAsync(string search, short page, int? id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetCurrencyDropDownListAsync(string search, short page, int? id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetCategoryDropDownListAsync(string search, short page, int? id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> AddGroupAsync(VendorGroupModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> UpdateVendorInfoAsync(VendorInformationModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int> AddVendorInfoAsync(VendorInformationModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">The Identifier</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<VendorInformationModel> GetVendorById(int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> DeleteAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetVendorContractsByIdAsync(DataTableParameter model, int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetVendorContactsByIdAsync(DataTableParameter model, int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<VendorContactModel> GetVendorContactsByIdentifierAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<VendorContractModel> GetVendorContractsByIdentifierAsync(int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> AddVendorContactAsync(VendorContactModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> AddVendorContractAsync(VendorContractModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> UpdateVendorContactAsync(VendorContactModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> UpdateVendorContractAsync(VendorContractModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> DeleteContactAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetVendorBankDetailByIdAsync(DataTableParameter model, int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> AddVendorBankDetailAsync(VendorBankDetailModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> UpdateVendorBankDetailtAsync(VendorBankDetailModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> DeleteBankDetailAsync(int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> DeleteContractAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<VendorBankDetailModel> GetVendorBankDetailByIdentifierAsync(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        Task<IList<Dropdown>> GetAllVendorServiceTypeItems();

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetFlightVendors(string search, short page, int? id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>InformationModel</returns>
        Task<int[]> GetServiceTypesByVendorId(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="vendorId">Vendor Id</param>
        /// <param name="serviceId">Service Id</param>
        /// <returns>InformationModel</returns>
        Task<int?> InsertVendorServiceRecord(int vendorId, int serviceId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>InformationModel</returns>
        Task<int?> DeleteVendorServicesById(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllVendorGroupsAsync(DataTableParameter model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">The Identifier</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<VendorGroupModel> GetVendorGroupById(int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The Identifier</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> UpdateGroupAsync(VendorGroupModel model);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<int?> DeleteVendorGroupAsync(int id);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="vendorTypeId">Group Identifier .</param>
        /// <param name="vendorGroupId">vendorGroupId</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetVendorByVendorType(string search, short page, int? vendorTypeId, int vendorGroupId);

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        Task<IList<Dropdown>> GetVendorDropDownListAsync(string search, short page, int? id);

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified no of nights].
        /// </summary>
        /// <param name="name">The urltitle.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// IsDuplicateAsync
        /// </returns>
        Task<bool> IsDuplicateVendor(string name, int id);
    }
}