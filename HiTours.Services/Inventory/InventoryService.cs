// <copyright file="InventoryService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="HiTours.Services.IInventoryService" />
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<RoomInventoryModel> roomInventoryRepository;
        private readonly IRepository<RoomConfigurationModel> roomConfigurationRepository;
        private readonly IRepository<RoomInventoryGridViewModel> roomInventoryGridModel;
        private readonly IRepository<Dropdown> dropdownRespository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService" /> class.
        /// </summary>
        /// <param name="dropdownRespository">DropDown Repo</param>
        /// <param name="roomInventoryRepository">The Room Configuration Repositry.</param>
        /// <param name="roomInventoryGridModel">Grid</param>
        /// <param name="roomConfigurationRepository">Room Config Repository</param>
        public InventoryService(IRepository<Dropdown> dropdownRespository, IRepository<RoomInventoryModel> roomInventoryRepository, IRepository<RoomInventoryGridViewModel> roomInventoryGridModel, IRepository<RoomConfigurationModel> roomConfigurationRepository)
        {
            this.roomInventoryRepository = roomInventoryRepository;
            this.roomInventoryGridModel = roomInventoryGridModel;
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
        public async Task<DataTableResult> GetPackageInventoryAsync(DataTableParameter model, Guid packageId)
        {
            var query = this.roomInventoryRepository.Table.Where(x => x.RoomConfigModel.PR_PackageId == packageId && !x.RI_IsDeleted).Select(x => new RoomInventoryGridViewModel
            {
                RoomName = x.RoomConfigModel.RoomTypeModel.Name,
                RI_BaseRate = x.RI_BaseRate,
                RI_ExtraAdultCost = x.RI_ExtraAdultCost,
                RI_ExtraChildCost = x.RI_ExtraChildCost,
                RI_ExtraInfantCost = x.RI_ExtraInfantCost,
                RI_Id = x.RI_Id,
                RI_Inventory = x.RI_Inventory,
                RI_IsActive = x.RI_IsActive,
                RI_IsDeleted = x.RI_IsDeleted,
                RI_RoomConfigId = x.RI_RoomConfigId,
                RI_SurgeRate = x.RI_SurgeRate,
                RI_EndDate = x.RI_EndDate,
                RI_StartDate = x.RI_StartDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
            });

            return await this.roomInventoryGridModel.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomInventoryId">Room Configuration ID</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<RoomInventoryModel> GetByIdAsync(int roomInventoryId)
        {
            return await this.roomInventoryRepository.Table.Where(x => x.RI_Id == roomInventoryId).FirstOrDefaultAsync();
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
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Room Configuration Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Room Configuration</exception>
        public async Task<int> UpdateAsync(RoomInventoryModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("vendor");
                }

                return await this.roomInventoryRepository.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Room Configuration Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int> AddAsync(RoomInventoryModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("Vendor");
                }

                return await this.roomInventoryRepository.InsertAsync(model);
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
        public async Task<int> DeleteInventoryAsync(int id)
        {
            try
            {
                if (!(id > 0))
                {
                    throw new ArgumentNullException("Vendor");
                }

                var record = await this.roomInventoryRepository.Table.Where(x => x.RI_Id == id).FirstOrDefaultAsync();
                record.RI_IsDeleted = true;
                return await this.roomInventoryRepository.UpdateAsync(record);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}