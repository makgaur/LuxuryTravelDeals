// <copyright file="CancellationService.cs" company="Luxury Travel Deals">
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
    /// Hotelier Service
    /// </summary>
    public class CancellationService : ICancellationService
    {
        private readonly IRepository<CancellationPolicyModel> cancellationPolicyRepo;
        private readonly IRepository<HotelierCancellationPolicyModel> hotelierCancellationPolicyRepo;
        private readonly IRepository<DealsCancellationPolicyModel> dealsCancellationPolicyRepo;
        private readonly IRepository<PackageMarginTypeModel> marginTypeRepo;
        private readonly IRepository<Dropdown> dropDownRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancellationService"/> class.
        /// Deals Service
        /// </summary>
        /// <param name="cancellationPolicyRepo">Cancellation Policy Repo</param>
        /// <param name="marginTypeRepo">Margin Type Repo</param>
        /// <param name="dropDownRepo">DropDown Repo</param>
        /// <param name="hotelierCancellationPolicyRepo">Hotelier Cancellation Policy Repo</param>
        /// <param name="dealsCancellationPolicyRepo">Deals Cancellation Policy Repo</param>
        public CancellationService(
            IRepository<CancellationPolicyModel> cancellationPolicyRepo,
            IRepository<PackageMarginTypeModel> marginTypeRepo,
            IRepository<Dropdown> dropDownRepo,
            IRepository<HotelierCancellationPolicyModel> hotelierCancellationPolicyRepo,
            IRepository<DealsCancellationPolicyModel> dealsCancellationPolicyRepo)
        {
            this.hotelierCancellationPolicyRepo = hotelierCancellationPolicyRepo;
            this.dealsCancellationPolicyRepo = dealsCancellationPolicyRepo;
            this.marginTypeRepo = marginTypeRepo;
            this.dropDownRepo = dropDownRepo;
            this.cancellationPolicyRepo = cancellationPolicyRepo;
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="dealType">Deal Type</param>
        /// <returns>InformationModel</returns>
        public async Task<List<CancellationPolicyViewModel>> GetCancellationPolicyByDealType(int dealType)
        {
            try
            {
                var result = await this.cancellationPolicyRepo.Table.Where(x => x.DealType == dealType && !x.IsDeleted).Select(x => new CancellationPolicyViewModel
                {
                    Id = x.Id,
                    Charge = x.Charge,
                    ChargeType = x.ChargeType,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    DealType = x.DealType,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    MaxDay = x.MaxDay,
                    MinDay = x.MinDay,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                }).ToListAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new List<CancellationPolicyViewModel>();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<CancellationPolicyViewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<HotelierCancellationPolicyViewModel>> GetHotelierCancellationPolicyByHotelId(int hotelId)
        {
            try
            {
                if (this.hotelierCancellationPolicyRepo.Table.Where(x => x.HotelId == hotelId).Count() > 0)
                {
                    return await this.hotelierCancellationPolicyRepo.Table.Where(x => x.HotelId == hotelId).Select(x => new HotelierCancellationPolicyViewModel
                    {
                        Id = x.Id,
                        HotelId = Convert.ToInt32(x.HotelId),
                        Charge = x.Charge,
                        MarginType = x.MarginType,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        IsDeleted = false,
                        MaxDay = x.MaxDay,
                        MinDay = x.MinDay,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate
                    }).ToListAsync();
                }
                else
                {
                    var result = await this.cancellationPolicyRepo.Table.Where(x => x.DealType == 1 && !x.IsDeleted).Select(x => new HotelierCancellationPolicyViewModel
                    {
                        Id = 0,
                        Charge = x.Charge,
                        MarginType = x.ChargeType,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        IsDeleted = false,
                        MaxDay = x.MaxDay,
                        MinDay = x.MinDay,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        HotelId = hotelId
                    }).ToListAsync();
                    if (result.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return new List<HotelierCancellationPolicyViewModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<HotelierCancellationPolicyViewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        public async Task<IList<Dropdown>> GetMarginTypeItems()
        {
            try
            {
                var query = this.marginTypeRepo.Table
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });

                return await this.dropDownRepo.ToOptionListAsync(query, 1);
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
        /// <param name="model">Deal Type</param>
        public async Task<int?> DeleteCancellationPolicy(CancellationPolicyModel model)
        {
            try
            {
                return await this.cancellationPolicyRepo.DeleteAsync(model);
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
        /// <param name="model">Deal Type</param>
        public async Task<int?> AddCancellationPolicy(CancellationPolicyModel model)
        {
            try
            {
                return await this.cancellationPolicyRepo.InsertAsync(model);
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
        /// <param name="model">Deal Type</param>
        public async Task<int?> UpdateCancellationPolicy(CancellationPolicyModel model)
        {
            try
            {
                return await this.cancellationPolicyRepo.UpdateAsync(model);
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
        /// <param name="id">Deal Type</param>
        public async Task<CancellationPolicyModel> GetCancellationPolicyById(int id)
        {
            try
            {
                return await this.cancellationPolicyRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
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
        /// <param name="id">Deal Type</param>
        public async Task<HotelierCancellationPolicyModel> GetHotelierCancellationPolicyById(int id)
        {
            try
            {
                return await this.hotelierCancellationPolicyRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
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
        /// <param name="model">Deal Type</param>
        public async Task<int?> UpdateHotelierCancellationPolicy(HotelierCancellationPolicyModel model)
        {
            try
            {
                return await this.hotelierCancellationPolicyRepo.UpdateAsync(model);
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
        /// <param name="model">Deal Type</param>
        public async Task<int?> AddHotelierCancellationPolicy(HotelierCancellationPolicyModel model)
        {
            try
            {
                return await this.hotelierCancellationPolicyRepo.InsertAsync(model);
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
        /// <param name="model">Deal Type</param>
        public async Task<int?> DeleteHotelierCancellationPolicy(HotelierCancellationPolicyModel model)
        {
            try
            {
                return await this.hotelierCancellationPolicyRepo.DeleteAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// get cancellations policy
        /// </summary>
        /// <param name="packageId">Pacakege Identifer</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Deals Cacellation policy Model</returns>
        public async Task<List<DealsCancellationPolicyViewModel>> GetDealsCancellationPolicyByPackageId(int packageId, int packageTypeId)
        {
            try
            {
                if (this.dealsCancellationPolicyRepo.Table.Where(x => x.PackageId == packageId).Count() > 0)
                {
                    return await this.dealsCancellationPolicyRepo.Table.Where(x => x.PackageId == packageId).Select(x => new DealsCancellationPolicyViewModel
                    {
                        Id = x.Id,
                        PackageId = packageId,
                        Charge = x.Charge,
                        MarginType = x.MarginType,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        IsDeleted = false,
                        MaxDay = x.MaxDay,
                        MinDay = x.MinDay,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate
                    }).ToListAsync();
                }
                else
                {
                    var result = await this.cancellationPolicyRepo.Table.Where(x => x.DealType == packageTypeId && !x.IsDeleted).Select(x => new DealsCancellationPolicyViewModel
                    {
                        Id = 0,
                        Charge = x.Charge,
                        MarginType = x.ChargeType,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        IsDeleted = false,
                        MaxDay = x.MaxDay,
                        MinDay = x.MinDay,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        PackageId = packageId
                    }).ToListAsync();
                    if (result.Count > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return new List<DealsCancellationPolicyViewModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealsCancellationPolicyViewModel>();
            }
        }

        /// <summary>
        /// get cancellations policy
        /// </summary>
        /// <param name="model">Pacakege Identifer</param>
        /// <returns>Deals Cacellation policy Model</returns>
        public async Task<int?> DeleteDealsCancellationPolicy(DealsCancellationPolicyModel model)
        {
            try
            {
                return await this.dealsCancellationPolicyRepo.DeleteAsync(model);
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
        /// <param name="model">Deal Type</param>
        public async Task<int?> AddDealsCancellationPolicy(DealsCancellationPolicyModel model)
        {
            {
                try
                {
                    return await this.dealsCancellationPolicyRepo.InsertAsync(model);
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                    return null;
                }
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        public async Task<int?> UpdateDealsCancellationPolicy(DealsCancellationPolicyModel model)
        {
            try
            {
                return await this.dealsCancellationPolicyRepo.UpdateAsync(model);
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
        /// <param name="id">Deal Type</param>
        public async Task<DealsCancellationPolicyModel> GetDealsCancellationPolicyById(int id)
        {
            try
            {
                return await this.dealsCancellationPolicyRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }
    }
}


