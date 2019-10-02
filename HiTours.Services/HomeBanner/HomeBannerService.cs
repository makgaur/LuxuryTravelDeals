// <copyright file="HomeBannerService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// HomeBannerService
    /// </summary>
    public class HomeBannerService : IHomeBannerService
    {
        private readonly IRepository<HomeBannerModel> homeBannerRepository;
        private readonly IRepository<PopularDestinationModel> popularDestinationRepository;
        private readonly IRepository<PackageStateModel> packageStateRepository;
        private readonly IRepository<PackageCityModel> packageCityRepository;
        private readonly IRepository<PackageCountryModel> packageCountryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeBannerService"/> class.
        /// </summary>
        /// <param name="packageStateRepository">Package State Repository</param>
        /// <param name="packageCityRepository">Package City Repository</param>
        /// <param name="packageCountryRepository">Package Country Repository</param>
        /// <param name="homeBannerRepository">The package image repository.</param>
        /// <param name="popularDestinationRepository">Popular Destination Repository</param>
        public HomeBannerService(IRepository<PackageStateModel> packageStateRepository, IRepository<PackageCityModel> packageCityRepository, IRepository<PackageCountryModel> packageCountryRepository, IRepository<HomeBannerModel> homeBannerRepository, IRepository<PopularDestinationModel> popularDestinationRepository)
        {
            this.packageCityRepository = packageCityRepository;
            this.packageCountryRepository = packageCountryRepository;
            this.packageStateRepository = packageStateRepository;
            this.popularDestinationRepository = popularDestinationRepository;
            this.homeBannerRepository = homeBannerRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Insert Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> InsertAsync(HomeBannerModel packageImage)
        {
            if (packageImage == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            return await this.homeBannerRepository.InsertAsync(packageImage);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageImages">The package images.</param>
        /// <returns>Insert Multiple Records Async</returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> InsertAsync(IEnumerable<HomeBannerModel> packageImages)
        {
            if (packageImages == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            this.homeBannerRepository.AddToContext(packageImages);
            return await this.homeBannerRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="popularDestinationModel">The Popular Destination.</param>
        /// <returns>Insert Multiple Records Async</returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> InsertPopularDestinationAsync(PopularDestinationModel popularDestinationModel)
        {
            try
            {
                if (popularDestinationModel == null)
                {
                    throw new ArgumentNullException("packageImage");
                }

                return await this.popularDestinationRepository.InsertAsync(popularDestinationModel);
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> UpdateAsync(HomeBannerModel packageImage)
        {
            if (packageImage == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            return await this.homeBannerRepository.UpdateAsync(packageImage);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="popularDestinationModel">Popular Destination Model</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> UpdatePopularDestinationAsync(PopularDestinationModel popularDestinationModel)
        {
            if (popularDestinationModel == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            try
            {
                return await this.popularDestinationRepository.UpdateAsync(popularDestinationModel);
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Delete Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> DeleteAsync(HomeBannerModel packageImage)
        {
            if (packageImage == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            return await this.homeBannerRepository.DeleteAsync(packageImage);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Delete Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> DeletePopularDestinationAsync(PopularDestinationModel packageImage)
        {
            if (packageImage == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            return await this.popularDestinationRepository.DeleteAsync(packageImage);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="packageImageId">The package image identifier.</param>
        /// <returns>
        /// Get Record By Id Async
        /// </returns>
        public async Task<HomeBannerModel> GetByIdAsync(int packageImageId)
        {
            return await this.homeBannerRepository.Table.FirstOrDefaultAsync(m => m.Id == packageImageId);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="popularDestinationId">The package image identifier.</param>
        /// <returns>
        /// Get Record By Id Async
        /// </returns>
        public async Task<PopularDestinationModel> GetPopularDestinationByIdAsync(int popularDestinationId)
        {
            return await this.popularDestinationRepository.Table.Where(m => m.Id == popularDestinationId).FirstOrDefaultAsync();
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
            var query = this.homeBannerRepository.Table;
            var records = await this.homeBannerRepository.ToPagedListAsync(query, model);
            return records;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllPopularDestinationAsync(DataTableParameter model)
        {
            try
            {
                var query = this.popularDestinationRepository.Table;
                var records = await this.popularDestinationRepository.ToPagedListAsync(query, model);
                return records;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the banners list asynchronous.
        /// </summary>
        /// <returns>GetBannersListAsync</returns>
        public async Task<IList<HomeBannerViewModel>> GetBannersListAsync()
        {
            var query = this.homeBannerRepository.Table.Select(x => new HomeBannerViewModel
            {
                Id = x.Id,
                ImageName = x.ImageName,
                ImageNameMobileL = x.ImageNameMobileL,
                ImageNameMobileLaptop = x.ImageNameMobileLaptop,
                ImageNameMobileM = x.ImageNameMobileM,
                ImageNameMobileS = x.ImageNameMobileS,
                ImageNameMobileT = x.ImageNameMobileT,
                Text1 = x.Text1,
                Text2 = x.Text2,
                Text3 = x.Text3,
                Text4 = x.Text4,
                RedirectUrl = x.RedirectUrl
            }).ToListAsync();
            try
            {
                return await query;
            }
            catch (Exception ex)
            {
                string me = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the banners list asynchronous.
        /// </summary>
        /// <returns>GetBannersListAsync</returns>
        public async Task<List<PopularDestinationViewModel>> GetPopularDestinationListAsync()
        {
            var query = this.popularDestinationRepository.Table.Select(x => new PopularDestinationViewModel
            {
                Id = x.Id,
                Image = x.Image,
                Text1 = x.Text1,
                Text2 = x.Text2,
                Type = x.CountryId.HasValue ? (int)Enums.SearchType.Country : x.StateId.HasValue ? (int)Enums.SearchType.State : x.CityId.HasValue ? (int)Enums.SearchType.City : 0,
                CountryId = x.CountryId.HasValue ? Convert.ToInt16(x.CountryId.Value) : x.StateId.HasValue ? Convert.ToInt16(x.StateId.Value) : x.CityId.HasValue ? Convert.ToInt16(x.CityId.Value) : Convert.ToInt16(0),
                CountryName = x.CountryId.HasValue ? x.CountryName : x.StateId.HasValue ? x.StateName : x.CityId.HasValue ? x.CityName : string.Empty,
                SubName = x.CountryId.HasValue ? string.Empty : x.StateId.HasValue ? this.packageStateRepository.Table.Where(y => y.Id == x.StateId.Value).Select(y => y.PackageCountryModel.Name).FirstOrDefault() : x.CityId.HasValue ? this.packageCityRepository.Table.Where(y => y.Id == x.CityId.Value).Select(y => y.PackageCountryModel.Name).FirstOrDefault() : string.Empty
            }).ToListAsync();
            try
            {
                return await query;
            }
            catch (Exception ex)
            {
                string me = ex.ToString();
                return null;
            }
        }
    }
}
