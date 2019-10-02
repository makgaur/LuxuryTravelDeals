// <copyright file="HotelierService.cs" company="Luxury Travel Deals">
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
    public class HotelierService : IHotelierService
    {
        private readonly IRepository<HotelierGridViewModel> hotelierGridRepo;
        private readonly IRepository<HotelierRoomConfigGridViewModel> hotelierRoomConfigGridRepo;
        private readonly IRepository<HotelierPromotionGridViewModel> hotelierPromotionGridRepo;
        private readonly IRepository<HotelierPromotionModel> hotelierPromotionRepo;
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<PackageStateModel> packageStateRespository;
        private readonly IRepository<PackageCityModel> packageCityRespository;
        private readonly IRepository<PackageCountryModel> packageCountryRespository;
        ////private readonly IRepository<VendorInformationModel> vendorInformationRepo;
        private readonly IRepository<CurrencyModel> currencyRepo;
        private readonly IRepository<AmenitiesMasterModel> amenetieMasterRepo;
        private readonly IRepository<HotelierRoomAmenetiesModel> hotelierRoomAmenetieRepo;
        private readonly IRepository<HotelierPropertyTypeModel> hotelierPropertyTypeRepo;
        private readonly IRepository<HotelierInformationModel> hotelierInformationRepo;
        private readonly IRepository<HotelierContentModel> hotelierContentRepo;
        private readonly IRepository<VendorInformationModel> vendorInformationRepo;
        private readonly IRepository<HotelierRoomConfigurationModel> hotelierRoomConfigRepo;
        private readonly IRepository<HotelierImageModel> hotelierImageRepo;
        private readonly IRepository<HotelierRoomImageModel> hotelierRoomImageRepo;
        private readonly IRepository<HotelierAmenitiesModel> hotelierAmenetiRepo;
        private readonly IRepository<HotelierReviewModel> hotelierReviewRepo;
        private readonly IRepository<HotelierReviewsGridViewModel> hotelierReviewGridModelRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelierService"/> class.
        /// Deals Service
        /// </summary>
        /// <param name="packageCountryRespository">Package Country Repo</param>
        /// <param name="hotelierAmenetiRepo">Hotelier Ameneties Repo</param>
        /// <param name="hotelierGridRepo"> Hotelier Grid Videw Model</param>
        /// <param name="currencyRepo"> Currency</param>
        /// <param name="packageCityRespository">Package City</param>
        /// <param name="dropdownRespository">Drop Down</param>
        /// <param name="hotelierPropertyTypeRepo">Property Type</param>
        /// <param name="hotelierInformationRepo">Information Repo</param>
        /// <param name="vendorInformationRepo">Vendor Repo</param>
        /// <param name="hotelierRoomConfigRepo">Hotelier Room Config Repo</param>
        /// <param name="hotelierRoomConfigGridRepo">Hotelier Room Config Grid</param>
        /// <param name="hotelierContentRepo">Hotelier Content Repo</param>
        /// <param name="amenetieMasterRepo">Ameneties Master</param>
        /// <param name="hotelierRoomAmenetieRepo">Hotelier Room Amenetie Repo</param>
        /// <param name="hotelierImageRepo">Hotelier Image Repo</param>
        /// <param name="hotelierRoomImageRepo">Hotelier Room Image Repo</param>
        /// <param name="hotelierReviewGridModelRepo">Hotelier Review Grid Model</param>
        /// <param name="hotelierReviewRepo">Hotelier Review Repo</param>
        /// <param name="hotelierPromotionGridRepo">Hotelier Promotion Grid Model</param>
        /// <param name="hotelierPromotionRepo">Hotelier Promotion Repo</param>
        /// <param name="packageStateRespository">State Repo</param>
        public HotelierService(
            IRepository<PackageCountryModel> packageCountryRespository,
            IRepository<HotelierAmenitiesModel> hotelierAmenetiRepo,
            IRepository<HotelierGridViewModel> hotelierGridRepo,
            IRepository<CurrencyModel> currencyRepo,
            IRepository<PackageCityModel> packageCityRespository,
            IRepository<Dropdown> dropdownRespository,
            IRepository<HotelierPropertyTypeModel> hotelierPropertyTypeRepo,
            IRepository<HotelierInformationModel> hotelierInformationRepo,
            IRepository<VendorInformationModel> vendorInformationRepo,
            IRepository<HotelierRoomConfigurationModel> hotelierRoomConfigRepo,
            IRepository<HotelierRoomConfigGridViewModel> hotelierRoomConfigGridRepo,
            IRepository<HotelierContentModel> hotelierContentRepo,
            IRepository<AmenitiesMasterModel> amenetieMasterRepo,
            IRepository<HotelierRoomAmenetiesModel> hotelierRoomAmenetieRepo,
            IRepository<HotelierImageModel> hotelierImageRepo,
            IRepository<HotelierRoomImageModel> hotelierRoomImageRepo,
            IRepository<HotelierReviewsGridViewModel> hotelierReviewGridModelRepo,
            IRepository<HotelierReviewModel> hotelierReviewRepo,
            IRepository<HotelierPromotionGridViewModel> hotelierPromotionGridRepo,
            IRepository<HotelierPromotionModel> hotelierPromotionRepo,
            IRepository<PackageStateModel> packageStateRespository)
        {
            this.packageCountryRespository = packageCountryRespository;
            this.packageCityRespository = packageCityRespository;
            this.packageStateRespository = packageStateRespository;
            this.hotelierPromotionRepo = hotelierPromotionRepo;
            this.hotelierPromotionGridRepo = hotelierPromotionGridRepo;
            this.hotelierReviewRepo = hotelierReviewRepo;
            this.hotelierReviewGridModelRepo = hotelierReviewGridModelRepo;
            this.hotelierAmenetiRepo = hotelierAmenetiRepo;
            this.hotelierImageRepo = hotelierImageRepo;
            this.hotelierRoomImageRepo = hotelierRoomImageRepo;
            this.hotelierRoomAmenetieRepo = hotelierRoomAmenetieRepo;
            this.amenetieMasterRepo = amenetieMasterRepo;
            this.hotelierContentRepo = hotelierContentRepo;
            this.hotelierRoomConfigGridRepo = hotelierRoomConfigGridRepo;
            this.hotelierRoomConfigRepo = hotelierRoomConfigRepo;
            this.vendorInformationRepo = vendorInformationRepo;
            this.hotelierGridRepo = hotelierGridRepo;
            this.currencyRepo = currencyRepo;
            this.dropdownRespository = dropdownRespository;
            this.hotelierPropertyTypeRepo = hotelierPropertyTypeRepo;
            this.hotelierInformationRepo = hotelierInformationRepo;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetHotelsAsync(DataTableParameter model)
        {
            try
            {
                var records = this.hotelierInformationRepo.Table.Where(x => !x.IsDeleted).Select(x => new HotelierGridViewModel
                {
                    VendorId = x.VendorInformationModel.Id,
                    Id = x.Id,
                    Name = x.Name,
                    PropertyType = x.HotelierPropertyTypeModel.Name,
                    StarRating = x.StarRating.ToString(),
                    ////AmenitiesCount = x.HotelierInformationModels.Count > 0 ? x.HotelierInformationModels.FirstOrDefault().HotelierAmenitiesModels.Count : 0,
                    Group = x.VendorInformationModel.VendorGroupModel.Name,
                    Area = x.Area.ToString(),
                    City = x.City != null && x.City != 0 ? this.packageCityRespository.Table.Where(y => y.Id == x.City).Select(y => y.Name).FirstOrDefault() : string.Empty,
                    State = x.State == null || x.State == 0 ? string.Empty : this.packageStateRespository.Table.Where(y => y.Id == x.State).Select(y => y.Name).FirstOrDefault(),
                    Country = x.Country != null && x.Country != 0 ? this.packageCountryRespository.Table.Where(y => y.Id == x.Country).Select(y => y.Name).FirstOrDefault() : string.Empty,
                    FormattedAddress = x.Address1 + ", " + x.Address2 + ", " + x.City != null && x.City != 0 ? this.packageCityRespository.Table.Where(y => y.Id == x.City).Select(y => y.Name).FirstOrDefault() : string.Empty + x.Country != null && x.Country != 0 ? this.packageCountryRespository.Table.Where(y => y.Id == x.Country).Select(y => y.Name).FirstOrDefault() : string.Empty,
                    PrimaryContactId = x.VendorInformationModel.VendorContactModels.Where(y => y.IsPrimary == true).Select(y => y.Id).FirstOrDefault(),
                    PrimaryContact = x.VendorInformationModel.VendorContactModels.Where(y => y.IsPrimary == true).Select(y => y.Salutation).FirstOrDefault() + " " + x.VendorInformationModel.VendorContactModels.Where(y => y.IsPrimary == true).Select(y => y.FirstName).FirstOrDefault() + " " + x.VendorInformationModel.VendorContactModels.Where(y => y.IsPrimary == true).Select(y => y.LastName).FirstOrDefault(),
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                });

                return await this.hotelierGridRepo.ToPagedListAsync(records, model);
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
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetHotelierRoomCofigGrid(DataTableParameter model, int hotelId)
        {
            try
            {
                var records = this.hotelierRoomConfigRepo.Table.Where(x => x.HotelId == hotelId).Select(x => new HotelierRoomConfigGridViewModel
                {
                    Id = x.Id,
                    Adult = x.Adult,
                    Child = x.Child,
                    FreeChild = x.FreeChild,
                    FreeInfant = x.FreeInfant,
                    HotelId = x.HotelId,
                    Infant = x.Infant,
                    Max = x.Max,
                    RoomType = x.PackageHotelRoomTypeModel.Name,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                });

                return await this.hotelierRoomConfigGridRepo.ToPagedListAsync(records, model);
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
        /// <param name="id">Identifier</param>
        /// <returns>InformationModel</returns>
        public async Task<HotelierInformationModel> GetHotelierInfoAsync(int id)
        {
            try
            {
                return await this.hotelierInformationRepo.Table.Include(x => x.VendorInformationModel).Where(x => x.Id == id).FirstOrDefaultAsync();
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
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierInformationAsync(HotelierInformationModel model)
        {
            try
            {
                await this.hotelierInformationRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateHotelierInformationAsync(HotelierInformationModel model)
        {
            try
            {
                await this.hotelierInformationRepo.UpdateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetHotelierPropertyTypeDropDownListAsync(string search, short page, int? id)
        {
            var query = this.hotelierPropertyTypeRepo.Table
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });
            if (id != null && id != 0)
            {
                query = query.Where(x => x.Id == id.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>InformationModel</returns>
        public async Task<HotelierRoomConfigurationModel> GetHotelierRoomConfigByIdAsync(int id)
        {
            try
            {
                return await this.hotelierRoomConfigRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
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
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<HotelierRoomConfigurationModel>> GetAllHotelierRoomConfigByHotelIdAsync(int hotelId)
        {
            try
            {
                return await this.hotelierRoomConfigRepo.Table.Include(x => x.PackageHotelRoomTypeModel).Where(x => x.HotelId == hotelId && x.IsActive).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<HotelierRoomConfigurationModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteHotelierRoomConfigAsync(int id)
        {
            try
            {
                var record = await this.hotelierRoomConfigRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.hotelierRoomConfigRepo.DeleteAsync(record);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierRoomConfigAsync(HotelierRoomConfigurationModel model)
        {
            try
            {
                await this.hotelierRoomConfigRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateHotelierRoomConfigAsync(HotelierRoomConfigurationModel model)
        {
            try
            {
                await this.hotelierRoomConfigRepo.UpdateAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetieIds">The Ameneties Id</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetAmenitiesListAsync(string search, short page, int[] amenetieIds)
        {
            var ameneties = this.amenetieMasterRepo.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && x.IsRoomOnly);
            ////.OrderBy(x => x.Name)
            var query = ameneties
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });

            if (amenetieIds != null)
            {
                query = query.Where(x => amenetieIds.Contains(Convert.ToInt32(x.Id)));
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Get Vendor Dropdown List.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetieIds">The Ameneties Id</param>
        /// <param name="key">Key</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetHotelierAmenitiesListAsync(string search, short page, int[] amenetieIds, string key)
        {
            var ameneties = this.amenetieMasterRepo.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && x.IsHotelOnly);
            ////.OrderBy(x => x.Name)
            var query = ameneties
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });

            if (amenetieIds != null)
            {
                query = query.Where(x => amenetieIds.Contains(Convert.ToInt32(x.Id)));
            }

            if (key == "all")
            {
                return await query.ToListAsync();
            }
            else
            {
                return await this.dropdownRespository.ToOptionListAsync(query, page);
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<HotelierContentModel> GetHotelierContentByIdAsync(int id)
        {
            try
            {
                return await this.hotelierContentRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
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
        /// <param name="hotelId">Hotelier Id</param>
        /// <param name="roomTypeId">Room Type Id</param>
        /// <returns>InformationModel</returns>
        public async Task<HotelierRoomConfigurationModel> GetHotelierRoomRecordByHotelIdAndRoomTypeId(int hotelId, int roomTypeId)
        {
            try
            {
                return await this.hotelierRoomConfigRepo.Table.Where(x => x.HotelId == hotelId && x.RoomTypeId == roomTypeId).FirstOrDefaultAsync();
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
        /// <param name="roomConfigId">Room Config Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<HotelierRoomImageModel>> GetRoomImageFromRoomConfigId(int roomConfigId)
        {
            try
            {
                var roomImages = await this.hotelierRoomImageRepo.Table.Where(x => x.RoomConfigId == roomConfigId).OrderBy(x => x.SortOrder).ToListAsync();
                if (roomImages.Count > 0)
                {
                    return roomImages;
                }
                else
                {
                    return new List<HotelierRoomImageModel>();
                }
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
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<HotelierContentViewModel> GetHotelierContentByHotelIdAsync(int hotelId)
        {
            try
            {
                HotelierContentViewModel model = new HotelierContentViewModel
                {
                    Id = 0,
                    HotelId = hotelId
                };
                if (this.hotelierContentRepo.Table.Where(x => x.HotelId == hotelId).Count() > 0)
                {
                    model = await this.hotelierContentRepo.Table.Where(x => x.HotelId == hotelId).Select(x => new HotelierContentViewModel
                    {
                        About = x.About,
                        AboutImg = x.AboutImg,
                        HotelAmeneties = x.HotelierInformationModel.HotelierAmenitiesModels.Count > 0 ? x.HotelierInformationModel.HotelierAmenitiesModels.Select(z => z.AmentieId).ToArray() : null,
                        BannerImg2x2_1 = x.BannerImg2x2_1,
                        BannerImg2x2_2 = x.BannerImg2x2_2,
                        BannerImg2x2_3 = x.BannerImg2x2_3,
                        BannerImg2x2_4 = x.BannerImg2x2_4,
                        BannerImg2x4 = x.BannerImg2x4,
                        BannerImg4x4 = x.BannerImg4x4,
                        CardImg = x.CardImg,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        OverallCleaninessRating = x.OverallCleaninessRating,
                        OverallComfortRating = x.OverallComfortRating,
                        OverallRating = x.OverallRating,
                        OverallValueRating = x.OverallValueRating,
                        HotelId = x.HotelId,
                        Id = x.Id,
                        LogoImg = x.LogoImg,
                        TAUrl = x.TAUrl,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        HotelierRoomConfigurations = this.hotelierRoomConfigRepo.Table.Where(y => y.HotelId == hotelId && y.IsActive).Select(y => new HotelierRoomConfigurationViewModel
                        {
                            Id = y.Id,
                            Description = y.Description,
                            CardImg = y.CardImg,
                            Max = y.Max,
                            HotelId = y.HotelId,
                            RoomName = y.PackageHotelRoomTypeModel.Name,
                            Ameneties = y.HotelierRoomAmenetiesModels.Count > 0 ? y.HotelierRoomAmenetiesModels.Select(z => z.AmenetieId).ToArray() : null
                        }).ToList()
                    }).FirstOrDefaultAsync();
                }
                else
                {
                    model.HotelierRoomConfigurations = this.hotelierRoomConfigRepo.Table.Where(y => y.HotelId == hotelId).Count() > 0 ? this.hotelierRoomConfigRepo.Table.Where(y => y.HotelId == hotelId && y.IsActive).Select(y => new HotelierRoomConfigurationViewModel
                    {
                        Id = y.Id,
                        HotelId = y.HotelId,
                        RoomName = y.PackageHotelRoomTypeModel.Name,
                        Description = y.Description,
                        CardImg = y.CardImg,
                        Max = y.Max,
                        Ameneties = y.HotelierRoomAmenetiesModels.Count > 0 ? y.HotelierRoomAmenetiesModels.Select(z => z.AmenetieId).ToArray() : null
                    }).ToList() : new List<HotelierRoomConfigurationViewModel>();
                }

                return model;
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
        /// <param name="hotelId">Room Configuration Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteAllHotelierAmetiesByHotelId(int hotelId)
        {
            try
            {
                var records = await this.hotelierAmenetiRepo.Table.Where(x => x.HotelId == hotelId).ToListAsync();
                foreach (var items in records)
                {
                    await this.hotelierAmenetiRepo.DeleteAsync(items);
                }

                return 0;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="roomConfigId">Room Configuration Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteAllHotelierRoomAmetiesByRoomConfigId(int roomConfigId)
        {
            try
            {
                var records = await this.hotelierRoomAmenetieRepo.Table.Where(x => x.RoomConfigId == roomConfigId).ToListAsync();
                foreach (var items in records)
                {
                    await this.hotelierRoomAmenetieRepo.DeleteAsync(items);
                }

                return 0;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierRoomAmenity(HotelierRoomAmenetiesModel model)
        {
            try
            {
                return await this.hotelierRoomAmenetieRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierAmenity(HotelierAmenitiesModel model)
        {
            try
            {
                return await this.hotelierAmenetiRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierContent(HotelierContentModel model)
        {
            try
            {
                return await this.hotelierContentRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateHotelierContent(HotelierContentModel model)
        {
            try
            {
                return await this.hotelierContentRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateHotelierImageAsync(HotelierImageModel model)
        {
            try
            {
                return await this.hotelierImageRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Delete Hotel Image
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>Model</returns>
        public async Task<int> DeleteHotelierImageAsync(int id)
        {
            try
            {
                var record = await this.hotelierImageRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.hotelierImageRepo.DeleteAsync(record);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Delete Hotel Room Image
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>Model</returns>
        public async Task<int> DeleteHotelierRoomImageAsync(int id)
        {
            try
            {
                var record = await this.hotelierRoomImageRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.hotelierRoomImageRepo.DeleteAsync(record);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierImageAsync(HotelierImageModel model)
        {
            try
            {
                return await this.hotelierImageRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateHotelierRoomImageAsync(HotelierRoomImageModel model)
        {
            try
            {
                return await this.hotelierRoomImageRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierRoomImageAsync(HotelierRoomImageModel model)
        {
            try
            {
                return await this.hotelierRoomImageRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<HotelierImageModel>> GetHotelierImageByHotelId(int hotelId)
        {
            try
            {
                if (this.hotelierImageRepo.Table.Where(x => x.HotelId == hotelId).Count() > 0)
                {
                    return await this.hotelierImageRepo.Table.Where(x => x.HotelId == hotelId).OrderByDescending(x => x.SortOrder.HasValue).ThenBy(x => x.SortOrder).ToListAsync();
                }

                return new List<HotelierImageModel>();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<HotelierImageModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<HotelierRoomImageModel>> GetHotelierRoomImagesByHotelId(int hotelId)
        {
            try
            {
                if (this.hotelierRoomConfigRepo.Table.Where(x => x.HotelId == hotelId).Count() > 0)
                {
                    var roomConfigIds = await this.hotelierRoomConfigRepo.Table.Where(x => x.HotelId == hotelId && x.IsActive).Select(x => x.Id).ToArrayAsync();
                    return await this.hotelierRoomImageRepo.Table.Where(x => roomConfigIds.Contains(x.RoomConfigId)).OrderByDescending(x => x.SortOrder.HasValue).ThenBy(x => x.SortOrder).ToListAsync();
                }

                return new List<HotelierRoomImageModel>();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<HotelierRoomImageModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<HotelierReviewModel> GetHotelReviewsById(int hotelId)
        {
            try
            {
                return await this.hotelierReviewRepo.Table.Where(x => x.Id == hotelId).FirstOrDefaultAsync();
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
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllHotelReviewsByHotelId(DataTableParameter model, int hotelId)
        {
            try
            {
                var records = this.hotelierReviewRepo.Table.Where(x => x.HotelId == hotelId).Select(x => new HotelierReviewsGridViewModel
                {
                    Id = x.Id,
                    HotelId = x.HotelId,
                    Comment = x.Comment,
                    FullName = x.FName + " " + x.LName,
                    Rating = x.Rating,
                    Rating_Cleanliness = x.Rating_Cleanliness,
                    Rating_Comfort = x.Rating_Comfort,
                    Rating_Location = x.Rating_Location,
                    Rating_Value = x.Rating_Value,
                    UserRecommend = x.UserRecommend ? "Yes" : string.Empty,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                });

                return await this.hotelierReviewGridModelRepo.ToPagedListAsync(records, model);
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
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierReviewAsync(HotelierReviewModel model)
        {
            try
            {
                return await this.hotelierReviewRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateHotelierReviewAsync(HotelierReviewModel model)
        {
            try
            {
                return await this.hotelierReviewRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteHotelierReviewAsync(int id)
        {
            try
            {
                var record = await this.hotelierReviewRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.hotelierReviewRepo.DeleteAsync(record);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllHotelierPromotionAsync(DataTableParameter model, int hotelId)
        {
            try
            {
                var query = this.hotelierPromotionRepo.Table.Where(x => x.HotelId == hotelId && !x.IsDeleted).Select(x => new HotelierPromotionGridViewModel
                {
                    BookingEndDate = x.BookingEndDate,
                    BookingStartDate = x.BookingStartDate,
                    DiscountValue = x.DiscountValue.ToString(),
                    Id = x.Id,
                    IsActive = x.IsActive,
                    TravelEndDate = x.TravelEndDate,
                    TravelStartDate = x.TravelEndDate,
                    Type = x.PromotionTypeModel.Name
                });

                return await this.hotelierPromotionGridRepo.ToPagedListAsync(query, model);
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
        /// <param name="hotelId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<HotelierPromotionModel> GetHotelPromotionById(int hotelId)
        {
            try
            {
                return await this.hotelierPromotionRepo.Table.Where(x => x.Id == hotelId).FirstOrDefaultAsync();
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
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddHotelierPromotionAsync(HotelierPromotionModel model)
        {
            try
            {
                return await this.hotelierPromotionRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateHotelierPromotionAsync(HotelierPromotionModel model)
        {
            try
            {
                return await this.hotelierPromotionRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetActiveHoteliersForDeals(string search, short page, int? id)
        {
            var query = this.hotelierInformationRepo.Table.Where(x => x.IsActive && !x.IsDeleted && x.Name.Trim() != string.Empty)
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name + (x.City != null ? this.packageCityRespository.Table.Where(y => y.Id == Convert.ToInt32(x.City)).Select(y => ", " + y.Name).FirstOrDefault() : string.Empty) + (x.Country != null ? this.packageCountryRespository.Table.Where(y => y.Id == Convert.ToInt32(x.Country)).Select(y => ", " + y.Name).FirstOrDefault() : string.Empty) + " (" + x.StarRating + " Star) " });
            if (search != string.Empty)
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            if (id != null && id != 0)
            {
                query = query.Where(x => x.Id == id.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <param name="cities">Cities</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetActiveHoteliersInCities(string search, short page, int? id, List<int> cities)
        {
            var query = this.hotelierInformationRepo.Table.Where(x => x.IsActive && !x.IsDeleted && cities.Contains(x.City != null ? Convert.ToInt32(x.City) : 0))
                           .OrderBy(x => x.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name + (x.City != null ? this.packageCityRespository.Table.Where(y => y.Id == Convert.ToInt32(x.City)).Select(y => ", " + y.Name).FirstOrDefault() : string.Empty) + (x.Country != null ? this.packageCountryRespository.Table.Where(y => y.Id == Convert.ToInt32(x.Country)).Select(y => ", " + y.Name).FirstOrDefault() : string.Empty) + " (" + x.StarRating + " Star) " });
            if (search != string.Empty)
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            if (id != null && id != 0)
            {
                query = query.Where(x => x.Id == id.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }
    }
}
