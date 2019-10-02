// <copyright file="PackageNightsService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// TravelStyleService
    /// </summary>
    /// <seealso cref="HiTours.Services.IRegionService" />
    public class PackageNightsService : IPackageNightsService
    {
        private readonly IRepository<TourPackageNightModel> tourPackageNightRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageNightsService" /> class.
        /// </summary>
        /// <param name="tourPackageNightRepository">The tour package night repository.</param>
        public PackageNightsService(IRepository<TourPackageNightModel> tourPackageNightRepository)
        {
            this.tourPackageNightRepository = tourPackageNightRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The region model.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(TourPackageNightModel tourpackageNight)
        {
            try
            {
                if (tourpackageNight == null)
                {
                    throw new ArgumentNullException("tourpackageNight");
                }

                return await this.tourPackageNightRepository.InsertAsync(tourpackageNight);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(TourPackageNightModel tourpackageNight)
        {
            try
            {
                if (tourpackageNight == null)
                {
                    throw new ArgumentNullException("tourPackage");
                }

                this.tourPackageNightRepository.UpdateCompleteGraph(tourpackageNight, tourpackageNight.Id);
                return await this.tourPackageNightRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<TourPackageNightModel> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await this.tourPackageNightRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
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
            var query = this.tourPackageNightRepository.Table;

            return await this.tourPackageNightRepository.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The category.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(TourPackageNightModel tourPackage)
        {
            if (tourPackage == null)
            {
                throw new ArgumentNullException("hotelcategory");
            }

            return await this.tourPackageNightRepository.DeleteAsync(tourPackage);
        }
    }
}