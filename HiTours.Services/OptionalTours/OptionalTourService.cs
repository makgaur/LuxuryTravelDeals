// <copyright file="OptionalTourService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="IOptionalTourService" />
    public class OptionalTourService : IOptionalTourService
    {
        /// <summary>
        /// The user detail repository
        /// </summary>
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<OptionalToursMasterModel> optionalToursRespository;
        private readonly IRepository<OptionalToursItemsMasterModel> optionalToursItemRespository;
        private readonly IRepository<PackageCountryModel> packageCountryRespository;
        ////private readonly IRepository<VendorModel> vendorRespository;
        private readonly IRepository<OptionalTourMasterGridViewModel> optionalTourMasterGridViewModelRespository;
        private readonly IRepository<OptionalTourAndItemsAddViewModel> optionalTourAndItemsAddVMRepo;
        private readonly IRepository<TourPackageOptionalTourInfoModel> packageOptionalTourRepo;
        private readonly IRepository<DestinationModel> destinationRespository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalTourService" /> class.
        /// </summary>
        /// <param name="destinationRespository">Destination Repo</param>
        /// <param name="packageOptionalTourRepo">Package Optional Tour Repo</param>
        /// <param name="optionalTourAndItemsAddVMRepo">Add View Model Repo</param>
        /// <param name="optionalTourMasterGridViewModelRespository">Optional Tour Master Grid View Model Repository</param>
        /// <param name="optionalToursItemRespository">Optional Tours Item Repository</param>
        /// <param name="optionalToursRespository">Optiona Tours Repository</param>
        /// <param name="packageCountryRespository">Country Repository</param>
        /// <param name="dropdownRespository">DropDown</param>
        public OptionalTourService(IRepository<DestinationModel> destinationRespository, IRepository<TourPackageOptionalTourInfoModel> packageOptionalTourRepo, IRepository<OptionalTourAndItemsAddViewModel> optionalTourAndItemsAddVMRepo, IRepository<OptionalTourMasterGridViewModel> optionalTourMasterGridViewModelRespository, IRepository<OptionalToursItemsMasterModel> optionalToursItemRespository, IRepository<OptionalToursMasterModel> optionalToursRespository, IRepository<PackageCountryModel> packageCountryRespository, IRepository<Dropdown> dropdownRespository)
        {
            this.destinationRespository = destinationRespository;
            this.packageOptionalTourRepo = packageOptionalTourRepo;
            this.optionalTourAndItemsAddVMRepo = optionalTourAndItemsAddVMRepo;
            this.optionalTourMasterGridViewModelRespository = optionalTourMasterGridViewModelRespository;
            this.optionalToursRespository = optionalToursRespository;
            this.optionalToursItemRespository = optionalToursItemRespository;
            this.packageCountryRespository = packageCountryRespository;
            this.dropdownRespository = dropdownRespository;
        }

        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <returns>
        /////// Get All List
        /////// </returns>
        /////// <param name="model">Model.</param>
        ////public async Task<DataTableResult> GetAllOptionalTourMasterAsync(DataTableParameter model)
        ////{
        ////    try
        ////    {
        ////        var query = from ot in this.optionalToursRespository.Table
        ////                    join vendor in this.vendorRespository.Table on ot.VendorId equals vendor.Id
        ////                    join country in this.packageCountryRespository.Table on ot.CountryId equals country.Id
        ////                    select new OptionalTourMasterGridViewModel
        ////                    {
        ////                        Id = ot.Id,
        ////                        Country = country.Name,
        ////                        Vendor = vendor.BusinessName + ", " + vendor.VendorAddressModels.FirstOrDefault().CityModel.Name,
        ////                        IsActive = ot.IsActive,
        ////                        CreatedDate = ot.CreatedDate,
        ////                        CreatedBy = ot.CreatedBy,
        ////                        UpdatedDate = ot.UpdatedDate,
        ////                        UpdatedBy = ot.UpdatedBy
        ////                    };

        ////        return await this.optionalTourMasterGridViewModelRespository.ToPagedListAsync(query, model);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        string messege = ex.ToString();
        ////        return null;
        ////    }
        ////}

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="id">Identifier</param>
        public async Task<OptionalTourAndItemsAddViewModel> GetOptionalToursAndItemsAsyncByOTID(int id)
        {
            try
            {
                var query = await (from ot in this.optionalToursRespository.Table
                            where ot.Id == id
                            select new OptionalTourAndItemsAddViewModel
                            {
                                Id = ot.Id,
                                CountryId = ot.CountryId,
                                VendorId = ot.VendorId,
                                IsActive = ot.IsActive,
                                CreatedDate = ot.CreatedDate,
                                CreatedBy = ot.CreatedBy,
                                UpdatedDate = ot.UpdatedDate,
                                UpdatedBy = ot.UpdatedBy,
                                TourItems = (from oti in this.optionalToursItemRespository.Table
                                            where oti.OptionalTourId == id
                                            select new OptionalTourItemsViewModel
                                            {
                                                AdultPrice = oti.AdultPrice,
                                                ChildPrice = oti.ChildPrice,
                                                OptionalTourId = oti.OptionalTourId,
                                                Id = oti.Id,
                                                CreatedBy = oti.CreatedBy,
                                                CreatedDate = ot.CreatedDate,
                                                TourName = oti.TourName,
                                                UpdatedBy = oti.UpdatedBy,
                                                UpdatedDate = oti.UpdatedDate
                                            }).ToList()
                            }).FirstOrDefaultAsync();

                return query;
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="id">Identifier</param>
        public async Task<OptionalToursMasterModel> GetOptionalTourMasterByIdAsyn(int id)
        {
            try
            {
                return await this.optionalToursRespository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="record">Identifier</param>
        public async Task<int> UpdateOptionalTourMaster(OptionalToursMasterModel record)
        {
            try
            {
                return await this.optionalToursRespository.UpdateAsync(record);
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="records">Identifier</param>
        /// <param name="optionalTourId">Optional Tour Id</param>
        public async Task<int> AddOTItemByOptionalTourId(List<OptionalToursItemsMasterModel> records, int optionalTourId)
        {
            try
            {
                List<OptionalToursItemsMasterModel> existingRecords = await this.optionalToursItemRespository.Table.Where(x => x.OptionalTourId == optionalTourId).ToListAsync();
                if (existingRecords.Count > 0)
                {
                    foreach (var item in existingRecords)
                    {
                        await this.DeleteOptionalTourItem(item);
                    }
                }

                foreach (var item in records)
                {
                    await this.optionalToursItemRespository.InsertAsync(item);
                }

                return 1;
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="record">Identifier</param>
        public async Task<int> DeleteOptionalTourItem(OptionalToursItemsMasterModel record)
        {
            try
            {
                return await this.optionalToursItemRespository.DeleteAsync(record);
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Update All Async.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        /// <param name="record">Identifier</param>
        public async Task<int> AddOptionalTourMaster(OptionalToursMasterModel record)
        {
            try
            {
                await this.optionalToursRespository.InsertAsync(record);
                return record.Id;
            }
            catch (Exception ex)
            {
                string messege = ex.ToString();
                return 0;
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
        public async Task<int?> DeletePackageOptionalTour(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException("Visa");
                }

                if (this.packageOptionalTourRepo.Table.Where(x => x.PackageId == id).Any())
                {
                    var records = await this.packageOptionalTourRepo.Table.Where(x => x.PackageId == id).ToListAsync();
                    foreach (var item in records)
                    {
                        await this.packageOptionalTourRepo.DeleteAsync(item);
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
        public async Task<PackageOptionalTourViewModel> GetPackageOptionalTourAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException("Visa");
                }

                if (this.packageOptionalTourRepo.Table.Where(x => x.PackageId == id).Any())
                {
                    PackageOptionalTourViewModel viewModel = new PackageOptionalTourViewModel
                    {
                        PackageId = id,
                        IncludePackageOptionalTour = true,
                        PackageOptionalTourViewModels = await (from pv in this.packageOptionalTourRepo.Table
                                                           where pv.PackageId == id
                                                           select new TourPackageOptionalTourViewModel
                                                           {
                                                               Id = 0,
                                                               PackageId = pv.PackageId,
                                                               AdultPrice = pv.AdultPrice,
                                                               ChildPrice = pv.ChildPrice,
                                                               CountryId = pv.CountryId,
                                                               VendorId = pv.VendorId,
                                                               TourName = pv.TourName,
                                                               SelectItem = true
                                                           }).ToListAsync()
                    };
                    return viewModel;
                }
                else
                {
                    if (!this.destinationRespository.Table.Where(x => x.D_PackageId == id).Select(x => x.D_Country).Any())
                    {
                        return new PackageOptionalTourViewModel
                        {
                            IncludePackageOptionalTour = false,
                            PackageId = id,
                            PackageOptionalTourViewModels = new List<TourPackageOptionalTourViewModel>()
                        };
                    }
                    else
                    {
                        List<short> countriesId = await this.destinationRespository.Table.Where(x => x.D_PackageId == id).Select(x => x.D_Country).Distinct().ToListAsync();
                        PackageOptionalTourViewModel viewModel = new PackageOptionalTourViewModel
                        {
                            PackageId = id,
                            IncludePackageOptionalTour = false,
                            PackageOptionalTourViewModels = await this.optionalToursRespository.Table
                            .Join(this.optionalToursItemRespository.Table, ot => ot.Id, oti => oti.OptionalTourId, (ot, oti) => new { Tour = ot, TourItem = oti })
                            .Where(x => countriesId.Any(y => x.Tour.CountryId == y) && x.Tour.IsActive)
                            .Select(x => new TourPackageOptionalTourViewModel
                            {
                                Id = 0,
                                PackageId = id,
                                AdultPrice = x.TourItem.AdultPrice,
                                ChildPrice = x.TourItem.ChildPrice,
                                CountryId = x.Tour.CountryId,
                                VendorId = x.Tour.VendorId,
                                SelectItem = false,
                                TourName = x.TourItem.TourName
                            }).ToListAsync()
                        };
                        return viewModel;
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new PackageOptionalTourViewModel
                {
                    IncludePackageOptionalTour = false,
                    PackageId = id,
                    PackageOptionalTourViewModels = new List<TourPackageOptionalTourViewModel>()
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
        public async Task<int?> AddPackageOptionalTourInfo(TourPackageOptionalTourInfoModel record)
        {
            try
            {
                if (record == null)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.packageOptionalTourRepo.InsertAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}