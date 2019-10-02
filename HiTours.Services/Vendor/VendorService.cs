// <copyright file="VendorService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// UserDetailService
    /// </summary>
    /// <seealso cref="IVendorService" />
    public class VendorService : IVendorService
    {
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<VendorGridViewModel> vendorGridViewModelRepo;
        private readonly IRepository<VendorInformationModel> vendorInformationRepo;
        private readonly IRepository<VendorGroupModel> vendorGroupRepo;
        private readonly IRepository<CurrencyModel> currencyRepo;
        private readonly IRepository<PackageCityModel> cityRepo;
        private readonly IRepository<PackageCountryModel> countryRepo;
        private readonly IRepository<VendorCategoryModel> vendorCategoryRepo;
        private readonly IRepository<VendorContactModel> vendorContactsRepo;
        private readonly IRepository<VendorContractModel> vendorContractsRepo;
        private readonly IRepository<VendorContactGridViewModel> vendorContactGrid;
        private readonly IRepository<VendorContractGridViewModel> vendorContractGrid;
        private readonly IRepository<VendorBankDetailGridViewModel> vendorBankGrid;
        private readonly IRepository<VendorBankDetailModel> vendorBankRepo;
        private readonly IRepository<ServiceTypeMasterModel> serviceTypeMasterRepo;
        private readonly IRepository<VendorServiceModel> vendorServiceRepo;
        private readonly IRepository<VendorGroupGridViewModel> vendorGroupGridRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="VendorService" /> class.
        /// </summary>
        /// <param name="vendorServiceRepo">Vendor Service Repo</param>
        /// <param name="serviceTypeMasterRepo">Service Type Master Repo</param>
        /// <param name="vendorBankRepo">Bank Repo</param>
        /// <param name="vendorBankGrid">Grid Model</param>
        /// <param name="vendorContractGrid">Contract Grid</param>
        /// <param name="vendorContactGrid">Vendor Contct Grid</param>
        /// <param name="vendorContractsRepo">Contract Repo</param>
        /// <param name="vendorContactsRepo">Contact Repo</param>
        /// <param name="vendorCategoryRepo">Vendor Category Repo</param>
        /// <param name="currencyRepo">Currency</param>
        /// <param name="vendorGroupRepo">Vendor Group Repo</param>
        /// <param name="dropdownRespository">DropDown Repos</param>
        /// <param name="vendorGridViewModelRepo">The Vendor Grid Repo.</param>
        /// <param name="vendorInformationRepo">Vendor Info Repo</param>
        /// <param name="countryRepo">Country Repo</param>
        /// <param name="cityRepo"> City Repo</param>
        /// <param name="vendorGroupGridRepo"> Vendor Group Grid Repo</param>
        public VendorService(IRepository<VendorGroupGridViewModel> vendorGroupGridRepo, IRepository<PackageCountryModel> countryRepo, IRepository<PackageCityModel> cityRepo, IRepository<VendorServiceModel> vendorServiceRepo, IRepository<ServiceTypeMasterModel> serviceTypeMasterRepo, IRepository<VendorBankDetailModel> vendorBankRepo, IRepository<VendorBankDetailGridViewModel> vendorBankGrid, IRepository<VendorContractGridViewModel> vendorContractGrid, IRepository<VendorContactGridViewModel> vendorContactGrid, IRepository<VendorContractModel> vendorContractsRepo, IRepository<VendorContactModel> vendorContactsRepo, IRepository<VendorCategoryModel> vendorCategoryRepo, IRepository<CurrencyModel> currencyRepo, IRepository<VendorGroupModel> vendorGroupRepo, IRepository<Dropdown> dropdownRespository, IRepository<VendorGridViewModel> vendorGridViewModelRepo, IRepository<VendorInformationModel> vendorInformationRepo)
        {
            this.vendorGroupGridRepo = vendorGroupGridRepo;
            this.countryRepo = countryRepo;
            this.cityRepo = cityRepo;
            this.vendorServiceRepo = vendorServiceRepo;
            this.serviceTypeMasterRepo = serviceTypeMasterRepo;
            this.vendorBankGrid = vendorBankGrid;
            this.vendorBankRepo = vendorBankRepo;
            this.vendorCategoryRepo = vendorCategoryRepo;
            this.currencyRepo = currencyRepo;
            this.vendorGroupRepo = vendorGroupRepo;
            this.dropdownRespository = dropdownRespository;
            this.vendorGridViewModelRepo = vendorGridViewModelRepo;
            this.vendorInformationRepo = vendorInformationRepo;
            this.vendorContactGrid = vendorContactGrid;
            this.vendorContactsRepo = vendorContactsRepo;
            this.vendorContractGrid = vendorContractGrid;
            this.vendorContractsRepo = vendorContractsRepo;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllVendorsAsync(DataTableParameter model)
        {
            try
            {
                var records = this.vendorInformationRepo.Table.Where(x => !x.IsDeleted).Select(x => new VendorGridViewModel
                {
                    Id = x.Id,
                    Category = x.CategoryModel.Name,
                    Name = x.Name,
                    City = x.CityModel.Name,
                    Country = x.CountryModel.Name,
                    Group = x.Group == 0 ? string.Empty : x.VendorGroupModel.Name,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                });

                return await this.vendorGridViewModelRepo.ToPagedListAsync(records, model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllVendorGroupsAsync(DataTableParameter model)
        {
            try
            {
                var records = this.vendorGroupRepo.Table.Where(x => !x.IsDeleted).Select(x => new VendorGroupGridViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.AddressLine1 + ", " + x.AddressLine2 + ", " + this.cityRepo.Table.Where(y => y.Id == x.CityId).Select(y => y.Name).FirstOrDefault() + ", " + this.countryRepo.Table.Where(y => y.Id == x.CountryId).Select(y => y.Name).FirstOrDefault(),
                    Email = x.EmailId,
                    WorkPhone = x.WorkPhone,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    ContactPerson = x.Salutation + " " + x.FName + " " + x.LName
                });

                return await this.vendorGroupGridRepo.ToPagedListAsync(records, model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetVendorGroupDropDownListAsync(string search, short page, int? id)
        {
            var query = this.vendorGroupRepo.Table.Where(x => x.IsActive && !x.IsDeleted)
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });
            if (id != null && id != 0)
            {
                query = query.Where(x => x.Id == id.ToString());
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetVendorDropDownListAsync(string search, short page, int? id)
        {
            var query = this.vendorInformationRepo.Table.Where(x => x.IsActive && !x.IsDeleted)
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });
            if (id != null && id != 0)
            {
                query = query.Where(x => x.Id == id.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

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
        public async Task<IList<Dropdown>> GetVendorByVendorType(string search, short page, int? vendorTypeId, int vendorGroupId)
        {
            if (vendorTypeId != null && vendorTypeId != 0 && vendorGroupId != 0)
            {
                var query = this.vendorInformationRepo.Table.Where(x => x.IsActive && !x.IsDeleted && x.VendorServiceModels.Select(y => y.ServiceId).Contains(Convert.ToInt32(vendorTypeId)) && x.Group == vendorGroupId)
                         .OrderBy(x => x.Name)
                         .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.Name.Contains(search));
                }

                return await this.dropdownRespository.ToOptionListAsync(query, page);
            }
            else
            {
                var query = this.vendorInformationRepo.Table.Where(x => x.IsActive && !x.IsDeleted && x.VendorServiceModels.Select(y => y.ServiceId).Contains(Convert.ToInt32(vendorTypeId)))
                        .OrderBy(x => x.Name)
                        .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.Name.Contains(search));
                }

                return await this.dropdownRespository.ToOptionListAsync(query, page);
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetCurrencyDropDownListAsync(string search, short page, int? id)
        {
            try
            {
                var query = this.currencyRepo.Table
                           .Where(x => x.IsActive)
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name + " (" + x.Code + "), " + x.PackageCountry.Name });
                if (id != null && id != 0)
                {
                    query = query.Where(x => x.Id == id.ToString());
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.Name.Contains(search));
                }

                return await this.dropdownRespository.ToOptionListAsync(query, page);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetFlightVendors(string search, short page, int? id)
        {
            try
            {
                var query = this.vendorInformationRepo.Table
                           .OrderBy(x => x.Name)
                           .Where(x => x.VendorServiceModels.Select(y => y.ServiceId).Contains(3))
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name + x.CityModel.Name });
                if (id != null && id != 0)
                {
                    query = query.Where(x => x.Id == id.ToString());
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.Name.Contains(search));
                }

                return await this.dropdownRespository.ToOptionListAsync(query, page);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetCategoryDropDownListAsync(string search, short page, int? id)
        {
            try
            {
                var query = this.vendorCategoryRepo.Table
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });
                if (id != null && id != 0)
                {
                    query = query.Where(x => x.Id == id.ToString());
                }

                return await this.dropdownRespository.ToOptionListAsync(query, page);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> AddGroupAsync(VendorGroupModel model)
        {
            try
            {
                return await this.vendorGroupRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">The Identifier</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<VendorInformationModel> GetVendorById(int id)
        {
            try
            {
                return await this.vendorInformationRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">The Identifier</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<VendorGroupModel> GetVendorGroupById(int id)
        {
            try
            {
                return await this.vendorGroupRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The Identifier</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> UpdateGroupAsync(VendorGroupModel model)
        {
            try
            {
                return await this.vendorGroupRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int> AddVendorInfoAsync(VendorInformationModel model)
        {
            try
            {
                await this.vendorInformationRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> UpdateVendorInfoAsync(VendorInformationModel model)
        {
            try
            {
                return await this.vendorInformationRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> DeleteAsync(int id)
        {
            try
            {
                var model = await this.vendorInformationRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.vendorInformationRepo.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> DeleteVendorGroupAsync(int id)
        {
            try
            {
                var model = await this.vendorGroupRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.vendorGroupRepo.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> DeleteContactAsync(int id)
        {
            try
            {
                var model = await this.vendorContactsRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.vendorContactsRepo.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetVendorContactsByIdAsync(DataTableParameter model, int id)
        {
            try
            {
                var records = this.vendorContactsRepo.Table.Where(x => x.VendorId == id).Select(x => new VendorContactGridViewModel
                {
                    Id = x.Id,
                    FullName = x.Salutation + " " + x.FirstName + " " + x.LastName,
                    Email = x.Email,
                    Designation = x.Designation,
                    Mobile = x.Mobile,
                    WorkPhone = x.WorkPhone,
                    Primary = x.IsPrimary ? "Yes" : "No",
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                });

                return await this.vendorContactGrid.ToPagedListAsync(records, model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetVendorContractsByIdAsync(DataTableParameter model, int id)
        {
            try
            {
                var records = this.vendorContractsRepo.Table.Where(x => x.VendorId == id).Select(x => new VendorContractGridViewModel
                {
                    Id = x.Id,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate,
                    MarginType = x.MarginTypeModel.Name,
                    MarginValue = x.MarginValue.ToString(),
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                });

                return await this.vendorContractGrid.ToPagedListAsync(records, model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<VendorContactModel> GetVendorContactsByIdentifierAsync(int id)
        {
            try
            {
                return await this.vendorContactsRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<VendorContractModel> GetVendorContractsByIdentifierAsync(int id)
        {
            try
            {
                return await this.vendorContractsRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> AddVendorContactAsync(VendorContactModel model)
        {
            try
            {
                return await this.vendorContactsRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> AddVendorContractAsync(VendorContractModel model)
        {
            try
            {
                return await this.vendorContractsRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> UpdateVendorContactAsync(VendorContactModel model)
        {
            try
            {
                return await this.vendorContactsRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> UpdateVendorContractAsync(VendorContractModel model)
        {
            try
            {
                return await this.vendorContractsRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetVendorBankDetailByIdAsync(DataTableParameter model, int id)
        {
            try
            {
                var records = this.vendorBankRepo.Table.Where(x => x.VendorId == id).Select(x => new VendorBankDetailGridViewModel
                {
                    Id = x.Id,
                    AccountNumber = x.AccountNumber,
                    AccountType = x.AccountType,
                    BankName = x.BankName,
                    GST = x.GST,
                    HolderName = x.HolderName,
                    IFSCCode = x.IFSCCode,
                    PAN = x.PAN,
                    SwiftCode = x.SwiftCode,
                    IsPrimary = x.IsPrimary ? "Primary" : string.Empty,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                });

                return await this.vendorBankGrid.ToPagedListAsync(records, model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> UpdateVendorBankDetailtAsync(VendorBankDetailModel model)
        {
            try
            {
                return await this.vendorBankRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="model">The search.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> AddVendorBankDetailAsync(VendorBankDetailModel model)
        {
            try
            {
                return await this.vendorBankRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">Vendor Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<VendorBankDetailModel> GetVendorBankDetailByIdentifierAsync(int id)
        {
            try
            {
                return await this.vendorBankRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> DeleteContractAsync(int id)
        {
            try
            {
                var model = await this.vendorContractsRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.vendorContractsRepo.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<int?> DeleteBankDetailAsync(int id)
        {
            try
            {
                var model = await this.vendorBankRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.vendorBankRepo.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        public async Task<IList<Dropdown>> GetAllVendorServiceTypeItems()
        {
            try
            {
                var query = this.serviceTypeMasterRepo.Table
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });

                return await this.dropdownRespository.ToOptionListAsync(query, 1);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>InformationModel</returns>
        public async Task<int[]> GetServiceTypesByVendorId(int id)
        {
            try
            {
                return await this.vendorServiceRepo.Table.Where(x => x.VendorId == id).Select(x => x.ServiceId).ToArrayAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Delete.</param>
        /// <returns>InformationModel</returns>
        public async Task<int?> DeleteVendorServicesById(int id)
        {
            try
            {
                var records = await this.vendorServiceRepo.Table.Where(x => x.VendorId == id).ToListAsync();
                foreach (var item in records)
                {
                    await this.vendorServiceRepo.DeleteAsync(item);
                }

                return null;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="vendorId">Vendor Id</param>
        /// <param name="serviceId">Service Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int?> InsertVendorServiceRecord(int vendorId, int serviceId)
        {
            try
            {
                var record = new VendorServiceModel
                {
                    Id = 0,
                    ServiceId = serviceId,
                    VendorId = vendorId
                };

                return await this.vendorServiceRepo.InsertAsync(record);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified no of nights].
        /// </summary>
        /// <param name="name">The urltitle.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// IsDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateVendor(string name, int id)
        {
            var result = await this.vendorInformationRepo.Table.FirstOrDefaultAsync(x => x.Id != id && !x.IsDeleted && x.Name.ToLower() == name.ToLower().Trim());
            return result == null;
        }
    }
}