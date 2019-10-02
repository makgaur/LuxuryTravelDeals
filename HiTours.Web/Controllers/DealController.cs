// <copyright file="DealController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Api.Common;
    using Api.Common.Caching;
    using AutoMapper;
    using Core;
    using Data.DataBase.Model;
    using Framework;
    using HiTours.Services.Zoho;
    using HiTours.TBO.Models;
    using HiTours.TBO.Models.Response.FareRuleResponse;
    using HiTours.TBO.Models.SSRResponse;
    using HiTours.TBO.Models.ViewModel;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using Razorpay.Api;
    using Services;
    using ViewModels;
    using ViewModels.Deals.Product;
    using ViewModels.Deals.Product.Hotel;
    using ViewModels.Deals.Product.Tour;
    using Ticket = TBO.Models.Ticket;

    /// <summary>
    /// DealController
    /// </summary>
    /// <seealso cref="HiTours.Web.BaseController" />
    public class DealController : BaseController
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private const int SearchExpiryInSeconds = 9000;
        private readonly string googleMapKey;
        private readonly Guid bookingManager = new Guid("1cffb9e8-6592-422c-9b8d-2617e2f2f0fc");

        /// <summary>
        /// The domain setting
        /// </summary>
        private readonly DomainSetting domainSetting;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The view render service
        /// </summary>
        private readonly IViewRenderService viewRenderService;

        /// <summary>
        /// The view render service
        /// </summary>
        private readonly IProductService productService;

        /// <summary>
        /// The view render service
        /// </summary>
        private readonly IDealService dealService;
        private readonly IBookingService bookingService;
        private readonly IUserDetailService userService;
        private readonly IHotelierService hotelierService;
        private readonly IPromotionService promotionService;
        private readonly IConfiguration configuration;
        private readonly TBOController tboController;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly ITableCacheHandler tableCacheHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealController" /> class.
        /// </summary>
        /// <param name="stateService">State Service</param>
        /// <param name="tboService">TBO Service</param>
        /// <param name="promotionService">Promotion Service</param>
        /// <param name="userService">User Service</param>
        /// <param name="bookingService">Booking Service</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="countryService">Country Service</param>
        /// <param name="cityService">City Service</param>
        /// <param name="dealService">Deal Service</param>
        /// <param name="productService">Product Service</param>
        /// <param name="viewRenderService">The view render service.</param>
        /// <param name="domainSetting">The domain setting.</param>
        /// <param name="hotelierService">Hotelier Service</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="tableCacheHandler">Table Cache Handler</param>
        public DealController(
            IStateService stateService,
            ITBOService tboService,
            IPromotionService promotionService,
            IUserDetailService userService,
            IBookingService bookingService,
            IMapper mapper,
            ICountryService countryService,
            ICityService cityService,
            IDealService dealService,
            IProductService productService,
            IViewRenderService viewRenderService,
            IOptions<DomainSetting> domainSetting,
            IHotelierService hotelierService,
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            IHomePageService homePageService,
            ITableCacheHandler tableCacheHandler)
            : base(mapper, homePageService, cityService, countryService, configuration, stateService)
        {
            this.tableCacheHandler = tableCacheHandler;
            this.promotionService = promotionService;
            this.configuration = configuration;
            this.googleMapKey = domainSetting.Value.GoogleMapKey;
            this.userService = userService;
            this.bookingService = bookingService;
            this.dealService = dealService;
            this.productService = productService;
            this.domainSetting = domainSetting.Value;
            this.hotelierService = hotelierService;
            this.hostingEnvironment = hostingEnvironment;
            this.viewRenderService = viewRenderService;
            this.tableCacheHandler = tableCacheHandler;
            this.tboController = new TBOController(tboService, configuration, hostingEnvironment);
        }

        /// <summary>
        /// Gets or sets the clip board data.
        /// </summary>
        /// <value>
        /// The clip board data.
        /// </value>
        protected Dictionary<string, object> ClipBoardData { get; set; }

        /// <summary>
        /// Gets the json ignore nullable.
        /// </summary>
        /// <value>
        /// The json ignore nullable.
        /// </value>
        protected JsonSerializerSettings JsonIgnoreNullable => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="url">Hotel Deal Url</param>
        /// <param name="from">From</param>
        [Route("Hotel/{url}/{from?}")]
        public async Task<IActionResult> Hotel(string url, string from = "")
        {
            this.TempData["StartDate"] = this.TempData["StartDate"];
            this.TempData["EndDate"] = this.TempData["EndDate"];
            this.HttpContext.Session.SetString("LeadFrom", from);
            ProductViewModel model = new ProductViewModel
            {
                GoogleApiKey = this.googleMapKey,
                Type = 1
            };
            model.HotelProductViewModel = await this.productService.GetHotelDealByUrl(url, this.googleMapKey);
            this.ViewData["Title"] = model.HotelProductViewModel.Name;
            model.ModelSerialized = JsonConvert.SerializeObject(model.HotelProductViewModel);
            model.BookingInformationViewModel = new BookingInformationViewModel
            {
                DealId = model.HotelProductViewModel.Id,
                NightId = model.HotelProductViewModel.NightId
            };

            try
            {
                if (model.HotelProductViewModel.DealsFlightViewModels != null)
                {
                    var originCity = await this.dealService.GetCityByCityCode(model.HotelProductViewModel.DealsFlightViewModels.Origin);
                    var destinationCity = await this.dealService.GetCityByCityCode(model.HotelProductViewModel.DealsFlightViewModels.Destination);
                    model.HotelProductViewModel.DealsFlightViewModels.OriginName = originCity.CityName + ", " + originCity.CountryName;
                    model.HotelProductViewModel.DealsFlightViewModels.DestinationName = destinationCity.CityName + ", " + destinationCity.CountryName;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
            }

            if (model.HotelProductViewModel.Id > 0)
            {
                await this.productService.UpdateDealCounter(model.HotelProductViewModel.Id);
                if (this.Request.Cookies["rv"] != null)
                {
                    string value = this.Request.Cookies["rv"].ToString();
                    List<RecentlyViewedDealsViewModel> recentlyViewed = JsonConvert.DeserializeObject<List<RecentlyViewedDealsViewModel>>(value);
                    int maxCount = recentlyViewed.Select(x => x.SortOrder).Max();
                    if (recentlyViewed.Select(x => x.DealId).Contains(model.HotelProductViewModel.Id))
                    {
                        int index = recentlyViewed.FindIndex(a => a.DealId == model.HotelProductViewModel.Id);
                        recentlyViewed[index].SortOrder = maxCount + 1;
                    }
                    else
                    {
                        recentlyViewed.Add(new RecentlyViewedDealsViewModel
                        {
                            DealId = model.HotelProductViewModel.Id,
                            DealName = model.HotelProductViewModel.Name,
                            Image = model.HotelProductViewModel.CardImage,
                            IsHotel = true,
                            Location = new List<string>() { model.HotelProductViewModel.HotelName, model.HotelProductViewModel.City },
                            SortOrder = maxCount + 1,
                            Url = "Hotel/" + model.HotelProductViewModel.Url
                        });
                    }

                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddYears(1);
                    this.Response.Cookies.Append("rv", JsonConvert.SerializeObject(recentlyViewed), cookieOptions);
                }
                else
                {
                    List<RecentlyViewedDealsViewModel> recentlyViewed = new List<RecentlyViewedDealsViewModel>();
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddYears(1);
                    recentlyViewed.Add(new RecentlyViewedDealsViewModel
                    {
                        DealId = model.HotelProductViewModel.Id,
                        DealName = model.HotelProductViewModel.Name,
                        Image = model.HotelProductViewModel.CardImage,
                        IsHotel = true,
                        Location = new List<string>() { model.HotelProductViewModel.HotelName, model.HotelProductViewModel.City },
                        SortOrder = 1,
                        Url = "Hotel/" + model.HotelProductViewModel.Url
                    });
                    this.Response.Cookies.Append("rv", JsonConvert.SerializeObject(recentlyViewed), cookieOptions);
                }
            }

            this.TempData.Keep();
            return this.View("Hotel/Index", model);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="url">Hotel Deal Url</param>
        [Route("Holiday/{url}")]
        public async Task<IActionResult> Tour(string url)
        {
            ProductViewModel model = new ProductViewModel
            {
                GoogleApiKey = this.googleMapKey,
                Type = 2
            };
            model.TourProductViewModel = await this.productService.GetTourDealByUrl(url, this.googleMapKey);
            this.ViewData["Title"] = model.TourProductViewModel.Name;
            model.ModelSerialized = JsonConvert.SerializeObject(model.TourProductViewModel);
            model.BookingInformationViewModel = new BookingInformationViewModel
            {
                DealId = model.TourProductViewModel.Id
            };
            try
            {
                if (model.TourProductViewModel.DealsFlightViewModels != null)
                {
                    var originCity = await this.dealService.GetCityByCityCode(model.TourProductViewModel.DealsFlightViewModels.Origin);
                    var destinationCity = await this.dealService.GetCityByCityCode(model.TourProductViewModel.DealsFlightViewModels.Destination);
                    model.TourProductViewModel.DealsFlightViewModels.OriginName = originCity.CityName + ", " + originCity.CountryName;
                    model.TourProductViewModel.DealsFlightViewModels.DestinationName = destinationCity.CityName + ", " + destinationCity.CountryName;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
            }

            if (model.TourProductViewModel.Id <= 0)
            {
                return this.View("Tour/Index", model);
            }

            await this.productService.UpdateDealCounter(model.TourProductViewModel.Id);
            if (this.Request.Cookies["rv"] != null)
            {
                string value = this.Request.Cookies["rv"].ToString();
                List<RecentlyViewedDealsViewModel> recentlyViewed = JsonConvert.DeserializeObject<List<RecentlyViewedDealsViewModel>>(value);
                int maxCount = recentlyViewed.Select(x => x.SortOrder).Max();
                if (recentlyViewed.Select(x => x.DealId).Contains(model.TourProductViewModel.Id))
                {
                    int index = recentlyViewed.FindIndex(a => a.DealId == model.TourProductViewModel.Id);
                    recentlyViewed[index].SortOrder = maxCount + 1;
                }
                else
                {
                    recentlyViewed.Add(new RecentlyViewedDealsViewModel
                    {
                        DealId = model.TourProductViewModel.Id,
                        DealName = model.TourProductViewModel.Name,
                        Image = model.TourProductViewModel.CardImage,
                        IsHotel = false,
                        Location = model.TourProductViewModel.City,
                        SortOrder = maxCount + 1,
                        Url = "Holiday/" + model.TourProductViewModel.Url
                    });
                }

                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddYears(1);
                this.Response.Cookies.Append("rv", JsonConvert.SerializeObject(recentlyViewed), cookieOptions);
            }
            else
            {
                List<RecentlyViewedDealsViewModel> recentlyViewed = new List<RecentlyViewedDealsViewModel>();
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddYears(1);
                recentlyViewed.Add(new RecentlyViewedDealsViewModel
                {
                    DealId = model.TourProductViewModel.Id,
                    DealName = model.TourProductViewModel.Name,
                    Image = model.TourProductViewModel.CardImage,
                    IsHotel = false,
                    Location = model.TourProductViewModel.City,
                    SortOrder = 1,
                    Url = "Holiday/" + model.TourProductViewModel.Url
                });
                this.Response.Cookies.Append("rv", JsonConvert.SerializeObject(recentlyViewed), cookieOptions);
            }

            return this.View("Tour/Index", model);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="nightId">Night Id</param>
        /// <param name="isFixedDeparture">Is Fixed Departure</param>
        /// <param name="minDay">Min Day</param>
        /// <param name="view">View</param>
        public async Task<IActionResult> GetCalendarByNightId(int nightId, bool isFixedDeparture, int minDay, string view)
        {
            if (!isFixedDeparture)
            {
                List<DealsRatePlanViewModel> viewModel = await this.productService.GetDealRatePlanByNightId(nightId);
                return this.PartialView(view == "desktop" ? "_LTD_Calendar" : "_LTD_Calendar_Mobile", new DealCalendarViewModel { DealType = 2, MinDays = minDay, DealsRatePlanViewModels = viewModel, BufferDays = this.configuration.GetValue<int>("BookingBufferDays:Tour") });
            }

            List<DealFixedDepartureDateViewModel> viewModels = await this.productService.GetDealFixedDepartureDates(nightId, this.configuration.GetValue<int>("BookingBufferDays:TourFixedDeparture"));
            return this.PartialView("_LTD_Calendar_Fixed_Departure", viewModels);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="dealId">Deal Id</param>
        /// <param name="nightId">Night Id</param>
        /// <param name="inclusionId">Inclusion Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        public async Task<IActionResult> GetHotelRoomConfiguration(int dealId, int nightId, int inclusionId, string startDate, string endDate)
        {
            DateTime startDateVar = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
            DateTime endDateVar = DateTime.ParseExact(endDate, "dd/MM/yyyy", null);
            this.ViewBag.SelectedStartDate = startDateVar;
            this.ViewBag.SelectedEndDate = endDateVar;
            List<DealRoomConfigurationModel> result = await this.productService.GetHotelRoomConfiguration(dealId, nightId, inclusionId, startDateVar, endDateVar);
            DealsInclusionModel inclusionRecord = await this.dealService.GetDealsInclusion(inclusionId);
            List<DealRoomConfigViewModel> viewModel = new List<DealRoomConfigViewModel>();
            try
            {
                foreach (var item in result)
                {
                    HotelierRoomConfigurationModel roomRecord = await this.hotelierService.GetHotelierRoomRecordByHotelIdAndRoomTypeId(inclusionRecord.VendorInfoId.Value, item.RoomTypeId);

                    if (roomRecord != null)
                    {
                        var dealReview = await this.dealService.GetDealReviewsByPackageId(dealId);
                        DealRoomConfigViewModel element = new DealRoomConfigViewModel
                        {
                            Id = item.Id,
                            Adult = item.Adult,
                            AdultAge = item.AdultAge,
                            CardImg = roomRecord.CardImg,
                            Child = item.Child,
                            ChildAge = item.ChildAge,
                            Description = roomRecord.Description,
                            FreeChild = item.FreeChild,
                            InclusionId = item.InclusionId,
                            FreeInfant = item.FreeInfant,
                            Infant = item.Infant,
                            InfantAge = item.InfantAge,
                            IsActive = item.IsActive,
                            RoomName = string.Empty,
                            Max = item.Max,
                            RoomTypeId = item.RoomTypeId,
                            RoomTypeName = string.Empty,
                            RoomAmenties = new List<string>(),
                            DealsRatePlanViewModels = new List<DealsRatePlanViewModel>(),
                            RoomImageGalleryViewModel = new RoomImageGalleryViewModel
                            {
                                HotelierRoomImageViewModels = this.Mapper.Map<List<HotelierRoomImageViewModel>>(await this.hotelierService.GetRoomImageFromRoomConfigId(roomRecord.Id)),
                                ProductReviewViewModels = dealReview.Select(y => new ProductReviewViewModel
                                {
                                    Name = y.FName + " " + y.LName,
                                    Review = y.Comment,
                                    ReviewDate = y.CreatedDate,
                                    UserRecommend = y.UserRecommend
                                }).ToList()
                            }
                        };
                        element.RoomName = (await this.productService.GetRoomTypeRecord(element.RoomTypeId)).Name;
                        element.RoomAmenties = await this.productService.GetRoomAmeneties(element.RoomTypeId, inclusionId);
                        List<DealsRatePlanModel> ratePlanModels = await this.productService.GetDealRatePlanByRoomConfig(item.Id, startDateVar, endDateVar);
                        if (ratePlanModels != null && ratePlanModels.Count > 0)
                        {
                            for (int k = 0; k < ratePlanModels.Count; k++)
                            {
                                if (ratePlanModels[k].DealInventoryModels.Count > 0)
                                {
                                    element.DealsRatePlanViewModels.Add(new DealsRatePlanViewModel
                                    {
                                        Currency = ratePlanModels[k].Currency,
                                        ExtraAdult = ratePlanModels[k].ExtraAdult,
                                        ExtraChild_NB = ratePlanModels[k].ExtraChild_NB,
                                        ExtraChild_WB = ratePlanModels[k].ExtraChild_WB,
                                        ExtraInfant = ratePlanModels[k].ExtraInfant,
                                        Id = ratePlanModels[k].Id,
                                        IsActive = ratePlanModels[k].IsActive,
                                        LOS = ratePlanModels[k].LOS,
                                        MarkUp = ratePlanModels[k].MarkUp,
                                        Name = ratePlanModels[k].Name,
                                        NightId = ratePlanModels[k].NightId,
                                        Price = ratePlanModels[k].Price,
                                        RackRate = ratePlanModels[k].RackRate,
                                        RatePlanId = ratePlanModels[k].Id,
                                        RoomConfigId = element.Id,
                                        SingleSupplement = ratePlanModels[k].SingleSupplement,
                                        ValidFrom = ratePlanModels[k].ValidFrom,
                                        ValidTo = ratePlanModels[k].ValidTo,
                                        ExtraSupplement = ratePlanModels[k].ExtraSupplement,
                                        ExtraSupplementPerHead = ratePlanModels[k].ExtraSupplementPerHead.Value,
                                        DealInventoryModels = ratePlanModels[k].DealInventoryModels.ToList(),
                                        InventorySerialized = JsonConvert.SerializeObject(ratePlanModels[k].DealInventoryModels)
                                    });
                                }
                            }

                            if (element.DealsRatePlanViewModels.Count > 0)
                            {
                                List<DealsRatePlanViewModel> updatedRatePlans = new List<DealsRatePlanViewModel>();
                                List<List<DealsRatePlanViewModel>> groupedRatePlan = element.DealsRatePlanViewModels.GroupBy(x => x.Name.Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Trim().ToLower()).Select(x => x.ToList()).ToList();

                                for (int i = 0; i < groupedRatePlan.Count; i++)
                                {
                                    if (groupedRatePlan[i].Count == 1)
                                    {
                                        updatedRatePlans.AddRange(groupedRatePlan[i]);
                                    }
                                    else
                                    {
                                        DealsRatePlanViewModel thisRatePlan = groupedRatePlan[i].OrderByDescending(x => x.ValidFrom).Select(x => new DealsRatePlanViewModel
                                        {
                                            Currency = x.Currency,
                                            ExtraAdult = x.ExtraAdult,
                                            ExtraChild_NB = x.ExtraChild_NB,
                                            ExtraChild_WB = x.ExtraChild_WB,
                                            ExtraInfant = x.ExtraInfant,
                                            Id = x.Id,
                                            IsActive = x.IsActive,
                                            LOS = x.LOS,
                                            MarkUp = x.MarkUp,
                                            Name = x.Name,
                                            NightId = x.NightId,
                                            Price = x.Price,
                                            RackRate = x.RackRate,
                                            RatePlanId = x.RatePlanId,
                                            RoomConfigId = x.RoomConfigId,
                                            SingleSupplement = x.SingleSupplement,
                                            ValidFrom = x.ValidFrom,
                                            ValidTo = x.ValidTo,
                                            ExtraSupplement = x.ExtraSupplement,
                                            ExtraSupplementPerHead = x.ExtraSupplementPerHead,
                                            DealInventoryModels = groupedRatePlan[i].SelectMany(y => y.DealInventoryModels).ToList(),
                                            InventorySerialized = JsonConvert.SerializeObject(groupedRatePlan[i].SelectMany(y => y.DealInventoryModels).ToList())
                                        }).FirstOrDefault();
                                        updatedRatePlans.Add(thisRatePlan);
                                    }
                                }

                                element.DealsRatePlanViewModels = updatedRatePlans.OrderBy(y =>
                                {
                                    List<DealInventoryModel> inventoryModels = JsonConvert.DeserializeObject<List<DealInventoryModel>>(y.InventorySerialized);
                                    decimal totalPriceBase = 0;
                                    foreach (var itemPrice in inventoryModels)
                                    {
                                        totalPriceBase = totalPriceBase + (itemPrice.Price.Value + itemPrice.Surgcharge.Value /*+ ((item.Price.Value + item.Surgcharge.Value) * (minRatePlan.MarkUp.HasValue ? minRatePlan.MarkUp.Value : 0)) / 100*/);
                                    }
                                    totalPriceBase = totalPriceBase + (totalPriceBase * (y.MarkUp.HasValue ? y.MarkUp.Value : 0) / 100);
                                    totalPriceBase = totalPriceBase / 2;
                                    totalPriceBase = totalPriceBase + (y.ExtraSupplement.HasValue ? (y.ExtraSupplement.Value / 2) + ((y.ExtraSupplement.Value / 2) * (y.MarkUp.HasValue ? y.MarkUp.Value : 0) / 100) : 0);
                                    return totalPriceBase;
                                }).ToList();
                                viewModel.Add(element);
                            }
                        }
                    }
                }

                viewModel = viewModel.OrderBy(x =>
                {
                    DealsRatePlanViewModel minRatePlan = x.DealsRatePlanViewModels.FirstOrDefault();
                    List<DealInventoryModel> inventoryModels = JsonConvert.DeserializeObject<List<DealInventoryModel>>(minRatePlan.InventorySerialized);
                    decimal totalPriceBase = 0;
                    foreach (var item in inventoryModels)
                    {
                        totalPriceBase = totalPriceBase + (item.Price.Value + item.Surgcharge.Value /*+ ((item.Price.Value + item.Surgcharge.Value) * (minRatePlan.MarkUp.HasValue ? minRatePlan.MarkUp.Value : 0)) / 100*/);
                    }
                    totalPriceBase = totalPriceBase + (totalPriceBase * (minRatePlan.MarkUp.HasValue ? minRatePlan.MarkUp.Value : 0) / 100);
                    totalPriceBase = totalPriceBase / 2;
                    totalPriceBase = totalPriceBase + (minRatePlan.ExtraSupplement.HasValue ? (minRatePlan.ExtraSupplement.Value / 2) + ((minRatePlan.ExtraSupplement.Value / 2) * (minRatePlan.MarkUp.HasValue ? minRatePlan.MarkUp.Value : 0) / 100) : 0);
                    return totalPriceBase;
                }).ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return this.PartialView("_RoomConfiguration", new List<DealRoomConfigViewModel>());
            }

            return this.PartialView("_RoomConfiguration", viewModel);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="dealId">Deal Id</param>
        /// <param name="nightId">Night Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        public async Task<IActionResult> GetTourRoomConfiguration(int dealId, int nightId, string startDate, string endDate)
        {
            DateTime startDateVar = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
            DateTime endDateVar = DateTime.ParseExact(endDate, "dd/MM/yyyy", null);
            this.ViewBag.SelectedStartDate = startDateVar;
            this.ViewBag.SelectedEndDate = endDateVar;
            List<DealTourHotelInfoViewModel> result = await this.productService.GetTourRatePlans(dealId, nightId, startDateVar, endDateVar);
            try
            {
                result = result.OrderBy(x => JsonConvert.DeserializeObject<DealInventoryViewModel>(x.DealsRatePlanViewModels[0].InventorySerialized).Price + JsonConvert.DeserializeObject<DealInventoryViewModel>(x.DealsRatePlanViewModels[0].InventorySerialized).Surgcharge).ToList();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return this.PartialView("Tour/_RoomConfigurationTour", result);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="max">Max Passenger</param>
        /// <param name="adults">Included Adults</param>
        /// <param name="child">Included Childs</param>
        /// <param name="infants">Included Infants</param>
        /// <param name="freeChild">Free Child</param>
        /// <param name="freeInfant">Free Infant</param>
        /// <param name="ratePlanName">RatePlan Name</param>
        /// <param name="priceText">Price Text</param>
        /// <param name="roomConfigId">Room COnfig Id</param>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <param name="roomTypeId">RoomTypeId</param>
        /// <param name="roomName">Room Name</param>
        /// <param name="aPriceDbo">Adult Price Based On Double Occupency</param>
        /// <param name="aPriceSbo">Adult Price Single Occupancy Based</param>
        /// <param name="exAPrice">Extra Adult Price</param>
        /// <param name="exCPrice">Extra Child Price</param>
        /// <param name="exIPrice">Extra Infant Price</param>
        /// <param name="markup">Markup</param>
        /// <param name="supplement">Supplement</param>
        /// <param name="phSupplement">Supplement Per Head</param>
        /// <param name="inventorySerialized">Inventory Searialized</param>
        /// <param name="roomConfigSerialized">Room Configuration Serialized</param>
        [HttpPost]
        public IActionResult GetRoomPassengerBreakdown(
            int max,
            int adults,
            int child,
            int infants,
            int freeChild,
            int freeInfant,
            string ratePlanName,
            string priceText,
            int roomConfigId,
            int ratePlanId,
            int roomTypeId,
            string roomName,
            decimal aPriceDbo,
            decimal aPriceSbo,
            decimal exAPrice,
            decimal exCPrice,
            decimal exIPrice,
            decimal markup,
            decimal supplement,
            decimal phSupplement,
            string inventorySerialized,
            string roomConfigSerialized)
        {
            BookingHotelRoomViewModel model = new BookingHotelRoomViewModel
            {
                FreeChild = freeChild,
                FreeInfant = freeInfant,
                RoomTypeId = roomTypeId,
                Max = max,
                Adult = adults,
                Child = child,
                Infant = infants,
                Rate = priceText,
                RoomName = roomName,
                RatePlanName = ratePlanName,
                RoomConfigId = roomConfigId,
                RatePlanId = ratePlanId,
                AdultPriceDbo = aPriceDbo,
                AdultPriceSbo = aPriceSbo,
                ExtraAdultPrice = exAPrice,
                ExtraChildPrice = exCPrice,
                ExtraInfantPrice = exIPrice,
                Supplement = supplement,
                MarkUp = markup,
                InventorySerialized = inventorySerialized,
                SupplementPh = phSupplement,
                RoomConfigSerialized = roomConfigSerialized
            };
            return this.PartialView("_RoomPassengerBreakdown", model);
        }

        /// <summary>
        /// Gets lowest airfare
        /// </summary>
        /// <param name="inclusionId">Inclusion ID</param>
        /// <param name="startDate">Search Start Date</param>
        /// <param name="startDateBooking">Start Date Booking</param>
        /// <returns>lowest price</returns>
        [HttpGet]
        public async Task<string> GetLowestFlightPrice(int inclusionId, string startDate, string startDateBooking)
        {
            string tokenId = await this.tboController.GetTBOLoginToken();
            string price = "10000";
            DateTime startDateVar = !string.IsNullOrEmpty(startDate) ? DateTime.ParseExact(startDate, "dd-MM-yyyy hh:mm:ss", null) : DateTime.Now.AddDays(15);
            if (!string.IsNullOrEmpty(startDateBooking))
            {
                startDateVar = DateTime.ParseExact(startDateBooking, "dd/MM/yyyy", null);
            }

            var data = await this.dealService.GetFlightDetailsByInclusionId(inclusionId);
            AirSearch airsearch = new AirSearch
            {
                EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                TokenId = tokenId,
                AdultCount = 1,
                ChildCount = 0,
                InfantCount = 0,
                DirectFlight = true,
                OneStopFlight = true,
                PreferredAirlines = null,
                ReturnFlight = true,
                JourneyType = (int)JourneyType.Return
            };

            airsearch.FlightCabinClass = (int)FlightCabin.All;
            airsearch.JourneyType = (int)JourneyType.Return;
            airsearch.Segments = new List<Segments>()
                    {
                        new Segments()
                        {
                            Origin = "DEL", Destination = data.Destination, PreferredDepartureTime = startDateVar.Date, PreferredArrivalTime = startDateVar.Date.AddDays(1).Date, FlightCabinClass = (int)FlightCabinClass.Economy
                        },
                         new Segments()
                        {
                            Origin = data.Destination, Destination = "DEL", PreferredDepartureTime = startDateVar.AddDays(5).Date, PreferredArrivalTime = startDateVar.AddDays(6).Date, FlightCabinClass = (int)FlightCabinClass.Economy
                        }
                    }.ToArray(); ////check it out
            var sResponse = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.Search), JsonConvert.SerializeObject(airsearch, Formatting.Indented, this.JsonIgnoreNullable), TboMethods.Search);
            if (sResponse.IsSuccess)
            {
                var sResult = JsonConvert.DeserializeObject<AirSearchResponse>(sResponse.Response);
                if (sResult.Response.Results.Length == 0)
                {
                    price = (sResult.Response.Results[0][0].Fare.PublishedFare + sResult.Response.Results[1][0].Fare.PublishedFare).ToString();
                }
                else
                {
                    price = sResult.Response.Results[0][0].Fare.PublishedFare.ToString();
                }
            }

            return price;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="startDate">Start Date.</param>
        /// <param name="returnDate">Return Date</param>
        /// <param name="adults">No of Adults</param>
        /// <param name="childs">No Of Children</param>
        /// <param name="infants">No of Infants</param>
        /// <param name="inclusionId">Inclusion Id</param>
        /// <param name="departure">Departure</param>
        /// <param name="destination">Destination</param>
        /// <param name="reload">reload</param>
        [HttpGet]
        public async Task<ActionResult> GetFlightDetails(string startDate, string returnDate, int adults, int childs, int infants, int inclusionId, string departure, string destination, string reload)
        {
            destination = string.IsNullOrEmpty(destination) ? "BOM" : destination;
            DateTime startDateVar = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
            DateTime returnDateVar = DateTime.ParseExact(returnDate, "dd/MM/yyyy", null);
            DealsFlightViewModel dealFlightViewModel = new DealsFlightViewModel();
            string tokenId = await this.tboController.GetTBOLoginToken();
            try
            {
                var data = await this.dealService.GetFlightDetailsByInclusionId(inclusionId);
                if (!string.IsNullOrEmpty(tokenId))
                {
                    AirSearch airsearch = new AirSearch
                    {
                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                        TokenId = tokenId,
                        AdultCount = adults,
                        ChildCount = childs,
                        InfantCount = infants,
                        DirectFlight = true,
                        OneStopFlight = true,
                        PreferredAirlines = null,
                        ReturnFlight = true,
                        JourneyType = (int)JourneyType.Return
                    };
                    airsearch.FlightCabinClass = (int)FlightCabin.All;
                    airsearch.JourneyType = (int)JourneyType.Return;
                    airsearch.Segments = new List<Segments>()
                    {
                        new Segments()
                        {
                            Origin = departure, Destination = data.Destination, PreferredDepartureTime = startDateVar, PreferredArrivalTime = startDateVar, FlightCabinClass = (int)FlightCabinClass.All
                        },
                         new Segments()
                        {
                             Origin = data.Destination, Destination = departure, PreferredDepartureTime = returnDateVar, PreferredArrivalTime = returnDateVar.AddDays(1), FlightCabinClass = (int)FlightCabinClass.All
                        }
                    }.ToArray(); ////check it out

                    Response<AirSearchResponse> airresponse = null;
                    try
                    {
                        var searchkey = KeyCreator.Create(airsearch);
                        if (reload == "reload")
                        {
                            searchkey += "#reload";
                        }

                        airresponse =
                          await
                              this.tableCacheHandler.GetFromCacheAsync(
                                  searchkey,
                                  () => this.GetAirResults(airsearch),
                                  SearchExpiryInSeconds);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.ToString();
                    }

                    if (airresponse != null)
                    {
                        var sResult = airresponse.Result;
                        ////AirSearchResult[] airres = null;
                        ////foreach (var outbound in airresponse.Result.Response.Results[0])
                        ////{
                        ////    foreach (var inbound in airresponse.Result.Response.Results[1])
                        ////    {
                        ////    }
                        ////}

                        sResult.Response.TotalPassengers = adults + childs + infants;
                        sResult.Response.TokenId = tokenId;
                        if (sResult != null && sResult.Response != null && sResult.Response.Results != null && sResult.Response.ResponseStatus == (int)AuthenticateStatus.Successful)
                        {
                            this.AddClipBoard(nameof(Passengers), new { AdultCount = airsearch.AdultCount, ChildCount = airsearch.ChildCount, InfantCount = airsearch.InfantCount });
                            this.AddClipBoard(nameof(sResult.Response.TraceId), sResult.Response.TraceId);
                            var apiSearchResult = new List<AirSearchResult>();
                            this.AddClipBoard(nameof(AirSearchResult), sResult);
                            return this.PartialView("_FlightSearch", sResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string filePath = Path.Combine(this.hostingEnvironment.WebRootPath, "Logs\\TBOError.txt");

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
            }

            return this.PartialView("_FlightSearch", new AirSearchResponse());
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="startDate">Start Date.</param>
        /// <param name="returnDate">Return Date</param>
        /// <param name="adults">No of ADults</param>
        /// <param name="childs">No Of Childrens</param>
        /// <param name="infants">No of Infants</param>
        /// <param name="inclusionId">Inclusion Id</param>
        /// <param name="departure">Departure</param>
        /// <param name="destination">Destination</param>
        /// <param name="cabinclass">cabinclass</param>
        /// <param name="stopover">stopover</param>
        /// <param name="airline">airline</param>
        /// <param name="pricerange">pricerange</param>
        /// <param name="timings">timings</param>
        /// <param name="returntimings">returntimings</param>
        /// <param name="refundable">refundable</param>
        [HttpGet]
        public async Task<ActionResult> GetFlightFilter(string startDate, string returnDate, int adults, int childs, int infants, int inclusionId, string departure, string destination, string cabinclass, string stopover, string airline, string pricerange, string timings, string returntimings, string refundable)
        {
            destination = string.IsNullOrEmpty(destination) ? "BOM" : destination;
            DateTime startDateVar = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
            DateTime returnDateVar = DateTime.ParseExact(returnDate, "dd/MM/yyyy", null);
            DealsFlightViewModel dealFlightViewModel = new DealsFlightViewModel();
            string tokenId = await this.tboController.GetTBOLoginToken();
            try
            {
                var data = await this.dealService.GetFlightDetailsByInclusionId(inclusionId);
                if (!string.IsNullOrEmpty(tokenId))
                {
                    AirSearch airsearch = new AirSearch
                    {
                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                        TokenId = tokenId,
                        AdultCount = adults,
                        ChildCount = childs,
                        InfantCount = infants,
                        DirectFlight = true,
                        OneStopFlight = true,
                        PreferredAirlines = null,
                        ReturnFlight = true,
                        JourneyType = (int)JourneyType.Return
                    };
                    airsearch.FlightCabinClass = (int)FlightCabin.All;
                    airsearch.JourneyType = (int)JourneyType.Return;
                    airsearch.Segments = new List<Segments>()
                    {
                        new Segments()
                        {
                            Origin = departure, Destination = data.Destination, PreferredDepartureTime = startDateVar, PreferredArrivalTime = startDateVar, FlightCabinClass = (int)FlightCabinClass.All
                        },
                         new Segments()
                        {
                             Origin = data.Destination, Destination = departure, PreferredDepartureTime = returnDateVar, PreferredArrivalTime = returnDateVar.AddDays(1), FlightCabinClass = (int)FlightCabinClass.All
                        }
                    }.ToArray(); ////check it out

                    Response<AirSearchResponse> airresponse = null;
                    try
                    {
                        var searchkey = KeyCreator.Create(airsearch);
                        airresponse =
                          await
                              this.tableCacheHandler.GetFromCacheAsync(
                                  searchkey,
                                  () => this.GetAirResults(airsearch),
                                  SearchExpiryInSeconds);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.ToString();
                    }

                    if (airresponse != null)
                    {
                        this.ViewBag.airline = airline;
                        this.ViewBag.stopover = stopover;
                        this.ViewBag.refundable = refundable;
                        if (!string.IsNullOrEmpty(pricerange) && pricerange.Length > 1)
                        {
                            var range = pricerange.Split('#');
                            this.ViewBag.minprice = range[0];
                            this.ViewBag.maxprice = range[1];
                        }

                        if (!string.IsNullOrEmpty(timings) && timings.Length > 3)
                        {
                            var time = timings.Replace('(', ' ').Replace(')', ' ').Split('-');
                            var timesplit1 = time[0].Trim().Contains('A') ? time[0].Trim().Split('A') : time[0].Trim().Split('P');
                            var timesplit2 = time[1].Trim().Contains('A') ? time[1].Trim().Split('A') : time[1].Trim().Split('P');
                            var timesplithour1 = time[0].Trim().Contains('A') ? Convert.ToInt32(timesplit1[0]) : (Convert.ToInt32(timesplit1[0]) + 12);
                            var timesplithour2 = time[1].Trim().Contains('A') ? Convert.ToInt32(timesplit2[0]) : (Convert.ToInt32(timesplit2[0]) + 12);
                            this.ViewBag.minhour = timesplithour1;
                            this.ViewBag.maxhour = timesplithour2;
                        }

                        if (!string.IsNullOrEmpty(returntimings) && returntimings.Length > 3)
                        {
                            var time = returntimings.Replace('(', ' ').Replace(')', ' ').Split('-');
                            var timesplit1 = time[0].Trim().Contains('A') ? time[0].Trim().Split('A') : time[0].Trim().Split('P');
                            var timesplit2 = time[1].Trim().Contains('A') ? time[1].Trim().Split('A') : time[1].Trim().Split('P');
                            var timesplithour1 = time[0].Trim().Contains('A') ? Convert.ToInt32(timesplit1[0]) : (Convert.ToInt32(timesplit1[0]) + 12);
                            var timesplithour2 = time[1].Trim().Contains('A') ? Convert.ToInt32(timesplit2[0]) : (Convert.ToInt32(timesplit2[0]) + 12);
                            this.ViewBag.returnminhour = timesplithour1;
                            this.ViewBag.returnmaxhour = timesplithour2;
                        }

                        var sResult = airresponse.Result;
                        sResult.Response.TotalPassengers = adults + childs + infants;
                        sResult.Response.TokenId = tokenId;
                        if (sResult != null && sResult.Response != null && sResult.Response.Results != null && sResult.Response.ResponseStatus == (int)AuthenticateStatus.Successful)
                        {
                            this.AddClipBoard(nameof(Passengers), new { AdultCount = airsearch.AdultCount, ChildCount = airsearch.ChildCount, InfantCount = airsearch.InfantCount });
                            this.AddClipBoard(nameof(sResult.Response.TraceId), sResult.Response.TraceId);
                            var apiSearchResult = new List<AirSearchResult>();
                            this.AddClipBoard(nameof(AirSearchResult), sResult);
                            return this.PartialView("_FlightSearchResult", sResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string filePath = Path.Combine(this.hostingEnvironment.WebRootPath, "Logs\\TBOError.txt");

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
            }

            return this.PartialView("_FlightSearch", new AirSearchResponse());
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="airsearch">airsearchresponse</param>
        /// <returns>Air Search Response</returns>
        public async Task<Response<AirSearchResponse>> GetAirResults(AirSearch airsearch)
        {
            ////await Task.Delay(10);
            AirSearchResponse response = new AirSearchResponse();
            var sResponse = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.Search), JsonConvert.SerializeObject(airsearch, Formatting.Indented, this.JsonIgnoreNullable), TboMethods.Search);
            if (sResponse.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<AirSearchResponse>(sResponse.Response);
            }

            return await Task.FromResult(new Response<AirSearchResponse>() { ResultType = ResultType.Success, Result = response });
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="flightIndex">No of Infants</param>
        /// <param name="isLcc">Inclusion Id</param>
        /// <param name="traceId">Departure</param>
        /// <param name="tokenId">Destination</param>
        /// <param name="length">Length</param>
        /// <returns>View</returns>
        public ActionResult GetBaggage(string flightIndex, string isLcc, string traceId, string tokenId, int length)
        {
            List<TBO.Models.Response.FareRuleResponse.FareRule> fareRulesModel = new List<TBO.Models.Response.FareRuleResponse.FareRule>();
            if (length == 2) //// Local Flights
            {
                List<Tuple<string, bool>> flightIndexAndIsLcc = new List<Tuple<string, bool>>();
                flightIndexAndIsLcc.Add(new Tuple<string, bool>(flightIndex.Split(',')[0], isLcc.Split(',')[0] == "False" ? false : true));
                flightIndexAndIsLcc.Add(new Tuple<string, bool>(flightIndex.Split(',')[1], isLcc.Split(',')[1] == "False" ? false : true));
                foreach (var item in flightIndexAndIsLcc)
                {
                    ////Fare Rule Start
                    TBO.Models.ApiResponse apiResponseFr = this.tboController.PostCustom(
                    this.tboController.GetTboUrl(TboMethods.FareRule),
                    JsonConvert.SerializeObject(new FareRuleRequest
                    {
                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                        ResultIndex = item.Item1,
                        TokenId = tokenId,
                        TraceId = traceId
                    }), TboMethods.FareRule);
                    FareRuleResponse thisResponse = JsonConvert.DeserializeObject<FareRuleResponse>(apiResponseFr.Response);
                    if (thisResponse.Response.FareRules != null)
                    {
                        fareRulesModel.AddRange(thisResponse.Response.FareRules);
                    }
                }
            }
            else //// International Flights
            {
                ////Fare Rule Start
                var apiResponseFr = this.tboController.PostCustom(
                this.tboController.GetTboUrl(TboMethods.FareRule),
                JsonConvert.SerializeObject(new FareRuleRequest
                {
                    EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                    ResultIndex = flightIndex,
                    TokenId = tokenId,
                    TraceId = traceId
                }), TboMethods.FareRule);
                FareRuleResponse thisResponse = JsonConvert.DeserializeObject<FareRuleResponse>(apiResponseFr.Response);
                fareRulesModel.AddRange(thisResponse.Response.FareRules);
                ////Fare Rule End
            }

            return this.PartialView("_Baggage", fareRulesModel);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="model">The identifier.</param>
        [HttpPost]
        public async Task<ViewResult> Booking(ProductViewModel model)
        {
            switch (model.Type)
            {
                case 1:
                    model.HotelProductViewModel = JsonConvert.DeserializeObject<HotelProductViewModel>(model.ModelSerialized);
                    break;
                case 2:
                    model.TourProductViewModel = JsonConvert.DeserializeObject<TourProductViewModel>(model.ModelSerialized);
                    break;
                default: break;
            }

            var claimInformation = this.HttpContext.User.Claims.FirstOrDefault();
            if (claimInformation != null)
            {
                if (claimInformation.Issuer == "Google")
                {
                    model.BookingInformationViewModel.Email = this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email).Select(x => x.Value).FirstOrDefault();
                    model.BookingInformationViewModel.LeadFullName = this.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
                    ////model.BookingInformationViewModel.MobileNumber = userDetails.MobileNo
                }
                else
                {
                    var userId = claimInformation.Value;
                    var userDetails = await this.userService.GetByIdAsync(Convert.ToInt32(userId));
                    model.BookingInformationViewModel.Email = userDetails.EmailId;
                    model.BookingInformationViewModel.LeadFullName = userDetails.FirstName + " " + userDetails.LastName;
                    model.BookingInformationViewModel.MobileNumber = userDetails.MobileNo;
                }
            }

            model.BookingSummaryViewModel = model.BookingSummaryViewModelString != null ? JsonConvert.DeserializeObject<BookingSummaryViewModel>(model.BookingSummaryViewModelString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }) : new BookingSummaryViewModel();
            model.DealsPromotionViewModels = new List<DealsPromotionViewModel>();
            List<DealsPromotionViewModel> promotionModels = await this.promotionService.GetAllDealPromotionsById(model.BookingInformationViewModel.DealId);
            if (promotionModels.Count > 0)
            {
                for (int i = 0; i < promotionModels.Count; i++)
                {
                    bool bookingDateFlag = false;
                    bool travelDateFlag = false;
                    bool roomFlag = false;
                    bool arrivalFlagForEarlyBird = false;
                    bool lengthOfStayFlag = false;
                    if (promotionModels[i].BookingStartDate <= DateTime.Now && promotionModels[i].BookingEndDate >= DateTime.Now)
                    {
                        bookingDateFlag = true;
                    }
                    else
                    {
                        bookingDateFlag = false;
                    }

                    if (promotionModels[i].TravelStartDate <= model.BookingInformationViewModel.Checkin && promotionModels[i].TravelEndDate >= model.BookingInformationViewModel.Checkout)
                    {
                        travelDateFlag = true;
                    }
                    else
                    {
                        travelDateFlag = false;
                    }

                    DealsPromotion_RoomType dealsPromotion_RoomType = await this.promotionService.GetDealRoomPromotionByDealPromotionId(promotionModels[i].Id);
                    if (dealsPromotion_RoomType != null)
                    {
                        promotionModels[i].RoomType = dealsPromotion_RoomType.RoomTypeId;
                        int roomType = dealsPromotion_RoomType.RoomTypeId;
                        if (model.BookingHotelRoomViewModels.Select(x => x.RoomTypeId).Contains(roomType))
                        {
                            roomFlag = true;
                        }
                        else
                        {
                            roomFlag = false;
                        }
                    }
                    else
                    {
                        roomFlag = true;
                    }

                    if (promotionModels[i].Type == (int)Enums.DealPromotionType.EarlyBirdOffer)
                    {
                        if ((model.BookingInformationViewModel.Checkin - DateTime.Now).Days >= promotionModels[i].StartDay)
                        {
                            arrivalFlagForEarlyBird = true;
                        }
                        else
                        {
                            arrivalFlagForEarlyBird = false;
                        }

                        if (arrivalFlagForEarlyBird && roomFlag && bookingDateFlag && travelDateFlag)
                        {
                            model.DealsPromotionViewModels.Add(promotionModels[i]);
                        }
                    }

                    if (promotionModels[i].Type == (int)Enums.DealPromotionType.LengthOfStay)
                    {
                        if ((model.BookingInformationViewModel.Checkout - model.BookingInformationViewModel.Checkin).Days >= promotionModels[i].LengthOfStay)
                        {
                            lengthOfStayFlag = true;
                        }
                        else
                        {
                            lengthOfStayFlag = false;
                        }

                        if (lengthOfStayFlag && roomFlag && bookingDateFlag && travelDateFlag)
                        {
                            model.DealsPromotionViewModels.Add(promotionModels[i]);
                        }
                    }

                    if (promotionModels[i].Type == (int)Enums.DealPromotionType.FlatDiscount)
                    {
                    }

                    if (promotionModels[i].Type == (int)Enums.DealPromotionType.DayoftheWeek)
                    {
                    }
                }
            }

            if (model.DealsPromotionViewModels != null && model.DealsPromotionViewModels.Count > 0)
            {
                List<DealsPromotionViewModel> groupedPromotion = model.DealsPromotionViewModels.GroupBy(x => x.Type, (key, g) => g.OrderByDescending(e => e.DiscountValue).FirstOrDefault()).ToList();
                model.DealsPromotionViewModels = groupedPromotion;
            }

            this.ModelState.Clear();
            return this.View(model);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="model">The identifier.</param>
        [HttpPost]
        public async Task<IActionResult> BookingPayment(ProductViewModel model)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            if (model != null)
            {
                try
                {
                    BookingSummaryViewModel summaryViewModel = JsonConvert.DeserializeObject<BookingSummaryViewModel>(model.BookingSummaryViewModelString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    ////Login Logic Below
                    UserDetailModel user = null;
                    user = await this.userService.GetUserRecordByMobile(model.BookingInformationViewModel.MobileNumber);
                    if (user == null)
                    {
                        user = await this.userService.GetUserRecordByEmailId(model.BookingInformationViewModel.Email);
                        if (user != null && string.IsNullOrEmpty(user.MobileNo))
                        {
                            user.MobileNo = model.BookingInformationViewModel.MobileNumber;
                            await this.userService.UpdateAsync(user);
                        }
                    }

                    if (user == null)
                    {
                        UserDetailModel userModel = new UserDetailModel();
                        userModel.FirstName = model.BookingInformationViewModel.LeadFullName.Split(' ').FirstOrDefault();
                        userModel.LastName = model.BookingInformationViewModel.LeadFullName.Split(' ').Length > 1 ? model.BookingInformationViewModel.LeadFullName.Split(' ').Skip(1).FirstOrDefault() : string.Empty;
                        userModel.MobileNo = model.BookingInformationViewModel.MobileNumber;
                        userModel.EmailId = model.BookingInformationViewModel.Email;
                        userModel.IsActive = true;
                        userModel.IsGuest = true;
                        userModel.IsDelete = false;
                        userModel.SetAuditInfo(this.bookingManager);
                        await this.userService.InsertAsync(userModel);
                        user = await this.userService.GetUserRecordByMobile(model.BookingInformationViewModel.MobileNumber);

                        string company = "Hi Tours";
                        string json = "{\"data\":[{" +
                                "\"Company\": \"" + company + "\"," +
                                "\"Last_Name\": \"" + userModel.LastName + "\"," +
                                "\"First_Name\": \"" + userModel.FirstName + "\"," +
                                "\"Email\": \"" + userModel.EmailId + "\"," +
                                "\"Phone\": \"" + userModel.MobileNo + "\"," +
                                "}]," +
                                "\"trigger\": [" +
                                "\"approval\"," +
                                "\"workflow\"," +
                                "\"blueprint\"" +
                                "]}";

                        var resultZoho = Services.Zoho.ZohoUpdate.GetApiResponse(json, "https://www.zohoapis.com/crm/v2/Leads");
                    }

                    if (string.IsNullOrEmpty(user.MobileNo))
                    {
                        user.MobileNo = model.BookingInformationViewModel.MobileNumber;
                        await this.userService.UpdateAsync(user);
                    }

                    var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Email, model.BookingInformationViewModel.Email));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Role, Enums.RoleType.User.ToString()));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Name, (user.FirstName + " " + user.LastName).Trim()));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Actor, string.Empty));
                    ////claimIdentity.AddClaim(new Claim(ClaimTypes.HomePhone, string.Empty));
                    await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                    ////Login Finish

                    BookingInformationModel bInfoData = this.Mapper.Map<BookingInformationModel>(model.BookingInformationViewModel);
                    bInfoData.BookedDate = DateTime.Now;
                    bInfoData.CustomerId = user.Id;
                    bInfoData.SetAuditInfo(this.bookingManager);
                    bInfoData.Email = model.BookingInformationViewModel.Email;
                    bInfoData.PhoneNumber = model.BookingInformationViewModel.MobileNumber;
                    bInfoData.PackagePrice = summaryViewModel.TotalPrice;
                    bInfoData.TaxAmount = summaryViewModel.TotalTax;
                    bInfoData.ServiceFees = summaryViewModel.TotalServiceFee;
                    bInfoData.TotalAmount = summaryViewModel.TotalPrice + summaryViewModel.TotalServiceFee + summaryViewModel.TotalTax;
                    bInfoData.BookingSummarySerialized = model.BookingSummaryViewModelString;
                    bInfoData = await this.bookingService.AddBookingInformation(bInfoData);
                    DealsPackageModel packageModel = await this.dealService.GetDealPackageAsync(bInfoData.DealId);
                    bInfoData.ReferenceNumber = "LTD" + packageModel.Code.Substring(0, 2) + this.GenerateCharacters().Skip(26 + (bInfoData.Id / 1000)).FirstOrDefault() + (bInfoData.Id % 1000).ToString().PadLeft(3, '0');
                    await this.bookingService.UpdateBookingInformationRecord(bInfoData);
                    if (!string.IsNullOrEmpty(model.BookingInformationViewModel.LeadFullName))
                    {
                        string[] name = model.BookingInformationViewModel.LeadFullName.Split(' ');
                        BookingPassengerModel bookingPassenger = new BookingPassengerModel
                        {
                            IsLead = true,
                            BookingId = bInfoData.Id,
                            FirstName = name[0],
                            LastName = name.Length > 1 ? name[1] : string.Empty
                        };
                        await this.bookingService.AddPassenger(bookingPassenger);
                    }

                    if (model.BookingPassengerViewModels != null)
                    {
                        foreach (var item in model.BookingPassengerViewModels)
                        {
                            BookingPassengerModel bookingPassenger = new BookingPassengerModel
                            {
                                IsLead = false,
                                BookingId = bInfoData.Id,
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                DOB = item.DOB
                            };
                            await this.bookingService.AddPassenger(bookingPassenger);
                        }
                    }

                    if (summaryViewModel.BookingHotelRooms != null)
                    {
                        foreach (var item in summaryViewModel.BookingHotelRooms)
                        {
                            BookingHotelRoomModel bookingHotelRoom = this.Mapper.Map<BookingHotelRoomModel>(item);
                            bookingHotelRoom.BookingId = bInfoData.Id;
                            bookingHotelRoom.Status = 1;
                            bookingHotelRoom.MarkUp = item.MarkUp;
                            bookingHotelRoom.Supplement = item.Supplement;
                            bookingHotelRoom.GST = model.Type == 1 ? 18 : 5;
                            bookingHotelRoom.GSTAmount = item.Tax;
                            bookingHotelRoom.TotalAmount = (item.TotalPrice * item.Nights) + item.Supplement + item.ServiceFee + item.Tax;
                            bookingHotelRoom.RoomConfigSerialized = item.RoomConfigSerialized;
                            bookingHotelRoom.FreeChild = item.FreeChild;
                            bookingHotelRoom.FreeInfant = item.FreeInfant;
                            bookingHotelRoom.ChargableChild = item.ChargableChild;
                            bookingHotelRoom.ChargableInfant = item.ChargableInfant;
                            bookingHotelRoom.SetAuditInfo(this.bookingManager);
                            int hotelRoomId = await this.bookingService.AddHotelRoom(bookingHotelRoom);
                        }
                    }

                    if (summaryViewModel.VisaInformation != null)
                    {
                        foreach (var item in summaryViewModel.VisaInformation)
                        {
                            BookingVisaModel bookingVisaModel = new BookingVisaModel
                            {
                                BookingId = bInfoData.Id,
                                MarkUp = item.ServiceFee,
                                Count = item.Count,
                                Price = item.Price,
                                Tax = item.Tax,
                                TotalAmount = item.Tax + item.Price + item.ServiceFee,
                                VisaId = item.VisaId
                            };

                            await this.bookingService.AddVisa(bookingVisaModel);
                        }
                    }

                    int razorPayAmount = 0;
                    if (model.BookingInformationViewModel.DiscountApplied)
                    {
                        razorPayAmount = (int)(Math.Round(summaryViewModel.TotalPrice + summaryViewModel.TotalServiceFee + summaryViewModel.TotalTax - model.BookingInformationViewModel.DiscountAmount) * 100);
                    }
                    else
                    {
                        razorPayAmount = (int)(Math.Round(summaryViewModel.TotalPrice + summaryViewModel.TotalServiceFee + summaryViewModel.TotalTax) * 100);
                    }

                    var input = new Dictionary<string, object>
                    {
                        { "amount", razorPayAmount }, // this amount should be same as transaction amount
                        { "currency", Constants.RazorPayCurrency },
                        { "receipt", "12121" },
                        { "payment_capture", 1 }
                    };
                    this.GetKeys(out string key, out string secret);
                    var client = new RazorpayClient(key, secret);
                    var order = client.Order.Create(input);
                    var orderDetail = new BookingPayment
                    {
                        ////DataAmount = (int)((model.BookingPrice + model.GstAmount) * 100),
                        DataAmount = razorPayAmount,
                        DataDescription = string.Empty,
                        DataImage = Constants.RazorPayDataImage, ////"https://razorpay.com/favicon.png",
                        DataKey = key,
                        DataName = Constants.RazorPay,
                        DataOrderId = order["id"].ToString(),
                        DataPrefillContact = model.BookingInformationViewModel.MobileNumber,
                        DataPrefillEmail = model.BookingInformationViewModel.Email,
                        DataPrefillName = model.BookingInformationViewModel.LeadFullName,
                        DataThemeColor = Constants.RazorPayDataThemeColor,
                        JsSrc = Constants.RazorPayCheckoutJs
                    };
                    response = new Dictionary<string, object>();
                    response.Add("ErrorCode", "0");
                    response.Add("Result", orderDetail);
                    this.TempData["BookingId"] = bInfoData.Id;
                    return this.Json(response);
                }
                catch (Exception ex)
                {
                    var msg = ex.ToString();
                }
            }
            else
            {
                response.Add("ErrorCode", "Data Not valid");
                return this.Json(response);
            }

            return this.View(model);
        }

        /// <summary>
        /// Payment Failed Lead on zoho
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>
        /// view
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PaymentFailed(ProductViewModel model)
        {
            int bookingId = Convert.ToInt32(this.TempData["BookingId"]);
            MyBookingDescriptionViewModel modelbooking = await this.userService.GetMyBookingDescriptionByBookingId(bookingId);

            int adult = 0, child = 0, infant = 0;
            foreach (var item in model.BookingHotelRoomViewModels)
            {
                adult += item.Adult;
                child += item.Child;
                infant += item.Infant;
            }

            var highlights = string.Join(",", modelbooking.dealPackageViewModel.DealsHighlightViewModels.Select(p => p.Title.ToString()));
            var nights = (modelbooking.bookingInformationViewModel.Checkout - modelbooking.bookingInformationViewModel.Checkin).TotalDays;
            string company = "Hi Tours";
            string json = "{\"data\":[{" +
                    "\"Company\": \"" + company + "\"," +
                    "\"Last_Name\": \"" + model.BookingInformationViewModel.LeadFullName.Split(' ')[1] + "\"," +
                    "\"First_Name\": \"" + model.BookingInformationViewModel.LeadFullName.Split(' ')[0] + "\"," +
                    "\"Email\": \"" + model.BookingInformationViewModel.Email + "\"," +
                    "\"Phone\": \"" + model.BookingInformationViewModel.MobileNumber + "\"," +
                    "\"Lead_Source\": \"Website\"," +
                    "\"Lead_Status\": \"Unconfirmed\"," +
                    "\"Choose_Hotel_Deal\": \"" + modelbooking.dealPackageViewModel.Name + "\"," +
                    "\"Choose_Tour_Deal\": \"" + modelbooking.dealPackageViewModel.Name + "\"," +
                    "\"Country_to_Visit\": \"" + modelbooking.LocationNames[2] + "\"," +
                    "\"City_to_Travel\": \"" + modelbooking.LocationNames[1] + "\"," +
                    "\"Trip_Start_Date\": \"" + modelbooking.bookingInformationViewModel.Checkin.ToString("yyyy-MM-dd") + "\"," +
                    "\"Trip_End_Date\": \"" + modelbooking.bookingInformationViewModel.Checkout.ToString("yyyy-MM-dd") + "\"," +
                    "\"No_of_Nights\": \"" + nights + "\"," +
                    "\"No_of_Adults\": \"" + adult + "\"," +
                    "\"No_of_Children\": \"" + child + "\"," +
                    "\"No_of_Infant\": \"" + infant + "\"," +
                    "\"No_of_Room\": \"" + model.BookingHotelRoomViewModels.Count + "\"," +
                    "\"Room_Type\": \"" + model.BookingHotelRoomViewModels[0].RoomName + "\"," +
                    "\"Meal_Plan\": \"" + model.BookingHotelRoomViewModels[0].RatePlanName + "\"," +
                    "\"Product_Inclusions\": \"" + highlights + "\"," +
                    "\"Expected_Turnover\": \"" + modelbooking.bookingInformationViewModel.TotalAmount + "\"," +
                    "\"Visa\": \"" + (model.BookingVisaViewModels != null ? "Checked" : "Unchecked") + "\"," +
                    "\"Flight\": \"" + (model.FlightRequired == true ? "Checked" : "Unchecked") + "\"," +
                    "\"Optional_Tour\": \"Unchecked\"," +
                    "\"Insurance\": \"Unchecked\"," +
                    "}]," +
                    "\"trigger\": [" +
                    "\"approval\"," +
                    "\"workflow\"," +
                    "\"blueprint\"" +
                    "]}";

            var resultZoho = Services.Zoho.ZohoUpdate.GetApiResponse(json, "https://www.zohoapis.com/crm/v2/Leads");

            return this.Json(new { Status = true, BookingId = bookingId });
        }

        /// <summary>
        /// Bookings the confirmation.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>
        /// view
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> BookingConfirmation(ProductViewModel model)
        {
            int bookingId = Convert.ToInt32(this.TempData["BookingId"]);

            string paymentId = model.razorpay_payment_id;
            this.GetKeys(out string key, out string secret);
            var client = new RazorpayClient(key, secret);
            var attributes = new Dictionary<string, string>
            {
                { "razorpay_payment_id", paymentId },
                { "razorpay_order_id", model.razorpay_order_id },
                { "razorpay_signature", model.razorpay_signature }
            };
            Utils.verifyPaymentSignature(attributes);
            var outer = JToken.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(client.Payment.Fetch(paymentId)));
            var payment = outer["Attributes"].Value<JObject>();
            var bookingInformationRecord = await this.bookingService.GetBookingRecordById(bookingId);
            bookingInformationRecord.IsConfirmed = true;
            bookingInformationRecord.IsCompleted = false;
            bookingInformationRecord.FlightRequired = model.FlightRequired;
            if (model.FlightRequired)
            {
                bookingInformationRecord.FlightTraceId = model.BookingFlightViewModel.TraceId;
            }

            await this.bookingService.UpdateBookingInformationRecord(bookingInformationRecord);
            //// Flight Booking Start
            if (model.FlightRequired)
            {
                if (model.BookingFlightViewModel.Length == 2)
                {
                    List<Tuple<string, bool>> flightIndexAndIsLcc = new List<Tuple<string, bool>>();
                    flightIndexAndIsLcc.Add(new Tuple<string, bool>(model.BookingFlightViewModel.FlightIndex.Split(',')[0], model.BookingFlightViewModel.IsLCCString.Split(',')[0] == "False" ? false : true));
                    flightIndexAndIsLcc.Add(new Tuple<string, bool>(model.BookingFlightViewModel.FlightIndex.Split(',')[1], model.BookingFlightViewModel.IsLCCString.Split(',')[1] == "False" ? false : true));
                    flightIndexAndIsLcc = flightIndexAndIsLcc.OrderBy(x => x.Item1).ToList();
                    foreach (var item in flightIndexAndIsLcc)
                    {
                        BookingFlightModel flightModel = new BookingFlightModel
                        {
                            BookingDate = DateTime.Now,
                            FlightCabinClass = (int)FlightCabinClass.Economy,
                            BookingId = bookingId,
                            Destination = string.Empty,
                            NightId = model.BookingInformationViewModel.NightId,
                            Origin = string.Empty,
                            PNR = string.Empty,
                            TBOBookingId = string.Empty,
                            UserTransactionId = payment.ToString(),
                            TokenId = model.BookingFlightViewModel.TokenId.ToUpper(),
                            TraceId = model.BookingFlightViewModel.TraceId.ToUpper(),
                            TicketGenerated = false,
                            FlightIndex = item.Item1
                        };
                        flightModel.SetAuditInfo(this.bookingManager);
                        int bookingFlightId = await this.bookingService.AddBookingFlightInformation(flightModel);

                        var fareQuoteAndSSRRequest = new TBO.Models.FareRule
                        {
                            EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                            ResultIndex = item.Item1,
                            TokenId = model.BookingFlightViewModel.TokenId,
                            TraceId = model.BookingFlightViewModel.TraceId,
                            IsLCC = item.Item2
                        };
                        ////Fare Rule Start
                        var apiResponseFr = this.tboController.PostCustom(
                        this.tboController.GetTboUrl(TboMethods.FareRule),
                        JsonConvert.SerializeObject(new FareRuleRequest
                        {
                            EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                            ResultIndex = item.Item1,
                            TokenId = model.BookingFlightViewModel.TokenId,
                            TraceId = model.BookingFlightViewModel.TraceId
                        }), TboMethods.FareRule);
                        ////Fare Rule End
                        ////Fare Quote Start
                        var apiResponseFq = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.FareQuote), JsonConvert.SerializeObject(fareQuoteAndSSRRequest), TboMethods.FareQuote);
                        ////Fare Quote End
                        ////SSR Start
                        var apiResponse = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.Ssr), JsonConvert.SerializeObject(fareQuoteAndSSRRequest), TboMethods.Ssr);
                        ////SSR Ends
                        if (apiResponseFq.IsSuccess)
                        {
                            var fareQuoteResult = JsonConvert.DeserializeObject<FareQuoteResponse>(apiResponseFq.Response);
                            model.FareQuoteResponseString = apiResponseFq.Response;
                            if (fareQuoteResult != null && fareQuoteResult.Response != null && fareQuoteResult.Response.ResponseStatus == (int)AuthenticateStatus.Successful)
                            {
                                if (fareQuoteResult.Response.Results.IsLCC)
                                {
                                    TicketViewModel ticketModel = new TicketViewModel
                                    {
                                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                                        ResultIndex = item.Item1,
                                        TokenId = model.BookingFlightViewModel.TokenId,
                                        TraceId = model.BookingFlightViewModel.TraceId,
                                        Passengers = new List<TicketPassengerViewModel>()
                                    };
                                    int i = 0;
                                    foreach (var itemPass in model.BookingPassengerViewModels)
                                    {
                                        FareBreakdown typeFare = fareQuoteResult.Response.Results.FareBreakdown.Where(x => x.PassengerType == itemPass.PassengerType.Value).FirstOrDefault();
                                        ticketModel.Passengers.Add(new TicketPassengerViewModel
                                        {
                                            FirstName = itemPass.FirstName,
                                            LastName = itemPass.LastName,
                                            DateOfBirth = itemPass.DOB.Value,
                                            AddressLine1 = "321-329 III Floor, Vipul Agora",
                                            AddressLine2 = "M.G. Road, Sector - 28",
                                            City = "Gurugram",
                                            CountryCode = "IN",
                                            CountryName = "India",
                                            Nationality = "IN",
                                            ContactNo = model.BookingInformationViewModel.MobileNumber,
                                            Email = model.BookingInformationViewModel.Email,
                                            Gender = itemPass.Salutation == "Mrs" || itemPass.Salutation == "Ms" || itemPass.Salutation == "Miss" ? "2" : "1",
                                            PassportNo = string.IsNullOrEmpty(itemPass.PassportNumber) ? null : itemPass.PassportNumber,
                                            PassportExpiry = string.IsNullOrEmpty(itemPass.PassportNumber) ? null : itemPass.PassportExpiryDate,
                                            PaxType = itemPass.PassengerType.Value.ToString(),
                                            Title = itemPass.Salutation,
                                            IsLeadPax = i == 0 ? true : false,
                                            Fare = new TicketFareViewModel
                                            {
                                                PassengerType = itemPass.PassengerType.Value,
                                                AdditionalTxnFee = typeFare.AdditionalTxnFee / typeFare.PassengerCount,
                                                BaseFare = typeFare.BaseFare / typeFare.PassengerCount,
                                                Tax = typeFare.Tax / typeFare.PassengerCount,
                                                YQTax = typeFare.YQTax / typeFare.PassengerCount,
                                                Currency = typeFare.Currency
                                            }
                                        });
                                        i++;
                                    } //// Add Passengers

                                    var apiResponseTicket = this.tboController.PostCustom(
                                    this.tboController.GetTboUrl(TboMethods.Ticket),
                                    JsonConvert.SerializeObject(ticketModel, this.JsonIgnoreNullable),
                                    TboMethods.Ticket);
                                    var ticketResponse = JsonConvert.DeserializeObject<TicketLCCResponseRoot>(apiResponseTicket.Response);
                                    if (ticketResponse.Response.ResponseStatus == 1) //// Success
                                    {
                                        flightModel = await this.bookingService.GetBookingFlightById(bookingFlightId);
                                        flightModel.Destination = ticketResponse.Response.Response.FlightItinerary.Destination;
                                        flightModel.Origin = ticketResponse.Response.Response.FlightItinerary.Origin;
                                        flightModel.PNR = ticketResponse.Response.Response.Pnr;
                                        flightModel.TBOBookingId = ticketResponse.Response.Response.BookingId.ToString();
                                        flightModel.TicketGenerated = true;
                                        flightModel.UpdateAuditInfo(this.bookingManager);
                                        await this.bookingService.UpdateBookingFlightInformation(flightModel);
                                        var bookingFlightRecord = await this.bookingService.GetBookingRecordById(bookingId);
                                        bookingFlightRecord.FlightSuccessful = true;
                                        bookingFlightRecord.IsConfirmed = true;
                                        await this.bookingService.UpdateBookingInformationRecord(bookingFlightRecord);
                                    }
                                }
                                else ////Booking then Ticketing
                                {
                                    TicketViewModel book = new TicketViewModel
                                    {
                                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                                        ResultIndex = item.Item1,
                                        TokenId = model.BookingFlightViewModel.TokenId,
                                        TraceId = model.BookingFlightViewModel.TraceId,
                                        Passengers = new List<TicketPassengerViewModel>()
                                    };
                                    int i = 0;
                                    foreach (var itemPass in model.BookingPassengerViewModels)
                                    {
                                        FareBreakdown typeFare = fareQuoteResult.Response.Results.FareBreakdown.Where(x => x.PassengerType == itemPass.PassengerType.Value).FirstOrDefault();
                                        book.Passengers.Add(new TicketPassengerViewModel
                                        {
                                            FirstName = itemPass.FirstName,
                                            LastName = itemPass.LastName,
                                            DateOfBirth = itemPass.DOB.Value,
                                            AddressLine1 = "321-329 III Floor, Vipul Agora",
                                            AddressLine2 = "M.G. Road, Sector - 28",
                                            City = "Gurugram",
                                            CountryCode = "IN",
                                            CountryName = "India",
                                            Nationality = "IN",
                                            ContactNo = model.BookingInformationViewModel.MobileNumber,
                                            Email = model.BookingInformationViewModel.Email,
                                            Gender = itemPass.Salutation == "Mrs" || itemPass.Salutation == "Ms" || itemPass.Salutation == "Miss" ? "2" : "1",
                                            PassportNo = string.IsNullOrEmpty(itemPass.PassportNumber) ? null : itemPass.PassportNumber,
                                            PassportExpiry = string.IsNullOrEmpty(itemPass.PassportNumber) ? null : itemPass.PassportExpiryDate,
                                            PaxType = itemPass.PassengerType.Value.ToString(),
                                            Title = itemPass.Salutation,
                                            IsLeadPax = i == 0 ? true : false,
                                            Fare = new TicketFareViewModel
                                            {
                                                PassengerType = itemPass.PassengerType.Value,
                                                AdditionalTxnFee = typeFare.AdditionalTxnFee / typeFare.PassengerCount,
                                                BaseFare = typeFare.BaseFare / typeFare.PassengerCount,
                                                Tax = typeFare.Tax / typeFare.PassengerCount,
                                                YQTax = typeFare.YQTax / typeFare.PassengerCount,
                                                Currency = typeFare.Currency
                                            }
                                        });
                                        i++;
                                    }

                                    try
                                    {
                                        var apiResponseBooking = this.tboController.PostCustom(
                                        this.tboController.GetTboUrl(TboMethods.Book), JsonConvert.SerializeObject(book, this.JsonIgnoreNullable), TboMethods.Book);
                                        var bookingResponse = JsonConvert.DeserializeObject<BookingResponseRoot>(apiResponseBooking.Response);
                                        if (bookingResponse.Response.ResponseStatus == 1) //// Success
                                        {
                                            var bookingPnr = bookingResponse.Response.Response.Pnr;
                                            flightModel = await this.bookingService.GetBookingFlightById(bookingFlightId);
                                            flightModel.Destination = bookingResponse.Response.Response.FlightItinerary.Destination;
                                            flightModel.Origin = bookingResponse.Response.Response.FlightItinerary.Origin;
                                            flightModel.PNR = bookingResponse.Response.Response.Pnr;
                                            flightModel.TBOBookingId = bookingResponse.Response.Response.BookingId.ToString();
                                            flightModel.UpdateAuditInfo(this.bookingManager);
                                            await this.bookingService.UpdateBookingFlightInformation(flightModel);
                                            Ticket ticketViewMode = new Ticket
                                            {
                                                BookingId = bookingResponse.Response.Response.BookingId,
                                                EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                                                PNR = bookingResponse.Response.Response.Pnr,
                                                TokenId = model.BookingFlightViewModel.TokenId,
                                                TraceId = model.BookingFlightViewModel.TraceId
                                            };
                                            var apiResponseTicket = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.Ticket), JsonConvert.SerializeObject(ticketViewMode, this.JsonIgnoreNullable), TboMethods.Ticket);
                                            TicketLCCResponseRoot ticket = JsonConvert.DeserializeObject<TicketLCCResponseRoot>(apiResponse.Response);
                                            if (ticket.Response.ResponseStatus == 1)
                                            {
                                                var bookFlightRecord = await this.bookingService.GetBookingFlightById(bookingFlightId);
                                                if (bookFlightRecord != null)
                                                {
                                                    bookFlightRecord.TicketGenerated = true;
                                                    await this.bookingService.UpdateBookingFlightInformation(bookFlightRecord);
                                                    var bookingFlightRecord = await this.bookingService.GetBookingRecordById(bookingId);
                                                    bookingFlightRecord.FlightSuccessful = true;
                                                    bookingFlightRecord.IsConfirmed = true;
                                                    await this.bookingService.UpdateBookingInformationRecord(bookingFlightRecord);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        var msg = ex.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    BookingFlightModel flightModel = new BookingFlightModel
                    {
                        BookingDate = DateTime.Now,
                        FlightCabinClass = (int)FlightCabinClass.Economy,
                        BookingId = bookingId,
                        Destination = string.Empty,
                        NightId = model.BookingInformationViewModel.NightId,
                        Origin = string.Empty,
                        PNR = string.Empty,
                        TBOBookingId = string.Empty,
                        UserTransactionId = payment.ToString(),
                        TokenId = model.BookingFlightViewModel.TokenId.ToUpper(),
                        TraceId = model.BookingFlightViewModel.TraceId.ToUpper(),
                        TicketGenerated = false,
                        FlightIndex = model.BookingFlightViewModel.FlightIndex
                    };
                    flightModel.SetAuditInfo(this.bookingManager);
                    int bookingFlightId = await this.bookingService.AddBookingFlightInformation(flightModel);
                    var fareQuoteAndSSRRequest = new TBO.Models.FareRule
                    {
                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                        ResultIndex = model.BookingFlightViewModel.FlightIndex,
                        TokenId = model.BookingFlightViewModel.TokenId,
                        TraceId = model.BookingFlightViewModel.TraceId,
                        IsLCC = model.BookingFlightViewModel.IsLCCString == "False" ? false : true
                    };
                    ////Fare Rule Start
                    var apiResponseFr = this.tboController.PostCustom(
                    this.tboController.GetTboUrl(TboMethods.FareRule),
                    JsonConvert.SerializeObject(new FareRuleRequest
                    {
                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                        ResultIndex = model.BookingFlightViewModel.FlightIndex,
                        TokenId = model.BookingFlightViewModel.TokenId,
                        TraceId = model.BookingFlightViewModel.TraceId
                    }), TboMethods.FareRule);
                    ////Fare Rule End
                    ////Fare Quote Start
                    var apiResponseFq = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.FareQuote), JsonConvert.SerializeObject(fareQuoteAndSSRRequest), TboMethods.FareQuote);
                    ////Fare Quote End
                    if (apiResponseFq.IsSuccess)
                    {
                        var fareQuoteResult = JsonConvert.DeserializeObject<FareQuoteResponse>(apiResponseFq.Response);
                        model.FareQuoteResponseString = apiResponseFq.Response;
                        if (fareQuoteResult != null && fareQuoteResult.Response != null && fareQuoteResult.Response.ResponseStatus == (int)AuthenticateStatus.Successful)
                        {
                            if (fareQuoteResult.Response.Results.IsLCC)
                            {
                                ////SSR Start
                                var apiResponse = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.Ssr), JsonConvert.SerializeObject(fareQuoteAndSSRRequest), TboMethods.Ssr);
                                SSRLCCResponse sSRLCCResponse = new SSRLCCResponse();
                                try
                                {
                                    sSRLCCResponse = JsonConvert.DeserializeObject<SSRLCCResponse>(apiResponse.Response);
                                }
                                catch (Exception ex)
                                {
                                    var msg = ex.ToString();
                                }

                                ////SSR Ends
                                TicketViewModel ticketModel = new TicketViewModel
                                {
                                    EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                                    ResultIndex = model.BookingFlightViewModel.FlightIndex,
                                    TokenId = model.BookingFlightViewModel.TokenId,
                                    TraceId = model.BookingFlightViewModel.TraceId,
                                    Passengers = new List<TicketPassengerViewModel>()
                                };
                                int i = 0;
                                foreach (var item in model.BookingPassengerViewModels)
                                {
                                    FareBreakdown typeFare = fareQuoteResult.Response.Results.FareBreakdown.Where(x => x.PassengerType == item.PassengerType.Value).FirstOrDefault();
                                    ticketModel.Passengers.Add(new TicketPassengerViewModel
                                    {
                                        FirstName = item.FirstName,
                                        LastName = item.LastName,
                                        DateOfBirth = item.DOB.Value,
                                        AddressLine1 = "321-329 III Floor, Vipul Agora",
                                        AddressLine2 = "M.G. Road, Sector - 28",
                                        City = "Gurugram",
                                        CountryCode = "IN",
                                        CountryName = "India",
                                        Nationality = "IN",
                                        ContactNo = model.BookingInformationViewModel.MobileNumber,
                                        Email = model.BookingInformationViewModel.Email,
                                        Gender = item.Salutation == "Mrs" || item.Salutation == "Ms" || item.Salutation == "Miss" ? "2" : "1",
                                        PassportNo = string.IsNullOrEmpty(item.PassportNumber) ? null : item.PassportNumber,
                                        PassportExpiry = string.IsNullOrEmpty(item.PassportNumber) ? null : item.PassportExpiryDate,
                                        PaxType = item.PassengerType.Value.ToString(),
                                        Title = item.Salutation,
                                        IsLeadPax = i == 0 ? true : false,
                                        Baggage = sSRLCCResponse.Response.Baggage.SelectMany(x => x).Where(x => x.Price == 0 && x.Weight > 0 && x.Description == (int)Description.Included).Select(x => x).FirstOrDefault(),
                                        Fare = new TicketFareViewModel
                                        {
                                            PassengerType = item.PassengerType.Value,
                                            AdditionalTxnFee = typeFare.AdditionalTxnFee / typeFare.PassengerCount,
                                            BaseFare = typeFare.BaseFare / typeFare.PassengerCount,
                                            Tax = typeFare.Tax / typeFare.PassengerCount,
                                            YQTax = typeFare.YQTax / typeFare.PassengerCount,
                                            Currency = typeFare.Currency
                                        },
                                    });
                                    i++;
                                }

                                var apiResponseTicket = this.tboController.PostCustom(
                                this.tboController.GetTboUrl(TboMethods.Ticket),
                                JsonConvert.SerializeObject(ticketModel, this.JsonIgnoreNullable),
                                TboMethods.Ticket);
                                var ticketResponse = JsonConvert.DeserializeObject<TicketLCCResponseRoot>(apiResponseTicket.Response);
                                if (ticketResponse.Response.ResponseStatus == 1) //// Success
                                {
                                    flightModel = await this.bookingService.GetBookingFlightById(bookingFlightId);
                                    flightModel.Destination = ticketResponse.Response.Response.FlightItinerary.Destination;
                                    flightModel.Origin = ticketResponse.Response.Response.FlightItinerary.Origin;
                                    flightModel.PNR = ticketResponse.Response.Response.Pnr;
                                    flightModel.TBOBookingId = ticketResponse.Response.Response.BookingId.ToString();
                                    flightModel.TicketGenerated = true;
                                    flightModel.UpdateAuditInfo(this.bookingManager);
                                    await this.bookingService.UpdateBookingFlightInformation(flightModel);
                                    var bookingFlightRecord = await this.bookingService.GetBookingRecordById(bookingId);
                                    bookingFlightRecord.FlightSuccessful = true;
                                    bookingFlightRecord.IsConfirmed = true;
                                    await this.bookingService.UpdateBookingInformationRecord(bookingFlightRecord);
                                }
                            }
                            else ////Booking then Ticketing
                            {
                                ////SSR Start
                                var apiResponse = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.Ssr), JsonConvert.SerializeObject(fareQuoteAndSSRRequest), TboMethods.Ssr);
                                ////SSR Ends
                                TicketViewModel book = new TicketViewModel
                                {
                                    EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                                    ResultIndex = model.BookingFlightViewModel.FlightIndex,
                                    TokenId = model.BookingFlightViewModel.TokenId,
                                    TraceId = model.BookingFlightViewModel.TraceId,
                                    Passengers = new List<TicketPassengerViewModel>()
                                };
                                int i = 0;
                                foreach (var item in model.BookingPassengerViewModels)
                                {
                                    FareBreakdown typeFare = fareQuoteResult.Response.Results.FareBreakdown.Where(x => x.PassengerType == item.PassengerType.Value).FirstOrDefault();
                                    book.Passengers.Add(new TicketPassengerViewModel
                                    {
                                        FirstName = item.FirstName,
                                        LastName = item.LastName,
                                        DateOfBirth = item.DOB.Value,
                                        AddressLine1 = "321-329 III Floor, Vipul Agora",
                                        AddressLine2 = "M.G. Road, Sector - 28",
                                        City = "Gurugram",
                                        CountryCode = "IN",
                                        CountryName = "India",
                                        Nationality = "IN",
                                        ContactNo = model.BookingInformationViewModel.MobileNumber,
                                        Email = model.BookingInformationViewModel.Email,
                                        Gender = item.Salutation == "Mrs" || item.Salutation == "Ms" || item.Salutation == "Miss" ? "2" : "1",
                                        PassportNo = string.IsNullOrEmpty(item.PassportNumber) ? null : item.PassportNumber,
                                        PassportExpiry = string.IsNullOrEmpty(item.PassportNumber) ? null : item.PassportExpiryDate,
                                        PaxType = item.PassengerType.Value.ToString(),
                                        Title = item.Salutation,
                                        IsLeadPax = i == 0 ? true : false,
                                        Fare = new TicketFareViewModel
                                        {
                                            PassengerType = item.PassengerType.Value,
                                            AdditionalTxnFee = typeFare.AdditionalTxnFee / typeFare.PassengerCount,
                                            BaseFare = typeFare.BaseFare / typeFare.PassengerCount,
                                            Tax = typeFare.Tax / typeFare.PassengerCount,
                                            YQTax = typeFare.YQTax / typeFare.PassengerCount,
                                            Currency = typeFare.Currency
                                        },
                                    });
                                    i++;
                                }

                                var apiResponseBooking = this.tboController.PostCustom(
                                this.tboController.GetTboUrl(TboMethods.Book),
                                JsonConvert.SerializeObject(book, this.JsonIgnoreNullable),
                                TboMethods.Book);
                                var bookingResponse = JsonConvert.DeserializeObject<BookingResponseRoot>(apiResponseBooking.Response);
                                if (bookingResponse.Response.ResponseStatus == 1) //// Success
                                {
                                    ////BookingFlightModel flightModel = new BookingFlightModel
                                    ////{
                                    ////    BookingDate = DateTime.Now,
                                    ////    BookingId = bookingId,
                                    ////    FlightCabinClass = (int)FlightCabinClass.Economy,
                                    ////    Destination = bookingResponse.Response.Response.FlightItinerary.Destination,
                                    ////    NightId = model.BookingInformationViewModel.NightId,
                                    ////    Origin = bookingResponse.Response.Response.FlightItinerary.Origin,
                                    ////    PNR = bookingResponse.Response.Response.Pnr,
                                    ////    TBOBookingId = bookingResponse.Response.Response.BookingId.ToString(),
                                    ////    UserTransactionId = payment.ToString(),
                                    ////    TokenId = model.BookingFlightViewModel.TokenId.ToUpper(),
                                    ////    TraceId = model.BookingFlightViewModel.TraceId.ToUpper(),
                                    ////    TicketGenerated = false
                                    ////};
                                    flightModel = await this.bookingService.GetBookingFlightById(bookingFlightId);
                                    flightModel.Destination = bookingResponse.Response.Response.FlightItinerary.Destination;
                                    flightModel.Origin = bookingResponse.Response.Response.FlightItinerary.Origin;
                                    flightModel.PNR = bookingResponse.Response.Response.Pnr;
                                    flightModel.TBOBookingId = bookingResponse.Response.Response.BookingId.ToString();
                                    flightModel.TicketGenerated = false;
                                    flightModel.UpdateAuditInfo(this.bookingManager);
                                    await this.bookingService.UpdateBookingFlightInformation(flightModel);
                                    Ticket ticketViewMode = new Ticket
                                    {
                                        BookingId = bookingResponse.Response.Response.BookingId,
                                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                                        PNR = bookingResponse.Response.Response.Pnr,
                                        TokenId = model.BookingFlightViewModel.TokenId,
                                        TraceId = model.BookingFlightViewModel.TraceId
                                    };
                                    var apiResponseTicket = this.tboController.PostCustom(this.tboController.GetTboUrl(TboMethods.Ticket), JsonConvert.SerializeObject(ticketViewMode, this.JsonIgnoreNullable), TboMethods.Ticket);
                                    TicketLCCResponseRoot ticket = JsonConvert.DeserializeObject<TicketLCCResponseRoot>(apiResponse.Response);
                                    if (ticket.Response.ResponseStatus == 1)
                                    {
                                        BookingFlightModel bookFlightRecord = await this.bookingService.GetBookingFlightById(bookingFlightId);
                                        if (bookFlightRecord != null)
                                        {
                                            bookFlightRecord.TicketGenerated = true;
                                            await this.bookingService.UpdateBookingFlightInformation(bookFlightRecord);
                                            var bookingFlightRecord = await this.bookingService.GetBookingRecordById(bookingId);
                                            bookingFlightRecord.FlightSuccessful = true;
                                            bookingFlightRecord.IsConfirmed = true;
                                            await this.bookingService.UpdateBookingInformationRecord(bookingFlightRecord);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                bookingInformationRecord = await this.bookingService.GetBookingRecordById(bookingId);
                bookingInformationRecord.IsConfirmed = true;
                await this.bookingService.UpdateBookingInformationRecord(bookingInformationRecord);
            }

            //// Booking End
            bookingInformationRecord = await this.bookingService.GetBookingRecordById(bookingId);
            bookingInformationRecord.IsCompleted = true;
            await this.bookingService.UpdateBookingInformationRecord(bookingInformationRecord);
            ////var trans = await this.hotelBookingService.GetOrderDetailById(razorpay_order_id);
            ////trans.PaymentId = paymentId;
            ////trans.PaymentStatus = payment["status"].ToString();
            ////trans.PaymentMethod = payment["method"].ToString();
            ////trans.CardId = payment["card_id"].ToString();
            ////trans.WalletName = payment["wallet"].ToString();
            ////trans.Bank = payment["bank"].ToString();
            ////trans.ContactNo = payment["contact"].ToString();
            ////trans.Description = payment["description"].ToString();
            ////trans.Email = payment["email"].ToString();
            ////trans.ErrorCode = payment["error_code"].ToString();
            ////trans.ErrorDescription = payment["error_description"].ToString();
            ////trans.Fee = (decimal)(string.IsNullOrEmpty(payment["fee"].ToString()) ? 0 : payment["fee"]);
            ////trans.IsInternational = (bool)payment["international"];
            ////trans.PaymentDate = DateTime.Now; ////(DateTime)(string.IsNullOrEmpty(payment["created_at"].ToString()) ? null : payment["created_at"]);
            ////trans.Tax = (decimal)(string.IsNullOrEmpty(payment["tax"].ToString()) ? 0 : payment["tax"]);

            ////await this.hotelBookingService.UpdateOrderDetail(trans);
            MyBookingDescriptionViewModel modelbooking = await this.userService.GetMyBookingDescriptionByBookingId(bookingId);
            int adult = 0, child = 0, infant = 0;
            foreach (var item in model.BookingHotelRoomViewModels)
            {
                adult += item.Adult;
                child += item.Child;
                infant += item.Infant;
            }

            string company = "Hi Tours";
            string json = "{\"data\":[{" +
                          "\"Company\": \"" + company + "\"," +
                          "\"Last_Name\": \"" + model.BookingPassengerViewModels[0].LastName + "\"," +
                          "\"First_Name\": \"" + model.BookingPassengerViewModels[0].FirstName + "\"," +
                          "\"Email\": \"" + model.BookingInformationViewModel.Email + "\"," +
                          "\"State\": \"\"," +
                          "\"Deal_Name\": \"" + modelbooking.dealPackageViewModel.Name + "\"," +
                          "\"Hotel_Deal\": \"" + (modelbooking.LocationNames.Any() ? modelbooking.LocationNames[0] : string.Empty) + "\"," +
                          "\"Tour_Deal\": \"\"," +
                          "\"No_of_Adult\": \"" + adult + "\"," +
                          "\"No_of_Children\": \"" + child + "\"," +
                          "\"No_of_Infant\": \"" + infant + "\"," +
                          "\"No_of_Room\": \"" + model.BookingHotelRoomViewModels.Count + "\"" +
                          "}]," +
                          "\"trigger\": [" +
                          "\"approval\"," +
                          "\"workflow\"," +
                          "\"blueprint\"" +
                          "]}";
            var resultZoho = Services.Zoho.ZohoUpdate.GetApiResponse(json, "https://www.zohoapis.com/crm/v2/Deals");
            if (this.domainSetting.RazorpayLive)
            {
                ////send sms
                await this.SendSms(model.BookingInformationViewModel.MobileNumber, "Your happy holiday is a done deal," + bookingInformationRecord.LeadFullName + " ! Your booking reference ID is " + bookingInformationRecord.ReferenceNumber + "– thank for booking with Luxury Travel Deals. You can access this Luxury Travel Deal via your account in this link: https://www.luxurytravel.deals/user/MyBookings");
            }

            //////  sent mail--------------------
            if (this.User.Identity.IsAuthenticated && (this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor) == null || this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor).Value == string.Empty))
            {
                await this.SendBookingConfirmationMail(Convert.ToInt32(this.TempData["BookingId"]), this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value, model.BookingInformationViewModel.LeadFullName);
            }

            this.TempData["PaymentStatus"] = true;
            this.TempData["BookingAmount"] = Convert.ToDecimal(payment["amount"].ToString()) / 100;
            this.ShowMessage(Messages.BookingSuccess);
            return this.Json(new { Status = true, BookingId = bookingId });
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="bookingId">Booking Id</param>
        /// <returns>
        /// my account index view
        /// </returns>
        public async Task<ActionResult> ThankYou(int bookingId)
        {
            BookingThankYouViewModel viewModel = await this.bookingService.GetBookingThankYouById(bookingId);
            return this.View(viewModel);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="model">The identifier.</param>
        [HttpPost]
        public ActionResult GetBookingSummary(BookingSummaryViewModel model)
        {
            return this.PartialView("_BookingSummary", model);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// my account index view
        /// </returns>
        /// <param name="code">The identifier.</param>
        [HttpPost]
        public async Task<ActionResult> ApplyCoupon(string code)
        {
            PromotionModel result = await this.promotionService.GetPromotionByCode(code);
            if (result != null)
            {
                return this.Json(new
                {
                    Status = true,
                    DiscountType = result.DiscountType,
                    DiscountValue = result.DiscountValue,
                    MaxDiscount = result.MaxDiscountFlat
                });
            }
            else
            {
                return this.Json(new
                {
                    Status = false
                });
            }
        }

        /// <summary>
        /// Adds the clip board.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected void AddClipBoard(string key, object value)
        {
            if (this.ClipBoardData == null)
            {
                this.ClipBoardData = new Dictionary<string, object>();
            }

            if (this.ClipBoardData.ContainsKey(key))
            {
                this.ClipBoardData[key] = value;
            }
            else
            {
                this.ClipBoardData.Add(key, value);
            }
        }

        /// <summary>
        /// Sends the booking mail.
        /// </summary>
        /// <param name="bookingId">The hotel booking identifier.</param>
        /// <param name="toEmail">Reciepient Email Id</param>
        /// <param name="userName">User Name</param>
        /// <returns>
        /// void
        /// </returns>
        private async Task SendBookingConfirmationMail(int bookingId, string toEmail, string userName)
        {
            ////var model = await this.bookingService.GetBookingEmail(bookingId);
            BookingThankYouViewModel viewModel = await this.bookingService.GetBookingThankYouById(bookingId);
            if (this.HttpContext.Session.GetString("LeadFrom") == "lead")
            {
                var api = ZohoUpdate.Authenticate();
                var url = "https://www.zohoapis.com/crm/v2/Deals";
                var requestData = JsonConvert.SerializeObject(new
                {
                    Authorization = "Zoho-oauthtoken 1000.1db431254869bd656b21f2251a757a8a.6e21d8b631c5af13bb8a6d41f7806abb",
                    TokenId = "1000.f1001b622ca674e2bfb65b9a9e8b3cee.01b727b24df2ff102bfc34daab2c2309"
                });

                var resultZoho = ZohoUpdate.GetApiResponse(requestData, url);
                ////var resp = JsonConvert.DeserializeObject<ApiBalance>(result);
                ////return this.Json(resp);
            }

            ////var model = new BookingSendMailViewModel();
            ////model = await this.bookingService.GetBookingEmail(bookingId);
            viewModel.SiteUrl = this.configuration.GetValue<string>("AzureBlobAppSetting:ImageInitializer") + "/";
            viewModel.LeadName = userName;

            ////Customer Mail
            string result = await this.viewRenderService.RenderToStringAsync("MailTemplate/_BookingConfirmation", viewModel);
            string subject = Constants.BookingMailSubject;
            SendMail.MailSend(subject, this.Content(result).Content, toEmail, this.configuration.GetValue<string>("BookingConfirmationMailList"));
        }

        /////// <summary>
        /////// Sends the booking mail.
        /////// </summary>
        /////// <param name="bookingId">The hotel booking identifier.</param>
        /////// <param name="toEmail">Reciepient Email Id</param>
        /////// <returns>
        /////// void
        /////// </returns>
        ////private async Task SendBookingMail(int bookingId, string toEmail)
        ////{
        ////    ////var model = await this.bookingService.GetBookingEmail(bookingId);
        ////    BookingThankYouViewModel viewModel = await this.bookingService.GetBookingThankYouById(bookingId);
        ////    ////var model = new BookingSendMailViewModel();
        ////    ////model = await this.bookingService.GetBookingEmail(bookingId);
        ////    viewModel.SiteUrl = this.domainSetting.WebSiteUrl;

        ////    ////Customer Mail
        ////    var result = await this.viewRenderService.RenderToStringAsync("MailTemplate/_BookingConfirmation", viewModel);
        ////    var subject = Constants.BookingMailSubject;
        ////    SendMail.MailSend(subject, this.Content(result).Content, toEmail);
        ////}

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="secret">The secret.</param>
        private void GetKeys(out string key, out string secret)
        {
            if (this.domainSetting.RazorpayLive)
            {
                key = Constants.LiveRazorPayApiKey;
                secret = Constants.LiveRazorPaySecretKey;
            }
            else
            {
                key = Constants.TestRazorPayApiKey;
                secret = Constants.TestRazorPaySecretKey;
            }
        }

        private string ToBase26(long i)
        {
            if (i == 0)
            {
                return string.Empty;
            }

            i--;
            return this.ToBase26(i / 26) + (char)('A' + (i % 26));
        }

        private IEnumerable<string> GenerateCharacters()
        {
            long n = 0;
            while (true)
            {
                yield return this.ToBase26(++n);
            }
        }
    }
}