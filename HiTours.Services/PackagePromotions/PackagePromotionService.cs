// <copyright file="PackagePromotionService.cs" company="Luxury Travel Deals">
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

    /// <summary>
    /// ApplicationUserService
    /// </summary>
    /// <seealso cref="HiTours.Services.IApplicationUserService" />
    /// <seealso cref="IApplicationUserService" />
    public class PackagePromotionService : IPackagePromotionService
    {
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<PackagePromotionsModel> packagePromotionsRepository;
        private readonly IRepository<PackagePromotionsViewModel> packagePromotionsViewModelRepository;
        private readonly IRepository<PackagePromotions_PackageModel> packagePromoRelModelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackagePromotionService"/> class.
        /// </summary>
        /// <param name="dropdownRespository">Dropdown Repository</param>
        /// <param name="packagePromoRelModelRepository">Package Promotion Relation Model Repo.</param>
        /// <param name="packagePromotionsRepository">The Package Promoton service repository.</param>
        /// <param name="packagePromotionsViewModelRepository">Package promotion view Model Repository</param>
        public PackagePromotionService(IRepository<Dropdown> dropdownRespository, IRepository<PackagePromotions_PackageModel> packagePromoRelModelRepository, IRepository<PackagePromotionsModel> packagePromotionsRepository, IRepository<PackagePromotionsViewModel> packagePromotionsViewModelRepository)
        {
            this.dropdownRespository = dropdownRespository;
            this.packagePromoRelModelRepository = packagePromoRelModelRepository;
            this.packagePromotionsRepository = packagePromotionsRepository;
            this.packagePromotionsViewModelRepository = packagePromotionsViewModelRepository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllAsync(DataTableParameter model)
        {
            var query = this.packagePromotionsRepository.Table.Where(x => !x.IsDeleted).Select(x => new PackagePromotionsViewModel
            {
                Id = x.Id,
                RecId = 0,
                Name = x.DisplayName,
                DisplayValue = x.Value.ToString() + " " + x.MarginTypeModel.Description + " off Cost before " + x.Days.ToString() + " days.",
                IsActive = x.IsActive,
                IsSuper = x.IsDeleted,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate
            });
            var list = await this.packagePromotionsViewModelRepository.ToPagedListAsync(query, model);
            return list;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="promotionId">The Cancellation Policy identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackagePromotionsModel> GetByIdAsync(int promotionId)
        {
            if (promotionId == 0)
            {
                return new PackagePromotionsModel { Id = 0 };
            }

            return await this.packagePromotionsRepository.Table.FirstOrDefaultAsync(m => m.Id == promotionId);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Currency Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Package Currency</exception>
        public async Task<int> UpdateAsync(PackagePromotionsModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("vendor");
                }

                return await this.packagePromotionsRepository.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Vendor Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int> AddAsync(PackagePromotionsModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("Vendor");
                }

                return await this.packagePromotionsRepository.InsertAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">TThe Package Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllPackagePromotionsListAsync(DataTableParameter model, Guid id)
        {
            try
            {
                var query = this.packagePromoRelModelRepository.Table.Join(this.packagePromotionsRepository.Table, rel => rel.PromotionId, p => p.Id, (rel, p) => new { rel, p })
                .Where(x => x.rel.PackageId == id).Select(x => new PackagePromotionsViewModel
                {
                    Id = x.p.Id,
                    Name = x.p.DisplayName,
                    RecId = x.rel.Id,
                    DisplayValue = x.p.Value.ToString() + " " + x.p.MarginTypeModel.Description + " off Cost before " + x.p.Days.ToString() + " days.",
                    IsActive = x.rel.IsActive
                });
                var list = await this.packagePromotionsViewModelRepository.ToPagedListAsync(query, model);
                return list;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">TThe Package Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<int>> GetAllPackagePromotionsAsync(Guid id)
        {
            return await this.packagePromoRelModelRepository.Table
                .Where(x => x.PackageId == id && x.IsActive).Select(x => x.PromotionId).ToListAsync();
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="promotionId">The currency identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetDropdownPromotionListAsync(string search, short page, List<int> promotionId)
        {
            var promotions = this.packagePromotionsRepository.Table.Where(x => x.DisplayName.StartsWith(search));

            var query = promotions.OrderBy(x => x.DisplayName)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.DisplayName
            });

            if (promotionId.Count > 0)
            {
                query = query.Where(x => promotionId.Contains(Convert.ToInt32(x.Id)));
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="packageId">Package Id</param>
        /// <param name="promotionId">Promotion Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public bool CheckDuplicatePackagePromotion(Guid packageId, int promotionId)
        {
            var count = this.packagePromoRelModelRepository.Table.Where(x => x.PackageId == packageId && x.PromotionId == promotionId).Count();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The Vendor Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int> AddPackagePromotionAsync(PackagePromotions_PackageModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("Vendor");
                }

                return await this.packagePromoRelModelRepository.InsertAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">TThe Package Identifier</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<int> DeletePackagePromotionRelRecord(int id)
        {
            var record = await this.packagePromoRelModelRepository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            return await this.packagePromoRelModelRepository.DeleteAsync(record);
        }
    }
}