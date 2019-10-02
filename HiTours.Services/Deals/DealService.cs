// <copyright file="DealService.cs" company="Luxury Travel Deals">
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
    using HiTours.ViewModels.Deals;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// UserDetailService
    /// </summary>
    /// <seealso cref="IDealService" />
    public class DealService : IDealService
    {
        private readonly IRepository<DealsGridViewModel> dealsGridRepo;
        private readonly IRepository<DealsPromotion_RoomType> dealsPromoRoomTypeRepo;
        private readonly IRepository<HotelierReviewModel> hotelReviewRepo;
        private readonly IRepository<HotelierContentModel> hotelierContentRepo;
        private readonly IRepository<HotelierImageModel> hotelierImageRepo;
        private readonly IRepository<DealsPromotionGridViewModel> dealsPromotionGridRepo;
        private readonly IRepository<PackageStateModel> stateModelRepository;
        private readonly IRepository<HotelierInformationModel> hotelInformationRepository;
        private readonly IRepository<DealRoomConfigurationModel> dealHotelRoomConfigRepo;
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<VendorInformationModel> vendorInformationRepo;
        private readonly IRepository<CurrencyModel> currencyRepo;
        private readonly IRepository<DealsPromotionModel> dealsPromotionRepo;
        private readonly IRepository<DealsTypeModel> dealTypeRepo;
        private readonly IRepository<DealsPackageModel> dealPackageRepo;
        private readonly IRepository<DealsNightModel> dealNightRepo;
        private readonly IRepository<DealsInclusionModel> dealsInclusionRepo;
        private readonly IRepository<DealsContentModel> dealContentRepo;
        private readonly IRepository<DealsHighlightModel> dealHighlightRepo;
        private readonly IRepository<DealsDestinationModel> dealDestinationRepo;
        private readonly IRepository<DealsBookingValidityModel> dealsBookingValidityRepo;
        private readonly IRepository<DealsItineraryModel> dealsItineraryRepo;
        private readonly IRepository<DealsPaxCombinationModel> dealsPaxCombinationRepo;
        private readonly IRepository<DealsRatePlanModel> dealsRatePlanRepo;
        private readonly IRepository<DealsReviewModel> dealsReviewRepo;
        private readonly IRepository<DealsReviewsGridViewModel> dealsReviewsGridViewRepo;
        private readonly IRepository<DealsImageModel> dealsImageRepo;
        private readonly IRepository<DealsAddOnModel> dealsAddOnRepo;
        private readonly IRepository<DealVisaModel> dealsVisaRepo;
        private readonly IRepository<VisaModel> visaMasterRepo;
        private readonly IRepository<DealsAddOnGridViewModel> dealsAddOnGridViewRepo;
        private readonly IRepository<DealsSeoDetail> dealsSeoRepo;
        private readonly IRepository<FlightDestination> flightDestinationRepo;
        private readonly IRepository<DealsFlightModel> dealFlightRepo;
        private readonly IRepository<DealInventoryModel> dealInventoryRepo;
        private readonly IRepository<DealsDepartureDatesModel> dealDepartureDateRepo;
        private readonly IRepository<BookingInformationModel> bookingInformationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealService"/> class.
        /// Deals Service
        /// </summary>
        /// <param name="bookingInformationRepo">Booking Information Repository</param>
        /// <param name="dealInventoryRepo">Deal Inventory Repo</param>
        /// <param name="dealDepartureDateRepo">Deals Departure Date Repo</param>
        /// <param name="dealsPromoRoomTypeRepo">Deals Promotion Room Type Repo</param>
        /// <param name="hotelReviewRepo">Hotel Review Repo</param>
        /// <param name="dealFlightRepo">Deal Flight Repo</param>
        /// <param name="dealsSeoRepo">Deals Seo Repo</param>
        /// <param name="hotelierImageRepo">Hotelier Image Repository</param>
        /// <param name="hotelierContentRepo">Hotelier Content Repository</param>
        /// <param name="hotelInformationRepository">Hotelier Information</param>
        /// <param name="dealHotelRoomConfigRepo">Deal Hotel Room Configuration</param>
        /// <param name="dealsGridRepo">Deal Grid</param>
        /// <param name="currencyRepo"> Currency</param>
        /// <param name="dropdownRespository"> Dropdown</param>
        /// <param name="vendorInformationRepo">Vendor Info</param>
        /// <param name="dealTypeRepo">deal type</param>
        /// <param name="dealPackageRepo">deal package</param>
        /// <param name="dealsBookingValidityRepo">deal booking validity repo</param>
        /// <param name="dealNightRepo">deal night repo</param>
        /// <param name="dealContentRepo">deal content repo</param>
        /// <param name="dealHighlightRepo"> deal highlight repo</param>
        /// <param name="dealDestinationRepo"> deal destination repo</param>
        /// <param name="dealsItineraryRepo"> deal itineray repo</param>
        /// <param name="dealsPaxCombinationRepo"> deal pax combination repo</param>
        /// <param name="dealsRatePlanRepo"> deal rate plan combination repo</param>
        /// <param name="dealsReviewRepo"> deal review repo</param>
        /// <param name="dealsReviewsGridViewRepo"> deal review grid view repo</param>
        /// <param name="dealsImageRepo"> deal image repo</param>
        /// <param name="dealsAddOnRepo"> deal add ons repo</param>
        /// <param name="dealsAddOnGridViewRepo"> deal add ons Grid repo</param>
        /// <param name="dealsPromotionGridRepo">Deals Promotion Grid Model</param>
        /// <param name="dealsPromotionRepo">Deal Promotion Repo</param>
        /// <param name="dealsInclusionRepo">Deals Inclusion Model</param>
        /// <param name="stateModelRepository">State Model Repo</param>
        /// <param name="dealsVisaRepo">Deals Visa Repo</param>
        /// <param name="visaMasterRepo">Visa Master Repo</param>
        /// <param name="flightDestinationRepo">Flight Destination Repo</param>
        public DealService(
            IRepository<BookingInformationModel> bookingInformationRepo,
            IRepository<DealInventoryModel> dealInventoryRepo,
            IRepository<DealsDepartureDatesModel> dealDepartureDateRepo,
            IRepository<DealsPromotion_RoomType> dealsPromoRoomTypeRepo,
            IRepository<HotelierReviewModel> hotelReviewRepo,
            IRepository<DealsFlightModel> dealFlightRepo,
            IRepository<DealsSeoDetail> dealsSeoRepo,
            IRepository<HotelierImageModel> hotelierImageRepo,
            IRepository<HotelierContentModel> hotelierContentRepo,
            IRepository<HotelierInformationModel> hotelInformationRepository,
            IRepository<DealRoomConfigurationModel> dealHotelRoomConfigRepo,
            IRepository<DealsGridViewModel> dealsGridRepo,
            IRepository<CurrencyModel> currencyRepo,
            IRepository<Dropdown> dropdownRespository,
            IRepository<VendorInformationModel> vendorInformationRepo,
            IRepository<DealsTypeModel> dealTypeRepo,
            IRepository<DealsPackageModel> dealPackageRepo,
            IRepository<DealsBookingValidityModel> dealsBookingValidityRepo,
            IRepository<DealsNightModel> dealNightRepo,
            IRepository<DealsContentModel> dealContentRepo,
            IRepository<DealsHighlightModel> dealHighlightRepo,
            IRepository<DealsDestinationModel> dealDestinationRepo,
            IRepository<DealsItineraryModel> dealsItineraryRepo,
            IRepository<DealsPaxCombinationModel> dealsPaxCombinationRepo,
            IRepository<DealsRatePlanModel> dealsRatePlanRepo,
            IRepository<DealsReviewModel> dealsReviewRepo,
            IRepository<DealsReviewsGridViewModel> dealsReviewsGridViewRepo,
            IRepository<DealsImageModel> dealsImageRepo,
            IRepository<DealsAddOnModel> dealsAddOnRepo,
            IRepository<DealsAddOnGridViewModel> dealsAddOnGridViewRepo,
            IRepository<DealsPromotionGridViewModel> dealsPromotionGridRepo,
            IRepository<DealsPromotionModel> dealsPromotionRepo,
            IRepository<DealsInclusionModel> dealsInclusionRepo,
            IRepository<PackageStateModel> stateModelRepository,
            IRepository<DealVisaModel> dealsVisaRepo,
            IRepository<VisaModel> visaMasterRepo,
            IRepository<FlightDestination> flightDestinationRepo)
        {
            this.bookingInformationRepo = bookingInformationRepo;
            this.dealInventoryRepo = dealInventoryRepo;
            this.dealDepartureDateRepo = dealDepartureDateRepo;
            this.dealsPromoRoomTypeRepo = dealsPromoRoomTypeRepo;
            this.hotelReviewRepo = hotelReviewRepo;
            this.dealFlightRepo = dealFlightRepo;
            this.flightDestinationRepo = flightDestinationRepo;
            this.dealsSeoRepo = dealsSeoRepo;
            this.visaMasterRepo = visaMasterRepo;
            this.dealsVisaRepo = dealsVisaRepo;
            this.hotelierImageRepo = hotelierImageRepo;
            this.hotelierContentRepo = hotelierContentRepo;
            this.hotelInformationRepository = hotelInformationRepository;
            this.stateModelRepository = stateModelRepository;
            this.dealsInclusionRepo = dealsInclusionRepo;
            this.dealsPromotionRepo = dealsPromotionRepo;
            this.dealsPromotionGridRepo = dealsPromotionGridRepo;
            this.dealsGridRepo = dealsGridRepo;
            this.currencyRepo = currencyRepo;
            this.dropdownRespository = dropdownRespository;
            this.vendorInformationRepo = vendorInformationRepo;
            this.dealPackageRepo = dealPackageRepo;
            this.dealTypeRepo = dealTypeRepo;
            this.dealNightRepo = dealNightRepo;
            this.dealsBookingValidityRepo = dealsBookingValidityRepo;
            this.dealContentRepo = dealContentRepo;
            this.dealHighlightRepo = dealHighlightRepo;
            this.dealDestinationRepo = dealDestinationRepo;
            this.dealsItineraryRepo = dealsItineraryRepo;
            this.dealsPaxCombinationRepo = dealsPaxCombinationRepo;
            this.dealsRatePlanRepo = dealsRatePlanRepo;
            this.dealsReviewRepo = dealsReviewRepo;
            this.dealsReviewsGridViewRepo = dealsReviewsGridViewRepo;
            this.dealsImageRepo = dealsImageRepo;
            this.dealsAddOnRepo = dealsAddOnRepo;
            this.dealsAddOnGridViewRepo = dealsAddOnGridViewRepo;
            this.dealHotelRoomConfigRepo = dealHotelRoomConfigRepo;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="dealType">Deal Type</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetDealsAsync(DataTableParameter model, int dealType)
        {
            try
            {
                var records = this.dealPackageRepo.Table.Where(x => x.Type == dealType && !x.IsDeleted).Select(x => new DealsGridViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    HotelName = dealType == 1 ? this.hotelInformationRepository.Table.Where(p => p.Id == x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(k => k.VendorInfoId).FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).Select(p => p.Name).FirstOrDefault() : string.Empty,
                    Code = x.Code,
                    PreviewUrl = this.GetPreviewUrl(x, dealType),
                    Type = x.Type,
                    IsLive = x.IsLive,
                    TypeName = x.DealTypeModel.Name,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    ValidFrom = x.DealsBookingValidityModels.Select(y => y.ValidFrom).Min(),
                    ValidTo = x.DealsBookingValidityModels.Select(y => y.ValidTo).Max()
                });

                return await this.dealsGridRepo.ToPagedListAsync(records, model);
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
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetDealsPromotionsAsync(DataTableParameter model, int packageId)
        {
            try
            {
                var records = this.dealsPromotionRepo.Table.Where(x => x.PackageId == packageId && !x.IsDeleted).Select(x => new DealsPromotionGridViewModel
                {
                    Id = x.Id,
                    BookingEndDate = x.BookingEndDate,
                    BookingStartDate = x.BookingStartDate,
                    DiscountValue = x.DiscountValue.ToString(),
                    TravelEndDate = x.TravelEndDate,
                    TravelStartDate = x.TravelStartDate,
                    Type = x.PromotionTypeModel.Name,
                    IsActive = x.IsActive,
                });

                return await this.dealsPromotionGridRepo.ToPagedListAsync(records, model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="promotionId">Promotion indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsPromotionModel> GetDealPromotionById(int promotionId)
        {
            try
            {
                return await this.dealsPromotionRepo.Table.Include(x => x.DealPromotionRoomTypeModel).Where(x => x.Id == promotionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsPackageModel> GetDealPackageAsync(int packageId)
        {
            try
            {
                return await this.dealPackageRepo.Table.Where(x => x.Id == packageId).Include(x => x.DealsDestinationModels).Include(x => x.DealsNightModels).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <returns>Deal package model</returns>
        public async Task<SendLeadViewModel> GetSendDealinfo(int dealId)
        {
            try
            {
                DealsPackageModel dealModel = await this.dealPackageRepo.Table
                    .Include("DealsDestinationModels")
                    .Include("DealContentModels")
                    .Include("DealsNightModels.DealsItineraryModels.DealsInclusionModels")
                    .Where(x => x.Id == dealId).FirstOrDefaultAsync();

                int hotelId = this.dealPackageRepo.Table.Where(x => x.Id == dealId).Select(x => x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(a => a.VendorInfoId.Value).FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
                var hotelname = this.hotelInformationRepository.Table.Where(x => x.Id == hotelId).Select(x => x.Name).FirstOrDefault();
                SendLeadViewModel sendLeadViewModel = new SendLeadViewModel()
                {
                    DealName = dealModel.Name,
                    CardImage = dealModel.DealContentModels.Select(y => y.CardImg).FirstOrDefault(),
                    DealType = dealModel.Type,
                    DealUrl = dealModel.Url,
                    HotelName = Convert.ToString(hotelname)
                };
                return sendLeadViewModel;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get Deal Night Record
        /// </summary>
        /// <param name="nightValue">Night Value</param>
        /// <param name="dealId">Deal Id</param>
        /// <returns>Deal Night Record</returns>
        public async Task<DealsNightModel> GetNightRecordByValueAndDealId(int nightValue, int dealId)
        {
            return await this.dealNightRepo.Table.FirstOrDefaultAsync(x => x.Value == nightValue && x.PackageId == dealId);
        }

        /// <summary>
        /// Get Deal Night Record
        /// </summary>
        /// <param name="record">Deal Night Record</param>
        /// <returns>Delete Flag</returns>
        public async Task<int> DeleteDealNightPackageTask(DealsNightModel record)
        {
            try
            {
                List<DealsItineraryModel> dealsItineraryModels = await this.dealsItineraryRepo.Table.Where(x => x.NightId == record.Id).ToListAsync();
                if (dealsItineraryModels.Any())
                {
                    foreach (var itiItem in dealsItineraryModels)
                    {
                        List<DealsInclusionModel> dealInclusionModels = await this.dealsInclusionRepo.Table.Where(x => x.ItineraryId == itiItem.Id).ToListAsync();
                        if (dealInclusionModels.Any())
                        {
                            foreach (var inclusion in dealInclusionModels)
                            {
                                List<DealsAddOnModel> dealsAddOnModel = await this.dealsAddOnRepo.Table.Where(x => x.InclusionId == inclusion.Id).ToListAsync();
                                if (dealsAddOnModel.Any())
                                {
                                    foreach (var dealAddOnItem in dealsAddOnModel)
                                    {
                                        await this.dealsAddOnRepo.DeleteAsync(dealAddOnItem);
                                    }
                                }

                                List<DealsFlightModel> dealsFlightModels = await this.dealFlightRepo.Table.Where(x => x.InclusionId == inclusion.Id).ToListAsync();
                                if (dealsFlightModels.Any())
                                {
                                    foreach (var dealFlightItem in dealsFlightModels)
                                    {
                                        await this.dealFlightRepo.DeleteAsync(dealFlightItem);
                                    }
                                }

                                List<DealRoomConfigurationModel> roomConfigurationModels = await this.dealHotelRoomConfigRepo.Table.Where(x => x.InclusionId == inclusion.Id).ToListAsync();
                                if (roomConfigurationModels.Any())
                                {
                                    foreach (var roomConfigItem in roomConfigurationModels)
                                    {
                                        await this.dealHotelRoomConfigRepo.DeleteAsync(roomConfigItem);
                                    }
                                }

                                await this.dealsInclusionRepo.DeleteAsync(inclusion);
                            }
                        }

                        await this.dealsItineraryRepo.DeleteAsync(itiItem);
                    }
                }

                List<DealsRatePlanModel> ratePlanModels = await this.dealsRatePlanRepo.Table.Where(x => x.NightId == record.Id).ToListAsync();
                if (ratePlanModels.Any())
                {
                    foreach (var ratePlanItem in ratePlanModels)
                    {
                        List<DealInventoryModel> inventoryModels = await this.dealInventoryRepo.Table.Where(x => x.RatePlanId == ratePlanItem.Id).ToListAsync();
                        if (inventoryModels.Any())
                        {
                            foreach (var inventItem in inventoryModels)
                            {
                                await this.dealInventoryRepo.DeleteAsync(inventItem);
                            }
                        }

                        await this.dealsRatePlanRepo.DeleteAsync(ratePlanItem);
                    }
                }

                List<DealsDepartureDatesModel> departureDatesModels = await this.dealDepartureDateRepo.Table.Where(x => x.NightId == record.Id).ToListAsync();
                if (departureDatesModels.Any())
                {
                    foreach (var departureItem in departureDatesModels)
                    {
                        await this.dealDepartureDateRepo.DeleteAsync(departureItem);
                    }
                }

                await this.dealNightRepo.DeleteAsync(record);
                return 1;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<int?> GetDealHotelierFromNightId(int nightId)
        {
            try
            {
                return await this.dealsItineraryRepo.Table.Where(x => x.NightId == nightId).Select(x => x.DealsInclusionModels.Where(y => y.ItineraryId == x.Id).Select(z => z.VendorInfoId).FirstOrDefault()).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsDepartureDateViewModel> GetDealDepartureByNightIdAsync(int nightId)
        {
            try
            {
                List<string> dates = await this.dealDepartureDateRepo.Table.Where(x => x.NightId == nightId).Select(x => x.Date.ToString("dd/MM/yyyy")).ToListAsync();
                DealsDepartureDateViewModel model = new DealsDepartureDateViewModel
                {
                    NightId = nightId,
                    Dates = string.Join(',', dates)
                };

                return model;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new DealsDepartureDateViewModel { NightId = nightId };
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsDepartureDatesModel>> GetAllDealDepartureRecordsByNightIdAsync(int nightId)
        {
            try
            {
                return await this.dealDepartureDateRepo.Table.Where(x => x.NightId == nightId).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealsDepartureDatesModel>();
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">THe Night Id</param>
        /// <returns>Deal package model</returns>
        public async Task<int> DeleteAllDealsDeparture(int nightId)
        {
            try
            {
                var records = await this.dealDepartureDateRepo.Table.Where(x => x.NightId == nightId).ToListAsync();
                if (records != null)
                {
                    foreach (var item in records)
                    {
                        await this.dealDepartureDateRepo.DeleteAsync(item);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddDealsDepartureAsync(DealsDepartureDatesModel model)
        {
            try
            {
                await this.dealDepartureDateRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddDealsNightAsync(DealsNightModel model)
        {
            try
            {
                await this.dealNightRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddDealsItenaryAsync(DealsItineraryModel model)
        {
            try
            {
                await this.dealsItineraryRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddDealsInclusionAsync(DealsInclusionModel model)
        {
            try
            {
                await this.dealsInclusionRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddDealsFlightAsync(DealsFlightModel model)
        {
            try
            {
                await this.dealFlightRepo.InsertAsync(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package ID</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsNightModel> GetDealNightHotelByPackageId(int packageId)
        {
            try
            {
                return await this.dealNightRepo.Table.Where(x => x.PackageId == packageId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="nightId">Package ID</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsItineraryModel> GetDealItenaryHotelByNightId(int nightId)
        {
            try
            {
                return await this.dealsItineraryRepo.Table.Where(x => x.NightId == nightId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="itenaryId">Itenary ID</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsInclusionModel> GetDealInclusionHotelByItenaryId(int itenaryId)
        {
            try
            {
                return await this.dealsInclusionRepo.Table.Where(x => x.ItineraryId == itenaryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> UpdateDealsInclusionAsync(DealsInclusionModel model)
        {
            try
            {
                return await this.dealsInclusionRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="dealFlightId">Deals Flight ID</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsFlightModel> GetDealFlightAsync(int dealFlightId)
        {
            try
            {
                return await this.dealFlightRepo.Table.Where(x => x.Id == dealFlightId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="dealAddOnId">Deals Add On ID</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsAddOnModel> GetDealAddOnAsync(int dealAddOnId)
        {
            try
            {
                return await this.dealsAddOnRepo.Table.Where(x => x.Id == dealAddOnId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> UpdateDealAddOnAsync(DealsAddOnModel model)
        {
            try
            {
                return await this.dealsAddOnRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> UpdateDealsItenaryAsync(DealsItineraryModel model)
        {
            try
            {
                return await this.dealsItineraryRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Flight Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> UpdateDealFlightAsync(DealsFlightModel model)
        {
            try
            {
                return await this.dealFlightRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Deals Night Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> UpdateDealNight(DealsNightModel model)
        {
            try
            {
                return await this.dealNightRepo.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsNightModel>> GetNightsAsync(int packageId)
        {
            try
            {
                return await this.dealNightRepo.Table.Where(x => x.PackageId == packageId).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsDestinationModel>> GetDestinationsAsync(int packageId)
        {
            try
            {
                return await this.dealDestinationRepo.Table.Where(x => x.PackageId == packageId).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsPaxCombinationModel>> GetPaxCombinationsAsync(int packageId)
        {
            try
            {
                return await this.dealsPaxCombinationRepo.Table.Where(x => x.PackageId == packageId).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsBookingValidityModel>> GetBookingValiditiesAsync(int packageId)
        {
            try
            {
                return await this.dealsBookingValidityRepo.Table.Where(x => x.PackageId == packageId && x.ValidTo > DateTime.Now).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="promotionId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsPromotion_RoomType> GetDealPromotionRoomTypeRecord(int promotionId)
        {
            try
            {
                return await this.dealsPromoRoomTypeRepo.Table.Where(x => x.PromotionId == promotionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="itenaryID">Itenary Id id</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsItineraryModel> GetItineraryByIdAsync(int itenaryID)
        {
            try
            {
                var result = await this.dealsItineraryRepo.Table.Where(x => x.Id == itenaryID).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="fromNightId">from night id</param>
        /// <param name="toNightId">to night id</param>
        /// <param name="packageId">package Id</param>
        /// <returns>1- Success, 2- Data Already Filled, 3 - Internal Server Error</returns>
        public async Task<int> CopyItinerary(int fromNightId, int toNightId, int packageId)
        {
            try
            {
                var existingRecords = await this.dealsItineraryRepo.Table.Where(x => x.NightId == toNightId && x.IsActive).ToListAsync();
                if (existingRecords != null && existingRecords.Count > 0)
                {
                    return 2;
                }

                var result = await this.dealsItineraryRepo.Table.Include("DealsInclusionModels")
                                                                  .Include("DealsInclusionModels.DealRoomConfigurationModels")
                                                                  .Include("DealsInclusionModels.DealsFlightModels")
                                                                  .Include("DealsInclusionModels.DealsAddOnModels")
                                                                  .Include("DealsInclusionModels.DealRoomConfigurationModels.PackageHotelRoomTypeModel")
                                                                  .Where(x => x.NightId == fromNightId && x.IsActive).ToListAsync();
                foreach (var item in result)
                {
                    DealsItineraryModel itinerarySaveModel = new DealsItineraryModel
                    {
                        Id = 0,
                        NightId = toNightId,
                        CardImg = item.CardImg,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        Days = item.Days,
                        Description = item.Description,
                        EndDay = item.EndDay,
                        IconClass = item.IconClass,
                        Nights = item.Nights,
                        IsActive = item.IsActive,
                        StartDay = item.StartDay,
                        Title = item.Title,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedDate = item.UpdatedDate
                    };
                    await this.dealsItineraryRepo.InsertAsync(itinerarySaveModel);
                    if (item.DealsInclusionModels != null && item.DealsInclusionModels.Count > 0)
                    {
                        foreach (var inclusionItem in item.DealsInclusionModels)
                        {
                            DealsInclusionModel dealsInclusionSave = new DealsInclusionModel
                            {
                                Id = 0,
                                ItineraryId = itinerarySaveModel.Id,
                                CreatedBy = inclusionItem.CreatedBy,
                                CreatedDate = inclusionItem.CreatedDate,
                                Day = inclusionItem.Day,
                                IsChargeable = inclusionItem.IsChargeable,
                                TypeId = inclusionItem.TypeId,
                                UpdatedBy = inclusionItem.UpdatedBy,
                                UpdatedDate = inclusionItem.UpdatedDate,
                                VendorInfoId = inclusionItem.VendorInfoId
                            };
                            await this.dealsInclusionRepo.InsertAsync(dealsInclusionSave);
                            if (inclusionItem.DealsAddOnModels != null && inclusionItem.DealsAddOnModels.Count > 0)
                            {
                                foreach (var addOnItem in inclusionItem.DealsAddOnModels)
                                {
                                    DealsAddOnModel addOnSaveModel = new DealsAddOnModel
                                    {
                                        Id = 0,
                                        InclusionId = dealsInclusionSave.Id,
                                        AdultCharge = addOnItem.AdultCharge,
                                        AdultMinimumAge = addOnItem.AdultMinimumAge,
                                        ChildCharge = addOnItem.ChildCharge,
                                        ChildMinimumAge = addOnItem.ChildMinimumAge,
                                        CreatedBy = addOnItem.CreatedBy,
                                        CreatedDate = addOnItem.CreatedDate,
                                        Description = addOnItem.Description,
                                        Image = addOnItem.Image,
                                        InfantCharge = addOnItem.InfantCharge,
                                        IsChargeable = addOnItem.IsChargeable,
                                        IsIncluded = addOnItem.IsIncluded,
                                        Name = addOnItem.Name,
                                        UpdatedBy = addOnItem.UpdatedBy,
                                        UpdatedDate = addOnItem.UpdatedDate
                                    };
                                    await this.dealsAddOnRepo.InsertAsync(addOnSaveModel);
                                }
                            }

                            if (inclusionItem.DealsFlightModels != null && inclusionItem.DealsFlightModels.Count > 0)
                            {
                                foreach (var dealFlightModel in inclusionItem.DealsFlightModels)
                                {
                                    DealsFlightModel flightAddModel = new DealsFlightModel
                                    {
                                        Id = 0,
                                        InclusionId = dealsInclusionSave.Id,
                                        AllDay = dealFlightModel.AllDay,
                                        CabinClass = dealFlightModel.CabinClass,
                                        CreatedBy = dealFlightModel.CreatedBy,
                                        CreatedDate = dealFlightModel.CreatedDate,
                                        Destination = dealFlightModel.Destination,
                                        EndTime = dealFlightModel.EndTime,
                                        Origin = dealFlightModel.Origin,
                                        StartTime = dealFlightModel.StartTime,
                                        UpdatedBy = dealFlightModel.UpdatedBy,
                                        UpdatedDate = dealFlightModel.UpdatedDate
                                    };
                                    await this.dealFlightRepo.InsertAsync(flightAddModel);
                                }
                            }

                            if (inclusionItem.DealRoomConfigurationModels != null && inclusionItem.DealRoomConfigurationModels.Count > 0)
                            {
                                foreach (var dealRoomConfig in inclusionItem.DealRoomConfigurationModels)
                                {
                                    DealRoomConfigurationModel dealRoomConfigSave = new DealRoomConfigurationModel
                                    {
                                        Id = 0,
                                        InclusionId = dealsInclusionSave.Id,
                                        Adult = dealRoomConfig.Adult,
                                        AdultAge = dealRoomConfig.AdultAge,
                                        CardImg = dealRoomConfig.CardImg,
                                        Child = dealRoomConfig.Child,
                                        ChildAge = dealRoomConfig.ChildAge,
                                        CreatedBy = dealRoomConfig.CreatedBy,
                                        CreatedDate = dealRoomConfig.CreatedDate,
                                        Description = dealRoomConfig.Description,
                                        FreeChild = dealRoomConfig.FreeChild,
                                        FreeInfant = dealRoomConfig.FreeInfant,
                                        Infant = dealRoomConfig.Infant,
                                        InfantAge = dealRoomConfig.InfantAge,
                                        IsActive = dealRoomConfig.IsActive,
                                        Max = dealRoomConfig.Max,
                                        RoomTypeId = dealRoomConfig.RoomTypeId,
                                        UpdatedBy = dealRoomConfig.UpdatedBy,
                                        UpdatedDate = dealRoomConfig.UpdatedDate
                                    };
                                    await this.dealHotelRoomConfigRepo.InsertAsync(dealRoomConfigSave);
                                }
                            }
                        }
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 3;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="nightId">deals package night id</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsItineraryModel>> GetItinerariesAsync(int nightId)
        {
            try
            {
                var result = await this.dealsItineraryRepo.Table.Include("DealsInclusionModels")
                                                                .Include("DealsInclusionModels.DealRoomConfigurationModels")
                                                                .Include("DealsInclusionModels.DealsFlightModels")
                                                                .Include("DealsInclusionModels.DealsAddOnModels")
                                                                .Include("DealsInclusionModels.DealRoomConfigurationModels.PackageHotelRoomTypeModel")
                                                                .Where(x => x.NightId == nightId && x.IsActive).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="code">Airport/City Code</param>
        /// <returns>Deal package model</returns>
        public async Task<FlightDestination> GetAirportDetailsByCode(string code)
        {
            try
            {
                var result = await this.flightDestinationRepo.Table.Where(x => x.CityCode == code).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="nightId">Airport/City Code</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsNightModel> GetNightByNightId(int nightId)
        {
            try
            {
                var result = await this.dealNightRepo.Table.Where(x => x.Id == nightId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="nightId">deals package night id</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsRatePlanModel>> GetratePlansAsync(int nightId)
        {
            try
            {
                return await this.dealsRatePlanRepo.Table.Where(x => x.NightId == nightId && x.IsActive).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="packageId">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<DealsBookingValidityModel> GetLastBookingValidityAsync(int packageId)
        {
            try
            {
                return await this.dealsBookingValidityRepo.Table.Where(x => x.Id == packageId && x.ValidTo > DateTime.Now).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="dealId">Deal indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<List<DealsRatePlanModel>> GetRatePlanByDealIdAsync(int dealId)
        {
            try
            {
                return await this.dealPackageRepo.Table.Where(x => x.Id == dealId).SelectMany(x => x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.ValidTo >= DateTime.Now && z.IsActive))).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="hotelId">Hotel indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<HotelierContentModel> GetHotelierContentByHotelIdAsync(int hotelId)
        {
            try
            {
                return await this.hotelierContentRepo.Table.Where(x => x.HotelId == hotelId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="hotelId">Hotel indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<List<HotelierImageModel>> GetHotelierImagesByHotelIdAsync(int hotelId)
        {
            try
            {
                return await this.hotelierImageRepo.Table.Where(x => x.HotelId == hotelId).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package booking validity information asynchromusly
        /// </summary>
        /// <param name="imageRecordId">Hotel indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<int?> DeletePackageImageById(int imageRecordId)
        {
            try
            {
                var record = this.dealsImageRepo.Table.Where(x => x.Id == imageRecordId).FirstOrDefault();
                return await this.dealsImageRepo.DeleteAsync(record);
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
        /// <param name="packageId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsContentViewModel> GetDealsContentByPackageIdAsync(int packageId)
        {
            try
            {
                DealsContentViewModel model = new DealsContentViewModel
                {
                    Id = 0,
                    PackageId = packageId
                };
                if (this.dealContentRepo.Table.Where(x => x.PackageId == packageId).Count() > 0)
                {
                    model = await this.dealContentRepo.Table.Where(x => x.PackageId == packageId).Select(x => new DealsContentViewModel
                    {
                        About = x.About,
                        AboutImg = x.AboutImg,
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
                        PackageId = x.PackageId,
                        Id = x.Id,
                        LogoImg = x.LogoImg,
                        TAUrl = x.TAUrl,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                    }).FirstOrDefaultAsync();
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
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealsContent(DealsContentModel model)
        {
            try
            {
                return await this.dealContentRepo.InsertAsync(model);
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
        public async Task<int> UpdateDealsContent(DealsContentModel model)
        {
            try
            {
                return await this.dealContentRepo.UpdateAsync(model);
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
        /// <param name="highLightId">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteDealHighlightByIdAsync(int highLightId)
        {
            try
            {
                var record = this.dealHighlightRepo.Table.Where(x => x.Id == highLightId).FirstOrDefault();
                return await this.dealHighlightRepo.DeleteAsync(record);
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
        public async Task<int> AddDealHighlightAsync(DealsHighlightModel model)
        {
            try
            {
                return await this.dealHighlightRepo.InsertAsync(model);
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
        public async Task<int> UpdateDealHighlight(DealsHighlightModel model)
        {
            try
            {
                return await this.dealHighlightRepo.UpdateAsync(model);
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
        /// <param name="packageId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealsHighlightViewModel>> GetAllDealHighlightsFromPackageIdAsync(int packageId)
        {
            try
            {
                return await this.dealHighlightRepo.Table.Where(x => x.PackageId == packageId).OrderByDescending(x => x.SortOrder.HasValue).ThenBy(x => x.SortOrder).Select(x => new DealsHighlightViewModel
                {
                    Id = x.Id,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    IsDeleted = false,
                    RandomIdentifier = Guid.NewGuid(),
                    PackageId = packageId,
                    StarRating = x.StarRating,
                    Title = x.Title,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    SortOrder = x.SortOrder
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealsHighlightViewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealsImageModel>> GetDealsImagesByPackageId(int packageId)
        {
            try
            {
                return await this.dealsImageRepo.Table.Where(x => x.PackageId == packageId).OrderByDescending(x => x.SortOrder.HasValue).ThenBy(x => x.SortOrder).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealsImageModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Room Amenetiy Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealsImageAsync(DealsImageModel model)
        {
            try
            {
                return await this.dealsImageRepo.InsertAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Delete Deal Image
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>Model</returns>
        public async Task<int> DeleteDealTourImageAsync(int id)
        {
            try
            {
                var record = await this.dealsImageRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.dealsImageRepo.DeleteAsync(record);
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
        public async Task<int> UpdateDealsImageAsync(DealsImageModel model)
        {
            try
            {
                return await this.dealsImageRepo.UpdateAsync(model);
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
        /// <param name="packageId">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsReviewModel> GetDealsReviewsById(int packageId)
        {
            try
            {
                var result = await this.dealsReviewRepo.Table.Where(x => x.Id == packageId).FirstOrDefaultAsync();
                return result;
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
        /// <param name="packageId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllDealsReviewsByPackageId(DataTableParameter model, int packageId)
        {
            try
            {
                var records = this.dealsReviewRepo.Table.Where(x => x.PackageId == packageId).Select(x => new DealsReviewsGridViewModel
                {
                    Id = x.Id,
                    PackageId = x.PackageId,
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

                return await this.dealsReviewsGridViewRepo.ToPagedListAsync(records, model);
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
        /// <param name="packageId">Hotel Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<DealsReviewModel>> GetDealReviewsByPackageId(int packageId)
        {
            try
            {
                var records = await this.dealsReviewRepo.Table.Where(x => x.PackageId == packageId && x.IsActive).ToListAsync();

                return records;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealsReviewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealsReviewAsync(DealsReviewModel model)
        {
            try
            {
                return await this.dealsReviewRepo.InsertAsync(model);
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
        public async Task<int> UpdateDealsReviewAsync(DealsReviewModel model)
        {
            try
            {
                return await this.dealsReviewRepo.UpdateAsync(model);
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
        public async Task<int> AddDealsPromotionAsync(DealsPromotionModel model)
        {
            try
            {
                await this.dealsPromotionRepo.InsertAsync(model);
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
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealPromotionRoomTypeRecord(DealsPromotion_RoomType model)
        {
            try
            {
                await this.dealsPromoRoomTypeRepo.InsertAsync(model);
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
        /// <param name="model">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealsPromotionAsync(DealsPromotionModel model)
        {
            try
            {
                return await this.dealsPromotionRepo.UpdateAsync(model);
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
        public async Task<int> DeleteDealsReviewAsync(int id)
        {
            try
            {
                var record = await this.dealsReviewRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.dealsReviewRepo.DeleteAsync(record);
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
        public async Task<int> DeleteItineraryAsync(int id)
        {
            var record = await this.dealsItineraryRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            return await this.dealsItineraryRepo.DeleteAsync(record);
        }

        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <param name="model">The model.</param>
        /////// <param name="packageId">Hotel Id</param>
        /////// <returns>
        /////// GetAllAsync
        /////// </returns>
        ////public async Task<DataTableResult> GetAllAddOnsByPackageId(DataTableParameter model, int packageId)
        ////{
        ////    try
        ////    {
        ////        var records = this.dealsAddOnRepo.Table.Where(x => x.PackageId == packageId).Include(x => x.DealsAddOnTypeModel).Select(x => new DealsAddOnGridViewModel
        ////        {
        ////            Id = x.Id,
        ////            Type = x.Type,
        ////            AdultCharge = x.AdultCharge,
        ////            ChildCharge = x.ChildCharge,
        ////            IsChargeable = x.IsChargeable,
        ////            TypeName = x.DealsAddOnTypeModel.Name,
        ////            PackageId = x.PackageId,
        ////            CreatedBy = x.CreatedBy,
        ////            CreatedDate = x.CreatedDate,
        ////            UpdatedBy = x.UpdatedBy,
        ////            UpdatedDate = x.UpdatedDate
        ////        });

        ////        return await this.dealsAddOnGridViewRepo.ToPagedListAsync(records, model);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        string msg = ex.ToString();
        ////        return null;
        ////    }
        ////}

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">Hotelier Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsAddOnModel> AddDealsAddOnByIdAsync(int id)
        {
            try
            {
                return await this.dealsAddOnRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
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
        public async Task<int> AddDealsAddOnAsync(DealsAddOnModel model)
        {
            try
            {
                return await this.dealsAddOnRepo.InsertAsync(model);
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
        public async Task<int> UpdateDealsAddOnAsync(DealsAddOnModel model)
        {
            try
            {
                return await this.dealsAddOnRepo.UpdateAsync(model);
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
        public async Task<int> DeleteAddOnAsync(int id)
        {
            try
            {
                var record = await this.dealsAddOnRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.dealsAddOnRepo.DeleteAsync(record);
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
        /// <param name="model">The Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealPackageInfoAsync(DealsPackageModel model)
        {
            try
            {
                await this.dealPackageRepo.InsertAsync(model);
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
        /// <param name="model">The Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealPackageInfoAsync(DealsPackageModel model)
        {
            try
            {
                await this.dealPackageRepo.UpdateAsync(model);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteAllPackageDestinationByPackageId(int packageId)
        {
            try
            {
                var records = await this.dealDestinationRepo.Table.Where(x => x.PackageId == packageId).ToListAsync();
                foreach (var item in records)
                {
                    await this.dealDestinationRepo.DeleteAsync(item);
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
        /// <param name="validityId">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeletePackageBookingValidityById(int validityId)
        {
            try
            {
                var records = await this.dealsBookingValidityRepo.Table.Where(x => x.Id == validityId).FirstOrDefaultAsync();
                await this.dealsBookingValidityRepo.DeleteAsync(records);
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
        /// <param name="paxId">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeletePackagePaxCombinationById(int paxId)
        {
            try
            {
                var records = await this.dealsPaxCombinationRepo.Table.Where(x => x.Id == paxId).FirstOrDefaultAsync();
                await this.dealsPaxCombinationRepo.DeleteAsync(records);
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
        /// <param name="destinationId">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteDealsDestinationById(int destinationId)
        {
            try
            {
                var records = await this.dealDestinationRepo.Table.Where(x => x.Id == destinationId).FirstOrDefaultAsync();
                await this.dealDestinationRepo.DeleteAsync(records);
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
        /// <param name="model">Deal Destination Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealDestination(DealsDestinationModel model)
        {
            try
            {
                return await this.dealDestinationRepo.UpdateAsync(model);
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
        /// <param name="model">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealPaxCombination(DealsPaxCombinationModel model)
        {
            try
            {
                await this.dealsPaxCombinationRepo.UpdateAsync(model);
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
        /// <param name="model">The Booking Validity Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealBookingValidty(DealsBookingValidityModel model)
        {
            try
            {
                await this.dealsBookingValidityRepo.UpdateAsync(model);
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
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealPackageDestinationAsync(DealsDestinationModel model)
        {
            try
            {
                return await this.dealDestinationRepo.InsertAsync(model);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteAllPackagePaxCombinationByPackageId(int packageId)
        {
            try
            {
                var records = await this.dealsPaxCombinationRepo.Table.Where(x => x.PackageId == packageId).ToListAsync();
                foreach (var item in records)
                {
                    await this.dealsPaxCombinationRepo.DeleteAsync(item);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteAllPackageBookingValidtyByPackageId(int packageId)
        {
            try
            {
                var records = await this.dealsBookingValidityRepo.Table.Where(x => x.PackageId == packageId).ToListAsync();
                foreach (var item in records)
                {
                    await this.dealsBookingValidityRepo.DeleteAsync(item);
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
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealPackagePaxCombinationAsync(DealsPaxCombinationModel model)
        {
            try
            {
                return await this.dealsPaxCombinationRepo.InsertAsync(model);
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
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealPackageBookingValidityAsync(DealsBookingValidityModel model)
        {
            try
            {
                return await this.dealsBookingValidityRepo.InsertAsync(model);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public DealsInclusionModel GetInclusionRecordForHotelFromPackageId(int packageId)
        {
            try
            {
                return this.dealPackageRepo.Table.Where(x => x.Id == packageId).Select(x => x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
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
        /// <param name="id">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsHotelInfoViewModel> GetDealPackageHotelInfoByPackageIdAsync(int id)
        {
            try
            {
                int? hotelId = this.dealPackageRepo.Table.Where(x => x.Id == id).Select(x => x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(a => a.VendorInfoId).FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
                var model = await this.hotelInformationRepository.Table.Where(x => x.Id == hotelId).Include(x => x.VendorInformationModel).Select(x => new DealsHotelInfoViewModel
                {
                    AddressLine1 = x.VendorInformationModel.Address1,
                    AddressLine2 = x.VendorInformationModel.Address2,
                    CheckInTime = x.CheckIn,
                    CheckOutTime = x.CheckOut,
                    CityName = x.VendorInformationModel.CityModel.Name,
                    ContactPersonName = x.VendorInformationModel.VendorContactModels.OrderByDescending(y => y.IsPrimary).Select(y => y.Salutation + " " + y.FirstName + " " + y.LastName).FirstOrDefault(),
                    CountryName = x.VendorInformationModel.CountryModel.Name,
                    CurrencyName = x.VendorInformationModel.CurrencyModel.Name + " (" + x.VendorInformationModel.CurrencyModel.Code + ")",
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    EmailId = x.VendorInformationModel.VendorContactModels.OrderByDescending(y => y.IsPrimary).Select(y => y.Email).FirstOrDefault(),
                    HotelId = x.Id,
                    VendorId = x.VendorInformationModel.Id,
                    HotelName = x.VendorInformationModel.Name,
                    IsOpenCheckIn = x.IsOpenCheckIn,
                    MobilePhone = x.VendorInformationModel.VendorContactModels.OrderByDescending(y => y.IsPrimary).Select(y => y.Mobile).FirstOrDefault(),
                    PackageId = id,
                    PostalCode = x.VendorInformationModel.PostalCode,
                    PropertyType = x.HotelierPropertyTypeModel.Name,
                    StarRating = x.StarRating,
                    StateName = x.VendorInformationModel.State != 0 && x.VendorInformationModel.State != null ? this.stateModelRepository.Table.Where(y => y.Id == x.VendorInformationModel.State).Select(y => y.Name).FirstOrDefault() : string.Empty,
                    Website = x.Url,
                    WorkPhone = x.VendorInformationModel.VendorContactModels.OrderByDescending(y => y.IsPrimary).FirstOrDefault().WorkPhone,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                }).FirstOrDefaultAsync();
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
        /// <param name="id">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsFlightViewModel> GetFlightsFromInclusion(int id)
        {
            try
            {
                var model = await this.dealsInclusionRepo.Table.Where(x => x.Id == id).Select(x => new DealsFlightViewModel
                {
                    Id = x.DealsFlightModels.Select(y => y.Id).FirstOrDefault(),
                    AllDay = x.DealsFlightModels.Select(y => y.AllDay).FirstOrDefault(),
                    CabinClass = x.DealsFlightModels.Select(y => y.CabinClass).FirstOrDefault(),
                    Days = x.Day,
                    ItenaryId = x.ItineraryId,
                    Destination = x.DealsFlightModels.Select(y => y.Destination).FirstOrDefault(),
                    EndTime = x.DealsFlightModels.Select(y => y.EndTime).FirstOrDefault(),
                    Origin = x.DealsFlightModels.Select(y => y.Origin).FirstOrDefault(),
                    InclusionId = x.Id,
                    StartTime = x.DealsFlightModels.Select(y => y.StartTime).FirstOrDefault(),
                    TotalDays = x.DealsItineraryModel.Days,
                    TypeId = x.TypeId,
                    VendorId = x.VendorInfoId,
                    CreatedBy = x.DealsFlightModels.Select(y => y.CreatedBy).FirstOrDefault(),
                    CreatedDate = x.DealsFlightModels.Select(y => y.CreatedDate).FirstOrDefault(),
                    UpdatedBy = x.DealsFlightModels.Select(y => y.UpdatedBy).FirstOrDefault(),
                    UpdatedDate = x.DealsFlightModels.Select(y => y.UpdatedDate).FirstOrDefault(),
                }).FirstOrDefaultAsync();
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
        /// <param name="inclusionId">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsAddOnViewModel> GetDealActivitiesByInclusionId(int inclusionId)
        {
            try
            {
                var addOnId = await this.dealsAddOnRepo.Table.Where(x => x.InclusionId == inclusionId).Select(x => x.Id).FirstOrDefaultAsync();
                var model = await this.dealsAddOnRepo.Table.Where(x => x.Id == addOnId).Select(x => new DealsAddOnViewModel
                {
                    Id = x.Id,
                    AdultCharge = x.AdultCharge,
                    AdultMinimumAge = x.AdultMinimumAge,
                    ChildCharge = x.ChildCharge,
                    ChildMinimumAge = x.ChildMinimumAge,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    Day = Convert.ToInt32(x.DealInclusionModel.Day),
                    Description = x.Description,
                    Image = x.Image,
                    InclusionId = x.InclusionId,
                    InfantCharge = x.InfantCharge,
                    IsChargeable = x.IsChargeable,
                    IsIncluded = x.IsIncluded,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    VendorId = x.DealInclusionModel.VendorInfoId,
                    ItenaryId = x.DealInclusionModel.ItineraryId
                }).FirstOrDefaultAsync();
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
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealPackageHotelRoomConfiguration(DealRoomConfigurationModel model)
        {
            try
            {
                return await this.dealHotelRoomConfigRepo.InsertAsync(model);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealRoomConfigViewModel>> GetDealPackageRoomConfigByPackageIdAsync(int packageId)
        {
            try
            {
                var inclusionRecord = this.GetInclusionRecordForHotelFromPackageId(packageId);
                var result = await this.dealHotelRoomConfigRepo.Table.Where(x => x.InclusionId == inclusionRecord.Id).Select(x => new DealRoomConfigViewModel
                {
                    Id = x.Id,
                    Adult = x.Adult,
                    AdultAge = x.AdultAge,
                    InclusionId = x.InclusionId,
                    CardImg = x.CardImg,
                    Child = x.Child,
                    ChildAge = x.ChildAge,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    FreeChild = x.FreeChild,
                    FreeInfant = x.FreeInfant,
                    Infant = x.Infant,
                    InfantAge = x.InfantAge,
                    IsActive = x.IsActive,
                    Max = x.Max,
                    RoomTypeId = x.RoomTypeId,
                    RoomTypeName = x.PackageHotelRoomTypeModel.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealRoomConfigViewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="id">The Deal.RoomConfig Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DealDeletePackageRoomConfig(int id)
        {
            try
            {
                var record = await this.dealHotelRoomConfigRepo.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
                return await this.dealHotelRoomConfigRepo.DeleteAsync(record);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealVisaViewModel>> GetVisaItemsByPackageId(int packageId)
        {
            try
            {
                if (this.dealsVisaRepo.Table.Where(x => x.PackageId == packageId).Count() > 0)
                {
                    return await this.dealsVisaRepo.Table.Where(x => x.PackageId == packageId).Select(x => new DealVisaViewModel
                    {
                        Id = x.Id,
                        PackageId = packageId,
                        AdultPrice = (decimal)x.AdultPrice,
                        ChildPrice = (decimal)x.ChildPrice,
                        BufferDays = (int)x.BufferDays,
                        CountryId = (short)x.CountryId,
                        VendorId = (int)x.VendorId,
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        IsDeleted = false,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        DocumentsRequired = x.DocumentsRequired,
                        GeneralPolicy = x.GeneralPolicy,
                        Markup = x.Markup,
                        PhotoSpecification = x.PhotoSpecification,
                        ProcessingTime = x.ProcessingTime
                    }).ToListAsync();
                }
                else
                {
                    return new List<DealVisaViewModel>();
                }

                ////else
                ////{
                ////    ////var packageCountries = await this.dealDestinationRepo.Table.Where(x => x.PackageId == packageId).Select(x => x.Country).ToListAsync();
                ////    ////if (packageCountries.Count > 0)
                ////    ////{
                ////    ////    return await this.visaMasterRepo.Table.Where(x => packageCountries.Contains(x.CountryId) && x.IsActive).Select(x => new DealVisaViewModel
                ////    ////    {
                ////    ////        Id = 0,
                ////    ////        CountryId = x.CountryId,
                ////    ////        AdultPrice = x.AdultPrice,
                ////    ////        BufferDays = x.BufferDays,
                ////    ////        ChildPrice = x.ChildPrice,
                ////    ////        CreatedBy = x.CreatedBy,
                ////    ////        CreatedDate = x.CreatedDate,
                ////    ////        IsActive = x.IsActive,
                ////    ////        IsDeleted = false,
                ////    ////        PackageId = packageId,
                ////    ////        UpdatedBy = x.UpdatedBy,
                ////    ////        UpdatedDate = x.UpdatedDate,
                ////    ////        VendorId = x.VendorID,
                ////    ////        DocumentsRequired = x.DocumentsRequired,
                ////    ////        ProcessingTime = x.ProcessingTime,
                ////    ////        PhotoSpecification = x.PhotoSpecification,
                ////    ////        GeneralPolicy = x.GeneralPolicy,
                ////    ////        Markup = x.Markup
                ////    ////    }).ToListAsync();
                ////    ////}
                ////    ////else
                ////    ////{
                ////    ////    return new List<DealVisaViewModel>();
                ////    ////}
                ////}
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealVisaViewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> MoveVisaItemsByPackageDestinations(int packageId)
        {
            var packageCountries = await this.dealDestinationRepo.Table.Where(x => x.PackageId == packageId).Select(x => x.Country).ToListAsync();
            if (packageCountries.Count > 0)
            {
                var visaMasterData = await this.visaMasterRepo.Table.Where(x => packageCountries.Contains(x.CountryId) && x.IsActive).ToListAsync();
                if (visaMasterData.Count > 0)
                {
                    foreach (var item in visaMasterData)
                    {
                        if (this.dealsVisaRepo.Table.Where(x => x.PackageId == packageId && x.CountryId == item.CountryId).Count() == 0)
                        {
                            DealVisaModel dealVisaModel = new DealVisaModel
                            {
                                Id = 0,
                                CountryId = item.CountryId,
                                AdultPrice = item.AdultPrice,
                                BufferDays = item.BufferDays,
                                ChildPrice = item.ChildPrice,
                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                IsActive = item.IsActive,
                                PackageId = packageId,
                                UpdatedBy = item.UpdatedBy,
                                UpdatedDate = item.UpdatedDate,
                                VendorId = item.VendorID,
                                DocumentsRequired = item.DocumentsRequired,
                                ProcessingTime = item.ProcessingTime,
                                PhotoSpecification = item.PhotoSpecification,
                                GeneralPolicy = item.GeneralPolicy,
                                Markup = item.Markup
                            };

                            await this.dealsVisaRepo.InsertAsync(dealVisaModel);
                        }
                    }

                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealRatePlan(DealsRatePlanModel model)
        {
            try
            {
                await this.dealsRatePlanRepo.InsertAsync(model);
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
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealPackageVisa(DealVisaModel model)
        {
            try
            {
                await this.dealsVisaRepo.InsertAsync(model);
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
        /// <param name="countryId">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<VisaModel> GetVisaByCountryId(short? countryId)
        {
            try
            {
                if (countryId == null)
                {
                    return null;
                }

                return await this.visaMasterRepo.Table.Where(x => x.IsActive && x.CountryId == countryId).FirstOrDefaultAsync();
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
        /// <param name="countryId">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealVisaModel>> GetAllVisaFromActiveDealsFromCountryId(short? countryId)
        {
            try
            {
                if (countryId == null)
                {
                    return null;
                }

                return await this.dealPackageRepo.Table.Where(x => x.IsActive && !x.IsDeleted).SelectMany(x => x.DealsVisaModels).Where(x => x.CountryId == countryId && x.IsActive).ToListAsync();
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
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealPackageVisa(DealVisaModel model)
        {
            try
            {
                await this.dealsVisaRepo.UpdateAsync(model);
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
        /// <param name="visaId">The Visa ID</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeletePackageVisaById(int visaId)
        {
            try
            {
                var record = await this.dealsVisaRepo.Table.Where(x => x.Id == visaId).FirstOrDefaultAsync();
                return await this.dealsVisaRepo.DeleteAsync(record);
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
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealRatePlan(DealsRatePlanModel model)
        {
            try
            {
                return await this.dealsRatePlanRepo.UpdateAsync(model);
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
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealPromotionRoomTypeRecord(DealsPromotion_RoomType model)
        {
            try
            {
                return await this.dealsPromoRoomTypeRepo.UpdateAsync(model);
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
        /// <param name="model">The Deal Rate Plan Model</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeletePromotionRoomTypeRecord(DealsPromotion_RoomType model)
        {
            try
            {
                return await this.dealsPromoRoomTypeRepo.DeleteAsync(model);
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
        /// <param name="roomConfigId">Room Config Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealsRatePlanViewModel>> GetDealRoomConfigAllRatePlans(int roomConfigId)
        {
            try
            {
                return await this.dealsRatePlanRepo.Table.Where(x => x.ValidTo >= DateTime.Now && x.RoomConfigId == roomConfigId && x.IsActive).Select(x => new DealsRatePlanViewModel
                {
                    Id = x.Id,
                    Currency = x.Currency,
                    CurrencyName = x.Currency != null ? this.currencyRepo.Table.Where(y => y.Id == Convert.ToInt32(x.Currency)).Select(y => y.Name).FirstOrDefault() : string.Empty,
                    RoomConfigId = x.RoomConfigId,
                    SingleSupplement = x.SingleSupplement,
                    CreatedBy = x.CreatedBy,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    ExtraAdult = x.ExtraAdult,
                    ExtraChild_NB = x.ExtraChild_NB,
                    ExtraChild_WB = x.ExtraChild_WB,
                    ExtraInfant = x.ExtraInfant,
                    Name = x.Name,
                    Price = x.Price,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealsRatePlanViewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsRatePlanModel> GetDealRatePlanById(int ratePlanId)
        {
            try
            {
                return await this.dealsRatePlanRepo.Table.Where(x => x.Id == ratePlanId).FirstOrDefaultAsync();
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
        /// <param name="inclusionId">inclusionId</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsInclusionModel> GetDealsInclusion(int inclusionId)
        {
            try
            {
                return await this.dealsInclusionRepo.Table.Where(x => x.Id == inclusionId).FirstOrDefaultAsync();
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
        /// <param name="model">The Deal.RoomConfig Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealPackageRoomConfig(DealRoomConfigurationModel model)
        {
            try
            {
                return await this.dealHotelRoomConfigRepo.UpdateAsync(model);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateMinPriceForPackage(int packageId)
        {
            try
            {
                decimal minPrice = 0;
                var nightRecord = await this.dealNightRepo.Table.Where(x => x.PackageId == packageId).OrderBy(x => x.Value).FirstOrDefaultAsync();
                List<DealsRatePlanModel> ratePlans = await this.dealsRatePlanRepo.Table.Where(x => x.NightId == nightRecord.Id && x.IsActive && x.ValidTo >= DateTime.Now).ToListAsync();
                if (ratePlans != null)
                {
                    minPrice = ratePlans.OrderBy(x => x.Price)
                            .Join(this.currencyRepo.Table, r => r.Currency, c => c.Id, (r, c) => new { r, c })
                            .Select(x => (x.r.Price * Convert.ToDecimal(x.c.ExchangeRate)) + (x.r.ExtraSupplement != null ? Convert.ToDecimal(x.r.ExtraSupplement) * Convert.ToDecimal(x.c.ExchangeRate) : 0) + (x.r.MarkUp != null ? (x.r.Price * Convert.ToDecimal(x.c.ExchangeRate) * Convert.ToDecimal(x.r.MarkUp)) / 100 : 0)).FirstOrDefault();
                }

                DealsPackageModel packageRecord = await this.dealPackageRepo.Table.Where(x => x.Id == packageId).FirstOrDefaultAsync();
                packageRecord.MinPrice = minPrice;
                return await this.dealPackageRepo.UpdateAsync(packageRecord);
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
        /// <param name="packageId">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<DealsSeoDetail> GetSeoDetail(int packageId)
        {
            try
            {
                return await this.dealsSeoRepo.Table.Where(x => x.DealId == packageId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new DealsSeoDetail { Id = 0, DealId = packageId };
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealSeo(DealsSeoDetail model)
        {
            try
            {
                return await this.dealsSeoRepo.UpdateAsync(model);
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
        /// <param name="model">The Package Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealSeo(DealsSeoDetail model)
        {
            try
            {
                return await this.dealsSeoRepo.InsertAsync(model);
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
        /// <param name="ratePlanId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteRatePlanAsync(int ratePlanId)
        {
            var record = await this.dealsRatePlanRepo.Table.Where(x => x.Id == ratePlanId).FirstOrDefaultAsync();
            record.IsActive = false;
            return await this.dealsRatePlanRepo.UpdateAsync(record);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteHotelInclusion(int hotelId)
        {
            List<DealRoomConfigurationModel> roomConfigRecords = await this.dealHotelRoomConfigRepo.Table.Where(x => x.InclusionId == hotelId).ToListAsync();
            foreach (DealRoomConfigurationModel roomConfigRecord in roomConfigRecords)
            {
                await this.dealHotelRoomConfigRepo.DeleteAsync(roomConfigRecord);
            }

            var record = await this.dealsInclusionRepo.Table.Where(x => x.Id == hotelId).FirstOrDefaultAsync();
            return await this.dealsInclusionRepo.DeleteAsync(record);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inclusionId">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteFlightInclusion(int inclusionId)
        {
            List<DealsFlightModel> flightRecords = await this.dealFlightRepo.Table.Where(x => x.InclusionId == inclusionId).ToListAsync();
            foreach (DealsFlightModel roomConfigRecord in flightRecords)
            {
                await this.dealFlightRepo.DeleteAsync(roomConfigRecord);
            }

            var record = await this.dealsInclusionRepo.Table.Where(x => x.Id == inclusionId).FirstOrDefaultAsync();
            return await this.dealsInclusionRepo.DeleteAsync(record);
        }

        /// <summary>
        /// get Flight Details by Inclusion Id
        /// </summary>
        /// <param name="inclusionId">Inclusion Id</param>param>
        /// <returns>Flight model</returns>
        public async Task<DealsFlightModel> GetFlightDetailsByInclusionId(int inclusionId)
        {
            return await this.dealFlightRepo.Table.Where(x => x.InclusionId == inclusionId).FirstOrDefaultAsync();
           ////var city = await from this.dealFlightRepo.Table.Where(x => x.InclusionId == inclusionId).Join<this.flightDestinationRepo.Table.Where(y => y.cityCode == x.cityCode)> .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get City name by city code
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>flight destination model</returns>
        public async Task<FlightDestination> GetCityByCityCode(string code)
        {
            return await this.flightDestinationRepo.Table.Where(x => x.CityCode == code).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="roomTypeId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        public async Task<int> DeleteRoomTypeInclusion(int roomTypeId)
        {
            var record = await this.dealHotelRoomConfigRepo.Table.Where(x => x.Id == roomTypeId).FirstOrDefaultAsync();
            return await this.dealHotelRoomConfigRepo.DeleteAsync(record);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageId">The Deal.RoomConfig Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealsHotelRatePlanViewModel>> GetRoomConfigsForHotelRatePlans(int packageId)
        {
            try
            {
                var inclusionRecord = this.GetInclusionRecordForHotelFromPackageId(packageId);
                return await this.dealHotelRoomConfigRepo.Table.Where(x => x.InclusionId == inclusionRecord.Id && x.IsActive).Select(x => new DealsHotelRatePlanViewModel
                {
                    RoomConfigId = x.Id,
                    RoomName = x.PackageHotelRoomTypeModel.Name,
                    PackageId = packageId
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<DealsHotelRatePlanViewModel>();
            }
        }

        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="id">Group Identifier .</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetRoomConfigDropDownListForRatePlanAsync(string search, short page, int? id, int packageId)
        {
            var inclusionRecord = this.GetInclusionRecordForHotelFromPackageId(packageId);
            var query = this.dealHotelRoomConfigRepo.Table.Where(x => x.InclusionId == inclusionRecord.Id && x.IsActive)
                           .OrderBy(x => x.PackageHotelRoomTypeModel.Name)
                           .Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.PackageHotelRoomTypeModel.Name });
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
        /// <returns>
        /// list of dropdown
        /// </returns>
        public async Task<IList<Dropdown>> GetAirportsCodesDropdownAsync(string search, short page, string id)
        {
            var query = this.flightDestinationRepo.Table
                           .OrderBy(x => x.CityCode)
                           .Select(x => new Dropdown { Id = x.CityCode.ToString(), Name = x.ShortDetail + " (" + x.CityCode + ") " + x.CountryName });
            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(x => x.Id == id);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelierId">The Hotel Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<HotelierReviewModel>> GetAllHotelReviews(int hotelierId)
        {
            try
            {
                return await this.hotelReviewRepo.Table.Where(x => x.HotelId == hotelierId && x.IsActive).ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<HotelierReviewModel>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="packageIds">The Package Id</param>
        /// <returns>InformationModel</returns>
        public List<short> GetAllRoomTypeIdsFromPackageId(int packageIds)
        {
            try
            {
                List<short> roomIds = new List<short>();
                var nights = this.dealNightRepo.Table.Where(x => x.PackageId == packageIds)
                    .Include("DealsItineraryModels")
                    .Include("DealsItineraryModels.DealsInclusionModels")
                    .Include("DealsItineraryModels.DealsInclusionModels.DealRoomConfigurationModels");
                foreach (var night in nights)
                {
                    foreach (var itinary in night.DealsItineraryModels)
                    {
                        if (itinary.IsActive)
                        {
                            foreach (var inclusion in itinary.DealsInclusionModels)
                            {
                                roomIds.AddRange(inclusion.DealRoomConfigurationModels.Select(roomConfig => roomConfig.RoomTypeId));
                            }
                        }
                    }
                }

                return roomIds;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<short>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="search">Search</param>
        /// <param name="page">Page</param>
        /// <param name="nightId">The Package Id</param>
        /// <param name="inclusionIds">The Inclusion Ids</param>
        /// <returns>Get Inclusion Hoteliers From NightId</returns>
        public async Task<IList<Dropdown>> GetInclusionHoteliersFromNightId(string search, short page, int nightId, int[] inclusionIds)
        {
            try
            {
                var hotels = this.dealNightRepo.Table.Where(x => x.Id == nightId).SelectMany(x => x.DealsItineraryModels.Where(y => y.IsActive).SelectMany(y => y.DealsInclusionModels.Where(z => z.TypeId == 1))).GroupBy(x => x.VendorInfoId).Select(x => x.FirstOrDefault());
                ////var groupedResult = hotels.;
                var dropdown = hotels.Select(x => new Dropdown
                {
                    Id = x.Id.ToString(),
                    Name = this.hotelInformationRepository.Table.Where(y => y.Id == Convert.ToInt32(x.VendorInfoId)).Select(y => y.Name).FirstOrDefault()
                });
                if (inclusionIds != null && inclusionIds.Any())
                {
                    dropdown = dropdown.Where(x => inclusionIds.Contains(Convert.ToInt32(x.Id)));
                }

                if (!string.IsNullOrEmpty(search))
                {
                    dropdown = dropdown.Where(x => x.Name.Contains(search));
                }

                return await dropdown.ToListAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new List<Dropdown>();
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inclusionId">The Inclusion Id</param>
        /// <returns>InformationModel</returns>
        public DealsFlightModel GetHotelFlightRecordFromInclusionId(int inclusionId)
        {
            return this.dealFlightRepo.Table.FirstOrDefault(x => x.InclusionId == inclusionId);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="dealId">The Deal Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealsRatePlanModel>> GetRatePlansByDealIdAsync(int dealId)
        {
            var records = await this.dealNightRepo.Table.Where(x => x.PackageId == dealId).SelectMany(x => x.DealsRatePlanModel.Where(y => y.IsActive && y.ValidTo > DateTime.Now)).Join(this.dealHotelRoomConfigRepo.Table.Include(z => z.PackageHotelRoomTypeModel), x => x.RoomConfigId, r => r.Id, (x, r) => new { x, r }).ToListAsync();
            for (int i = 0; i < records.Count; i++)
            {
                records[i].x.Inclusions = records[i].r.PackageHotelRoomTypeModel.Name;
            }

            return records.Select(y => y.x).ToList();
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="dealId">The Deal Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealsDestinationModel>> GetDealDestinationByDealIdAsync(int dealId)
        {
            return await this.dealDestinationRepo.Table.Where(x => x.Id == dealId).ToListAsync();
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="dealId">The Deal Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealsRatePlanModel>> GetRatePlansByDealIdForTourAsync(int dealId)
        {
            var records = await this.dealNightRepo.Table.Where(x => x.PackageId == dealId).SelectMany(x => x.DealsRatePlanModel.Where(y => y.IsActive && y.ValidTo > DateTime.Now)).Join(this.dealNightRepo.Table, r => r.NightId, n => n.Id, (r, n) => new { r, n }).ToListAsync();
            for (int i = 0; i < records.Count; i++)
            {
                records[i].r.Inclusions = records[i].n.Value + " nights";
            }

            return records.Select(y => y.r).ToList();
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inventoryRecord">Inventory Record</param>
        /// <returns>InformationModel</returns>
        public async Task<int> AddDealInventoryRecordAsync(DealInventoryModel inventoryRecord)
        {
            return await this.dealInventoryRepo.InsertAsync(inventoryRecord);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="inventoryRecord">Inventory Record</param>
        /// <returns>InformationModel</returns>
        public async Task<int> UpdateDealInventoryRecordAsync(DealInventoryModel inventoryRecord)
        {
            return await this.dealInventoryRepo.UpdateAsync(inventoryRecord);
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="ratePlanId">The Rate Plan Id</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealInventoryModel>> GetInventoryByRatePlanId(int ratePlanId)
        {
            try
            {
                var records = await this.dealInventoryRepo.Table.Where(x => x.RatePlanId == ratePlanId && x.IsActive).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="ratePlanId">The Rate Plan Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="days">Days of Week</param>
        /// <returns>InformationModel</returns>
        public async Task<List<DealInventoryModel>> GetFilteredInventoryByRatePlanId(int ratePlanId, DateTime? startDate, DateTime? endDate, int[] days)
        {
            try
            {
                var records = this.dealInventoryRepo.Table.Where(x => x.RatePlanId == ratePlanId && x.IsActive);
                if (startDate != null && endDate != null)
                {
                    records = records.Where(x => x.Date >= startDate && x.Date <= endDate);
                }

                if (days != null && days.Any())
                {
                    records = records.Where(x => days.Contains((int)x.Date.DayOfWeek));
                }

                return await records.ToListAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        private string GetPreviewUrl(DealsPackageModel model, int dealType)
        {
            string previewUrl = "/";
            switch (dealType)
            {
                case 1:
                    return previewUrl += "Hotel/" + model.Url;
                case 2:
                    return previewUrl += "Holiday/" + model.Url;
            }

            return previewUrl;
        }
    }
}