// <copyright file="RatePlanService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="HiTours.Services.IRatePlanService" />
    public class RatePlanService : IRatePlanService
    {
        private readonly IRepository<RatePlanModel> ratePlanRepository;
        private readonly IRepository<AmenitiesMasterModel> amenetiesMasterRepository;
        private readonly IRepository<RatePlanAmenitiesModel> ratePlanAmenitiesRepository;
        private readonly IRepository<RoomConfigurationModel> roomConfigurationRepository;
        private readonly IRepository<RatePlanGridViewModel> ratePlanGridModel;
        private readonly IRepository<Dropdown> dropdownRespository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatePlanService" /> class.
        /// </summary>
        /// <param name="ratePlanGridModel">The model.</param>
        /// <param name="ratePlanAmenitiesRepository">The Package Id</param>
        /// <param name="dropdownRespository">Dropdown</param>
        /// <param name="amenetiesMasterRepository">Ameneties Master</param>
        /// <param name="ratePlanRepository">Rate Plan Repo</param>
        /// <param name="roomConfigurationRepository">Room Configuration Repo</param>
        public RatePlanService(IRepository<RatePlanGridViewModel> ratePlanGridModel, IRepository<RatePlanAmenitiesModel> ratePlanAmenitiesRepository, IRepository<Dropdown> dropdownRespository, IRepository<AmenitiesMasterModel> amenetiesMasterRepository, IRepository<RatePlanModel> ratePlanRepository, IRepository<RoomConfigurationModel> roomConfigurationRepository)
        {
            this.ratePlanAmenitiesRepository = ratePlanAmenitiesRepository;
            this.ratePlanGridModel = ratePlanGridModel;
            this.ratePlanRepository = ratePlanRepository;
            this.amenetiesMasterRepository = amenetiesMasterRepository;
            this.roomConfigurationRepository = roomConfigurationRepository;
            this.dropdownRespository = dropdownRespository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="packageId">The Package Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetRatePlanAsync(DataTableParameter model, Guid packageId)
        {
            var query = this.ratePlanRepository.Table.Where(x => x.RoomConfigModel.PR_PackageId == packageId && !x.RP_IsDeleted).Select(x => new RatePlanGridViewModel
            {
                RoomName = x.RoomConfigModel.RoomTypeModel.Name,
                RP_BookingEndDate = x.RP_BookingEndDate,
                RP_BookingStartDate = x.RP_BookingStartDate,
                RP_ExtraAdultPrice = x.RP_ExtraAdultPrice,
                RP_ExtraChildPrice = x.RP_ExtraChildPrice,
                RP_ExtraInfantPrice = x.RP_ExtraInfantPrice,
                RP_FakePrice = x.RP_FakePrice,
                RP_Id = x.RP_Id,
                RP_IsActive = x.RP_IsActive,
                RP_IsDeleted = x.RP_IsDeleted,
                RP_Name = x.RP_Name,
                RP_RoomConfigId = x.RP_RoomConfigId,
                RP_SellingPrice = x.RP_SellingPrice,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                RP_TravelEndDate = x.RP_TravelEndDate,
                RP_TravelStartDate = x.RP_TravelStartDate
            });

            return await this.ratePlanGridModel.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="ratePlanId">Rate Plan ID</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<RatePlanModel> GetByIdAsync(int ratePlanId)
        {
            return await this.ratePlanRepository.Table.Where(x => x.RP_Id == ratePlanId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomId">The Room Id</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetRoomConfigurationListAsync(string search, short page, int? roomId, Guid packageId)
        {
            var roomConfig = this.roomConfigurationRepository.Table.Where(x => x.RoomTypeModel.Name.StartsWith(search) && x.PR_PackageId == packageId && x.PR_IsActive && !x.PR_IsDeleted);

            var query = roomConfig.OrderBy(x => x.RoomTypeModel.Name)
            .Select(x => new Dropdown
            {
                Id = x.PR_Id.ToString(),
                Name = x.RoomTypeModel.Name
            });

            if (roomId != 0 && roomId != null)
            {
                query = query.Where(x => x.Id == roomId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetieId">The Ameneties Id</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetAmenitiesListAsync(string search, short page, int? amenetieId)
        {
            var ameneties = this.amenetiesMasterRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && !x.IsHotelOnly);

            var query = ameneties.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });

            if (amenetieId != 0 && amenetieId != null)
            {
                query = query.Where(x => x.Id == amenetieId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Rate Plan Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Room Configuration</exception>
        public async Task<int> UpdateAsync(RatePlanModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("vendor");
                }

                return await this.ratePlanRepository.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Rate Plan Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int> AddAsync(RatePlanModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("Vendor");
                }

                await this.ratePlanRepository.InsertAsync(model);
                return model.RP_Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <param name="amenetieId">Ameneties Id</param>
        /// <returns>
        /// Add Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int> AddRatePlanAmenetiesAsync(int ratePlanId, int amenetieId)
        {
            try
            {
                if (ratePlanId == 0 || amenetieId == 0)
                {
                    throw new ArgumentNullException("Vendor");
                }

                RatePlanAmenitiesModel record = new RatePlanAmenitiesModel
                {
                    RA_AmenitiesId = amenetieId,
                    RA_IsActive = true,
                    RA_RatePlanId = ratePlanId
                };
                return await this.ratePlanAmenitiesRepository.InsertAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The Room Configuration Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int> DeleteRatePlanAsync(int id)
        {
            try
            {
                if (!(id > 0))
                {
                    throw new ArgumentNullException("Vendor");
                }

                var record = await this.ratePlanRepository.Table.Where(x => x.RP_Id == id).FirstOrDefaultAsync();
                record.RP_IsDeleted = true;
                return await this.ratePlanRepository.UpdateAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}