// <copyright file="VisaService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="IVisaService" />
    public class VisaService : IVisaService
    {
        /// <summary>
        /// The user detail repository
        /// </summary>
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<VisaModel> visaRespository;
        private readonly IRepository<TourPackageVisaInfoModel> packageVisaInfoRespository;
        private readonly IRepository<PackageCountryModel> packageCountryRespository;
        private readonly IRepository<DestinationModel> destinationRespository;
        private readonly IRepository<VendorInformationModel> vendorRespository;
        private readonly IRepository<VendorServiceModel> vendorServiceRespository;
        private readonly IRepository<VisaMasterGridViewModel> visaMasterGridRespository;

        private readonly IMasterService masterService;
        ////private readonly IVendorService vendorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisaService" /> class.
        /// </summary>
        /// <param name="vendorServiceRespository">Vendor Service</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="vendorRespository">vendorRespository</param>
        /// <param name="destinationRespository">Destination Repository</param>
        /// <param name="packageVisaInfoRespository">Package Visa Information Repository</param>
        /// <param name="visaMasterGridRespository">Visa Grid Master Grid Repos</param>
        /// <param name="packageCountryRespository">Country Repository</param>
        /// <param name="dropdownRespository">DropDown</param>
        /// <param name="visaRespository">Visa Master</param>
        public VisaService(IRepository<VendorServiceModel> vendorServiceRespository, IMasterService masterService, IRepository<VendorInformationModel> vendorRespository, IRepository<DestinationModel> destinationRespository, IRepository<TourPackageVisaInfoModel> packageVisaInfoRespository, IRepository<VisaMasterGridViewModel> visaMasterGridRespository, IRepository<PackageCountryModel> packageCountryRespository, IRepository<Dropdown> dropdownRespository, IRepository<VisaModel> visaRespository)
        {
            this.vendorServiceRespository = vendorServiceRespository;
            this.masterService = masterService;
            this.destinationRespository = destinationRespository;
            this.packageVisaInfoRespository = packageVisaInfoRespository;
            this.visaMasterGridRespository = visaMasterGridRespository;
            this.packageCountryRespository = packageCountryRespository;
            this.visaRespository = visaRespository;
            this.dropdownRespository = dropdownRespository;
            this.vendorRespository = vendorRespository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="model">Model.</param>
        public async Task<DataTableResult> GetAllVisaMasterAsync(DataTableParameter model)
        {
            try
            {
                var query = from visa in this.visaRespository.Table
                            join country in this.packageCountryRespository.Table on visa.CountryId equals country.Id
                            join vendor in this.vendorRespository.Table on visa.VendorID equals vendor.Id
                            select new VisaMasterGridViewModel
                            {
                                Id = visa.Id,
                                Vendor = vendor.Name + ", " + vendor.CityModel.Name + " ," + vendor.CountryModel.Name,
                                IsActive = visa.IsActive,
                                AdultPrice = visa.AdultPrice,
                                ChildPrice = visa.ChildPrice,
                                Country = country.Name,
                                CreatedBy = visa.CreatedBy,
                                CreatedDate = visa.CreatedDate,
                                UpdatedBy = visa.UpdatedBy,
                                UpdatedDate = visa.UpdatedDate
                            };
                return await this.visaMasterGridRespository.ToPagedListAsync(query, model);
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
                return null;
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
        public async Task<int?> AddVisaMasterAsync(VisaModel record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.visaRespository.InsertAsync(record);
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
        public async Task<VisaModel> GetVisaMasterByIdAsyn(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.visaRespository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
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
        public async Task<IList<Dropdown>> GetVendorVisaDropDownListAsync(string search, short page, int? id)
        {
            var vendors = await this.vendorServiceRespository.Table.Where(x => x.ServiceId == 6).Select(x => x.VendorId).Distinct().ToListAsync();
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
        public async Task<int?> UpdateVisaMasterAsync(VisaModel record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.visaRespository.UpdateAsync(record);
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

                var record = await this.visaRespository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.visaRespository.DeleteAsync(record);
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
        public async Task<int?> DeletePackageVisa(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException("Visa");
                }

                if (this.packageVisaInfoRespository.Table.Where(x => x.PackageId == id).Any())
                {
                    var records = await this.packageVisaInfoRespository.Table.Where(x => x.PackageId == id).ToListAsync();
                    foreach (var item in records)
                    {
                        await this.packageVisaInfoRespository.DeleteAsync(item);
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
        public async Task<PackageVisaViewModel> GetPackageVisaAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException("Visa");
                }

                if (this.packageVisaInfoRespository.Table.Where(x => x.PackageId == id).Any())
                {
                    PackageVisaViewModel viewModel = new PackageVisaViewModel
                    {
                        PackageId = id,
                        IncludePackageVisa = true,
                        PackageVisaInfoViewModels = await (from pv in this.packageVisaInfoRespository.Table
                                                           where pv.PackageId == id
                                                           select new TourPackageVisaInfoViewModel
                                                           {
                                                               Id = 0,
                                                               PackageId = pv.PackageId,
                                                               AdultPrice = pv.AdultPrice,
                                                               ChildPrice = pv.ChildPrice,
                                                               CountryId = pv.CountryId,
                                                               VendorId = pv.VendorId
                                                           }).ToListAsync()
                    };
                    return viewModel;
                }
                else
                {
                    if (!this.destinationRespository.Table.Where(x => x.D_PackageId == id).Select(x => x.D_Country).Any())
                    {
                        return new PackageVisaViewModel
                        {
                            IncludePackageVisa = false,
                            PackageId = id,
                            PackageVisaInfoViewModels = new List<TourPackageVisaInfoViewModel>()
                        };
                    }
                    else
                    {
                        List<short> countriesId = await this.destinationRespository.Table.Where(x => x.D_PackageId == id).Select(x => x.D_Country).Distinct().ToListAsync();
                        PackageVisaViewModel viewModel = new PackageVisaViewModel
                        {
                            PackageId = id,
                            IncludePackageVisa = false,
                            PackageVisaInfoViewModels = await this.visaRespository.Table
                            .Where(x => countriesId.Any(y => x.CountryId == y))
                            .Select(x => new TourPackageVisaInfoViewModel
                            {
                                Id = 0,
                                PackageId = id,
                                AdultPrice = Convert.ToDecimal(x.AdultPrice),
                                ChildPrice = Convert.ToDecimal(x.ChildPrice),
                                CountryId = x.CountryId,
                                VendorId = x.VendorID
                            }).ToListAsync()
                        };
                        return viewModel;
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new PackageVisaViewModel
                {
                    IncludePackageVisa = false,
                    PackageId = id,
                    PackageVisaInfoViewModels = new List<TourPackageVisaInfoViewModel>()
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
        public async Task<int?> AddPackageVisaInfo(TourPackageVisaInfoModel record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.packageVisaInfoRespository.InsertAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}