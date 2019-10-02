// <copyright file="SearchController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Api.Common;
    using Api.Common.Caching;
    using Api.Configuration.Business;
    using AutoMapper;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Models;
    using Services;
    using ViewModels;

    /// <summary>
    /// Home Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class SearchController : BaseController
    {
        private const int SearchExpiryInSeconds = 9000;
        private readonly IListingService listingService;
        private readonly ITableCacheHandler tableCacheHandler;
        private readonly IHomePageService homePageService;
        private readonly IHomePageBusiness homePageBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController" /> class.
        /// </summary>
        /// <param name="homePageBusiness">Home Page Business</param>
        /// <param name="tableCacheHandler">Table Cache Handler</param>
        /// <param name="stateService">State Service</param>
        /// <param name="configuration">Web Config</param>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="countryService">The Country Service.</param>
        /// <param name="cityService">The City Service.</param>
        /// <param name="listingService">Listing Service</param>
        /// <param name="mapper">The mapper.</param>
        public SearchController(
            IHomePageBusiness homePageBusiness,
            ITableCacheHandler tableCacheHandler,
            IStateService stateService,
            IConfiguration configuration,
            IHomePageService homePageService,
            ICountryService countryService,
            ICityService cityService,
            IListingService listingService,
            IMapper mapper)
            : base(mapper, homePageService, cityService, countryService, configuration, stateService)
        {
            this.homePageBusiness = homePageBusiness;
            this.homePageService = homePageService;
            this.tableCacheHandler = tableCacheHandler;
            this.listingService = listingService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>view for home</returns>
        [HttpPost]
        [Route("SearchFilter")]
        public IActionResult SearchFilter(ListingViewModel model)
        {
            var result = this.listingService.GetSearchReFilterResults(model);
            this.ViewBag.FilterActivated = true;
            result.FiltersViewModels = result.FiltersViewModels.OrderBy(x => x.SortOrder).ToList();
            if (!string.IsNullOrEmpty(model.SearchTermViewModel.StartDate))
            {
                this.TempData["StartDate"] = DateTime.ParseExact(model.SearchTermViewModel.StartDate, "dd/MM/yyyy", null);
            }
            else
            {
                this.TempData["StartDate"] = null;
            }

            if (!string.IsNullOrEmpty(model.SearchTermViewModel.EndDate))
            {
                this.TempData["EndDate"] = DateTime.ParseExact(model.SearchTermViewModel.EndDate, "dd/MM/yyyy", null);
            }
            else
            {
                this.TempData["EndDate"] = null;
            }

            this.ModelState.Clear();
            return this.View("Search", result);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="dealIds">Deal Ids</param>
        /// <param name="filterType">Filter Types</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <returns>view for home</returns>
        [Route("search/FilterSearchResult")]
        public IActionResult FilterSearchResult(string dealIds, int filterType, double? latitude, double? longitude)
        {
            var result = this.listingService.GetFilterSearchResults(dealIds.Split(',').Select(x => Convert.ToInt32(x)).ToList());

            switch (filterType)
            {
                case (int)Enums.SortFilterType.PriceLowToHight:
                    result = result.OrderBy(x => x.MinPrice).ToList();
                    break;
                case (int)Enums.SortFilterType.PriceHighToLow:
                    result = result.OrderByDescending(x => x.MinPrice).ToList();
                    break;
                case (int)Enums.SortFilterType.DiscountHighToLow:
                    result = result.OrderByDescending(x => x.Discount).ToList();
                    break;
                case (int)Enums.SortFilterType.DiscountLowToHigh:
                    result = result.OrderBy(x => x.Discount).ToList();
                    break;
                case (int)Enums.SortFilterType.PopularityHighToLow:
                    result = result.OrderByDescending(x => x.ViewCount).ToList();
                    break;
                case (int)Enums.SortFilterType.WhatsNew:
                    result = result.OrderByDescending(x => x.CreatedDate).ToList();
                    break;
                case (int)Enums.SortFilterType.NearMe:
                    result = result.OrderBy(x =>
                    {
                        if (x.LatLong.Item1.HasValue && x.LatLong.Item2.HasValue && latitude.HasValue && longitude.HasValue)
                        {
                            double latitudeDeal = Convert.ToDouble(x.LatLong.Item1.Value);
                            double longitudeDeal = Convert.ToDouble(x.LatLong.Item2.Value);
                            if ((latitudeDeal == latitude.Value) && (longitudeDeal == longitude.Value))
                            {
                                return 0;
                            }
                            else
                            {
                                double theta = latitude.Value - latitudeDeal;
                                double dist = ((Math.Sin(this.Deg2rad(latitude.Value)) * Math.Sin(this.Deg2rad(latitudeDeal))) + (Math.Cos(this.Deg2rad(latitude.Value)) * Math.Cos(this.Deg2rad(latitudeDeal)))) * Math.Cos(this.Deg2rad(theta));
                                dist = Math.Acos(dist);
                                dist = this.Rad2deg(dist);
                                dist = dist * 60 * 1.1515;
                                return dist;
                            }
                        }
                        else
                        {
                            return int.MaxValue;
                        }
                    }).ToList();
                    break;
                default:
                    break;
            }

            return this.PartialView("_SearchListing", result);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="showSearchTerm">Show Search Term</param>
        /// <param name="searchType">Search Type</param>
        /// <param name="value">Value</param>
        /// <param name="searchTerm">Search Term</param>
        /// <param name="subSearchTerm">Sub Search Term</param>
        /// <param name="adults">Adults</param>
        /// <param name="kids">Kids</param>
        /// <param name="infants">Infants</param>
        /// <param name="rooms">Rooms</param>
        /// <param name="kidsage">Kids Age</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of search result.</returns>
        /// <returns>view for home</returns>
        [Route("search/{subSearchTerm:required:regex(^[[a-zA-Z -]]*$)}/{searchTerm:required:regex(^[[a-zA-Z -]]*$)}/{adults:int?}/{kids:int?}/{infants:int?}/{rooms:int?}/{startDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{endDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{kidsage?}")]
        public async Task<IActionResult> SearchTwoLevel(
            bool showSearchTerm,
            int searchType,
            int value,
            string searchTerm,
            string subSearchTerm,
            int adults = 1,
            int kids = 0,
            int infants = 0,
            int rooms = 1,
            string kidsage = null,
            string startDate = null,
            string endDate = null)
        {
            Tuple<int, int, bool> valueSearchType =
                await this.listingService.GetValueListTypeOfSearchCityStateAsync(searchTerm);
            this.ViewBag.startDate = startDate;
            this.ViewBag.endDate = endDate;
            this.ViewBag.adults = adults;
            this.ViewBag.kids = kids;
            this.ViewBag.infants = infants;
            this.ViewBag.rooms = rooms;
            this.ViewBag.searchTerm = searchTerm.Replace("-", " ");
            this.ViewBag.subSearchTerm = subSearchTerm;
            this.ViewBag.showSearchTerm = valueSearchType.Item3;
            ListingViewModel listingModel = new ListingViewModel();
            listingModel.SearchTermViewModel = new SearchTermViewModel
            {
                Adults = adults,
                EndDate = endDate,
                SearchType = valueSearchType.Item2,
                StartDate = startDate,
                Kids = kids,
                Infants = infants,
                Rooms = rooms,
                SearchTerm = searchTerm.Replace("-", " "),
                ShowSearchTerm = valueSearchType.Item3,
                Value = valueSearchType.Item1,
                KidsAge = kidsage,
                StartDateVar = !string.IsNullOrEmpty(startDate) ? DateTime.ParseExact(startDate, "dd-MM-yyyy", null) : DateTime.Now.Date,
                EndDateVar = !string.IsNullOrEmpty(endDate) ? DateTime.ParseExact(endDate, "dd-MM-yyyy", null) : DateTime.Now.Date.AddYears(1),
            };

            if (!string.IsNullOrEmpty(startDate))
            {
                this.TempData["StartDate"] = listingModel.SearchTermViewModel.StartDateVar;
            }
            else
            {
                this.TempData["StartDate"] = null;
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                this.TempData["EndDate"] = listingModel.SearchTermViewModel.EndDateVar;
            }
            else
            {
                this.TempData["EndDate"] = null;
            }

            if (searchType == (int)Enums.SearchType.Product)
            {
                DealsPackageModel dealsPackageModel = await this.homePageService.GetDealPackageByIdAsync(listingModel.SearchTermViewModel.Value);
                return this.RedirectToAction(dealsPackageModel.Type == 1 ? "Hotel" : "Tour", "Deal", new { url = dealsPackageModel.Url });
            }

            ListingViewModel result = new ListingViewModel();
            try
            {
                var searchkey = KeyCreator.Create(listingModel);
                var productRs =
                  await
                      this.tableCacheHandler.GetFromCacheAsync(
                          searchkey,
                          () => this.homePageBusiness.GetSearchResults(listingModel),
                          SearchExpiryInSeconds);
                result = productRs.Result;
            }
            catch (Exception ex)
            {
                result = this.listingService.GetSearchResults(listingModel);
                string msg = ex.ToString();
            }

            this.ViewBag.FilterActivated = false;
            if (result.ResultViewModels.Count == 0)
            {
                result.TrendingDeals = this.listingService.GetTop6TrendingDeals();
            }

            return this.View("Search", result);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="showSearchTerm">Show Search Term</param>
        /// <param name="searchType">Search Type</param>
        /// <param name="value">Value</param>
        /// <param name="searchTerm">Search Term</param>
        /// <param name="adults">Adults</param>
        /// <param name="kids">Kids</param>
        /// <param name="infants">Infants</param>
        /// <param name="rooms">Rooms</param>
        /// <param name="kidsage">Kids Age</param>
        /// <param name="startDate">Staat date</param>
        /// <param name="endDate">endate </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of search result.</returns>
        /// <returns>view for home</returns>
        [Route("search/{searchTerm:required:regex(^[[a-zA-Z '’-]]*$)}/{adults:int?}/{kids:int?}/{infants:int?}/{rooms:int?}/{startDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{endDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{kidsage?}")]
        public async Task<IActionResult> SearchOneLevel(
            bool showSearchTerm,
            int searchType,
            int value,
            string searchTerm,
            int adults = 1,
            int kids = 0,
            int infants = 0,
            int rooms = 1,
            string kidsage = null,
            string startDate = null,
            string endDate = null)
        {
            Tuple<int, int, bool> valueSearchType;
            if (searchTerm.ToLower() == "flash-deal")
            {
                valueSearchType = new Tuple<int, int, bool>(0, (int)Enums.SearchType.FlashDeal, false);
            }
            else if (searchTerm.ToLower() == "deals-of-the-month")
            {
                valueSearchType = new Tuple<int, int, bool>(0, (int)Enums.SearchType.DealOfTheMonth, false);
            }
            else
            {
                valueSearchType =
                    await this.listingService.GetValueListTypeOfSearchCountryTravelStyleAsync(searchTerm);
            }

            this.ViewBag.startDate = startDate;
            this.ViewBag.endDate = endDate;
            this.ViewBag.adults = adults;
            this.ViewBag.kids = kids;
            this.ViewBag.infants = infants;
            this.ViewBag.rooms = rooms;
            this.ViewBag.searchTerm = searchTerm.Replace("-", " ");
            this.ViewBag.showSearchTerm = valueSearchType.Item3;
            ListingViewModel listingModel = new ListingViewModel();
            listingModel.SearchTermViewModel = new SearchTermViewModel
            {
                Adults = adults,
                EndDate = endDate,
                SearchType = valueSearchType.Item2,
                StartDate = startDate,
                Kids = kids,
                Infants = infants,
                Rooms = rooms,
                SearchTerm = searchTerm.Replace("-", " "),
                ShowSearchTerm = valueSearchType.Item3,
                Value = valueSearchType.Item1,
                KidsAge = kidsage,
                StartDateVar = !string.IsNullOrEmpty(startDate) ? DateTime.ParseExact(startDate, "dd-MM-yyyy", null) : DateTime.Now.Date,
                EndDateVar = !string.IsNullOrEmpty(endDate) ? DateTime.ParseExact(endDate, "dd-MM-yyyy", null) : DateTime.Now.Date.AddYears(1),
            };

            if (!string.IsNullOrEmpty(startDate))
            {
                this.TempData["StartDate"] = listingModel.SearchTermViewModel.StartDateVar;
            }
            else
            {
                this.TempData["StartDate"] = null;
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                this.TempData["EndDate"] = listingModel.SearchTermViewModel.EndDateVar;
            }
            else
            {
                this.TempData["EndDate"] = null;
            }

            if (searchType == (int)Enums.SearchType.Product)
            {
                DealsPackageModel dealsPackageModel = await this.homePageService.GetDealPackageByIdAsync(listingModel.SearchTermViewModel.Value);
                if (dealsPackageModel.Type == 1) ////Hotel
                {
                    return this.RedirectToAction("Hotel", "Deal", new { url = dealsPackageModel.Url });
                }
                else
                {
                    return this.RedirectToAction("Tour", "Deal", new { url = dealsPackageModel.Url });
                }
            }

            ListingViewModel result = new ListingViewModel();
            try
            {
                var searchkey = KeyCreator.Create(listingModel);
                var productRs =
                  await
                      this.tableCacheHandler.GetFromCacheAsync(
                          searchkey,
                          () => this.homePageBusiness.GetSearchResults(listingModel),
                          SearchExpiryInSeconds);
                result = productRs.Result;
            }
            catch (Exception ex)
            {
                result = this.listingService.GetSearchResults(listingModel);
                string msg = ex.ToString();
            }

            this.ViewBag.FilterActivated = false;
            if (result.ResultViewModels.Count == 0)
            {
                result.TrendingDeals = this.listingService.GetTop6TrendingDeals();
            }

            return this.View("Search", result);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="showSearchTerm">Show Search Term</param>
        /// <param name="searchType">Search Type</param>
        /// <param name="value">Value</param>
        /// <param name="adults">Adults</param>
        /// <param name="kids">Kids</param>
        /// <param name="infants">Infants</param>
        /// <param name="rooms">Rooms</param>
        /// <param name="searchTerm">Search Term</param>
        /// <param name="kidsage">Kids Age</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of search result.</returns>
        /// <returns>view for home</returns>
        [Route("search/{adults:int?}/{kids:int?}/{infants:int?}/{rooms:int?}/{startDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{endDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{kidsage?}")]
        public async Task<IActionResult> SearchNoLevel(
            bool showSearchTerm = true,
            int searchType = (int)Enums.SearchType.All,
            int value = 0,
            int adults = 1,
            int kids = 0,
            int infants = 0,
            int rooms = 1,
            string searchTerm = null,
            string kidsage = null,
            string startDate = null,
            string endDate = null)
        {
            this.ViewBag.startDate = startDate;
            this.ViewBag.endDate = endDate;
            this.ViewBag.adults = adults;
            this.ViewBag.kids = kids;
            this.ViewBag.infants = infants;
            this.ViewBag.rooms = rooms;
            this.ViewBag.searchTerm = searchTerm;
            this.ViewBag.showSearchTerm = showSearchTerm;
            ListingViewModel listingModel = new ListingViewModel();
            listingModel.SearchTermViewModel = new SearchTermViewModel
            {
                Adults = adults,
                EndDate = endDate,
                SearchType = searchType,
                StartDate = startDate,
                Kids = kids,
                Infants = infants,
                Rooms = rooms,
                SearchTerm = searchTerm,
                ShowSearchTerm = showSearchTerm,
                Value = value,
                KidsAge = kidsage,
                StartDateVar = !string.IsNullOrEmpty(startDate) ? DateTime.ParseExact(startDate, "dd-MM-yyyy", null) : DateTime.Now.Date,
                EndDateVar = !string.IsNullOrEmpty(endDate) ? DateTime.ParseExact(endDate, "dd-MM-yyyy", null) : DateTime.Now.Date.AddYears(1),
            };

            if (!string.IsNullOrEmpty(startDate))
            {
                this.TempData["StartDate"] = listingModel.SearchTermViewModel.StartDateVar;
            }
            else
            {
                this.TempData["StartDate"] = null;
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                this.TempData["EndDate"] = listingModel.SearchTermViewModel.EndDateVar;
            }
            else
            {
                this.TempData["EndDate"] = null;
            }

            if (searchType == (int)Enums.SearchType.Product)
            {
                DealsPackageModel dealsPackageModel = await this.homePageService.GetDealPackageByIdAsync(listingModel.SearchTermViewModel.Value);
                if (dealsPackageModel.Type == 1) ////Hotel
                {
                    return this.RedirectToAction("Hotel", "Deal", new { url = dealsPackageModel.Url });
                }
                else
                {
                    return this.RedirectToAction("Tour", "Deal", new { url = dealsPackageModel.Url });
                }
            }

            ListingViewModel result = new ListingViewModel();
            try
            {
                var searchkey = KeyCreator.Create(listingModel);
                var productRs =
                  await
                      this.tableCacheHandler.GetFromCacheAsync(
                          searchkey,
                          () => this.homePageBusiness.GetSearchResults(listingModel),
                          SearchExpiryInSeconds);
                result = productRs.Result;
            }
            catch (Exception ex)
            {
                result = this.listingService.GetSearchResults(listingModel);
                string msg = ex.ToString();
            }

            this.ViewBag.FilterActivated = false;
            if (result.ResultViewModels.Count == 0)
            {
                result.TrendingDeals = this.listingService.GetTop6TrendingDeals();
            }

            return this.View("Search", result);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="showSearchTerm">Show Search Term</param>
        /// <param name="searchType">Search Type</param>
        /// <param name="value">Value</param>
        /// <param name="searchTerm">Search Term</param>
        /// <param name="subSearchTerm">Sub Search Term</param>
        /// <param name="adults">Adults</param>
        /// <param name="kids">Kids</param>
        /// <param name="infants">Infants</param>
        /// <param name="rooms">Rooms</param>
        /// <param name="kidsage">Kids Age</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of search result.</returns>
        /// <returns>view for home</returns>
        [Route("search/Hotel/{searchTerm:required:regex(^[[A-Za-z0-9-'’_, &]]+$)}/{adults:int?}/{kids:int?}/{infants:int?}/{rooms:int?}/{startDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{endDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{kidsage?}")]
        public async Task<IActionResult> SearchHotelProduct(
            bool showSearchTerm,
            int searchType,
            int value,
            string searchTerm,
            string subSearchTerm,
            int adults = 1,
            int kids = 0,
            int infants = 0,
            int rooms = 1,
            string kidsage = null,
            string startDate = null,
            string endDate = null)
        {
            Tuple<int, int, bool, string> valueSearchType;
            valueSearchType = await this.listingService.GetProductUrlAsync(searchTerm);
            if (valueSearchType.Item2 == (int)Enums.SearchType.Product)
            {
                return this.RedirectToAction("Hotel", "Deal", new { url = valueSearchType.Item4 });
            }

            this.ViewBag.startDate = startDate;
            this.ViewBag.endDate = endDate;
            this.ViewBag.adults = adults;
            this.ViewBag.kids = kids;
            this.ViewBag.infants = infants;
            this.ViewBag.rooms = rooms;
            this.ViewBag.searchTerm = searchTerm;
            this.ViewBag.showSearchTerm = valueSearchType.Item3;
            ListingViewModel listingModel = new ListingViewModel();
            listingModel.SearchTermViewModel = new SearchTermViewModel
            {
                Adults = adults,
                EndDate = endDate,
                SearchType = valueSearchType.Item2,
                StartDate = startDate,
                Kids = kids,
                Infants = infants,
                Rooms = rooms,
                SearchTerm = searchTerm,
                ShowSearchTerm = valueSearchType.Item3,
                Value = valueSearchType.Item1,
                KidsAge = kidsage,
                StartDateVar = !string.IsNullOrEmpty(startDate) ? DateTime.ParseExact(startDate, "dd-MM-yyyy", null) : DateTime.Now.Date,
                EndDateVar = !string.IsNullOrEmpty(endDate) ? DateTime.ParseExact(endDate, "dd-MM-yyyy", null) : DateTime.Now.Date.AddYears(1),
            };

            if (!string.IsNullOrEmpty(startDate))
            {
                this.TempData["StartDate"] = listingModel.SearchTermViewModel.StartDateVar;
            }
            else
            {
                this.TempData["StartDate"] = null;
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                this.TempData["EndDate"] = listingModel.SearchTermViewModel.EndDateVar;
            }
            else
            {
                this.TempData["EndDate"] = null;
            }

            ListingViewModel result = new ListingViewModel();
            try
            {
                var searchkey = KeyCreator.Create(listingModel);
                var productRs =
                  await
                      this.tableCacheHandler.GetFromCacheAsync(
                          searchkey,
                          () => this.homePageBusiness.GetSearchResults(listingModel),
                          SearchExpiryInSeconds);
                result = productRs.Result;
            }
            catch (Exception ex)
            {
                result = this.listingService.GetSearchResults(listingModel);
                string msg = ex.ToString();
            }

            this.ViewBag.FilterActivated = false;
            if (result.ResultViewModels.Count == 0)
            {
                result.TrendingDeals = this.listingService.GetTop6TrendingDeals();
            }

            return this.View("Search", result);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="showSearchTerm">Show Search Term</param>
        /// <param name="searchType">Search Type</param>
        /// <param name="value">Value</param>
        /// <param name="searchTerm">Search Term</param>
        /// <param name="subSearchTerm">Sub Search Term</param>
        /// <param name="adults">Adults</param>
        /// <param name="kids">Kids</param>
        /// <param name="infants">Infants</param>
        /// <param name="rooms">Rooms</param>
        /// <param name="kidsage">Kids Age</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of search result.</returns>
        /// <returns>view for home</returns>
        [Route("search/Holiday/{searchTerm:required:regex(^[[A-Za-z0-9-’'_, &]]+$)}/{adults:int?}/{kids:int?}/{infants:int?}/{rooms:int?}/{startDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{endDate:regex(^\\d{{2}}-\\d{{2}}-\\d{{4}}$)?}/{kidsage?}")]
        public async Task<IActionResult> SearchHolidayProduct(
            bool showSearchTerm,
            int searchType,
            int value,
            string searchTerm,
            string subSearchTerm,
            int adults = 1,
            int kids = 0,
            int infants = 0,
            int rooms = 1,
            string kidsage = null,
            string startDate = null,
            string endDate = null)
        {
            Tuple<int, int, bool, string> valueSearchType;
            valueSearchType = await this.listingService.GetProductUrlAsync(searchTerm);
            if (valueSearchType.Item2 == (int)Enums.SearchType.Product)
            {
                return this.RedirectToAction("Holiday", "Deal", new { url = valueSearchType.Item4 });
            }

            this.ViewBag.startDate = startDate;
            this.ViewBag.endDate = endDate;
            this.ViewBag.adults = adults;
            this.ViewBag.kids = kids;
            this.ViewBag.infants = infants;
            this.ViewBag.rooms = rooms;
            this.ViewBag.searchTerm = searchTerm;
            this.ViewBag.showSearchTerm = valueSearchType.Item3;
            ListingViewModel listingModel = new ListingViewModel();
            listingModel.SearchTermViewModel = new SearchTermViewModel
            {
                Adults = adults,
                EndDate = endDate,
                SearchType = valueSearchType.Item2,
                StartDate = startDate,
                Kids = kids,
                Infants = infants,
                Rooms = rooms,
                SearchTerm = searchTerm,
                ShowSearchTerm = valueSearchType.Item3,
                Value = valueSearchType.Item1,
                KidsAge = kidsage,
                StartDateVar = !string.IsNullOrEmpty(startDate) ? DateTime.ParseExact(startDate, "dd-MM-yyyy", null) : DateTime.Now.Date,
                EndDateVar = !string.IsNullOrEmpty(endDate) ? DateTime.ParseExact(endDate, "dd-MM-yyyy", null) : DateTime.Now.Date.AddYears(1),
            };

            if (!string.IsNullOrEmpty(startDate))
            {
                this.TempData["StartDate"] = listingModel.SearchTermViewModel.StartDateVar;
            }
            else
            {
                this.TempData["StartDate"] = null;
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                this.TempData["EndDate"] = listingModel.SearchTermViewModel.EndDateVar;
            }
            else
            {
                this.TempData["EndDate"] = null;
            }

            ListingViewModel result = new ListingViewModel();
            try
            {
                var searchkey = KeyCreator.Create(listingModel);
                var productRs =
                  await
                      this.tableCacheHandler.GetFromCacheAsync(
                          searchkey,
                          () => this.homePageBusiness.GetSearchResults(listingModel),
                          SearchExpiryInSeconds);
                result = productRs.Result;
            }
            catch (Exception ex)
            {
                result = this.listingService.GetSearchResults(listingModel);
                string msg = ex.ToString();
            }

            this.ViewBag.FilterActivated = false;
            if (result.ResultViewModels.Count == 0)
            {
                result.TrendingDeals = this.listingService.GetTop6TrendingDeals();
            }

            return this.View("Search", result);
        }

        // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //  This function converts decimal degrees to radians             :::
        // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double Deg2rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //  This function converts radians to decimal degrees             :::
        // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double Rad2deg(double rad)
        {
            return rad / Math.PI * 180.0;
        }
    }
}