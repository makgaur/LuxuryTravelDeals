// <copyright file="InsuranceService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="IInsuranceService" />
    public class InsuranceService : IInsuranceService
    {
        /// <summary>
        /// The user detail repository
        /// </summary>
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<PackageCountryModel> packageCountryRespository;
        private readonly IRepository<InsuranceMasterGridViewModel> insuranceMasterGridRespository;
        private readonly IRepository<TourPackageInsuranceInfoModel> packageInsuranceRespository;
        private readonly IRepository<VendorInformationModel> vendorRespository;
        private readonly IRepository<VendorServiceModel> vendorServiceRespository;
        private readonly IRepository<InsuranceModel> insuranceRespository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceService" /> class.
        /// </summary>
        /// <param name="vendorRespository">vendorRepository</param>
        /// <param name="vendorServiceRespository">vendorServiceRepository</param>
        /// <param name="packageInsuranceRespository">Package Insurance Repo</param>
        /// <param name="insuranceMasterGridRespository">Insurance Grid Master Grid Repos</param>
        /// <param name="packageCountryRespository">Country Repository</param>
        /// <param name="dropdownRespository">DropDown</param>
        /// <param name="insuranceRespository">insuranceRespository</param>
        public InsuranceService(IRepository<VendorServiceModel> vendorServiceRespository, IRepository<VendorInformationModel> vendorRespository, IRepository<TourPackageInsuranceInfoModel> packageInsuranceRespository, IRepository<InsuranceMasterGridViewModel> insuranceMasterGridRespository, IRepository<PackageCountryModel> packageCountryRespository, IRepository<Dropdown> dropdownRespository, IRepository<InsuranceModel> insuranceRespository)
        {
            this.vendorServiceRespository = vendorServiceRespository;
            this.vendorRespository = vendorRespository;
            this.packageInsuranceRespository = packageInsuranceRespository;
            this.insuranceMasterGridRespository = insuranceMasterGridRespository;
            this.packageCountryRespository = packageCountryRespository;
            this.dropdownRespository = dropdownRespository;
            this.insuranceRespository = insuranceRespository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="model">Model.</param>
        public async Task<DataTableResult> GetAllInsuranceMasterAsync(DataTableParameter model)
        {
            try
            {
                var query = from insurance in this.insuranceRespository.Table
                            join vendor in this.vendorRespository.Table on insurance.VendorID equals vendor.Id
                            select new InsuranceMasterGridViewModel
                            {
                                Id = insurance.Id,
                                Vendor = vendor.Name + ", " + vendor.CityModel.Name + " ," + vendor.CountryModel.Name,
                                IsActive = insurance.IsActive,
                                AdultPrice = insurance.AdultRate,
                                ChildPrice = insurance.ChildRate,
                                Days = insurance.Days,
                                CreatedBy = insurance.CreatedBy,
                                CreatedDate = insurance.CreatedDate,
                                UpdatedBy = insurance.UpdatedBy,
                                UpdatedDate = insurance.UpdatedDate
                            };
                return await this.insuranceMasterGridRespository.ToPagedListAsync(query, model);
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
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
        public async Task<IList<Dropdown>> GetVendorInsuranceDropDownListAsync(string search, short page, int? id)
        {
            var vendors = await this.vendorServiceRespository.Table.Where(x => x.ServiceId == 5).Select(x => x.VendorId).Distinct().ToListAsync();
            var query = this.vendorRespository.Table.Where(x => vendors.Contains(x.Id) && x.IsActive)
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });
            if (id != null && id != 0)
            {
                query = query.Where(x => x.Id == id.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int?> AddInsuranceMasterAsync(InsuranceModel record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.insuranceRespository.InsertAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<InsuranceModel> GetInsuranceMasterByIdAsyn(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.insuranceRespository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int?> UpdateInsuranceMasterAsync(InsuranceModel record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.insuranceRespository.UpdateAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int?> DeleteMasterAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException("Visa");
                }

                var record = await this.insuranceRespository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.insuranceRespository.DeleteAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="id">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int?> DeletePackageInsurance(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException("Visa");
                }

                if (this.packageInsuranceRespository.Table.Where(x => x.PackageId == id).Any())
                {
                    var records = await this.packageInsuranceRespository.Table.Where(x => x.PackageId == id).ToListAsync();
                    foreach (var item in records)
                    {
                        await this.packageInsuranceRespository.DeleteAsync(item);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Package Visa View Model Record.
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns>
        /// Get Package Visa view model Record.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<PackageInsuranceViewModel> GetPackageInsuranceAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException("Visa");
                }

                if (this.packageInsuranceRespository.Table.Where(x => x.PackageId == id).Any())
                {
                    PackageInsuranceViewModel viewModel = new PackageInsuranceViewModel
                    {
                        PackageId = id,
                        IncludePackageInsurance = true,
                        PackageInsuranceInfoViewModels = await (from pv in this.packageInsuranceRespository.Table
                                                           where pv.PackageId == id
                                                           select new TourPackageInsuranceInfoViewModel
                                                           {
                                                               Id = 0,
                                                               PackageId = pv.PackageId,
                                                               AdultRate = pv.AdultRate,
                                                               ChildRate = pv.ChildRate,
                                                               VendorId = pv.VendorId,
                                                               Days = pv.Days,
                                                               SelectThis = true
                                                           }).ToListAsync()
                    };
                    return viewModel;
                }
                else
                {
                    PackageInsuranceViewModel viewModel = new PackageInsuranceViewModel
                    {
                        PackageId = id,
                        IncludePackageInsurance = false,
                        PackageInsuranceInfoViewModels = await this.insuranceRespository.Table
                        .Where(x => x.IsActive)
                        .Select(x => new TourPackageInsuranceInfoViewModel
                        {
                            Id = 0,
                            PackageId = id,
                            AdultRate = x.AdultRate,
                            ChildRate = x.ChildRate,
                            VendorId = x.VendorID,
                            Days = x.Days,
                            SelectThis = false
                        }).ToListAsync()
                    };
                    return viewModel;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new PackageInsuranceViewModel
                {
                    IncludePackageInsurance = false,
                    PackageId = id,
                    PackageInsuranceInfoViewModels = new List<TourPackageInsuranceInfoViewModel>()
                };
            }
        }

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="record">The Model</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int?> AddPackageInsuranceInfo(TourPackageInsuranceInfoModel record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.packageInsuranceRespository.InsertAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}