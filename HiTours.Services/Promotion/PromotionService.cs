// <copyright file="PromotionService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="HiTours.Services.IPromotionService" />
    public class PromotionService : IPromotionService
    {
        /// <summary>
        /// The Promotion repository
        /// </summary>
        private readonly IRepository<PromotionModel> promotionRepository;
        private readonly IRepository<Dropdown> dropDownRepository;

        /// <summary>
        /// The PromotionType repository
        /// </summary>
        private readonly IRepository<PromotionTypeModel> promotionTypeRepository;
        private readonly IRepository<PromotionGridViewModel> promotionGridViewRepo;
        private readonly IRepository<DealsPromotionModel> dealPromotionRepo;
        private readonly IRepository<DealsPromotion_RoomType> dealPromotionRoomTypeRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionService" /> class.
        /// </summary>
        /// <param name="dealPromotionRoomTypeRepo">Deal Promotion And Room Type Repository</param>
        /// <param name="dealPromotionRepo">Deal Promotion Repository</param>
        /// <param name="dropDownRepository">Drop Down Repo</param>
        /// <param name="promotionGridViewRepo">PromotionGrid Repo</param>
        /// <param name="promotionRepository">The Promotion repository.</param>
        /// <param name="promotionTypeRepository">The Promotion Type repository.</param>
        public PromotionService(IRepository<DealsPromotion_RoomType> dealPromotionRoomTypeRepo, IRepository<DealsPromotionModel> dealPromotionRepo, IRepository<Dropdown> dropDownRepository, IRepository<PromotionGridViewModel> promotionGridViewRepo, IRepository<PromotionModel> promotionRepository, IRepository<PromotionTypeModel> promotionTypeRepository)
        {
            this.dealPromotionRoomTypeRepo = dealPromotionRoomTypeRepo;
            this.dealPromotionRepo = dealPromotionRepo;
            this.dropDownRepository = dropDownRepository;
            this.promotionGridViewRepo = promotionGridViewRepo;
            this.promotionRepository = promotionRepository;
            this.promotionTypeRepository = promotionTypeRepository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllPromotionAsync(DataTableParameter model)
        {
            var query = this.promotionRepository.Table.Where(x => !x.IsDeleted).Select(x => new PromotionGridViewModel
            {
                CouponCode = x.CouponCode,
                DiscountValue = x.DiscountValue.ToString() + x.MarginTypeModel.Description,
                MaxDiscountFlat = x.MaxDiscountFlat,
                MaxCount = x.MaxCount,
                ValidityEnd = x.ValidityEnd.ToString("dd/MM/yyyy"),
                ValidityStart = x.ValidityStart.ToString("dd/MM/yyyy"),
                Id = x.Id,
                IsActive = x.IsActive
            });

            return await this.promotionGridViewRepo.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="promotionId">The Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<PromotionModel> GetPromotionById(int promotionId)
        {
            try
            {
                return await this.promotionRepository.Table.Where(x => x.Id == promotionId).FirstOrDefaultAsync();
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
        /// <param name="code">The Coupon Code.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<PromotionModel> GetPromotionByCode(string code)
        {
            try
            {
                return await this.promotionRepository.Table.Where(x => x.CouponCode.ToUpper() == code.ToUpper() && x.MaxCount > 0 && x.IsActive && !x.IsDeleted && (x.ValidityStart <= DateTime.Now && x.ValidityEnd >= DateTime.Now)).FirstOrDefaultAsync();
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
        public async Task<IList<Dropdown>> GetPromotionTypeItems()
        {
            try
            {
                var query = this.promotionTypeRepository.Table
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });

                return await this.dropDownRepository.ToOptionListAsync(query, 1);
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
        /// <param name="model">The Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<int?> AddPromotion(PromotionModel model)
        {
            try
            {
                return await this.promotionRepository.InsertAsync(model);
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
        /// <param name="model">The Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<int?> UpdatePromotion(PromotionModel model)
        {
            try
            {
                return await this.promotionRepository.UpdateAsync(model);
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
        /// <param name="dealId">The Deal Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<DealsPromotionViewModel>> GetAllDealPromotionsById(int dealId)
        {
            try
            {
                return await this.dealPromotionRepo.Table.Where(x => x.PackageId == dealId && x.IsActive && !x.IsDeleted).Select(x => new DealsPromotionViewModel
                {
                    Id = x.Id,
                    AllWeek = x.AllWeek,
                    BookingEndDate = x.BookingEndDate,
                    BookingStartDate = x.BookingStartDate,
                    CouponCode = string.Empty,
                    DiscountType = x.DiscountType,
                    DiscountValue = x.DiscountValue,
                    EndDay = x.EndDay,
                    Fri = x.Fri,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    LengthOfStay = x.LengthOfStay.HasValue ? x.LengthOfStay.Value : 0,
                    Mon = x.Mon,
                    PackageId = x.PackageId,
                    Remark = x.Remark,
                    Sat = x.Sat,
                    StartDay = x.StartDay,
                    Sun = x.Sun,
                    Thu = x.Thu,
                    TravelEndDate = x.TravelEndDate,
                    TravelStartDate = x.TravelStartDate,
                    Tue = x.Tue,
                    Type = x.Type,
                    Wed = x.Wed
                }).ToListAsync();
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
        /// <param name="dealPromotionId">The Deal Promotion Is.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DealsPromotion_RoomType> GetDealRoomPromotionByDealPromotionId(int dealPromotionId)
        {
            try
            {
                return await this.dealPromotionRoomTypeRepo.Table.Where(x => x.PromotionId == dealPromotionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }
    }
}