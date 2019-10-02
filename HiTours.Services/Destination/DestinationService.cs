// <copyright file="DestinationService.cs" company="Luxury Travel Deals">
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
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using static HiTours.Core.Enums;

    /// <summary>
    /// PackageService
    /// </summary>
    /// <seealso cref="HiTours.Services.IDestinationService" />
    public class DestinationService : IDestinationService
    {
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<DestinationModel> destinationRespository;
        private readonly IRepository<DestinationValidityModel> destinationValidityRespository;
        private readonly IRepository<DestinationGridViewModel> destinationGridViewModelRespository;
        private readonly IRepository<DestinationValidityGridViewModel> destinationValidityGridModelRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DestinationService" /> class.
        /// </summary>
        /// <param name="destinationValidityGridModelRepo">Destnation Validity grid View model repo.</param>
        /// <param name="destinationGridViewModelRespository">Destination Grid View Model</param>
        /// <param name="dropdownRespository">Dropdown</param>
        /// <param name="destinationRespository">Destination Repository</param>
        /// <param name="destinationValidityRespository">Destination Validity Repository</param>
        public DestinationService(IRepository<DestinationValidityGridViewModel> destinationValidityGridModelRepo, IRepository<DestinationGridViewModel> destinationGridViewModelRespository, IRepository<Dropdown> dropdownRespository, IRepository<DestinationModel> destinationRespository, IRepository<DestinationValidityModel> destinationValidityRespository)
        {
            this.destinationValidityGridModelRepo = destinationValidityGridModelRepo;
            this.destinationGridViewModelRespository = destinationGridViewModelRespository;
            this.destinationRespository = destinationRespository;
            this.destinationValidityRespository = destinationValidityRespository;
            this.dropdownRespository = dropdownRespository;
        }

        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <param name="model">The model.</param>
        /////// <param name="packageId">The Package Id</param>
        /////// <returns>
        /////// GetAllAsync
        /////// </returns>
        ////public async Task<DataTableResult> GetPackageDestinationAsync(DataTableParameter model, Guid packageId)
        ////{
        ////    var query = this.destinationRespository.Table.Where(x => x.D_PackageId == packageId).Select(x => new DestinationGridViewModel
        ////    {
        ////        CountryName = x.CountryModel.Name,
        ////        CityName = x.CityModel.Name,
        ////        VendorName = x.VendorModel.BusinessName + ", " + x.VendorModel.VendorAddressModels.FirstOrDefault().CityModel.Name,
        ////        DestinationId = x.D_Id,
        ////        IATACode = x.D_IATACode,
        ////        NightCount = x.D_Nights,
        ////        RegionName = x.RegionModel.Name,
        ////        IsActive = x.D_IsActive,
        ////        UpdatedBy = x.UpdatedBy,
        ////        UpdatedDate = x.UpdatedDate,
        ////        CreatedBy = x.CreatedBy,
        ////        CreatedDate = x.CreatedDate
        ////    });

        ////    return await this.destinationGridViewModelRespository.ToPagedListAsync(query, model);
        ////}

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="packageId">The Package Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetPackageDestinationValidityAsync(DataTableParameter model, Guid packageId)
        {
            var query = this.destinationValidityRespository.Table.Where(x => x.DestinationModel.D_PackageId == packageId).Select(x => new DestinationValidityGridViewModel
            {
                AdultRate = x.DV_AdultPriceDBL.ToString(),
                ChildRate = x.DV_ChildPriceDBL.ToString(),
                CountryCity = x.DestinationModel.CountryModel.Name + ", " + x.DestinationModel.CityModel.Name,
                DV_Id = x.DV_Id,
                EndDate = x.DV_ValidityEndDate,
                StartDate = x.DV_ValidityStartDate,
                InfantRate = x.DV_InfantPriceDBL.ToString(),
                IsActive = x.DV_IsActive,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate
            });

            return await this.destinationValidityGridModelRepo.ToPagedListAsync(query, model);
        }

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
        public async Task<IList<Dropdown>> GetDestinationForValidityListAsync(string search, short page, int? destinationId, Guid packageId)
        {
            var destination = this.destinationRespository.Table.Where(x => (x.CityModel.Name + ", " + x.CountryModel.Name).StartsWith(search) && x.D_PackageId == packageId);

            var query = destination.OrderBy(x => (x.CityModel.Name + ", " + x.CountryModel.Name))
            .Select(x => new Dropdown
            {
                Id = x.D_Id.ToString(),
                Name = x.CityModel.Name + ", " + x.CountryModel.Name
            });

            if (destinationId != 0 && destinationId != null)
            {
                query = query.Where(x => x.Id == destinationId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Add Destination asynchronous.
        /// </summary>
        /// <param name="model">The Destination Record.</param>
        /// <returns>
        /// Add Destination Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Destination</exception>
        public async Task<int> AddDestinationAsync(DestinationModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("Destination");
                }

                return await this.destinationRespository.InsertAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add Destination asynchronous.
        /// </summary>
        /// <param name="model">The Destination Validity Record.</param>
        /// <returns>
        /// Add Destination Validity Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Destination</exception>
        public async Task<int> AddDestinationValidityAsync(DestinationValidityModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("Destination");
                }

                return await this.destinationValidityRespository.InsertAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}