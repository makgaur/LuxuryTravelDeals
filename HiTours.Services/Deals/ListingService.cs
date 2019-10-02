// <copyright file="ListingService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using HiTours.ViewModels.Deals.Product;
    using HiTours.ViewModels.Deals.Product.Hotel;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using static HiTours.Core.Enums;

    /// <summary>
    /// PackageService
    /// </summary>
    /// <seealso cref="HiTours.Services.IListingService" />
    public class ListingService : IListingService
    {
        private readonly int localCountry = 61;
        private readonly int hotelMaxLength = 30;
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<VendorGroupModel> vendorGroupRespository;
        private readonly IRepository<HotelierAmenitiesModel> hotelierAmenitieRespository;
        private readonly IRepository<HotelierRoomAmenetiesModel> hotelierRoomAmenitieRespository;
        private readonly IRepository<AmenitiesMasterModel> amenitiesRepository;
        private readonly IRepository<DealsPackageModel> dealPackageRespository;
        private readonly IRepository<DealsRatePlanModel> dealRatePlanRespository;
        private readonly IRepository<HotelierInformationModel> hotelierInfoRepository;
        private readonly IRepository<PackageCityModel> packageCityRepository;
        private readonly IRepository<PackageCountryModel> packageCountryRepository;
        private readonly IRepository<PackageDealTypeModel> packageDealTypeRepository;
        private readonly IRepository<DealsPromoScheduleModel> dealsPromoScheduleRepository;
        private readonly IRepository<SettingPromoDiscountModel> settingsPromoDiscountRepository;
        private readonly IRepository<CurrencyModel> currencyRepository;
        private readonly IRepository<PackageStateModel> packageStateRepository;
        private readonly IRepository<DealRoomConfigurationModel> dealRoomConfigurationRespository;
        private readonly IRepository<PackageTravelStyleModel> travelStyleRepository;
        private readonly IRepository<HotelierPropertyTypeModel> hotelierPropertyTypeRepository;
        private readonly IRepository<PackageAreaModel> packageAreaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListingService" /> class.
        /// </summary>
        /// <param name="packageStateRepository">Package State Repository</param>
        /// <param name="dealRoomConfigurationRespository">deal Room Config Repository</param>
        /// <param name="packageAreaRepository">Package Area Repo</param>
        /// <param name="travelStyleRepository">Travel Style Repo</param>
        /// <param name="hotelierPropertyTypeRepository">Hotelier Property Type</param>
        /// <param name="currencyRepository">Currency Repository</param>
        /// <param name="vendorGroupRespository">Vendor Group Repo</param>
        /// <param name="hotelierRoomAmenitieRespository">Hotelier Room Amenity Repo</param>
        /// <param name="hotelierAmenitieRespository">Hotelier Ameneties Repo</param>
        /// <param name="amenitiesRepository">Amenities Repostory</param>
        /// <param name="settingsPromoDiscountRepository">Setting Promo Discount Model</param>
        /// <param name="dealsPromoScheduleRepository">Deals Promo Schedular</param>
        /// <param name="dealRatePlanRespository">Deal Rate Plan</param>
        /// <param name="packageCityRepository">Package City Model</param>
        /// <param name="packageCountryRepository">Package Country Model</param>
        /// <param name="dropdownRespository">Dropdown</param>
        /// <param name="dealPackageRespository">Deal Package Repo</param>
        /// <param name="hotelierInfoRepository">Hotelier Information Repository</param>
        /// <param name="packageDealTypeRepository">Package Deal Type Model</param>
        public ListingService(
            IRepository<PackageStateModel> packageStateRepository,
            IRepository<DealRoomConfigurationModel> dealRoomConfigurationRespository,
            IRepository<PackageAreaModel> packageAreaRepository,
            IRepository<PackageTravelStyleModel> travelStyleRepository,
            IRepository<HotelierPropertyTypeModel> hotelierPropertyTypeRepository,
            IRepository<CurrencyModel> currencyRepository,
            IRepository<VendorGroupModel> vendorGroupRespository,
            IRepository<HotelierRoomAmenetiesModel> hotelierRoomAmenitieRespository,
            IRepository<HotelierAmenitiesModel> hotelierAmenitieRespository,
            IRepository<AmenitiesMasterModel> amenitiesRepository,
            IRepository<SettingPromoDiscountModel> settingsPromoDiscountRepository,
            IRepository<DealsPromoScheduleModel> dealsPromoScheduleRepository,
            IRepository<DealsRatePlanModel> dealRatePlanRespository,
            IRepository<PackageCityModel> packageCityRepository,
            IRepository<PackageCountryModel> packageCountryRepository,
            IRepository<Dropdown> dropdownRespository,
            IRepository<DealsPackageModel> dealPackageRespository,
            IRepository<HotelierInformationModel> hotelierInfoRepository,
            IRepository<PackageDealTypeModel> packageDealTypeRepository)
        {
            this.packageStateRepository = packageStateRepository;
            this.dealRoomConfigurationRespository = dealRoomConfigurationRespository;
            this.packageAreaRepository = packageAreaRepository;
            this.travelStyleRepository = travelStyleRepository;
            this.hotelierPropertyTypeRepository = hotelierPropertyTypeRepository;
            this.currencyRepository = currencyRepository;
            this.vendorGroupRespository = vendorGroupRespository;
            this.hotelierRoomAmenitieRespository = hotelierRoomAmenitieRespository;
            this.hotelierAmenitieRespository = hotelierAmenitieRespository;
            this.amenitiesRepository = amenitiesRepository;
            this.settingsPromoDiscountRepository = settingsPromoDiscountRepository;
            this.dealsPromoScheduleRepository = dealsPromoScheduleRepository;
            this.dealRatePlanRespository = dealRatePlanRespository;
            this.packageDealTypeRepository = packageDealTypeRepository;
            this.packageCityRepository = packageCityRepository;
            this.packageCountryRepository = packageCountryRepository;
            this.hotelierInfoRepository = hotelierInfoRepository;
            this.dealPackageRespository = dealPackageRespository;
            this.dropdownRespository = dropdownRespository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="limit">Result Limit</param>
        /// <param name="offset">Result Offset</param>
        /// <param name="searchTerms">Search Criteria Values</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<PackageCurationViewModel> GetSearchListing(int limit, int offset, string searchTerms)
        {
            var searchRequirement = JsonConvert.DeserializeObject<SearchTermViewModel>(searchTerms);
            searchRequirement.StartDateVar = !string.IsNullOrEmpty(searchRequirement.StartDate) ? DateTime.ParseExact(searchRequirement.StartDate, "dd/MM/yyyy", null) : DateTime.Now;
            searchRequirement.EndDateVar = !string.IsNullOrEmpty(searchRequirement.EndDate) ? DateTime.ParseExact(searchRequirement.EndDate, "dd/MM/yyyy", null) : DateTime.Now.AddYears(1);
            var query = this.DealListingQuery();
            return this.QueryModelConversion(query).ToList();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealIds">Deal Ids</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<PackageCurationViewModel> GetFilterSearchResults(List<int> dealIds)
        {
            var query = this.DealListingQuery();
            query = query.Where(x => dealIds.Contains(x.Id));
            var result = query.ToList();
            ////for (int i = 0; i < result.Count; i++)
            ////{
            ////    result[i] = this.GetHotelDealRatePlan(result[i]);
            ////}

            return this.PackageCurationConversion(result);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="listingViewModel">Listing View Model</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public ListingViewModel GetSearchReFilterResults(ListingViewModel listingViewModel)
        {
            var query = this.DealListingQuery();
            if (listingViewModel.FiltersViewModels.Any(x => x.Type == (int)FilterTypes.Country && x.FilterOptions.Any(y => y.IsSelected)))
            {
                if (listingViewModel.FiltersViewModels.Any(x => x.Type == (int)FilterTypes.City && x.FilterOptions.Any(y => y.IsSelected)))
                {
                }
                else
                {
                    listingViewModel.FiltersViewModels.RemoveAll(x => x.Type == (int)FilterTypes.CityArea);
                }
            }
            else
            {
                listingViewModel.FiltersViewModels.RemoveAll(x => x.Type == (int)FilterTypes.City || x.Type == (int)FilterTypes.CityArea);
            }

            var selectedFilters = listingViewModel.FiltersViewModels.SelectMany(x => x.FilterOptions.Where(y => y.IsSelected)).ToList();
            if (selectedFilters != null && selectedFilters.Count > 0)
            {
                string dealIds = string.Empty;
                if (selectedFilters.Count >= 2)
                {
                    List<List<int>> formattedIds = selectedFilters.Select(x => x.DealIds.Split(',').Select(y => Convert.ToInt32(y)).ToList()).ToList();
                    var intersection = formattedIds.Skip(1).Aggregate(new HashSet<int>(formattedIds.First()), (h, e) =>
                    {
                        h.IntersectWith(e);
                        return h;
                    });
                    query = query.Where(x => intersection.Contains(x.Id));
                }
                else
                {
                    foreach (var filterItem in selectedFilters)
                    {
                        if (filterItem.IsSelected)
                        {
                            if (dealIds == string.Empty)
                            {
                                dealIds = filterItem.DealIds;
                            }
                            else
                            {
                                dealIds = string.Join(',', dealIds, filterItem.DealIds);
                            }
                        }
                    }

                    List<int> convertedDealIds = dealIds.Split(',').Select(x => Convert.ToInt32(x)).Distinct().ToList();
                    query = query.Where(x => convertedDealIds.Contains(x.Id));
                }
            }

            List<DealsPackageModel> result = query.ToList();
            if (result != null)
            {
                ////for (int i = 0; i < result.Count; i++)
                ////{
                ////    result[i] = this.GetHotelDealRatePlan(result[i]);
                ////}

                listingViewModel.FiltersViewModels = this.ReBindFilterViewModel(listingViewModel.FiltersViewModels, result, query);
                listingViewModel.FiltersViewModels = this.BindNestedFilters(listingViewModel.FiltersViewModels, result);
                listingViewModel.ResultViewModels = this.PackageCurationConversion(result);
            }

            return listingViewModel;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="listingViewModel">Search Criteria Values</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public ListingViewModel GetSearchResults(ListingViewModel listingViewModel)
        {
            IQueryable<DealsPackageModel> query = this.DealListingQuery();

            ////////Date Filter Start
            ////query = query.Where(x => x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.))
            ////////Date Filter End
            if (listingViewModel.SearchTermViewModel.SearchType == (int)Enums.SearchType.FlashDeal)
            {
                query = query.Where(x => x.TravelCategory.Contains("2,") || x.TravelCategory.Contains(",2") || x.TravelCategory == "2"); //// Flash Deal Filter
            }

            if (listingViewModel.SearchTermViewModel.SearchType == (int)Enums.SearchType.TravelStyle)
            {
                query = query.Where(x => x.TravelStyle.Contains(listingViewModel.SearchTermViewModel.Value.ToString() + ",") || x.TravelStyle.Contains("," + listingViewModel.SearchTermViewModel.Value.ToString()) || x.TravelStyle == listingViewModel.SearchTermViewModel.Value.ToString()); //// Flash Deal Filter
            }

            if (listingViewModel.SearchTermViewModel.SearchType == (int)Enums.SearchType.Country)
            {
                query = query.Where(x => x.DealsDestinationModels.Select(y => y.Country).Contains(Convert.ToInt16(listingViewModel.SearchTermViewModel.Value)));
            }

            if (listingViewModel.SearchTermViewModel.SearchType == (int)Enums.SearchType.City)
            {
                query = query.Where(x => x.DealsDestinationModels.Select(y => y.City).Contains(listingViewModel.SearchTermViewModel.Value));
            }

            if (listingViewModel.SearchTermViewModel.SearchType == (int)Enums.SearchType.Query)
            {
                if (!string.IsNullOrEmpty(listingViewModel.SearchTermViewModel.SearchTerm))
                {
                    var nameQuery = query.Where(x => x.Name.Contains(listingViewModel.SearchTermViewModel.SearchTerm.Replace("-", " ")));
                    List<int> cities = this.packageCityRepository.Table.Where(x => x.Name.Contains(listingViewModel.SearchTermViewModel.SearchTerm.Replace("-", " "))).Select(x => x.Id).ToList();
                    if (cities.Count > 0)
                    {
                        foreach (var item in cities)
                        {
                            nameQuery = nameQuery.AsEnumerable().Concat(query.Where(x => x.DealsDestinationModels.Select(y => y.City).Contains(item))).AsQueryable();
                        }
                    }

                    List<short> countries = this.packageCountryRepository.Table.Where(x => x.Name.Contains(listingViewModel.SearchTermViewModel.SearchTerm.Replace("-", " "))).Select(x => x.Id).ToList();
                    if (countries.Count > 0)
                    {
                        foreach (var item in countries)
                        {
                            nameQuery = nameQuery.AsEnumerable().Concat(query.Where(x => x.DealsDestinationModels.Select(y => y.Country).Contains(item))).AsQueryable();
                        }
                    }

                    List<int> hotels = this.hotelierInfoRepository.Table.Where(x => x.IsActive && !x.IsDeleted && x.Name.Contains(listingViewModel.SearchTermViewModel.SearchTerm.Replace("-", " "))).Select(x => x.Id).ToList();
                    if (hotels.Count > 0)
                    {
                        foreach (var item in hotels)
                        {
                            nameQuery = nameQuery.AsEnumerable().Concat(query.Where(x => x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.Where(z => z.IsActive).SelectMany(z => z.DealsInclusionModels.Select(i => i.VendorInfoId))).Contains(item))).AsQueryable();
                        }
                    }

                    //// Search in About Section
                    nameQuery = nameQuery.AsEnumerable().Concat(query.Where(x => x.DealContentModels.Where(y => y.About.ToLower().Contains(" " + listingViewModel.SearchTermViewModel.SearchTerm.Replace("-", " ").ToLower() + " ")).Any())).AsQueryable();
                    query = nameQuery.Distinct();
                }
            }

            if (listingViewModel.SearchTermViewModel.SearchType == (int)Enums.SearchType.Hotel)
            {
                query = query.Where(x => x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.Where(z => z.IsActive).SelectMany(z => z.DealsInclusionModels.Select(k => k.VendorInfoId))).Contains(listingViewModel.SearchTermViewModel.Value));
            }

            if (listingViewModel.SearchTermViewModel.SearchType == (int)Enums.SearchType.State)
            {
                query = query.Where(x => x.DealsDestinationModels.Select(y => y.State).Contains(listingViewModel.SearchTermViewModel.Value));
            }

            List<DealsPackageModel> result = query.ToList();
            if (result != null)
            {
                ////for (int i = 0; i < result.Count; i++)
                ////{
                ////    result[i] = this.GetHotelDealRatePlan(result[i]);
                ////}

                listingViewModel.ResultViewModels = this.PackageCurationConversion(result);
                listingViewModel.FiltersViewModels = this.BindFilterViewModel(result, query);
            }

            return listingViewModel;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="ids">Result Limit</param>
        /// <param name="offset">Result Offset</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<PackageCurationViewModel> GetFlashDealsAsync(string ids, int offset)
        {
            var query = this.DealListingQuery();
            List<int> skipIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            query = query.Where(x => !skipIds.Contains(x.Id) && (x.TravelStyle.Contains("1,") || x.TravelStyle.Contains(",1") || x.TravelStyle == "1" || x.TravelCategory.Contains("2,") || x.TravelCategory.Contains(",2") || x.TravelCategory == "2")); //// Flash Deal Filter
            var data = query.ToList();
            var tours = data.Where(x => x.Type == 2).GroupBy(x => x.DealsDestinationModels.Select(y => y.Country).FirstOrDefault()).Select(x => x.ToList()).ToList();
            var hotel = data.Where(x => x.Type == 1).GroupBy(x => x.DealsDestinationModels.Select(y => y.City).FirstOrDefault()).Select(x => x.ToList()).ToList();
            List<DealsPackageModel> resultModel = new List<DealsPackageModel>();
            foreach (var item in tours)
            {
                if (item.Count > 2)
                {
                    item.OrderBy(x => x.DealsNightModels.Select(y => y.DealsRatePlanModel.Select(z => z.Price)));
                    resultModel.Add(item.First());
                    resultModel.Add(item.Last());
                }
                else
                {
                    resultModel.AddRange(item);
                }
            }

            foreach (var item in hotel)
            {
                List<DealsPackageModel> processedRatePlans = new List<DealsPackageModel>();
                ////foreach (var subItem in item)
                ////{
                ////    processedRatePlans.Add(this.GetHotelDealRatePlan(subItem));
                ////}

                if (processedRatePlans.Count > 2)
                {
                    processedRatePlans.OrderBy(x => x.DealsNightModels.Select(y => y.DealsRatePlanModel.Select(z => z.Price)));
                    resultModel.Add(processedRatePlans.First());
                    resultModel.Add(processedRatePlans.Last());
                }
                else
                {
                    resultModel.AddRange(item);
                }
            }

            var result = this.PackageCurationConversion(resultModel.Skip(offset * 6).Take(6).ToList());
            return result;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="ids">Result Limit</param>
        /// <param name="offset">Result Offset</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<PackageCurationViewModel> GetDealsOfMonth(string ids, int offset)
        {
            var query = this.DealListingQuery();
            List<int> skipIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            var data = query.ToList();
            var tours = data.Where(x => x.Type == 2).GroupBy(x => x.DealsDestinationModels.Select(y => y.Country).FirstOrDefault()).Select(x => x.ToList()).ToList();
            var hotel = data.Where(x => x.Type == 1).GroupBy(x => x.DealsDestinationModels.Select(y => y.City).FirstOrDefault()).Select(x => x.ToList()).ToList();
            List<DealsPackageModel> resultModel = new List<DealsPackageModel>();
            foreach (var item in tours)
            {
                if (item.Count > 1)
                {
                    item.OrderBy(x => x.DealsNightModels.Select(y => y.DealsRatePlanModel.Select(z => z.Price)));
                    resultModel.Add(item.First());
                }
                else
                {
                    resultModel.AddRange(item);
                }
            }

            foreach (var item in hotel)
            {
                List<DealsPackageModel> processedRatePlans = new List<DealsPackageModel>();
                ////foreach (var subItem in item)
                ////{
                ////    processedRatePlans.Add(this.GetHotelDealRatePlan(subItem));
                ////}

                if (processedRatePlans.Count > 1)
                {
                    processedRatePlans.OrderBy(x => x.DealsNightModels.Select(y => y.DealsRatePlanModel.Select(z => z.Price)));
                    resultModel.Add(processedRatePlans.First());
                }
                else
                {
                    resultModel.AddRange(item);
                }
            }

            var result = this.PackageCurationConversion(resultModel.Skip(offset * 6).Take(6).ToList());
            return result;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<PackageCurationViewModel> GetTop6TrendingDeals()
        {
            var query = this.DealListingQuery();
            query = query.OrderByDescending(x => x.ViewCount).Take(6);
            return this.PackageCurationConversion(query.ToList());
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<PackageCurationViewModel>> GetTop3FlashDeals()
        {
            try
            {
                var promoDeals = await this.dealsPromoScheduleRepository.Table.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.PromoType == (int)PromoType.FlashDeal && x.DealPackageModel.IsActive && !x.DealPackageModel.IsDeleted).Select(x => x.PackageId).ToListAsync();
                if (promoDeals.Count > 0 && promoDeals.Count >= 3)
                {
                    var query = this.DealListingQuery().Where(x => promoDeals.Contains(x.Id)).Take(3);
                    var resultList = this.QueryModelConversion(query).ToList();
                    resultList.ForEach(x => x.DealType = "Flash Deal");
                    return resultList;
                }
                else
                {
                    var promoDealsExpired = await this.dealsPromoScheduleRepository.Table.Where(x => x.PromoType == (int)PromoType.FlashDeal).Select(x => x.PackageId).ToListAsync();
                    var discountsRanges = this.settingsPromoDiscountRepository.Table.Where(x => x.IsActive && x.PromoType == (int)PromoType.FlashDeal).ToList();
                    List<PackageCurationViewModel> model = new List<PackageCurationViewModel>();
                    var query = this.DealListingQuery().Where(x => !promoDealsExpired.Contains(x.Id));
                    model = await this.FlashDealPicker(query, discountsRanges);
                    if (model.Count < 3)
                    {
                        var dealsToDelete = await this.dealsPromoScheduleRepository.Table.Where(x => x.PromoType == (int)PromoType.FlashDeal).ToListAsync();
                        foreach (var item in dealsToDelete)
                        {
                            await this.dealsPromoScheduleRepository.DeleteAsync(item);
                        }

                        model = await this.FlashDealPicker(this.DealListingQuery(), discountsRanges);
                    }

                    model.ForEach(x => x.DealType = "Flash Deal");
                    return model;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new List<PackageCurationViewModel>();
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<PackageCurationViewModel>> GetTop3DealsOfTheMonth()
        {
            var promoDeals = await this.dealsPromoScheduleRepository.Table.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.PromoType == (int)PromoType.DealoftheMonth && x.DealPackageModel.IsActive && !x.DealPackageModel.IsDeleted).Select(x => x.PackageId).ToListAsync();
            if (promoDeals.Count > 0 && promoDeals.Count >= 3)
            {
                var query = this.DealListingQuery().Where(x => promoDeals.Contains(x.Id)).Take(3);
                return this.QueryModelConversion(query).ToList();
            }
            else
            {
                var promoDealsExpired = await this.dealsPromoScheduleRepository.Table.Where(x => x.PromoType == (int)PromoType.DealoftheMonth).Select(x => x.PackageId).ToListAsync();
                List<PackageCurationViewModel> model = new List<PackageCurationViewModel>();
                var query = this.DealListingQuery().Where(x => !promoDealsExpired.Contains(x.Id)).OrderByDescending(x => x.Id);
                model = await this.DealOfMonthPicker(query);
                if (model.Count < 3)
                {
                    var dealsToDelete = await this.dealsPromoScheduleRepository.Table.Where(x => x.PromoType == (int)PromoType.DealoftheMonth).ToListAsync();
                    foreach (var item in dealsToDelete)
                    {
                        await this.dealsPromoScheduleRepository.DeleteAsync(item);
                    }

                    model = await this.DealOfMonthPicker(this.DealListingQuery().OrderByDescending(x => x.Id));

                    return model;
                }

                return model;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<PackageCurationViewModel> GetTop3CityDeals(int cityId)
        {
            var query = this.DealListingQuery();
            query = query.Where(x => x.DealsDestinationModels.Select(y => y.City).Contains(cityId)).Take(3);
            var item = query.ToList();
            return this.PackageCurationConversion(item);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="searchTerm">Search Term</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<Tuple<int, int, bool>> GetValueListTypeOfSearchCityStateAsync(string searchTerm)
        {
            PackageStateModel stateModel =
                await this.packageStateRepository.Table.FirstOrDefaultAsync(x =>
                    x.Name.Replace(" ", string.Empty).Replace("-", string.Empty).ToLower().Contains(searchTerm.Replace("-", string.Empty).ToLower()));
            if (stateModel != null)
            {
                return new Tuple<int, int, bool>(stateModel.Id, (int)Enums.SearchType.State, true);
            }

            PackageCityModel cityModel =
                await this.packageCityRepository.Table.FirstOrDefaultAsync(x =>
                    x.Name.Replace(" ", string.Empty).Replace("-", string.Empty).ToLower().Contains(searchTerm.Replace("-", string.Empty).ToLower()));
            if (cityModel != null)
            {
                return new Tuple<int, int, bool>(cityModel.Id, (int)Enums.SearchType.City, true);
            }

            HotelierInformationModel hotelierInformationModel =
                await this.hotelierInfoRepository.Table.FirstOrDefaultAsync(x =>
                    x.Name.Replace(" ", string.Empty).Replace("-", string.Empty).ToLower().Contains(searchTerm.Replace("-", string.Empty).ToLower()));
            if (hotelierInformationModel != null)
            {
                return new Tuple<int, int, bool>(hotelierInformationModel.Id, (int)Enums.SearchType.Hotel, true);
            }

            return new Tuple<int, int, bool>(0, (int)Enums.SearchType.Query, true);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="searchTerm">Search Term</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<Tuple<int, int, bool>> GetValueListTypeOfSearchCountryTravelStyleAsync(string searchTerm)
        {
            PackageCountryModel countryModel =
                await this.packageCountryRepository.Table.FirstOrDefaultAsync(x =>
                    x.Name.Replace(" ", string.Empty).Replace("-", string.Empty).ToLower().Contains(searchTerm.Replace("-", string.Empty).ToLower()));
            if (countryModel != null)
            {
                return new Tuple<int, int, bool>(countryModel.Id, (int)Enums.SearchType.Country, true);
            }

            PackageTravelStyleModel travelStyleModel =
                await this.travelStyleRepository.Table.FirstOrDefaultAsync(x =>
                    x.IsActive && x.Name.Replace(" ", string.Empty).Replace("-", string.Empty).ToLower().Contains(searchTerm.Replace("-", string.Empty).ToLower()));
            if (travelStyleModel != null)
            {
                return new Tuple<int, int, bool>(travelStyleModel.Id, (int)Enums.SearchType.TravelStyle, false);
            }

            return new Tuple<int, int, bool>(0, (int)Enums.SearchType.Query, true);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="searchTerm">Search Term</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<Tuple<int, int, bool, string>> GetProductUrlAsync(string searchTerm)
        {
            DealsPackageModel model = await this.dealPackageRespository.Table.FirstOrDefaultAsync(x => x.IsActive && !x.IsDeleted && x.Name.Replace("-", string.Empty).Replace(" ", string.Empty).ToLower() == searchTerm.Replace("-", string.Empty).ToLower());
            if (model != null)
            {
                return new Tuple<int, int, bool, string>(model.Id, (int)Enums.SearchType.Product, false, model.Url);
            }

            return new Tuple<int, int, bool, string>(0, (int)Enums.SearchType.Query, true, searchTerm);
        }

        private async Task<List<PackageCurationViewModel>> DealOfMonthPicker(IQueryable<DealsPackageModel> query)
        {
            List<PackageCurationViewModel> model = new List<PackageCurationViewModel>();
            var localHotel = this.QueryModelConversion(query.Where(x => x.Type == 1 && x.DealsDestinationModels.Any(y => y.Country == this.localCountry))).FirstOrDefault();
            if (localHotel != null)
            {
                if (!this.dealsPromoScheduleRepository.Table.Any(x => x.PromoType == (int)PromoType.DealoftheMonth && x.PackageId == localHotel.Id))
                {
                    DealsPromoScheduleModel lhPromoModel = new DealsPromoScheduleModel
                    {
                        Id = 0,
                        Discount = 0,
                        StartDate = DateTime.Now.Date.AddHours(12),
                        EndDate = DateTime.Now.Date.AddDays(30).AddHours(12),
                        RenewalDate = DateTime.Now.Date.AddDays(16).AddHours(12),
                        PackageId = localHotel.Id,
                        PromoType = (int)PromoType.DealoftheMonth
                    };
                    await this.dealsPromoScheduleRepository.InsertAsync(lhPromoModel);
                }

                model.Add(localHotel);
            }

            var internationalHotel = this.QueryModelConversion(query.Where(x => x.Type == 1 && x.DealsDestinationModels.Any(y => y.Country != this.localCountry))).FirstOrDefault();
            if (internationalHotel != null)
            {
                if (!this.dealsPromoScheduleRepository.Table.Any(x => x.PromoType == (int)PromoType.DealoftheMonth && x.PackageId == internationalHotel.Id))
                {
                    DealsPromoScheduleModel ihPromoModel = new DealsPromoScheduleModel
                    {
                        Id = 0,
                        Discount = 0,
                        StartDate = DateTime.Now.Date.AddHours(12),
                        EndDate = DateTime.Now.Date.AddDays(30).AddHours(12),
                        RenewalDate = DateTime.Now.Date.AddDays(16).AddHours(12),
                        PackageId = internationalHotel.Id,
                        PromoType = (int)PromoType.DealoftheMonth
                    };
                    await this.dealsPromoScheduleRepository.InsertAsync(ihPromoModel);
                }

                model.Add(internationalHotel);
            }

            var tour = this.QueryModelConversion(query.Where(x => x.Type != 1)).FirstOrDefault();
            if (tour != null)
            {
                if (!this.dealsPromoScheduleRepository.Table.Any(x => x.PromoType == (int)PromoType.DealoftheMonth && x.PackageId == tour.Id))
                {
                    DealsPromoScheduleModel tPromoModel = new DealsPromoScheduleModel
                    {
                        Id = 0,
                        Discount = 0,
                        StartDate = DateTime.Now.Date.AddHours(12),
                        EndDate = DateTime.Now.Date.AddDays(30).AddHours(12),
                        RenewalDate = DateTime.Now.Date.AddDays(16).AddHours(12),
                        PackageId = tour.Id,
                        PromoType = (int)PromoType.DealoftheMonth
                    };
                    await this.dealsPromoScheduleRepository.InsertAsync(tPromoModel);
                }

                model.Add(tour);
            }

            return model;
        }

        private async Task<List<PackageCurationViewModel>> FlashDealPicker(IQueryable<DealsPackageModel> query, List<SettingPromoDiscountModel> discountsRanges)
        {
            List<PackageCurationViewModel> model = new List<PackageCurationViewModel>();
            query = query.Where(x => x.TravelCategory.Contains("2,") || x.TravelCategory.Contains(",2") || x.TravelCategory == "2"); //// Flash Deal Filter
            var localHotel = this.QueryModelConversion(query.Where(x => x.Type == 1 && x.DealsDestinationModels.Select(y => y.Country == this.localCountry).FirstOrDefault())).FirstOrDefault();
            if (localHotel != null)
            {
                if (!this.dealsPromoScheduleRepository.Table.Any(x => x.PromoType == (int)PromoType.FlashDeal && x.PackageId == localHotel.Id))
                {
                    decimal? markUp = this.DealListingQuery().FirstOrDefault(x => x.Id == localHotel.Id)?.MarkUp;
                    decimal promoDiscount = 0;
                    if (markUp == null)
                    {
                        promoDiscount = 0;
                    }
                    else
                    {
                        foreach (var item in discountsRanges)
                        {
                            if (markUp >= item.MinMarkUp && markUp <= item.MaxMarkUp)
                            {
                                promoDiscount = item.DiscountMarkUp;
                            }
                        }
                    }

                    DealsPromoScheduleModel lhPromoModel = new DealsPromoScheduleModel
                    {
                        Id = 0,
                        Discount = promoDiscount,
                        StartDate = DateTime.Now.Date.AddHours(12),
                        EndDate = DateTime.Now.Date.AddDays(3).AddHours(12),
                        RenewalDate = DateTime.Now.Date.AddDays(16).AddHours(12),
                        PackageId = localHotel.Id,
                        PromoType = (int)PromoType.FlashDeal
                    };
                    await this.dealsPromoScheduleRepository.InsertAsync(lhPromoModel);
                }

                model.Add(localHotel);
            }

            var internationalHotel = this.QueryModelConversion(query.Where(x => x.Type == 1 && x.DealsDestinationModels.Select(y => y.Country != this.localCountry).FirstOrDefault())).FirstOrDefault();
            if (internationalHotel != null)
            {
                if (!this.dealsPromoScheduleRepository.Table.Any(x => x.PromoType == (int)PromoType.FlashDeal && x.PackageId == internationalHotel.Id))
                {
                    decimal? markUp = this.DealListingQuery().FirstOrDefault(x => x.Id == internationalHotel.Id)?.MarkUp;
                    decimal promoDiscount = 0;
                    if (markUp == null)
                    {
                        promoDiscount = 0;
                    }
                    else
                    {
                        foreach (var item in discountsRanges)
                        {
                            if (markUp >= item.MinMarkUp && markUp <= item.MaxMarkUp)
                            {
                                promoDiscount = item.DiscountMarkUp;
                            }
                        }
                    }

                    DealsPromoScheduleModel ihPromoModel = new DealsPromoScheduleModel
                    {
                        Id = 0,
                        Discount = promoDiscount,
                        StartDate = DateTime.Now.Date.AddHours(12),
                        EndDate = DateTime.Now.Date.AddDays(3).AddHours(12),
                        RenewalDate = DateTime.Now.Date.AddDays(16).AddHours(12),
                        PackageId = internationalHotel.Id,
                        PromoType = (int)PromoType.FlashDeal
                    };
                    await this.dealsPromoScheduleRepository.InsertAsync(ihPromoModel);
                }

                model.Add(internationalHotel);
            }

            var tour = this.QueryModelConversion(query.Where(x => x.Type != 1)).FirstOrDefault();
            if (tour != null)
            {
                if (!this.dealsPromoScheduleRepository.Table.Any(x => x.PromoType == (int)PromoType.FlashDeal && x.PackageId == tour.Id))
                {
                    decimal? markUp = this.DealListingQuery().FirstOrDefault(x => x.Id == tour.Id)?.MarkUp;
                    decimal promoDiscount = 0;
                    if (markUp == null)
                    {
                        promoDiscount = 0;
                    }
                    else
                    {
                        foreach (var item in discountsRanges)
                        {
                            if (markUp >= item.MinMarkUp && markUp <= item.MaxMarkUp)
                            {
                                promoDiscount = item.DiscountMarkUp;
                            }
                        }
                    }

                    DealsPromoScheduleModel tPromoModel = new DealsPromoScheduleModel
                    {
                        Id = 0,
                        Discount = promoDiscount,
                        StartDate = DateTime.Now.Date.AddHours(12),
                        EndDate = DateTime.Now.Date.AddDays(3).AddHours(12),
                        RenewalDate = DateTime.Now.Date.AddDays(16).AddHours(12),
                        PackageId = tour.Id,
                        PromoType = (int)PromoType.FlashDeal
                    };
                    await this.dealsPromoScheduleRepository.InsertAsync(tPromoModel);
                }

                model.Add(tour);
            }

            return model;
        }

        private IQueryable<DealsPackageModel> DealListingQuery()
        {
            List<int> notToInclude = this.dealPackageRespository.Table
                .Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2)
                &&
                    (x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.ValidTo >= DateTime.Now && z.IsActive).OrderBy(z => z.Price)).Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p }).Select(k => Convert.ToDecimal(k.r.Price) * Convert.ToDecimal(k.p.ExchangeRate)).Any(k => k == 0) //// Exchange Rate is 0 or Rate Plan have 0
                    ||
                    !x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)).Any() //// NO Rate Plan
                    ||
                    !x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).SelectMany(r => r.DealInventoryModels.Where(i => i.Date > DateTime.Now.Date))).Any() //// If No Inventory Filled
                    ||
                    x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).SelectMany(r => r.DealInventoryModels.Where(i => i.Date > DateTime.Now.Date))).All(i => i.BlackOut) //// If all Remaining Dates are Blacked Out
                    ||
                    x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).SelectMany(r => r.DealInventoryModels.Where(i => i.Date > DateTime.Now.Date))).All(i => i.Inventory == 0))) //// if all Remaining Dates have no inventory
                .Select(x => x.Id).ToList();

            ////notToInclude.AddRange(this.dealPackageRespository.Table
            ////    .Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2) && x.DealsNightModels
            ////        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo > DateTime.Now)).Count() == 0)
            ////    .Select(x => x.Id).ToList());
            ////notToInclude.AddRange(this.dealPackageRespository.Table
            ////    .Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2) && x.DealsNightModels
            ////        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo > DateTime.Now).SelectMany(r => r.DealInventoryModels.Where(i => i.Date > DateTime.Now.Date))).All(i => i.BlackOut || i.Inventory == 0))
            ////    .Select(x => x.Id).ToList()); ////
            var query = this.dealPackageRespository.Table
                .Include("DealsPromoScheduleModel")
                .Include("DealsDestinationModels")
                .Include("DealsNightModels")
                .Include("DealContentModels")
                .Include("DealsHighlightModels")
                .Include("DealsVisaModels")
                .Include("DealsNightModels.DealsRatePlanModel")
                .Include("DealsNightModels.DealsRatePlanModel.DealInventoryModels")
                .Include("DealsNightModels.DealsItineraryModels")
                .Include("DealsNightModels.DealsItineraryModels.DealsInclusionModels")
                .Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2));
            query = query.Where(x => !notToInclude.Contains(x.Id));
            return query;
        }

        private IQueryable<PackageCurationViewModel> QueryModelConversion(IQueryable<DealsPackageModel> query)
        {
                var newQuery = query.Select(x => new PackageCurationViewModel
                {
                    Id = x.Id,
                    Ameneties = x.DealsHighlightModels.OrderByDescending(y => y.SortOrder.HasValue).ThenBy(y => y.SortOrder).Select(y => new KeyValuePair<bool, string>(y.StarRating > 0 ? true : false, y.Title)).Take(3).ToList(),
                    LatLong = new Tuple<decimal?, decimal?>(x.Lat, x.Long),
                    CreatedDate = x.CreatedDate,
                    Cities = this.packageCityRepository.Table.Where(y => x.DealsDestinationModels.Select(z => z.City).Contains(y.Id)).Select(y => y.Name).ToList(),
                    DealName = x.Name,
                    HotelId = x.Type == 1 ? Convert.ToInt32(x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.Select(di => di.VendorInfoId))).FirstOrDefault()) : 0,
                    Image = x.DealContentModels.Select(y => y.CardImg).FirstOrDefault(),
                    DealType = this.packageDealTypeRepository.Table.Where(y => (x.TravelCategory.Contains("," + y.Id.ToString()) || x.TravelCategory.Contains(y.Id.ToString() + ",") || x.TravelCategory.Contains(y.Id.ToString())) && y.IsActive).Select(y => y.Name).FirstOrDefault(),
                    IsHotel = x.Type == 1 ? true : false,
                    MinNights = x.Type == 1 ?
                                    x.DealsNightModels.Select(n => n.Value).FirstOrDefault()
                                    :
                                    x.DealsNightModels.OrderBy(y => y.Value).Select(y => y.Value).FirstOrDefault(),
                    MinPrice = x.Type == 1 ? //// Hotel
                                    x.DealsNightModels.OrderBy(y => y.Value)
                                    .Select(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                                    .OrderBy(z => z.Price)
                                    .Join(this.dealRoomConfigurationRespository.Table, drate => drate.RoomConfigId, droom => droom.Id, (drate, droom) => new { drate, droom }).Where(z => z.droom.IsActive).Select(z => z.drate)
                                    .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                                   .Select(z => z.r.MarkUp != null && z.r.MarkUp != 0 ?
                                        ((((z.r.Price + (Convert.ToDecimal(z.r.MarkUp) / 100 * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                        + //// Room Price + Extra Supplement
                                        ((z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0) + ((Convert.ToDecimal(z.r.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                        *
                                        Convert.ToDecimal(z.p.ExchangeRate)
                                    :
                                    z.r.DealsNightModel.DealsPackageModel.MarkUp != null && z.r.DealsNightModel.DealsPackageModel.MarkUp != 0 ?
                                            ((((z.r.Price + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                            + //// Room Price + Extra Supplement
                                            ((z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0) + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                            *
                                            Convert.ToDecimal(z.p.ExchangeRate)
                                            :
                                            (((z.r.Price * z.r.DealsNightModel.Value) / 2)
                                            + //// Room Price + Extra Supplement
                                            (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))
                                            * Convert.ToDecimal(z.p.ExchangeRate)).OrderBy(z => z).FirstOrDefault()).FirstOrDefault()
                                    :
                                    x.DealsNightModels.OrderBy(y => y.Value) //// Tour
                                    .Select(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                                    .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                                    .OrderBy(z => z.r.Price * Convert.ToDecimal(z.p.ExchangeRate))
                                    .Select(z => z.r.MarkUp != null && z.r.MarkUp != 0 ?
                                        ((z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0) + ((Convert.ToDecimal(z.r.MarkUp) / 100) * (z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)))) * Convert.ToDecimal(z.p.ExchangeRate))
                                        :
                                         z.r.DealsNightModel.DealsPackageModel.MarkUp != null ?
                                            ((z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0) + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * (z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)))) * Convert.ToDecimal(z.p.ExchangeRate))
                                            :
                                            ((z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)) * Convert.ToDecimal(z.p.ExchangeRate)))
                                         .OrderBy(z => z).FirstOrDefault()).FirstOrDefault(),
                    Discount = x.DealsNightModels.OrderBy(y => y.Value)
                                    .Select(y => y.DealsRatePlanModel.Where(z => z.IsActive)
                                    .OrderBy(z => z.Price)
                                    .Select(z => ((z.RackRate - z.Price) / z.RackRate) * 100).FirstOrDefault()).FirstOrDefault() == null ? 0 : Math.Ceiling(Convert.ToDecimal(x.DealsNightModels.OrderBy(y => y.Value).Select(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).OrderBy(z => z.Price).Select(z => ((z.RackRate - z.Price) / z.RackRate) * 100).FirstOrDefault()).FirstOrDefault())),
                    Url = x.Url
                }).ToList();
                newQuery.Where(x => x.IsHotel).ToList().ForEach(x => x.Cities.InsertRange(0, this.hotelierInfoRepository.Table.Where(y => y.Id == x.HotelId).Select(y => y.Name.Length <= this.hotelMaxLength ? y.Name : y.Name.Substring(0, this.hotelMaxLength) + "...").ToList()));
                /////newQuery = newQuery.Where(x => x.MinPrice != 0);
                return newQuery.AsQueryable();
        }

        private List<PackageCurationViewModel> PackageCurationConversion(List<DealsPackageModel> model)
        {
            var newData = model.Select(x => new PackageCurationViewModel
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Ameneties = x.DealsHighlightModels != null ? x.DealsHighlightModels.OrderByDescending(y => y.SortOrder.HasValue).ThenBy(y => y.SortOrder).Select(y => new KeyValuePair<bool, string>(y.StarRating > 0 ? true : false, y.Title)).Take(3).ToList() : new List<KeyValuePair<bool, string>>(),
                Cities = this.packageCityRepository.Table.Where(y => x.DealsDestinationModels.Select(z => z.City).Contains(y.Id)).Select(y => y.Name).ToList(),
                DealName = x.Name,
                HotelId = x.Type == 1 ? Convert.ToInt32(x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.Select(di => di.VendorInfoId))).FirstOrDefault()) : 0,
                Image = x.DealContentModels != null ? x.DealContentModels.Select(y => y.CardImg).FirstOrDefault() : string.Empty,
                DealType = this.packageDealTypeRepository.Table.Where(y => (x.TravelCategory.Contains("," + y.Id.ToString()) || x.TravelCategory.Contains(y.Id.ToString() + ",") || x.TravelCategory.Contains(y.Id.ToString())) && y.IsActive).Select(y => y.Name).FirstOrDefault(),
                IsHotel = x.Type == 1 ? true : false,
                LatLong = new Tuple<decimal?, decimal?>(x.Lat, x.Long),
                MinNights = x.Type == 1 ?
                    x.DealsNightModels.Select(n => n.Value).FirstOrDefault()
                    :
                    x.DealsNightModels.OrderBy(y => y.Value).Select(y => y.Value).FirstOrDefault(),
                MinPrice = x.Type == 1 ? //// Hotel
                    x.DealsNightModels.OrderBy(y => y.Value)
                    .Select(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                    .Join(this.dealRoomConfigurationRespository.Table, drate => drate.RoomConfigId, droom => droom.Id, (drate, droom) => new { drate, droom }).Where(z => z.droom.IsActive).Select(z => z.drate)
                    .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                    .OrderBy(z => z.r.Price * Convert.ToDecimal(z.p.ExchangeRate))
                    .Select(z => z.r.MarkUp != null && z.r.MarkUp != 0 ?
                                ((((z.r.Price + (Convert.ToDecimal(z.r.MarkUp) / 100 * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                + //// Room Price + Extra Supplement
                                ((z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0) + ((Convert.ToDecimal(z.r.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                *
                                Convert.ToDecimal(z.p.ExchangeRate)
                            :
                            z.r.DealsNightModel.DealsPackageModel.MarkUp != null && z.r.DealsNightModel.DealsPackageModel.MarkUp != 0 ?
                                    ((((z.r.Price + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    ((z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0) + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                    *
                                    Convert.ToDecimal(z.p.ExchangeRate)
                                    :
                                    (((z.r.Price * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))
                                    * Convert.ToDecimal(z.p.ExchangeRate)).OrderBy(z => z).FirstOrDefault()).FirstOrDefault()
                    :
                    x.DealsNightModels.OrderBy(y => y.Value) //// Tour
                    .Select(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                    .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                    .OrderBy(z => z.r.Price * Convert.ToDecimal(z.p.ExchangeRate))
                    .Select(z => z.r.MarkUp != null && z.r.MarkUp != 0 ?
                        ((z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0) + ((Convert.ToDecimal(z.r.MarkUp) / 100) * (z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)))) * Convert.ToDecimal(z.p.ExchangeRate))
                        :
                         z.r.DealsNightModel.DealsPackageModel.MarkUp != null ?
                            ((z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0) + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * (z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)))) * Convert.ToDecimal(z.p.ExchangeRate))
                            :
                            ((z.r.Price + (z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)) * Convert.ToDecimal(z.p.ExchangeRate)))
                         .OrderBy(z => z).FirstOrDefault()).FirstOrDefault(),
                Discount = x.DealsNightModels
                    .OrderBy(y => y.Value)
                    .Select(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).OrderBy(z => z.Price).Select(z => ((z.RackRate - z.Price) / z.RackRate) * 100).FirstOrDefault()).FirstOrDefault() == null
                    ?
                    0
                    :
                    Math.Ceiling(Convert.ToDecimal(x.DealsNightModels.OrderBy(y => y.Value).Select(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).OrderBy(z => z.Price).Select(z => ((z.RackRate - z.Price) / z.RackRate) * 100).FirstOrDefault()).FirstOrDefault())),
                Url = x.Url
            }).ToList();
            newData.Where(x => x.IsHotel).ToList().ForEach(x => x.Cities.InsertRange(0, this.hotelierInfoRepository.Table.Where(y => y.Id == x.HotelId).Select(y => y.Name.Length <= this.hotelMaxLength ? y.Name : y.Name.Substring(0, this.hotelMaxLength) + "...").ToList()));
            ////newData = newData.Where(x => x.MinPrice != 0).ToList();
            ////newData = newData.Where(x => x.MinPrice != 0).ToList();
            return newData;
        }

        ////private DealsPackageModel GetHotelDealRatePlan(DealsPackageModel model)
        ////{
        ////    if (model.Type == 1)
        ////    {
        ////        try
        ////        {
        ////            for (int i = 0; i < model.DealsNightModels.Count; i++)
        ////            {
        ////                model.DealsNightModels[i].DealsRatePlanModel = new List<DealsRatePlanModel>();
        ////                List<DealsRatePlanModel> ratePlans = this.dealRatePlanRespository.Table.Where(r => model.DealsNightModels.Select(nm => nm.DealsItineraryModels.Where(ditn => ditn.IsActive).Select(ditn => ditn.DealsInclusionModels.Select(din => din.DealRoomConfigurationModels.Where(rc => rc.IsActive).Select(rc => rc.Id).Contains((int)r.RoomConfigId)).FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).ToList();
        ////                model.DealsNightModels[i].DealsRatePlanModel.AddRange(ratePlans);
        ////            }
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            var msg = ex.ToString();
        ////        }
        ////    }

        ////    return model;
        ////}

        private List<FilterViewModel> BindFilterViewModel(List<DealsPackageModel> result, IQueryable<DealsPackageModel> query)
        {
            List<FilterViewModel> filterViewModels = new List<FilterViewModel>();
            filterViewModels.Add(this.GetTypeFilter(result));
            filterViewModels.Add(this.GetPriceFilters(result));
            filterViewModels.Add(this.GetAccomodationFilter(result));
            ////filterViewModels.Add(this.GetStarCategoryFilter(result));
            filterViewModels.Add(this.GetCountryFilter(result));
            ////filterViewModels.Add(this.GetCityFilter(result));
            filterViewModels.Add(this.GetTravelStylesFilter(result));
            ////filterViewModels.Add(this.GetReviewsFilter(result));
            filterViewModels.Add(this.GetHotelAmenetiesFilter(result));
            //// filterViewModels.Add(this.GetRoomAmenetiesFilter(result));
            ////filterViewModels.Add(this.GetHotelChainFilter(result));
            filterViewModels.Add(this.GetVisaFilter(result));
            ////filterViewModels.Add(this.GetFlightFilter(result));
            filterViewModels = filterViewModels.Where(x => x.FilterOptions.Count > 0).ToList();
            return filterViewModels;
        }

        private List<FilterViewModel> ReBindFilterViewModel(List<FilterViewModel> filtersViewModels, List<DealsPackageModel> result, IQueryable<DealsPackageModel> query)
        {
            for (int i = 0; i < filtersViewModels.Count; i++)
            {
                if (filtersViewModels[i].Type == (int)FilterTypes.Budget)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetPriceFilters(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.Type)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetTypeFilter(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.Accomodation)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetAccomodationFilter(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.TravelStyle)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetTravelStylesFilter(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.HotelAmeneties)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetHotelAmenetiesFilter(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.Visa)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetVisaFilter(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.Flight)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetFlightFilter(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.Country)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetCountryFilter(result);
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.City)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetCityFilter(result, filtersViewModels.Where(x => x.Type == (int)FilterTypes.Country).SelectMany(x => x.FilterOptions.Where(y => y.IsSelected).Select(y => y.Value)).FirstOrDefault());
                    }
                }

                if (filtersViewModels[i].Type == (int)FilterTypes.CityArea)
                {
                    if (filtersViewModels[i].FilterOptions.Any(x => x.IsSelected))
                    {
                        filtersViewModels[i].FilterOptions.RemoveAll(x => !x.IsSelected);
                    }
                    else
                    {
                        filtersViewModels[i] = this.GetCityAreaFilter(result, filtersViewModels.Where(x => x.Type == (int)FilterTypes.City).SelectMany(x => x.FilterOptions.Where(y => y.IsSelected).Select(y => y.Value)).FirstOrDefault()); ////  Modify
                    }
                }
            }

            return filtersViewModels;
        }

        private List<FilterViewModel> BindNestedFilters(List<FilterViewModel> filtersViewModels, List<DealsPackageModel> result)
        {
            if (filtersViewModels.Any(x => x.Type == (int)FilterTypes.Country && x.FilterOptions.Any(y => y.IsSelected)))
            {
                if (filtersViewModels.All(x => x.Type != (int)FilterTypes.City))
                {
                    filtersViewModels.Add(this.GetCityFilter(result, filtersViewModels.Where(x => x.Type == (int)FilterTypes.Country).SelectMany(x => x.FilterOptions.Where(y => y.IsSelected).Select(y => y.Value)).FirstOrDefault()));
                }
                else if (filtersViewModels.Any(x => x.Type == (int)FilterTypes.City && x.FilterOptions.Any(y => y.IsSelected)))
                {
                    if (filtersViewModels.All(x => x.Type == (int)FilterTypes.CityArea))
                    {
                        filtersViewModels.Add(this.GetCityAreaFilter(result, filtersViewModels.Where(x => x.Type == (int)FilterTypes.City).SelectMany(x => x.FilterOptions.Where(y => y.IsSelected).Select(y => y.Value)).FirstOrDefault())); //// Modify To Area
                    }
                }
                else
                {
                    filtersViewModels.RemoveAll(x => x.Type == (int)FilterTypes.CityArea);
                }
            }
            else
            {
                filtersViewModels.RemoveAll(x => x.Type == (int)FilterTypes.City || x.Type == (int)FilterTypes.CityArea);
            }

            return filtersViewModels;
        }

        private FilterViewModel GetCityAreaFilter(List<DealsPackageModel> result, int cityId)
        {
            FilterViewModel areaFilter = new FilterViewModel();
            areaFilter.Name = "AREA";
            areaFilter.FilterId = Guid.NewGuid();
            areaFilter.Type = (int)FilterTypes.CityArea;
            areaFilter.SortOrder = (int)FilterTypes.CityArea;
            areaFilter.SelectType = (int)FilterSelectTypes.SingleSelect;
            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            List<int> areas = result.SelectMany(x => x.DealsDestinationModels).Where(y => y.City == cityId && y.Area != 0).Select(y => Convert.ToInt32(y.Area)).Distinct().ToList();
            foreach (var areaItem in areas)
            {
                if (filterOptionViewModels.Select(x => x.Value).ToList().Contains(areaItem))
                {
                    int indexmodelEdit = filterOptionViewModels.FindIndex(x => x.Value == areaItem);
                    filterOptionViewModels[indexmodelEdit].ResultCount = filterOptionViewModels[indexmodelEdit].ResultCount + 1;
                }
                else
                {
                    filterOptionViewModels.Add(new FilterOptionViewModel
                    {
                        FilterOptionId = Guid.NewGuid(),
                        Display = this.packageAreaRepository.Table.Where(x => x.Id == areaItem).Select(x => x.Name).FirstOrDefault(),
                        IsRange = false,
                        IsSelected = false,
                        ResultCount = result.Where(x => x.DealsDestinationModels.Where(y => y.Area == areaItem).Count() > 0).Count(),
                        DealIds = string.Join(",", result.Where(x => x.DealsDestinationModels.Any(y => y.Area == areaItem)).Select(x => x.Id).ToList()),
                        Value = areaItem
                    });
                }
            }

            areaFilter.FilterOptions = filterOptionViewModels;
            return areaFilter;
        }

        private FilterViewModel GetTravelStylesFilter(List<DealsPackageModel> result)
        {
            FilterViewModel travelStyleFilter = new FilterViewModel();
            travelStyleFilter.Name = "TRAVEL STYLES";
            travelStyleFilter.FilterId = Guid.NewGuid();
            travelStyleFilter.Type = (int)FilterTypes.TravelStyle;
            travelStyleFilter.SortOrder = (int)FilterTypes.TravelStyle;
            travelStyleFilter.SelectType = (int)FilterSelectTypes.SingleSelect;
            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            List<PackageTravelStyleModel> travelStyles = this.travelStyleRepository.Table.Where(x => x.IsActive).ToList();
            foreach (var packageItem in result)
            {
                string packageTravelStyle = packageItem.TravelStyle;
                foreach (var travelStyleItem in travelStyles)
                {
                    if (packageTravelStyle.Contains("," + travelStyleItem.Id.ToString()) || packageTravelStyle.Contains(travelStyleItem.Id.ToString() + ",") || packageTravelStyle == travelStyleItem.Id.ToString())
                    {
                        if (filterOptionViewModels.Select(x => x.Value).ToList().Contains(travelStyleItem.Id))
                        {
                            int indexmodelEdit = filterOptionViewModels.FindIndex(x => x.Value == travelStyleItem.Id);
                            filterOptionViewModels[indexmodelEdit].ResultCount = filterOptionViewModels[indexmodelEdit].ResultCount + 1;
                            filterOptionViewModels[indexmodelEdit].DealIds = string.Join(",", new string[] { filterOptionViewModels[indexmodelEdit].DealIds, packageItem.Id.ToString() });
                        }
                        else
                        {
                            filterOptionViewModels.Add(new FilterOptionViewModel
                            {
                                FilterOptionId = Guid.NewGuid(),
                                Display = travelStyleItem.Name,
                                IsRange = false,
                                IsSelected = false,
                                ResultCount = 1,
                                DealIds = packageItem.Id.ToString(),
                                Value = travelStyleItem.Id
                            });
                        }
                    }
                }
            }

            travelStyleFilter.FilterOptions = filterOptionViewModels;
            return travelStyleFilter;
        }

        private FilterViewModel GetTypeFilter(List<DealsPackageModel> result)
        {
            FilterViewModel typeFilter = new FilterViewModel();
            typeFilter.FilterId = Guid.NewGuid();
            typeFilter.Name = "TYPE";
            typeFilter.Type = (int)FilterTypes.Type;
            typeFilter.SortOrder = (int)FilterTypes.Type;
            typeFilter.SelectType = (int)FilterSelectTypes.SingleSelect;
            if (result.Any(x => x.Type == 1))
            {
                typeFilter.FilterOptions.Add(new FilterOptionViewModel
                {
                    FilterOptionId = Guid.NewGuid(),
                    Display = "HOTEL",
                    IsRange = false,
                    IsSelected = false,
                    ResultCount = result.Count(x => x.Type == 1),
                    DealIds = string.Join(",", result.Where(x => x.Type == 1).Select(x => x.Id).ToList()),
                    Value = 1
                });
            }

            if (result.Any(x => x.Type == 2))
            {
                typeFilter.FilterOptions.Add(new FilterOptionViewModel
                {
                    FilterOptionId = Guid.NewGuid(),
                    Display = "TOUR",
                    IsRange = false,
                    IsSelected = false,
                    ResultCount = result.Count(x => x.Type == 2),
                    DealIds = string.Join(",", result.Where(x => x.Type == 2).Select(x => x.Id).ToList()),
                    Value = 2
                });
            }

            return typeFilter;
        }

        private FilterViewModel GetAccomodationFilter(List<DealsPackageModel> result)
        {
            FilterViewModel accomodateFilter = new FilterViewModel();
            accomodateFilter.FilterId = Guid.NewGuid();
            accomodateFilter.Name = "ACCOMMODATION";
            accomodateFilter.Type = (int)FilterTypes.Accomodation;
            accomodateFilter.SortOrder = (int)FilterTypes.Accomodation;
            accomodateFilter.SelectType = (int)FilterSelectTypes.SingleSelect;
            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            foreach (var item in result)
            {
                List<int?> hotelierIds = item.Type == 1 ? item.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(i => i.VendorInfoId).FirstOrDefault()).FirstOrDefault()).ToList() : item.DealsNightModels.SelectMany(x => x.DealsItineraryModels.SelectMany(y => y.DealsInclusionModels.Where(z => z.VendorInfoId != null).Select(z => z.VendorInfoId).Distinct().ToList())).Distinct().ToList();
                foreach (var hotel in hotelierIds)
                {
                    int propertyType = this.hotelierInfoRepository.Table.Where(x => x.Id == Convert.ToInt32(hotel)).Select(x => x.PropertyType).FirstOrDefault();
                    if (filterOptionViewModels.Select(x => x.Value).ToList().Contains(propertyType))
                    {
                        int indexmodelEdit = filterOptionViewModels.FindIndex(x => x.Value == propertyType);
                        filterOptionViewModels[indexmodelEdit].ResultCount = filterOptionViewModels[indexmodelEdit].ResultCount + 1;
                        filterOptionViewModels[indexmodelEdit].DealIds = string.Join(",", new string[] { filterOptionViewModels[indexmodelEdit].DealIds, item.Id.ToString() });
                    }
                    else
                    {
                        filterOptionViewModels.Add(new FilterOptionViewModel
                        {
                            FilterOptionId = Guid.NewGuid(),
                            Display = this.hotelierPropertyTypeRepository.Table.Where(x => x.Id == propertyType).Select(x => x.Name).FirstOrDefault(),
                            IsRange = false,
                            IsSelected = false,
                            ResultCount = 1,
                            DealIds = item.Id.ToString(),
                            Value = propertyType
                        });
                    }
                }
            }

            accomodateFilter.FilterOptions = filterOptionViewModels;
            return accomodateFilter;
        }

        /*Temperory Discarded
        private FilterViewModel GetHotelChainFilter(List<DealsPackageModel> result)
        {
            FilterViewModel hotelChainFilter = new FilterViewModel();
            hotelChainFilter.Name = "HOTEL CHAINS";
            hotelChainFilter.Type = (int)FilterTypes.HotelChain;
            hotelChainFilter.SortOrder = (int)FilterTypes.HotelChain;
            hotelChainFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            foreach (var resultItem in result)
            {
                List<int?> hotelierIds = resultItem.Type == 1 ? resultItem.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(i => i.VendorInfoId).FirstOrDefault()).FirstOrDefault()).ToList() : resultItem.DealsNightModels.SelectMany(x => x.DealsItineraryModels.SelectMany(y => y.DealsInclusionModels.Where(z => z.VendorInfoId != null).Select(z => z.VendorInfoId).Distinct().ToList())).Distinct().ToList();
                foreach (var hotel in hotelierIds)
                {
                    int? vendorGorupId = this.hotelierInfoRepository.Table.Where(x => x.Id == hotel && x.VendorInformationModel.VendorGroupModel != null).Select(x => x.VendorInformationModel.VendorGroupModel.Id).FirstOrDefault();
                    if (vendorGorupId != null && vendorGorupId != 0)
                    {
                        if (filterOptionViewModels.Select(x => x.Value).ToList().Contains(Convert.ToInt32(vendorGorupId)))
                        {
                            int indexmodelEdit = filterOptionViewModels.FindIndex(x => x.Value == vendorGorupId);
                            filterOptionViewModels[indexmodelEdit].ResultCount = filterOptionViewModels[indexmodelEdit].ResultCount + 1;
                            filterOptionViewModels[indexmodelEdit].DealIds = string.Join(",", new string[] { filterOptionViewModels[indexmodelEdit].DealIds, resultItem.Id.ToString() });
                        }
                        else
                        {
                            filterOptionViewModels.Add(new FilterOptionViewModel
                            {
                                Display = this.vendorGroupRespository.Table.Where(x => x.Id == Convert.ToInt32(vendorGorupId)).Select(x => x.Name).FirstOrDefault(),
                                IsRange = false,
                                IsSelected = false,
                                ResultCount = 1,
                                DealIds = resultItem.Id.ToString(),
                                Value = Convert.ToInt32(vendorGorupId)
                            });
                        }
                    }
                }
            }

            hotelChainFilter.FilterOptions = filterOptionViewModels;
            return hotelChainFilter;
        }
        */
        private FilterViewModel GetFlightFilter(List<DealsPackageModel> result)
        {
            FilterViewModel flightFilter = new FilterViewModel();
            flightFilter.Name = "FLIGHTS";
            flightFilter.FilterId = Guid.NewGuid();
            flightFilter.Type = (int)FilterTypes.Flight;
            flightFilter.SortOrder = (int)FilterTypes.Flight;
            flightFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            flightFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "FREE",
                IsRange = false,
                IsSelected = false,
                ResultCount = 0,
                Value = 1
            });

            return flightFilter;
        }

        private FilterViewModel GetVisaFilter(List<DealsPackageModel> result)
        {
            FilterViewModel visaFilter = new FilterViewModel();
            visaFilter.Name = "VISA";
            visaFilter.FilterId = Guid.NewGuid();
            visaFilter.Type = (int)FilterTypes.Visa;
            visaFilter.SortOrder = (int)FilterTypes.Visa;
            visaFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            List<DealsPackageModel> internationalDeals = result.Where(x => x.DealsDestinationModels.Any(y => y.Country != this.localCountry)).ToList();
            List<int> freeVisaDeal = internationalDeals.Where(x => x.DealsVisaModels == null || x.DealsVisaModels.Count == 0).Select(x => x.Id).ToList();
            if (freeVisaDeal.Count > 0)
            {
                visaFilter.FilterOptions.Add(new FilterOptionViewModel
                {
                    FilterOptionId = Guid.NewGuid(),
                    Display = "FREE",
                    IsRange = false,
                    IsSelected = false,
                    ResultCount = freeVisaDeal.Count,
                    DealIds = string.Join(',', freeVisaDeal),
                    Value = 1
                });
            }

            return visaFilter;
        }

        /*Discarded Function
        private FilterViewModel GetRoomAmenetiesFilter(List<DealsPackageModel> result)
        {
            FilterViewModel roomAmenetiesFilter = new FilterViewModel();
            roomAmenetiesFilter.Name = "ROOM AMENETIES";
            roomAmenetiesFilter.Type = (int)FilterTypes.RoomAmeneties;
            roomAmenetiesFilter.SortOrder = (int)FilterTypes.RoomAmeneties;
            roomAmenetiesFilter.SelectType = (int)FilterSelectTypes.MultiSelect;

            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            var amenities = this.amenitiesRepository.Table.Where(x => x.IsRoomOnly).ToList();
            foreach (var item in amenities)
            {
                foreach (var resultItem in result)
                {
                    List<int?> hotelierIds = resultItem.Type == 1 ? resultItem.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(i => i.VendorInfoId).FirstOrDefault()).FirstOrDefault()).ToList() : resultItem.DealsNightModels.SelectMany(x => x.DealsItineraryModels.SelectMany(y => y.DealsInclusionModels.Where(z => z.VendorInfoId != null).Select(z => z.VendorInfoId).Distinct().ToList())).Distinct().ToList();
                    foreach (var hotel in hotelierIds)
                    {
                        List<int> roomConfigs = this.hotelierInfoRepository.Table.Where(x => x.Id == Convert.ToInt32(hotel)).SelectMany(x => x.HotelierRoomConfigModels.Where(y => y.IsActive).Select(y => y.Id)).ToList();
                        foreach (var roomItem in roomConfigs)
                        {
                            if (this.hotelierRoomAmenitieRespository.Table.Where(x => x.RoomConfigId == Convert.ToInt32(roomItem)).Select(x => x.AmenetieId).ToList().Contains(item.Id))
                            {
                                if (filterOptionViewModels.Select(x => x.Value).ToList().Contains(item.Id))
                                {
                                    int indexmodelEdit = filterOptionViewModels.FindIndex(x => x.Value == item.Id);
                                    filterOptionViewModels[indexmodelEdit].ResultCount = filterOptionViewModels[indexmodelEdit].ResultCount + 1;
                                    filterOptionViewModels[indexmodelEdit].DealIds = string.Join(",", new string[] { filterOptionViewModels[indexmodelEdit].DealIds, resultItem.Id.ToString() });
                                }
                                else
                                {
                                    filterOptionViewModels.Add(new FilterOptionViewModel
                                    {
                                        Display = item.Name,
                                        IsRange = false,
                                        IsSelected = false,
                                        ResultCount = 1,
                                        DealIds = resultItem.Id.ToString(),
                                        Value = item.Id
                                    });
                                }
                            }
                        }
                    }
                }
            }

            roomAmenetiesFilter.FilterOptions = filterOptionViewModels;
            return roomAmenetiesFilter;
        }
        */
        private FilterViewModel GetHotelAmenetiesFilter(List<DealsPackageModel> result)
        {
            FilterViewModel hotelAmenetiesFilter = new FilterViewModel();
            hotelAmenetiesFilter.Name = "HOTEL AMENETIES";
            hotelAmenetiesFilter.FilterId = Guid.NewGuid();
            hotelAmenetiesFilter.Type = (int)FilterTypes.HotelAmeneties;
            hotelAmenetiesFilter.SortOrder = (int)FilterTypes.HotelAmeneties;
            hotelAmenetiesFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            var amenities = this.amenitiesRepository.Table.Where(x => x.IsActive && x.IsHotelOnly && x.IsFilter).ToList();
            foreach (var item in amenities)
            {
                foreach (var resultItem in result)
                {
                    List<int?> hotelierIds = resultItem.Type == 1 ? resultItem.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(i => i.VendorInfoId).FirstOrDefault()).FirstOrDefault()).ToList() : resultItem.DealsNightModels.SelectMany(x => x.DealsItineraryModels.SelectMany(y => y.DealsInclusionModels.Where(z => z.VendorInfoId != null).Select(z => z.VendorInfoId).Distinct().ToList())).Distinct().ToList();
                    foreach (var hotel in hotelierIds)
                    {
                        if (this.hotelierAmenitieRespository.Table.Where(x => x.HotelId == Convert.ToInt32(hotel)).Select(x => x.AmentieId).ToList().Contains(item.Id))
                        {
                            if (filterOptionViewModels.Select(x => x.Value).ToList().Contains(item.Id))
                            {
                                int indexmodelEdit = filterOptionViewModels.FindIndex(x => x.Value == item.Id);
                                filterOptionViewModels[indexmodelEdit].ResultCount = filterOptionViewModels[indexmodelEdit].ResultCount + 1;
                                filterOptionViewModels[indexmodelEdit].DealIds = string.Join(",", new string[] { filterOptionViewModels[indexmodelEdit].DealIds, resultItem.Id.ToString() });
                            }
                            else
                            {
                                filterOptionViewModels.Add(new FilterOptionViewModel
                                {
                                    FilterOptionId = Guid.NewGuid(),
                                    Display = item.Name,
                                    IsRange = false,
                                    IsSelected = false,
                                    ResultCount = 1,
                                    DealIds = resultItem.Id.ToString(),
                                    Value = item.Id
                                });
                            }
                        }
                    }
                }
            }

            hotelAmenetiesFilter.FilterOptions = filterOptionViewModels;
            return hotelAmenetiesFilter;
        }

        /*Discarded Function
        private FilterViewModel GetReviewsFilter(List<DealsPackageModel> result)
        {
            FilterViewModel reviewFilter = new FilterViewModel();
            reviewFilter.Name = "REVIEW RATING";
            reviewFilter.Type = (int)FilterTypes.Reviews;
            reviewFilter.SortOrder = (int)FilterTypes.Reviews;
            reviewFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            reviewFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                Display = "5 - 4",
                IsRange = true,
                IsSelected = false,
                ResultCount = result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 4 && y.Rating < 5).Count() > 0).Count(),
                DealIds = string.Join(",", result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 4 && y.Rating < 5).Count() > 0).Select(x => x.Id).ToList()),
                MaxValue = 5,
                MinValue = 4
            });
            reviewFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                Display = "4 - 3",
                IsRange = true,
                IsSelected = false,
                DealIds = string.Join(",", result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 3 && y.Rating < 4).Count() > 0).Select(x => x.Id).ToList()),
                ResultCount = result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 3 && y.Rating < 4).Count() > 0).Count(),
                MaxValue = 4,
                MinValue = 3
            });
            reviewFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                Display = "3 - 2",
                IsRange = true,
                IsSelected = false,
                DealIds = string.Join(",", result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 2 && y.Rating < 3).Count() > 0).Select(x => x.Id).ToList()),
                ResultCount = result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 2 && y.Rating < 3).Count() > 0).Count(),
                MaxValue = 3,
                MinValue = 2
            });
            reviewFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                Display = "2 - 1",
                IsRange = true,
                IsSelected = false,
                DealIds = string.Join(",", result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 1 && y.Rating < 2).Count() > 0).Select(x => x.Id).ToList()),
                ResultCount = result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 1 && y.Rating < 2).Count() > 0).Count(),
                MaxValue = 2,
                MinValue = 1
            });
            reviewFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                Display = "1 - 0",
                IsRange = true,
                IsSelected = false,
                DealIds = string.Join(",", result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 0 && y.Rating < 1).Count() > 0).Select(x => x.Id).ToList()),
                ResultCount = result.Where(x => x.DealsReviewModels.Where(y => y.Rating >= 0 && y.Rating < 1).Count() > 0).Count(),
                MaxValue = 1,
                MinValue = 0
            });
            reviewFilter.FilterOptions = reviewFilter.FilterOptions.Where(x => x.ResultCount != 0).ToList();
            return reviewFilter;
        }
        */
        private FilterViewModel GetCityFilter(List<DealsPackageModel> result, int countryId)
        {
            FilterViewModel cityFilter = new FilterViewModel();
            cityFilter.Name = "CITY";
            cityFilter.FilterId = Guid.NewGuid();
            cityFilter.Type = (int)FilterTypes.City;
            cityFilter.SortOrder = (int)FilterTypes.City;
            cityFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            List<int> cities = result.SelectMany(x => x.DealsDestinationModels).Where(y => y.Country == countryId).Select(y => Convert.ToInt32(y.City)).Distinct().ToList();
            foreach (var cityItem in cities)
            {
                if (filterOptionViewModels.Select(x => x.Value).ToList().Contains(cityItem))
                {
                    int indexmodelEdit = filterOptionViewModels.FindIndex(x => x.Value == cityItem);
                    filterOptionViewModels[indexmodelEdit].ResultCount = filterOptionViewModels[indexmodelEdit].ResultCount + 1;
                }
                else
                {
                    filterOptionViewModels.Add(new FilterOptionViewModel
                    {
                        FilterOptionId = Guid.NewGuid(),
                        Display = this.packageCityRepository.Table.Where(x => x.Id == cityItem).Select(x => x.Name).FirstOrDefault(),
                        IsRange = false,
                        IsSelected = false,
                        ResultCount = result.Where(x => x.DealsDestinationModels.Where(y => y.City == cityItem).Count() > 0).Count(),
                        DealIds = string.Join(",", result.Where(x => x.DealsDestinationModels.Where(y => y.City == cityItem).Count() > 0).Select(x => x.Id).ToList()),
                        Value = cityItem
                    });
                }
            }

            cityFilter.FilterOptions = filterOptionViewModels;
            return cityFilter;
        }

        private FilterViewModel GetCountryFilter(List<DealsPackageModel> result)
        {
            FilterViewModel countryFilter = new FilterViewModel();
            countryFilter.Name = "COUNTRY";
            countryFilter.FilterId = Guid.NewGuid();
            countryFilter.Type = (int)FilterTypes.Country;
            countryFilter.SortOrder = (int)FilterTypes.Country;
            countryFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            List<FilterOptionViewModel> filterOptionViewModels = new List<FilterOptionViewModel>();
            List<int> countries = result.SelectMany(x => x.DealsDestinationModels).Select(y => Convert.ToInt32(y.Country)).Distinct().ToList();
            foreach (var countryItem in countries)
            {
                filterOptionViewModels.Add(new FilterOptionViewModel
                {
                    FilterOptionId = Guid.NewGuid(),
                    Display = this.packageCountryRepository.Table.Where(x => x.Id == Convert.ToInt16(countryItem)).Select(x => x.Name).FirstOrDefault(),
                    IsRange = false,
                    IsSelected = false,
                    ResultCount = result.Where(x => x.DealsDestinationModels.Where(y => y.Country == Convert.ToInt16(countryItem)).Count() > 0).Count(),
                    DealIds = string.Join(",", result.Where(x => x.DealsDestinationModels.Where(y => y.Country == countryItem).Count() > 0).Select(x => x.Id).ToList()),
                    Value = countryItem
                });
            }

            countryFilter.FilterOptions = filterOptionViewModels;
            return countryFilter;
        }

        /* Discarded Function
        private FilterViewModel GetStarCategoryFilter(List<DealsPackageModel> result)
        {
            FilterViewModel categoryFilter = new FilterViewModel();
            categoryFilter.Name = "STAR CATEGORY";
            categoryFilter.Type = (int)FilterTypes.StarCategory;
            categoryFilter.SortOrder = (int)FilterTypes.StarCategory;
            categoryFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            for (int i = 5; i >= 1; i--)
            {
                categoryFilter.FilterOptions.Add(new FilterOptionViewModel
                {
                    Display = i.ToString() + " Star",
                    IsRange = false,
                    Value = i,
                    IsSelected = false,
                    ResultCount = 0,
                    DealIds = string.Empty
                });
            }

            foreach (var resultItem in result)
            {
                List<int?> hotelierIds = resultItem.Type == 1 ? resultItem.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(i => i.VendorInfoId).FirstOrDefault()).FirstOrDefault()).ToList() : resultItem.DealsNightModels.SelectMany(x => x.DealsItineraryModels.SelectMany(y => y.DealsInclusionModels.Where(z => z.VendorInfoId != null).Select(z => z.VendorInfoId).Distinct().ToList())).Distinct().ToList();
                foreach (var hotel in hotelierIds)
                {
                    int starRating = this.hotelierInfoRepository.Table.Where(x => x.Id == Convert.ToInt32(hotel)).Select(x => x.StarRating).FirstOrDefault();
                    if (categoryFilter.FilterOptions.Select(x => x.Value).ToList().Contains(starRating))
                    {
                        int indexmodelEdit = categoryFilter.FilterOptions.FindIndex(x => x.Value == starRating);
                        categoryFilter.FilterOptions[indexmodelEdit].ResultCount = categoryFilter.FilterOptions[indexmodelEdit].ResultCount + 1;
                        categoryFilter.FilterOptions[indexmodelEdit].DealIds = categoryFilter.FilterOptions[indexmodelEdit].DealIds == string.Empty ? resultItem.Id.ToString() : string.Join(",", new string[] { categoryFilter.FilterOptions[indexmodelEdit].DealIds, resultItem.Id.ToString() });
                    }
                }
            }

            categoryFilter.FilterOptions = categoryFilter.FilterOptions.Where(x => x.ResultCount != 0).ToList();
            return categoryFilter;
        }
        */

        private FilterViewModel GetPriceFilters(List<DealsPackageModel> result)
        {
            var resultConverted = this.PackageCurationConversion(result);
            FilterViewModel priceFilter = new FilterViewModel();
            priceFilter.FilterId = Guid.NewGuid();
            priceFilter.Name = "BUDGET";
            priceFilter.Type = (int)FilterTypes.Budget;
            priceFilter.SortOrder = (int)FilterTypes.Budget;
            priceFilter.SelectType = (int)FilterSelectTypes.MultiSelect;
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹0 – ₹6,999",
                IsRange = true,
                MaxValue = 6999,
                MinValue = 0,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 0 && x.MinPrice <= 6999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 0 && x.MinPrice <= 6999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹7,000 – ₹13,999",
                IsRange = true,
                MaxValue = 13999,
                MinValue = 7000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 7000 && x.MinPrice <= 13999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 7000 && x.MinPrice <= 13999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹14,000 – ₹22,999",
                IsRange = true,
                MaxValue = 22999,
                MinValue = 14000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 14000 && x.MinPrice <= 22999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 14000 && x.MinPrice <= 22999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹23,000 - ₹29,999",
                IsRange = true,
                MaxValue = 29999,
                MinValue = 23000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 23000 && x.MinPrice <= 29999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 23000 && x.MinPrice <= 29999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹30,000 - ₹39,999",
                IsRange = true,
                MaxValue = 39999,
                MinValue = 30000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 30000 && x.MinPrice <= 39999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 30000 && x.MinPrice <= 39999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹40,000 - ₹49,999",
                IsRange = true,
                MaxValue = 49999,
                MinValue = 40000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 40000 && x.MinPrice <= 49999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 40000 && x.MinPrice <= 49999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹50,000 - ₹59,999",
                IsRange = true,
                MaxValue = 59999,
                MinValue = 50000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 50000 && x.MinPrice <= 59999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 50000 && x.MinPrice <= 59999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹60,000 - ₹69,999",
                IsRange = true,
                MaxValue = 69999,
                MinValue = 60000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 60000 && x.MinPrice <= 69999).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 60000 && x.MinPrice <= 69999).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions.Add(new FilterOptionViewModel
            {
                FilterOptionId = Guid.NewGuid(),
                Display = "₹70,000 and Above",
                IsRange = true,
                MaxValue = decimal.MaxValue,
                MinValue = 70000,
                IsSelected = false,
                DealIds = string.Join(",", resultConverted.Where(x => x.MinPrice >= 70000 && x.MinPrice <= decimal.MaxValue).Select(x => x.Id)),
                ResultCount = resultConverted.Where(x => x.MinPrice >= 70000 && x.MinPrice <= decimal.MaxValue).Select(x => x.Id).Count()
            });
            priceFilter.FilterOptions = priceFilter.FilterOptions.Where(x => x.ResultCount != 0).ToList();
            return priceFilter;
        }
    }
}