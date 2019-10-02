// <copyright file="HomePageService.cs" company="Luxury Travel Deals">
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
    /// <seealso cref="IHomePageService" />
    public class HomePageService : IHomePageService
    {
        private readonly IRepository<LocationDealModel> locationDealRepository;
        private readonly IRepository<PackageCityModel> packageCityRepository;
        private readonly IRepository<PackageCountryModel> packageCountryRepository;
        private readonly IRepository<DealsPackageModel> dealPackageRepository;
        private readonly IRepository<LocationDealsGridViewModel> locationDealGridRepo;
        private readonly IRepository<PackageTravelStyleModel> travelStyleRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageService" /> class.
        /// </summary>
        /// <param name="packageCountryRepository">Package Country Repos</param>
        /// <param name="travelStyleRepo">Travel Style Repo</param>
        /// <param name="dealPackageRepository">Deal Package Repository</param>
        /// <param name="locationDealRepository">Location Deal Repository</param>
        /// <param name="locationDealGridRepo">Location Deal Grid Repo</param>
        /// <param name="packageCityRepository">Package City Repo</param>
        public HomePageService(IRepository<PackageCountryModel> packageCountryRepository, IRepository<PackageTravelStyleModel> travelStyleRepo, IRepository<DealsPackageModel> dealPackageRepository, IRepository<LocationDealModel> locationDealRepository, IRepository<LocationDealsGridViewModel> locationDealGridRepo, IRepository<PackageCityModel> packageCityRepository)
        {
            this.packageCountryRepository = packageCountryRepository;
            this.travelStyleRepo = travelStyleRepo;
            this.dealPackageRepository = dealPackageRepository;
            this.locationDealRepository = locationDealRepository;
            this.locationDealGridRepo = locationDealGridRepo;
            this.packageCityRepository = packageCityRepository;
        }

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<DealsPackageModel> GetDealPackageByIdAsync(int dealId)
        {
            return await this.dealPackageRepository.Table.Where(x => x.Id == dealId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="model">Data Table</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<DataTableResult> GetAllSpecialDealsAsync(DataTableParameter model)
        {
            var result = this.locationDealRepository.Table.Join(this.packageCityRepository.Table, l => l.City, c => c.Id, (c, l) => new { c, l }).Select(x => new LocationDealsGridViewModel
            {
                Id = x.c.Id,
                City = x.l.Name,
                IsActive = x.c.IsActive,
                Text = x.c.Name
            });
            return await this.locationDealGridRepo.ToPagedListAsync(result, model);
        }

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<LocationDealModel> GetLocationDealByIdAsync(int id)
        {
            return await this.locationDealRepository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<List<LocationDealsCurationViewModel>> GetActiveLocations()
        {
            return await this.locationDealRepository.Table.Where(x => x.IsActive && x.City != null)
                .Join(this.packageCityRepository.Table, lo => lo.City, ci => ci.Id, (lo, ci) => new { lo, ci })
                .Select(x => new LocationDealsCurationViewModel
            {
                City = Convert.ToInt32(x.lo.City),
                Text = x.lo.Name,
                CityName = x.ci.Name,
                CountryName = x.ci.PackageCountryModel.Name,
                Results = new List<PackageCurationViewModel>()
            }).ToListAsync();
        }

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="data">Identifier</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<int> AddLocationDeal(LocationDealModel data)
        {
            try
            {
                return await this.locationDealRepository.InsertAsync(data);
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Initialize Grid
        /// </summary>
        /// <param name="data">Identifier</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<int> UpdateLocationDeal(LocationDealModel data)
        {
            try
            {
                return await this.locationDealRepository.UpdateAsync(data);
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get Top 6 Tagged Travel Styles
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public List<PackageTravelStyleModel> GetTop6TaggedTravelStyles()
        {
            try
            {
                var travelStyle = this.dealPackageRepository.Table
                    .Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2))
                    .SelectMany(x => x.TravelStyle.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                    .Select(x => Convert.ToInt32(x))
                    .GroupBy(x => x)
                    .Select(x => new Tuple<int, int>(x.Key, x.Count()))
                    .OrderByDescending(x => x.Item2)
                    .Take(6)
                    .ToList();

                return this.travelStyleRepo.Table.Where(x => travelStyle.Select(y => y.Item1).Contains(x.Id))
                    .Select(x => new Tuple<PackageTravelStyleModel, int>(x, travelStyle.Where(y => y.Item1 == x.Id).Select(y => y.Item2).FirstOrDefault()))
                    .OrderByDescending(x => x.Item2)
                    .Select(x => x.Item1)
                    .ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new List<PackageTravelStyleModel>();
            }
        }

        /// <summary>
        /// Get Top 6 Country Destinations
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public List<PackageCountryModel> GetTop6CountryDestinations()
        {
            try
            {
                var destinations = this.dealPackageRepository.Table
                    .Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2))
                    .SelectMany(x => x.DealsDestinationModels.Select(y => y.Country).Distinct())
                    .GroupBy(x => x)
                    .Select(x => new Tuple<short, int>(x.Key, x.Count()))
                    .OrderByDescending(x => x.Item2)
                    .Take(6)
                    .ToList();

                return this.packageCountryRepository.Table.Where(x => destinations.Select(y => y.Item1).Contains(x.Id))
                    .Select(x => new Tuple<PackageCountryModel, int>(x, destinations.Where(y => y.Item1 == x.Id).Select(y => y.Item2).FirstOrDefault()))
                    .OrderByDescending(x => x.Item2)
                    .Select(x => x.Item1)
                    .ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new List<PackageCountryModel>();
            }
        }
    }
}