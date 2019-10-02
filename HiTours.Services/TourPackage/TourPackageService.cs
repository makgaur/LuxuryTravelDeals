// <copyright file="TourPackageService.cs" company="Luxury Travel Deals">
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
    /// TravelStyleService
    /// </summary>
    /// <seealso cref="HiTours.Services.ITourPackageService" />
    /// <seealso cref="HiTours.Services.IRegionService" />
    public class TourPackageService : ITourPackageService
    {
        /// <summary>
        /// The tour package repository
        /// </summary>
        private readonly IRepository<TourPackageModel> tourPackageRepository;

        /// <summary>
        /// The packagenight repository
        /// </summary>
        private readonly IRepository<TourPackageNightModel> packagenightRepository;

        /// <summary>
        /// The packagecity repository
        /// </summary>
        private readonly IRepository<TourPackageCityModel> packagecityRepository;

        /// <summary>
        /// The bookdate repository
        /// </summary>
        private readonly IRepository<TourPackageBookDateModel> bookdateRepository;

        /// <summary>
        /// The package travel style
        /// </summary>
        private readonly IRepository<TourPackageTravelStyleModel> packageTravelStyle;

        /// <summary>
        /// The package nights validity
        /// </summary>
        private readonly IRepository<TourPackageNightsValidityModel> packageNightsValidity;

        /// <summary>
        /// The package image
        /// </summary>
        private readonly IRepository<TourPackageImageModel> packageImage;

        /// <summary>
        /// The dropdown respository
        /// </summary>
        private readonly IRepository<Dropdown> dropdownRespository;

        /// <summary>
        /// packageCityModelRepository
        /// </summary>
        private readonly IRepository<PackageCityModel> packageCityModelRepository;
        private readonly IRepository<DestinationModel> destinationModelRepository;
        private readonly IRepository<DestinationValidityModel> destinationValidityModelRepository;
        private readonly IRepository<BlockBookingModel> blockBookingModelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourPackageService" /> class.
        /// </summary>
        /// <param name="blockBookingModelRepository">Block Booking Repository</param>
        /// <param name="destinationValidityModelRepository">Destination Validity</param>
        /// <param name="destinationModelRepository">Destination Repo</param>
        /// <param name="tourPackageRepository">The hotel category.</param>
        /// <param name="packagenightRepository">The packagenight repository.</param>
        /// <param name="packagecityRepository">The packagecity Repository.</param>
        /// <param name="bookdateRepository">The bookdate repository.</param>
        /// <param name="dropdownRespository">The dropdown respository.</param>
        /// <param name="packageTravelStyle">The packageTravelStyle respository.</param>
        /// <param name="packageNightsValidity">The packageNightsValidity respository.</param>
        /// <param name="packageImage">fdf</param>
        /// <param name="packageCityModelRepository">packageCityModelRepository</param>
        public TourPackageService(IRepository<BlockBookingModel> blockBookingModelRepository, IRepository<DestinationValidityModel> destinationValidityModelRepository, IRepository<DestinationModel> destinationModelRepository, IRepository<TourPackageModel> tourPackageRepository, IRepository<TourPackageNightModel> packagenightRepository, IRepository<TourPackageCityModel> packagecityRepository, IRepository<TourPackageBookDateModel> bookdateRepository, IRepository<Dropdown> dropdownRespository, IRepository<TourPackageTravelStyleModel> packageTravelStyle, IRepository<TourPackageNightsValidityModel> packageNightsValidity, IRepository<TourPackageImageModel> packageImage, IRepository<PackageCityModel> packageCityModelRepository)
        {
            this.blockBookingModelRepository = blockBookingModelRepository;
            this.destinationModelRepository = destinationModelRepository;
            this.destinationValidityModelRepository = destinationValidityModelRepository;
            this.tourPackageRepository = tourPackageRepository;
            this.packagenightRepository = packagenightRepository;
            this.dropdownRespository = dropdownRespository;
            this.packagecityRepository = packagecityRepository;
            this.bookdateRepository = bookdateRepository;
            this.packageTravelStyle = packageTravelStyle;
            this.packageNightsValidity = packageNightsValidity;
            this.packageImage = packageImage;
            this.packageCityModelRepository = packageCityModelRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The region model.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(TourPackageModel tourPackage)
        {
            if (tourPackage == null)
            {
                throw new ArgumentNullException("tourPackage");
            }

            return await this.tourPackageRepository.InsertAsync(tourPackage);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="tourPackagenightsvalidity">The tour Packagenightsvalidity model.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">Nights validity</exception>
        public async Task<int> InsertTourPackagesnightsValidityAsync(TourPackageNightsValidityModel tourPackagenightsvalidity)
        {
            if (tourPackagenightsvalidity == null)
            {
                throw new ArgumentNullException("tourPackagenightsvalidity");
            }

            tourPackagenightsvalidity.RateTypeApplied = (int)Enums.RateTypeApplied.Double;

            return await this.packageNightsValidity.InsertAsync(tourPackagenightsvalidity);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="tourPackageNight">The tour package night.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> SavePackageNights(TourPackageNightModel tourPackageNight)
        {
            if (tourPackageNight == null)
            {
                throw new ArgumentNullException("tourPackage");
            }

            return await this.packagenightRepository.InsertAsync(tourPackageNight);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageId">Package Id</param>
        /// <param name="isHotelOnly">Is Hotel Only Flag</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<BlockBookingViewModel> GetPackageBlockedDatesAsync(Guid packageId, bool isHotelOnly)
        {
            BlockBookingViewModel viewModel = new BlockBookingViewModel
            {
                BB_PackageId = packageId,
                IsHotel = isHotelOnly
            };

            if (!isHotelOnly) ////Package
            {
                var dates = await this.blockBookingModelRepository.Table.Where(x => x.BB_PackageId == packageId).Select(x => x.BB_Date.Date.ToString("dd/MM/yyyy")).ToListAsync();
                viewModel.Dates = string.Join(",", dates);
                List<KeyValuePair<DateTime, DateTime>> dateRanges = await this.destinationModelRepository.Table.Where(x => x.D_PackageId == packageId).Join(this.destinationValidityModelRepository.Table, d => d.D_Id, dv => dv.DV_DestinationId, (d, dv) => new { d, dv }).Select(x => new KeyValuePair<DateTime, DateTime>(x.dv.DV_ValidityStartDate, x.dv.DV_ValidityEndDate)).ToListAsync();
                viewModel.Ranges = new List<KeyValuePair<string, string>>();
                foreach (var range in dateRanges)
                {
                    viewModel.Ranges.Add(new KeyValuePair<string, string>(range.Key.ToString("yyyy/MM/dd"), range.Value.ToString("yyyy/MM/dd")));
                }

                viewModel.PackageEndDate = dateRanges.Select(x => x.Value).Max().ToString("yyyy/MM/dd");
                viewModel.PackageStartDate = dateRanges.Select(x => x.Key).Min().ToString("yyyy/MM/dd");
                viewModel.UpdatedDate = viewModel.CreatedDate = DateTime.Now;
            }

            return viewModel;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="model">Block Booking Model</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> AddPackageBlockedDatesAsync(BlockBookingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Blocked Dates");
            }

            return await this.blockBookingModelRepository.InsertAsync(model);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageId">Package Id</param>
        /// <param name="date">Date</param>
        /// <param name="isHotel">Is Hotel Fag</param>
        /// <returns>
        /// Get
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<BlockBookingModel> GetBlockBookingDateEntryAsync(Guid packageId, DateTime date, bool isHotel)
        {
            if (isHotel)
            {
                return await this.blockBookingModelRepository.Table.Where(x => x.BB_HotelId == packageId && x.BB_Date == date).FirstOrDefaultAsync();
            }
            else
            {
                return await this.blockBookingModelRepository.Table.Where(x => x.BB_PackageId == packageId && x.BB_Date == date).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="model">The Model</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeletePackageBlockedDatesAsync(BlockBookingModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Delete");
            }

            return await this.blockBookingModelRepository.DeleteAsync(model);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The stylemodel.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(TourPackageModel tourPackage)
        {
            if (tourPackage == null)
            {
                throw new ArgumentNullException("tourPackage");
            }

            this.tourPackageRepository.UpdateCompleteGraph(tourPackage, tourPackage.Id);
            return await this.tourPackageRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The tourpackage night.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateTourPackageNightAsync(TourPackageNightModel tourpackageNight)
        {
            if (tourpackageNight == null)
            {
                throw new ArgumentNullException("tourpackageNight");
            }

            this.packagenightRepository.UpdateCompleteGraph(tourpackageNight, tourpackageNight.Id);
            return await this.packagenightRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the tour package night rates asynchronous.
        /// </summary>
        /// <param name="tourpackageNight">The tourpackage night.</param>
        /// <returns>
        /// UpdateTourPackageNightRatesAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">tourpackageNight</exception>
        public async Task<int> UpdateTourPackageNightRatesAsync(TourPackageNightModel tourpackageNight)
        {
            if (tourpackageNight == null)
            {
                throw new ArgumentNullException("tourpackageNight");
            }

            return await this.packagenightRepository.UpdateAsync(tourpackageNight);
            //// return await this.packagenightRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="tourpackageNightvalidity">The tourpackage night.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateTourPackageNightValidityAsync(TourPackageNightsValidityModel tourpackageNightvalidity)
        {
            if (tourpackageNightvalidity == null)
            {
                throw new ArgumentNullException("tourpackageNight");
            }

            tourpackageNightvalidity.RateTypeApplied = (int)Enums.RateTypeApplied.Double;

            return await this.packageNightsValidity.UpdateAsync(tourpackageNightvalidity);
            //// return await this.packageNightsValidity.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<TourPackageModel> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await this.tourPackageRepository.Table.Include(x => x.TourPackageBookDate).Include(y => y.TourPackageTravelStyle).Include(pc => pc.TourPackageCity).Include(pi => pi.TourPackageImage).FirstOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<TourPackageNightModel> GetTourPackageNightByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await this.packagenightRepository.Table.Include(x => x.TourPackageNightsValidity).Include(y => y.TourPackageNightsDepartCity).FirstOrDefaultAsync(m => m.TourPackageId == id);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<TourPackageNightModel> GetTourPackageNightBytouPackageIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            ////.Include(y => y.TourPackageNightsDepartCity)
            var record = await this.packagenightRepository.Table.Include(x => x.TourPackageNightsValidity).FirstOrDefaultAsync(m => m.Id == id);
            return record;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nights">The nights.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<List<TourPackageNightModel>> GetAllTourPackageNightsBytourPackageIdAsync(Guid id, byte nights)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            ////.Include(y => y.TourPackageNightsDepartCity)
            var record = await this.packagenightRepository.Table.Where(x => x.TourPackageId == id && x.NoOfNights == nights).AsNoTracking().ToListAsync();
            return record;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<TourPackageModel> GetTourPackageByIdAsyn(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            ////.Include(y => y.TourPackageNightsDepartCity)
            var record = await this.tourPackageRepository.Table.FirstOrDefaultAsync(m => m.Id == id);
            return record;
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
            var query = this.tourPackageRepository.Table.Where(x => !x.IsDeleted && x.TourPackageType == 1).Select(x => new TourPackageModel
            {
                Prefix = x.Prefix + x.DealCode + x.Suffix,
                PackageName = x.PackageName,
                PackageValidFrom = x.PackageValidFrom,
                PackageValidTo = x.PackageValidTo,
                IsActive = x.IsActive,
                Id = x.Id
            });

            return await this.tourPackageRepository.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllHotelListAsync(DataTableParameter model)
        {
            var query = this.tourPackageRepository.Table.Where(x => !x.IsDeleted && x.TourPackageType == 2);

            return await this.tourPackageRepository.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<TourPackageNightModel>> GetAllPackageNightAsync()
        {
            var query = await this.packagenightRepository.Table.Include(x => x.TourPackageNightsValidity).ToListAsync();
            return query;
        }

        /// <summary>
        /// Gets the nights by package asynchronous.
        /// </summary>
        /// <param name="packageid">The packageid.</param>
        /// <returns>
        /// GetNightsByPackageAsync
        /// </returns>
        public async Task<List<TourPackageNightModel>> GetNightsByPackageAsync(Guid packageid)
        {
            var tourPackageNights = await this.packagenightRepository.Table.Include(x => x.TourPackageNightsValidity)
                                  .Where(x => x.TourPackageId == packageid)
                                  .ToListAsync();

            return tourPackageNights;
        }

        /// <summary>
        /// Gets the nights by package asynchronous.
        /// </summary>
        /// <param name="packagenightid">The packagenightid.</param>
        /// <returns>
        /// GetNightsByPackageAsync
        /// </returns>
        public async Task<TourPackageNightModel> GetNightsByPackageNightIDAsync(Guid packagenightid)
        {
            var tourPackageNights = await this.packagenightRepository.Table.Include(x => x.TourPackageNightsValidity).Where(x => x.Id == packagenightid).FirstOrDefaultAsync();
            return tourPackageNights;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The tour package.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(TourPackageModel tourPackage)
        {
            if (tourPackage == null)
            {
                throw new ArgumentNullException("tourPackage");
            }

            tourPackage.IsDeleted = !tourPackage.IsDeleted;
            this.tourPackageRepository.UpdateCompleteGraph(tourPackage, tourPackage.Id);
            return await this.tourPackageRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="tourPackage">The tour package.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> TourPackageValidityDeleteAsync(TourPackageNightsValidityModel tourPackage)
        {
            if (tourPackage == null)
            {
                throw new ArgumentNullException("tourPackage");
            }

            return await this.packageNightsValidity.DeleteAsync(tourPackage);
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetDropDownListAsync(string search, short page, params object[] param)
        {
            var query = this.tourPackageRepository.Table
                           .Where(x => x.PackageName.StartsWith(search))
                           .OrderBy(x => x.PackageName)
                           .Select(x => new Dropdown { Id = x.UrlTitle.ToString(), Name = x.PackageName });

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Set entity status deleted
        /// </summary>
        /// <param name="entity">entity</param>
        public void RemoveTourpackageCityEntity(TourPackageCityModel entity)
        {
            this.packagecityRepository.RemoveToContext(entity);
        }

        /// <summary>
        /// Set entity status deleted
        /// </summary>
        /// <param name="entity">entity</param>
        public void RemoveTourPackageBookDateModelEntity(TourPackageBookDateModel entity)
        {
            this.bookdateRepository.RemoveToContext(entity);
        }

        /// <summary>
        /// Set entity status deleted
        /// </summary>
        /// <param name="entity">entity</param>
        public void RemoveTourPackageTravelStyleModelEntity(TourPackageTravelStyleModel entity)
        {
            this.packageTravelStyle.RemoveToContext(entity);
        }

        /// <summary>
        /// Set entity status deleted
        /// </summary>
        /// <param name="entity">entity</param>
        public void RemoveTourPackageNightModelEntity(TourPackageNightModel entity)
        {
            this.packagenightRepository.RemoveToContext(entity);
        }

        /// <summary>
        /// Set entity status deleted
        /// </summary>
        /// <param name="entity">entity</param>
        public void RemoveTourPackageImageEntity(TourPackageImageModel entity)
        {
            this.packageImage.RemoveToContext(entity);
        }

        /// <summary>
        /// Set entity status deleted
        /// </summary>
        /// <param name="entity">entity</param>
        public void RemoveTourPackageNightsValidityModelEntity(TourPackageNightsValidityModel entity)
        {
            this.packageNightsValidity.RemoveToContext(entity);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<TourPackageImageModel> GetTourPackageImageByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await this.packageImage.Table.Include(x => x.TourPackage).FirstOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        /// DeleteTourPackageImage
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>
        /// Delete
        /// </returns>
        /// <exception cref="ArgumentNullException">tourPackageImage</exception>
        public async Task<int> DeleteTourPackageImageAsync(TourPackageImageModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("tourPackageImage");
            }

            return await this.packageImage.DeleteAsync(entity);
        }

        /// <summary>
        /// DeleteTourPackageImage
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>
        /// Delete
        /// </returns>
        /// <exception cref="ArgumentNullException">tourPackageImage</exception>
        public async Task<int> TourPackageImageUpdateAsync(TourPackageImageModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("tourPackageImage");
            }

            return await this.packageImage.UpdateAsync(entity);
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="noOfNights">The no of nights.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateAsync(int noOfNights, Guid id)
        {
            var packageNights =
              await this.packagenightRepository.Table.FirstOrDefaultAsync(x => x.TourPackageId != id && x.NoOfNights == noOfNights);
            return packageNights == null;
        }

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified no of nights].
        /// </summary>
        /// <param name="urltitle">The urltitle.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// IsDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateUrl(string urltitle, Guid id)
        {
            var result = await this.tourPackageRepository.Table.FirstOrDefaultAsync(x => x.Id != id && x.UrlTitle.ToLower() == urltitle.ToLower().Trim());
            return result == null;
        }

        /// <summary>
        /// Get TourPackages Nights Validity List By Tourpackages nightsIDAsync
        /// </summary>
        /// <param name="packagenightsvalidityid">The identifier.</param>
        /// <returns>
        /// Get package nights validity list
        /// </returns>
        public async Task<List<TourPackageNightsValidityModel>> GetTourPackagesNightsValidityListByTourpackagesnightsIDAsync(Guid packagenightsvalidityid)
        {
            if (packagenightsvalidityid == Guid.Empty)
            {
                return null;
            }

            var list = await this.packageNightsValidity.Table.Where(x => x.TourPackageNightsId == packagenightsvalidityid).ToListAsync();
            return list;
        }

        /// <summary>
        /// GetTourPackagesNightsValidityByTourpackagesnightsIDAsync
        /// </summary>
        /// <param name="packagenightsvalidityid">The identifier.</param>
        /// <returns>
        /// Get package nights validity list
        /// </returns>
        public async Task<TourPackageNightsValidityModel> GetTourPackagesNightsValidityByTourpackagesnightsIDAsync(Guid packagenightsvalidityid)
        {
            if (packagenightsvalidityid == Guid.Empty)
            {
                return null;
            }

            var list = await this.packageNightsValidity.Table.LastOrDefaultAsync(x => x.TourPackageNightsId == packagenightsvalidityid);
            return list;
        }

        /// <summary>
        /// Get TourPackages Nights Validity List By Tourpackages nightsIDAsync
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <returns>
        /// Get package nights validity list
        /// </returns>
        public async Task<List<TourPackageNightsValidityModel>> GetTourPackagesNightsValidityListByAsync(Guid id, DateTime from, DateTime to)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var list = await this.packageNightsValidity.Table.Where(x => x.TourPackageNightsId == id && x.RateValidFrom == from && x.RateValidTo == to).ToListAsync();
            return list;
        }

        /// <summary>
        /// Gets the city by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetCityByIDAsync
        /// </returns>
        public async Task<PackageCityModel> GetCityByIDAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var city = await this.packageCityModelRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
            return city;
        }

        /// <summary>
        /// Gets the maximum deal code asynchronous.
        /// </summary>
        /// <returns>Get Max Number of Deal</returns>
        public async Task<int> GetMaxDealCodeAsync()
        {
            var max = 1001;

            var records = this.tourPackageRepository.Table;
            if (records.Count() > 0)
            {
                max = await records.MaxAsync(p => p.DealCode);
            }

            return max;
        }
    }
}