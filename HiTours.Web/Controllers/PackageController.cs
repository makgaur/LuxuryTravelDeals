// <copyright file="PackageController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Linq;
    using Razorpay.Api;

    /// <summary>
    /// PackageController
    /// </summary>
    /// <seealso cref="HiTours.Web.BaseController" />
    public class PackageController : BaseController
    {
        /// <summary>
        /// The package service
        /// </summary>
        private readonly IPackageService packageService;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The hotel booking service
        /// </summary>
        private readonly IHotelBookingService hotelBookingService;

        /// <summary>
        /// The user detail service
        /// </summary>
        private readonly IUserDetailService userDetailService;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// The domain setting
        /// </summary>
        private readonly DomainSetting domainSetting;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly ICityService cityService;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly ICountryService countryService;

        /// <summary>
        /// The view render service
        /// </summary>
        private readonly IViewRenderService viewRenderService;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageController" /> class.
        /// </summary>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="stateService">State Service</param>
        /// <param name="configuration">Configuratiom</param>
        /// <param name="countryService">The Country Service.</param>
        /// <param name="cityService">The City Service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="packageService">The package service.</param>
        /// <param name="viewRenderService">The view render service.</param>
        /// <param name="domainSetting">The domain setting.</param>
        /// <param name="hotelBookingService">The hotel booking service.</param>
        /// <param name="userDetailService">The user detail service.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public PackageController(IHomePageService homePageService, IStateService stateService, IConfiguration configuration, ICountryService countryService, ICityService cityService, IMapper mapper, IPackageService packageService, IViewRenderService viewRenderService, IOptions<DomainSetting> domainSetting, IHotelBookingService hotelBookingService, IUserDetailService userDetailService, IMasterService masterService, IHostingEnvironment hostingEnvironment)
            : base(mapper, homePageService, cityService, countryService, configuration, stateService)
        {
            this.configuration = configuration;
            this.cityService = cityService;
            this.countryService = countryService;
            this.packageService = packageService;
            this.hotelBookingService = hotelBookingService;
            this.userDetailService = userDetailService;
            this.masterService = masterService;
            this.domainSetting = domainSetting.Value;
            this.hostingEnvironment = hostingEnvironment;
            this.viewRenderService = viewRenderService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// my account index view
        /// </returns>
        public async Task<IActionResult> Details(string id)
        {
            //// this code for use check login user  and get emailid
            ////if (this.User.Claims.Count() > 0)
            ////{
            ////    var emailId = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;
            ////    if (emailId == null)
            ////    {
            ////        return this.Redirect("/");
            ////    }
            ////}
            ////else
            ////{
            ////    return this.Redirect("/");
            ////}

            var viewModel = new TourPackageDetailViewModel();
            var packageDetail = await this.packageService.GetTourPackageAsync(Guid.Empty, id);
            if (packageDetail == null)
            {
                return this.NotFound();
            }

            viewModel = this.Mapper.Map<TourPackageDetailViewModel>(packageDetail);

            ////this.TempData["recommended"] = await this.packageService.GetTourRecommendedDeals(viewModel.TourPackage.Id);

            return this.View(viewModel);
        }

        /// <summary>
        /// Gets the recommended deals.
        /// </summary>
        /// <param name="dealid">The dealid.</param>
        /// <returns>list of recommended deals</returns>
        public async Task<IActionResult> GetRecommendedDeals(Guid dealid)
        {
            var result = await this.packageService.GetTourRecommendedDeals(dealid);
            return this.PartialView("_RecommendedDeals", result);
        }

        /// <summary>
        /// Recentlies the view.
        /// </summary>
        /// <param name="ids">The deals identifier.</param>
        /// <returns>
        /// partial view
        /// </returns>
        public async Task<PartialViewResult> RecentlyView(string[] ids)
        {
            var recentlyViewDeals = await this.packageService.GetRecentelyViewDeals(ids);
            return this.PartialView("_DealRecentlyView", recentlyViewDeals);
        }

        /////// <summary>
        /////// Packages the reminder.
        /////// </summary>
        /////// <param name="packgaeId">The packgae identifier.</param>
        /////// <returns>
        /////// PackageReminder
        /////// </returns>
        ////[HttpPost]
        ////public async Task<IActionResult> PackageReminder(Guid packgaeId)
        ////{
        ////    if (this.HttpContext.User.Identity.IsAuthenticated)
        ////    {
        ////        var emailId = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;

        ////        var packageReminder = await this.packageService.GetPackagereminderByIdAsync(packgaeId, emailId);
        ////        if (packageReminder == null)
        ////        {
        ////            if (packgaeId != Guid.Empty && !string.IsNullOrEmpty(emailId))
        ////            {
        ////                var model = new PackageReminderModel
        ////                {
        ////                    CreatedDate = DateTime.Now,
        ////                    PackageId = packgaeId,
        ////                    UserId = emailId,
        ////                };
        ////                await this.packageService.InsertPackageReminder(model);
        ////                return this.Json(new { loginstatus = true, isremind = true, Message = Messages.Reminder });
        ////            }
        ////        }

        ////        await this.packageService.DeletePackageReminderAsync(packageReminder);
        ////        return this.Json(new { loginstatus = true, isremind = false, Message = Messages.ReminderReset });
        ////    }
        ////    else
        ////    {
        ////        return this.Json(new { loginstatus = false });
        ////    }
        ////}

        /// <summary>
        /// Gets the package calendar.
        /// </summary>
        /// <param name="packageid">The packageid.</param>
        /// <param name="rooms">The rooms.</param>
        /// <param name="packagenightid">The packagenightid.</param>
        /// <param name="packageNightsValidityId">The package nights validity identifier.</param>
        /// <param name="hotelroomtypeid">The hotelroomtypeid.</param>
        /// <returns>
        /// GetPackageCalendar
        /// </returns>
        public async Task<PartialViewResult> GetCalendar(Guid packageid, int rooms, Guid packagenightid, Guid packageNightsValidityId, int hotelroomtypeid)
        {
            var viewModel = new TourPackageDetailViewModel();

            viewModel = await this.GetCalenderInfo(packageid, packagenightid, packageNightsValidityId, hotelroomtypeid);

            viewModel.RequestedRooms = rooms;
            return this.PartialView("_Calendar", viewModel);
        }

        /// <summary>
        /// Gets the roomtypes.
        /// </summary>
        /// <param name="packageid">The packageid.</param>
        /// <param name="packagenightid">The packagenightid.</param>
        /// <returns>
        /// GetRoomtypes
        /// </returns>
        public async Task<PartialViewResult> GetRoomtypes(Guid packageid, Guid packagenightid)
        {
            var viewModel = new TourPackageDetailViewModel();
            var packageDetail = await this.packageService.GetTourPackageAsync(packageid);
            if (packageDetail != null && packageDetail.TourPackageNights != null)
            {
                viewModel = this.Mapper.Map<TourPackageDetailViewModel>(packageDetail);
                this.ViewBag.IsHotelOnly = packageDetail.TourPackage.IsHotelOnly;
                if (viewModel != null)
                {
                    viewModel.TourPackageNights = viewModel.TourPackageNights.Where(x => x.Id == packagenightid).ToList();
                    viewModel.TourPackageNights.ForEach(x =>
                    {
                        x.TourPackageNightsValidity = x.TourPackageNightsValidity ?? new List<TourPackageNightsValidityViewModel>();
                        x.TourPackageNightsValidity = x.TourPackageNightsValidity.Where(y => y.RateValidTo >= DateTime.Now).GroupBy(y => y.HotelRoomTypeId).Select(y => y.FirstOrDefault()).ToList();
                    });
                }
            }

            return this.PartialView("_HotelRoomTypes", viewModel.TourPackageNights);
        }

        /// <summary>
        /// Bookings this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// RoomBooking
        /// </returns>
        public IActionResult Booking(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }

            return this.View(id);
        }

        /// <summary>
        /// Bookings this instance.
        /// </summary>
        /// <param name="packageid">The packageid.</param>
        /// <param name="roomdetail">The roomdetail.</param>
        /// <param name="packagenightid">The packagenightid.</param>
        /// <param name="packageNightsValidityId">The package nights validity identifier.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="hotelroomtypeid">The hotelroomtypeid.</param>
        /// <param name="addflight">if set to <c>true</c> [addflight].</param>
        /// <returns>
        /// Booking
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Booking(Guid packageid, string roomdetail, Guid packagenightid, Guid packageNightsValidityId, DateTime checkInDate, DateTime checkOutDate, int hotelroomtypeid, bool addflight = false)
        {
            var passengerDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PackageRoomDetailViewModel>>(roomdetail);
            packageNightsValidityId = Guid.Empty;
            var packageDetail = await this.packageService.GetTourPackageAsync(packageid);
            if (packageDetail != null && packageDetail.TourPackageNights != null)
            {
                var tourPackageDetailViewModel = this.Mapper.Map<TourPackageDetailViewModel>(packageDetail);
                if (tourPackageDetailViewModel != null)
                {
                    tourPackageDetailViewModel.TourPackageNights = tourPackageDetailViewModel.TourPackageNights.Where(x => x.Id == packagenightid).ToList();

                    foreach (var packageNight in tourPackageDetailViewModel.TourPackageNights)
                    {
                        if (packageNight.TourPackageNightsValidity != null)
                        {
                            var checkout = checkOutDate.AddDays(-1);
                            var packageNightValidity = packageNight.TourPackageNightsValidity.OrderByDescending(x => x.TwinRateWeekDays)
                                .FirstOrDefault(x => x.HotelRoomTypeId == hotelroomtypeid && x.RateValidFrom <= checkout && x.RateValidTo >= checkout);
                            if (packageNightValidity != null)
                            {
                                packageNightsValidityId = packageNightValidity.Id;
                            }
                        }
                    }
                }
            }

            int userid = 0;
            if (this.User.Identity.IsAuthenticated)
            {
                var userClaim = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;
                if (userClaim != null)
                {
                    userid = await this.userDetailService.GetUserIdAsync(userClaim);
                }
            }

            ////var userid = await this.userDetailService.GetUserIdAsync(this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value);

            var viewModel = await this.GetCalenderInfo(packageid, packagenightid, packageNightsValidityId, hotelroomtypeid);

            var model = new HotelDealBookingViewModel()
            {
                HotelBooking = new HotelBookingViewModel(),
                PackageId = packageid,
                PackageNightId = packagenightid,
                PackageNightsValidityId = packageNightsValidityId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                TourPackageDetail = viewModel,
                UserId = userid,
                IsAddFlight = addflight,
                IsFlightProcess = packageDetail.TourPackage.IsFlightIncluded,
                HotelRoomTypeId = hotelroomtypeid
            };
            if (viewModel != null && viewModel.TourPackageNights != null)
            {
                var userRecord = await this.userDetailService.GetByIdAsync(model.UserId);
                if (model.HotelBooking == null)
                {
                    model.HotelBooking = new HotelBookingViewModel();
                }

                if (userRecord != null)
                {
                    if (string.IsNullOrEmpty(userRecord.FirstName) &&
                        string.IsNullOrEmpty(userRecord.LastName) &&
                         string.IsNullOrEmpty(userRecord.Address) &&
                        string.IsNullOrEmpty(userRecord.MobileNo))
                    {
                        model.HotelBooking.AuotUpdateInfo = true;
                    }

                    model.HotelBooking.FirstName = userRecord.FirstName;
                    model.HotelBooking.LastName = userRecord.LastName;
                    model.HotelBooking.Email = userRecord.EmailId;
                    model.HotelBooking.Mobile = userRecord.MobileNo;
                    model.HotelBooking.CountryId = (short)(userRecord.CountryId ?? Constants.DefaultCountry);
                    model.HotelBooking.City = userRecord.City;
                    if (short.TryParse(userRecord.City, out short cityid))
                    {
                        model.HotelBooking.CityId = cityid;
                    }

                    model.HotelBooking.BillingAddress = userRecord.Address;
                    model.HotelBooking.PinCode = userRecord.ZipCode;
                }

                model.PassengerDetails = new List<PassengerDetailsViewModel>();
                await this.AddPassengerDetail(passengerDetail, model.PassengerDetails);
                ////await this.AddPassenger(adults, Enums.PersonType.Adult, model.PassengerDetails);
                ////await this.AddPassenger(childs, Enums.PersonType.Child, model.PassengerDetails);
                ////await this.AddPassenger(infants, Enums.PersonType.Infant, model.PassengerDetails);
                model.TotalAdults = model.PassengerDetails.Sum(x => x.PersonDetails.Count());

                await this.CalculateHotelDetails(model, checkInDate, checkOutDate);

                await this.BindSelectList(model);
                return this.PartialView("_Information", model);
            }

            return this.PartialView("_Information", model);
        }

        /// <summary>
        /// Holidays the price.
        /// </summary>
        /// <param name="price">The price.</param>
        /// <param name="weekendprice">The weekendprice.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="dates">The dates.</param>
        /// <param name="deposit">The deposit.</param>
        /// <returns>
        /// Update Holiday Price Partial view
        /// </returns>
        public IActionResult HolidayPrice(decimal price, decimal weekendprice, string checkInDate, string checkOutDate, string dates, decimal deposit)
        {
            var model = new HolidayPriceViewModel();
            var adults = 0;
            foreach (var date in dates.Split(','))
            {
                if (date.Length > 0)
                {
                    var personType = this.GetPersonTypeByDateOfBirth(Convert.ToDateTime(date));
                    if (personType == Enums.PersonType.Adult)
                    {
                        adults++;
                    }
                }
                else
                {
                    adults++;
                }
            }

            model.BookingPrice = adults * price;

            for (DateTime i = Convert.ToDateTime(checkInDate); i < Convert.ToDateTime(checkOutDate); i = i.AddDays(1))
            {
                if (i.DayOfWeek == DayOfWeek.Friday || i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
                {
                    model.WeekendPrice = adults * weekendprice;
                    break;
                }
            }

            model.GstAmount = decimal.Parse((((model.WeekendPrice + model.BookingPrice) * Constants.GstInPercent) / 100).ToAmount(true));
            model.AdultsCount = adults;
            model.DepositAmount = adults * deposit;
            model.DepositAmount = model.DepositAmount <= 0 ? deposit : model.DepositAmount;
            return this.PartialView("_HolidayPrice", model);
        }

        /// <summary>
        /// Bookings the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Booking Hotel</returns>
        [HttpPost]
        public async Task<JsonResult> BookNow(HotelDealBookingViewModel model)
        {
            ////var invalid = this.ModelState.Values.Select(x => x.Errors.Count > 0);
            Dictionary<string, object> response = new Dictionary<string, object>();
            if (this.ModelState.IsValid)
            {
                ////var viewModel = await this.GetPackageInfo(model.PackageId, model.PackageNightId, model.PackageNightsValidityId);
                var viewModel = await this.GetCalenderInfo(model.PackageId, model.PackageNightId, model.PackageNightsValidityId, model.HotelRoomTypeId);
                if (viewModel != null)
                {
                    model.TourPackageDetail = viewModel;
                }

                var bookingCountry = await this.masterService.GetPackageCountryByIdAsync(model.HotelBooking.CountryId);
                var bookingCity = await this.masterService.GetPackageCityByIdAsync((short)model.HotelBooking.CityId);

                model.HotelBooking.City = bookingCity != null ? bookingCity.Name : string.Empty;
                model.HotelBooking.Mobile = model.HotelBooking.Mobile.Length > 10 ? model.HotelBooking.Mobile.Substring(0, 10) : model.HotelBooking.Mobile;
                if (model.PassengerDetails == null)
                {
                    model.PassengerDetails = new List<PassengerDetailsViewModel>();
                }

                ////foreach (var item in model.PassengerDetails)
                ////{
                ////    item.PersonDetails.RemoveAll(x => string.IsNullOrEmpty(x.FirstName) && string.IsNullOrEmpty(x.LastName));
                ////}

                ////model.PassengerDetails.ForEach(x =>
                ////{
                ////    x.PersonDetails = x.PersonDetails ?? new List<HotelBookingPersonDetailViewModel>();
                ////    x.PersonDetails.ForEach(y =>
                ////    {
                ////        y.Email = model.HotelBooking.Email;
                ////        ////y.PersonType = this.GetPersonTypeByDateOfBirth(y.DateOfBirth).ToString();
                ////        ////x.PersonType = (int)this.GetPersonTypeByDateOfBirth(y.DateOfBirth);
                ////        y.Mobile = model.HotelBooking.Mobile;
                ////        y.BillingAddress = model.HotelBooking.BillingAddress;
                ////        y.PinCode = model.HotelBooking.PinCode;
                ////        y.CountryCode = bookingCountry != null ? bookingCountry.SortName : string.Empty;
                ////        y.Country = bookingCountry != null ? bookingCountry.Name : string.Empty;
                ////        y.CityCode = bookingCity != null ? bookingCity.Code : string.Empty;
                ////        y.City = bookingCity != null ? bookingCity.Name : string.Empty;
                ////    });
                ////});

                ////if (model.PassengerDetails.FirstOrDefault(x => x.PersonType == (int)Enums.PersonType.Adult) == null)
                ////{
                ////    this.ShowMessage("Atleast One Passenger should be Adult.", Enums.MessageType.Warning);
                ////    return this.View("Booking", model.PackageId);
                ////}

                var canBook = viewModel != null;

                var bookingOnDates = new List<DateTime>();
                var checkOutDate = model.CheckOutDate.AddDays(-1);
                var checkInDate = model.CheckInDate;
                while (checkInDate <= checkOutDate)
                {
                    bookingOnDates.Add(checkInDate);
                    checkInDate = checkInDate.AddDays(1);
                }

                var userid = 0;
                var user = await this.userDetailService.GetUserProfileByEmailId(model.HotelBooking.Email);
                if (user == null)
                {
                    UserDetailModel userModel = new UserDetailModel();
                    userModel.FirstName = model.HotelBooking.FirstName;
                    userModel.LastName = model.HotelBooking.LastName;
                    userModel.MobileNo = model.HotelBooking.Mobile;
                    userModel.Address = model.HotelBooking.BillingAddress;
                    userModel.ZipCode = model.HotelBooking.PinCode;
                    userModel.City = model.HotelBooking.CityId.ToString();
                    userModel.CountryId = model.HotelBooking.CountryId;
                    userModel.EmailId = model.HotelBooking.Email;
                    var record = this.mapper.Map<UserDetailModel>(userModel);
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    record.IsActive = true;
                    record.IsActive = true;
                    record.IsGuest = true;
                    var isAlreadyGuest = false;
                    if (!isAlreadyGuest)
                    {
                        await this.userDetailService.InsertAsync(record);
                        user = await this.userDetailService.GetUserProfileByEmailId(model.HotelBooking.Email);
                    }
                }

                if (model.HotelBooking != null && model.HotelBooking.AuotUpdateInfo)
                {
                    var userDeatils = await this.userDetailService.GetByIdAsync(userid);
                    if (userDeatils != null)
                    {
                        userDeatils.FirstName = model.HotelBooking.FirstName;
                        userDeatils.LastName = model.HotelBooking.LastName;
                        userDeatils.MobileNo = model.HotelBooking.Mobile;
                        userDeatils.Gender = userDeatils.Gender;
                        userDeatils.DateOfBirth = userDeatils.DateOfBirth;
                        userDeatils.NationalityId = userDeatils.NationalityId;
                        userDeatils.Address = model.HotelBooking.BillingAddress;
                        userDeatils.ZipCode = model.HotelBooking.PinCode;
                        userDeatils.City = model.HotelBooking.CityId.ToString();
                        userDeatils.CountryId = model.HotelBooking.CountryId;
                        userDeatils.PhoneNumber = userDeatils.PhoneNumber;
                        userDeatils.PassportNo = userDeatils.PassportNo;
                        userDeatils.CountryofIssueId = userDeatils.CountryofIssueId;
                        userDeatils.ExpiryDate = userDeatils.ExpiryDate;
                        userDeatils.EmailId = userDeatils.EmailId;
                        await this.userDetailService.UpdateAsync(userDeatils);
                    }
                }

                var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                claimIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, model.HotelBooking.Email));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, Enums.RoleType.User.ToString()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, (user.FirstName + " " + user.LastName).Trim()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Actor, string.Empty));

                await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                model.UserId = user.Id;
                model.CreatedBy = new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
                ////model.TotalAdults = model.PassengerDetails.Count;
                model.BookingPrice = 0;
                await this.CalculateHotelDetails(model, model.CheckInDate, model.CheckOutDate);
                if (model.PaidAmount <= 0)
                {
                    this.ShowMessage("Deposit Amount must be greater than zero.", Enums.MessageType.Warning);
                    response = new Dictionary<string, object>();
                    response.Add("ErrorCode", "Deposit Amount must be greater than zero.");
                    return this.Json(response);
                }

                if (model.BookedOnDates != null && model.BookedOnDates.Count > 0)
                {
                    foreach (var date in bookingOnDates)
                    {
                        var bookOnDate = model.BookedOnDates.FirstOrDefault(x => x.BookedDate == date);
                        if (bookOnDate != null && bookOnDate.AvailableRoom < model.Rooms && canBook)
                        {
                            canBook = false;
                        }

                        if (!canBook)
                        {
                            break;
                        }
                    }
                }

                if (model.BookingPrice > 0 && canBook)
                {
                    model.BookingPackageType = Enums.CurrentDealType.Package.ToString();
                    model.HotelPriceId = model.PackageNightsValidityId;
                    var hotelBookingId = await this.hotelBookingService.Book(model);

                    if (!model.IsAddFlight)
                    {
                        var input = new Dictionary<string, object>
                    {
                        { "amount", model.PaidAmount * 100 }, // this amount should be same as transaction amount
                        { "currency", Constants.RazorPayCurrency },
                        { "receipt", "12121" },
                        { "payment_capture", 1 }
                    };

                        this.GetKeys(out string key, out string secret);

                        var client = new RazorpayClient(key, secret);

                        var order = client.Order.Create(input);

                        var orderId = order["id"].ToString();
                        var currency = order["currency"].ToString();
                        var receipt = order["receipt"].ToString();
                        var status = order["status"].ToString();

                        var userTransaction = new UserTransactionModel()
                        {
                            UserId = model.UserId,
                            HotelBookingId = hotelBookingId,
                            OrderId = orderId,
                            Currency = currency,
                            ReceiptNo = receipt,
                            OrderStatus = status
                        };

                        userTransaction.SetAuditInfo(model.CreatedBy);

                        await this.hotelBookingService.OrderGenerate(userTransaction);

                        var userdetail = await this.userDetailService.GetByIdAsync(userTransaction.UserId);

                        var orderDetail = new BookingPayment
                        {
                            ////DataAmount = (int)((model.BookingPrice + model.GstAmount) * 100),
                            DataAmount = (int)(model.PaidAmount * 100),
                            DataDescription = string.Empty,
                            DataImage = Constants.RazorPayDataImage, ////"https://razorpay.com/favicon.png",
                            DataKey = key,
                            DataName = Constants.RazorPay,
                            DataOrderId = userTransaction.OrderId,
                            DataPrefillContact = userdetail.MobileNo,
                            DataPrefillEmail = userdetail.EmailId,
                            DataPrefillName = userdetail.FirstName + " " + userdetail.LastName,
                            DataThemeColor = Constants.RazorPayDataThemeColor,
                            JsSrc = Constants.RazorPayCheckoutJs
                        };

                        orderDetail.CheckInDate = model.CheckInDate;
                        orderDetail.CheckOutDate = model.CheckOutDate;
                        orderDetail.Nights = model.Nights;
                        orderDetail.Days = model.Days;
                        orderDetail.TotalAdults = model.TotalAdults;
                        orderDetail.AdultDescriptions = model.AdultDescriptions;
                        orderDetail.BookingPrice = model.BookingPrice;
                        orderDetail.WeekendPrice = model.WeekendPrice;
                        orderDetail.GstAmount = model.GstAmount;
                        orderDetail.DepositAmount = model.DepositeAmount;
                        if (model.TourPackageDetail.TourPackageImages.Count > 0)
                        {
                            orderDetail.PackageImage = model.TourPackageDetail.TourPackageImages.OrderBy(x => x.SequenceNo).FirstOrDefault().ImageName;
                        }

                        orderDetail.HotelName = model.TourPackageDetail.TourPackage.PackageName;
                        orderDetail.CityName = model.TourPackageDetail.CityName;
                        orderDetail.CountryName = model.TourPackageDetail.CountryName;
                        orderDetail.DealQuotes = model.TourPackageDetail.TourPackage.Quote;
                        ////orderDetail.HotelBooking = model.HotelBooking;
                        orderDetail.Salutation = model.HotelBooking.Salutation;
                        orderDetail.FirstName = model.HotelBooking.FirstName;
                        orderDetail.LastName = model.HotelBooking.LastName;
                        orderDetail.Email = model.HotelBooking.Email;
                        orderDetail.Mobile = model.HotelBooking.Mobile;
                        this.TempData.Put("orderdetail", orderDetail);
                        ////--------------------------------------
                        response = new Dictionary<string, object>();
                        response.Add("ErrorCode", "0");
                        response.Add("Result", orderDetail);
                        return this.Json(response);
                    }
                    else
                    {
                        response = new Dictionary<string, object>();
                        response.Add("ErrorCode", "Flight");
                        return this.Json(response);
                        ////return this.RedirectToAction("SearchFlights", "AirService", new { bi = hotelBookingId });
                    }

                    ////this.ShowMessage(Messages.BookingSuccess);
                    ////return this.RedirectToRoute(Constants.RouteDefault, new { controller = "Account", action = "MyBooking" });
                }
                else
                {
                    this.ShowMessage("Some Rooms Not Available on Selected Dates.", Enums.MessageType.Warning);
                    ////return this.RedirectToRoute(Constants.RouteDefault, new { controller = "Package", action = "Details", id = model.PackageId });
                    response = new Dictionary<string, object>();
                    response.Add("ErrorCode", "Some Rooms Not Available on Selected Dates.");
                    return this.Json(response);
                }
            }

            await this.BindSelectList(model);

            this.ViewBag.PackageNameForUrl = SpecialChars.Remove(model.Package.PackageName);
            response = new Dictionary<string, object>();
            response.Add("ErrorCode", "Data Not valid");
            return this.Json(response);
            ////return this.View("Booking", model.PackageId);
        }

        /// <summary>
        /// Payments the specified model.
        /// </summary>
        /// <returns>
        /// view
        /// </returns>
        public IActionResult Payment()
        {
            BookingPayment orderdetail = this.TempData.Get<BookingPayment>("orderdetail");

            if (orderdetail == null)
            {
                this.ShowMessage(Messages.BookingFailed, Enums.MessageType.Warning);
                if (this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Actor) != null && this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Actor).Value != string.Empty)
                {
                    return this.RedirectToRoute(Constants.RouteDefault, new { controller = "Home", action = "Index" });
                }
                else
                {
                    return this.RedirectToRoute(Constants.RouteDefault, new { controller = "Account", action = "MyBooking" });
                }
            }

            return this.View("payment", orderdetail);
        }

        /// <summary>
        /// Talktoes the expert.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>view</returns>
        public async Task<IActionResult> TalktoExpert(TalkToExpertViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var htmlBody = string.Empty;
                var filePath = Path.Combine(this.hostingEnvironment.WebRootPath + "/Templates/", "TalktoExpert.html");
                var currentUrl = this.HttpContext.Request.Host.ToString().ToLower();
                using (StreamReader sourceReader = System.IO.File.OpenText(filePath))
                {
                    htmlBody = await sourceReader.ReadToEndAsync();
                    htmlBody = htmlBody.Replace("##HOSTURL##", this.domainSetting.WebSiteUrl);
                    htmlBody = htmlBody.Replace("##Name##", model.Name);
                    htmlBody = htmlBody.Replace("##Mobile##", model.Mobile);
                    htmlBody = htmlBody.Replace("##Email##", model.Email);
                    htmlBody = htmlBody.Replace("##RequestFrom##", model.PageUrl);
                    ////htmlBody = htmlBody.Replace("##City##", model.City);
                    ////htmlBody = htmlBody.Replace("##More##", model.More);
                }

                var subject = "Talk to Expert";

                SendMail.MailSend(subject, htmlBody, Constants.RequestCallBackAdminEmail);

                return this.Json(new { Status = true, Message = Messages.Requestcallback });
            }

            return this.Json(new { Status = false, Message = "Invalid" });
        }

        /// <summary>
        /// Bookings the confirmation.
        /// </summary>
        /// <param name="razorpay_payment_id">The Payment Id.</param>
        /// <param name="razorpay_order_id">The Order Id.</param>
        /// <param name="razorpay_signature">The Signature.</param>
        /// <returns>
        /// view
        /// </returns>
        public async Task<IActionResult> BookingConfirmation(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            string paymentId = razorpay_payment_id;

            ////var input = new Dictionary<string, object>
            ////{
            ////    { "amount", 1000 } // this amount should be same as transaction amount
            ////};

            this.GetKeys(out string key, out string secret);

            var client = new RazorpayClient(key, secret);

            var attributes = new Dictionary<string, string>
                {
                { "razorpay_payment_id", paymentId },
                { "razorpay_order_id", razorpay_order_id },
                { "razorpay_signature", razorpay_signature }
                };

            Utils.verifyPaymentSignature(attributes);

            var outer = JToken.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(client.Payment.Fetch(paymentId)));
            var payment = outer["Attributes"].Value<JObject>();

            var trans = await this.hotelBookingService.GetOrderDetailById(razorpay_order_id);
            trans.PaymentId = paymentId;
            trans.PaymentStatus = payment["status"].ToString();
            trans.PaymentMethod = payment["method"].ToString();
            trans.CardId = payment["card_id"].ToString();
            trans.WalletName = payment["wallet"].ToString();
            trans.Bank = payment["bank"].ToString();
            trans.ContactNo = payment["contact"].ToString();
            trans.Description = payment["description"].ToString();
            trans.Email = payment["email"].ToString();
            trans.ErrorCode = payment["error_code"].ToString();
            trans.ErrorDescription = payment["error_description"].ToString();
            trans.Fee = (decimal)(string.IsNullOrEmpty(payment["fee"].ToString()) ? 0 : payment["fee"]);
            trans.IsInternational = (bool)payment["international"];
            trans.PaymentDate = DateTime.Now; ////(DateTime)(string.IsNullOrEmpty(payment["created_at"].ToString()) ? null : payment["created_at"]);
            trans.Tax = (decimal)(string.IsNullOrEmpty(payment["tax"].ToString()) ? 0 : payment["tax"]);

            await this.hotelBookingService.UpdateOrderDetail(trans);
            if (this.domainSetting.RazorpayLive)
            {
                ////send sms
                await this.SendSms();
            }

            //////  sent mail--------------------
            await this.SendBookingMail(trans.HotelBookingId ?? Guid.Empty);
            this.TempData["PaymentStatus"] = true;
            this.TempData["BookingAmount"] = Convert.ToDecimal(payment["amount"].ToString()) / 100;
            this.ShowMessage(Messages.BookingSuccess);
            return this.RedirectToRoute(Constants.RouteDefault, new { controller = "Account", action = "MyBooking" });
        }

        /// <summary>
        /// Rooms the detail.
        /// </summary>
        /// <param name="roomcount">The roomcount.</param>
        /// <param name="adultcount">The adultcount.</param>
        /// <param name="childcount">The childcount.</param>
        /// <returns>
        /// partial view
        /// </returns>
        public IActionResult RoomDetail(short roomcount, short adultcount, short childcount)
        {
            return this.PartialView("_RoomDetails", new { roomcount, adultcount, childcount });
        }

        /// <summary>
        /// Sends the booking mail.
        /// </summary>
        /// <param name="hotelBookingId">The hotel booking identifier.</param>
        /// <returns>
        /// void
        /// </returns>
        private async Task SendBookingMail(Guid hotelBookingId)
        {
            var model = await this.hotelBookingService.GetTourBookingSendMailDetailAsync(hotelBookingId);
            model.SiteUrl = this.domainSetting.WebSiteUrl;

            ////Customer Mail
            model.IsContactDetail = false;
            var result = await this.viewRenderService.RenderToStringAsync("MailTemplate/_Booking", model);
            var subject = Constants.BookingMailSubject;
            SendMail.MailSend(subject, this.Content(result).Content, model.Email.Trim());
            if (this.domainSetting.RazorpayLive)
            {
                ////Admin Mail
                model.IsContactDetail = true;
                var result2 = await this.viewRenderService.RenderToStringAsync("MailTemplate/_Booking", model);
                SendMail.MailSend(subject, this.Content(result2).Content, Constants.AdminEmailId);
            }
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <returns>void</returns>
        private async Task SendSms()
        {
            try
            {
                var webClient = new WebClient();
                string to, message;
                to = Constants.SmsTo;
                message = Constants.SmsMessage;
                string baseURL = "http://sms.hspsms.com/sendSMS?username=HiTours&apikey=b97b2163-becd-4f3f-8500-a1146fb9e488&sendername=HITOUR&smstype=TRANS&numbers=" + to + "&message=" + message;
                await webClient.OpenReadTaskAsync(baseURL);
            }
            catch
            {
            }
        }

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

        /// <summary>
        /// Binds the select list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>BindSelectList</returns>
        private async Task BindSelectList(HotelDealBookingViewModel model)
        {
            model.HotelBooking.Countries = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, (short)model.HotelBooking.CountryId)).ToSelectList();
            if (model.HotelBooking.CityId > 0)
            {
                model.HotelBooking.Citites = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, (short)model.HotelBooking.CityId, 0)).ToSelectList();
            }
        }

        /// <summary>
        /// Calculates the hotel details.
        /// </summary>
        /// <param name="hotelBooking">The hotel booking.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <returns>
        /// CalculateHotelDetails
        /// </returns>
        private async Task CalculateHotelDetails(HotelDealBookingViewModel hotelBooking, DateTime checkInDate, DateTime checkOutDate)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (hotelBooking.UserId != 0)
                {
                    var userEmail = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;
                    var userid = await this.userDetailService.GetUserIdAsync(userEmail);
                }
            }

            if (hotelBooking != null && hotelBooking.TourPackageDetail != null &&
                hotelBooking.TourPackageDetail.TourPackageNights != null &&
                hotelBooking.TourPackageDetail.TourPackageNights.Count > 0)
            {
                hotelBooking.CheckInDate = checkInDate;
                hotelBooking.CheckOutDate = checkOutDate;
                hotelBooking.Nights = checkOutDate.Subtract(checkInDate).Days;
                hotelBooking.Days = hotelBooking.Nights + 1;

                var tourpackage = hotelBooking.TourPackageDetail ?? new TourPackageDetailViewModel();
                var tourPackageNights = tourpackage.TourPackageNights ?? new List<TourPackageNightViewModel>();
                var tourpackageNight = tourPackageNights.FirstOrDefault() ?? new TourPackageNightViewModel();
                var tourPackageNightValidities = tourpackageNight.TourPackageNightsValidity ?? new List<TourPackageNightsValidityViewModel>();
                var tourPackageNightValidity = tourPackageNightValidities.Where(x => (checkInDate >= x.RateValidFrom && checkInDate <= x.RateValidTo)
                    || (checkOutDate <= x.RateValidTo && checkOutDate >= x.RateValidFrom)).OrderByDescending(x => x.TwinRateWeekDays).FirstOrDefault() ?? new TourPackageNightsValidityViewModel();

                if (tourPackageNightValidity != null && tourpackageNight != null)
                {
                    int roomCount = 1;
                    if (hotelBooking.PassengerDetails != null)
                    {
                        roomCount = hotelBooking.PassengerDetails.Max(x => x.RoomNumber);
                        hotelBooking.Rooms = roomCount;
                    }

                    ////int adults = hotelBooking.PassengerDetails.Count(x => x.PersonType == (int)Enums.PersonType.Adult);
                    ////int childs = hotelBooking.PassengerDetails.Count(x => x.PersonType == (int)Enums.PersonType.Child);
                    ////int infants = hotelBooking.PassengerDetails.Count(x => x.PersonType == (int)Enums.PersonType.Infant);
                    tourPackageNightValidity.RateTypeApplied = (int)Enums.RateTypeApplied.Double;

                    hotelBooking.RoomPrice = tourPackageNightValidity.TwinRateWeekDays;
                    if (!hotelBooking.TourPackageDetail.TourPackage.IsHotelOnly)
                    {
                        if (tourPackageNightValidity.MaxAdult <= hotelBooking.PassengerDetails.Count())
                        {
                            hotelBooking.BookingPrice = tourPackageNightValidity.TwinRateWeekDays;
                        }
                    }
                    else
                    {
                        hotelBooking.BookingPrice = roomCount * tourPackageNightValidity.TwinRateWeekDays;
                    }

                    for (DateTime i = checkInDate; i < checkOutDate; i = i.AddDays(1))
                    {
                        if (i.DayOfWeek == DayOfWeek.Friday || i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
                        {
                            hotelBooking.WeekendPrice = roomCount * tourPackageNightValidity.TwinRateWeekend;
                            break;
                        }
                    }

                    if (hotelBooking.TourPackageDetail.TourPackage.IsHotelOnly)
                    {
                        hotelBooking.GstAmount = 0;
                        hotelBooking.GstPercent = 0;
                    }
                    else
                    {
                         hotelBooking.GstAmount = decimal.Parse((((hotelBooking.WeekendPrice + hotelBooking.BookingPrice) * Constants.GstInPercent) / 100).ToAmount(true));
                        hotelBooking.GstPercent = Constants.GstInPercent;
                    }

                    if (tourpackageNight.DepositAmount.HasValue && tourpackageNight.DepositAmount.Value > 0)
                    {
                        hotelBooking.DepositeAmount = hotelBooking.PaidAmount = roomCount * tourpackageNight.DepositAmount.Value;
                    }
                    else
                    {
                        hotelBooking.PaidAmount = hotelBooking.BookingPrice + hotelBooking.WeekendPrice + hotelBooking.GstAmount;
                    }

                    ////hotelBooking.PaidAmount = tourpackageNight.DepositAmount;
                }
            }
        }

        private async Task<decimal> GetBookingPrice(DateTime startDate, DateTime endDate, decimal roomPrice, Guid packageid)
        {
            var priceOnDates = new List<RoomPriceOnDate>();
            var specificDatePrice = await this.packageService.GetSpecificDatePriceAsync(packageid);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var price = roomPrice;

                if (specificDatePrice != null && specificDatePrice.FirstOrDefault(x => x.Date.Date == date.Date) != null)
                {
                    price = specificDatePrice.FirstOrDefault(x => x.Date.Date == date.Date).Price;
                }

                priceOnDates.Add(new RoomPriceOnDate
                {
                    Date = date,
                    Price = price
                });
            }

            return priceOnDates.Sum(x => x.Price);
        }

        private async Task<TourPackageDetailViewModel> GetCalenderInfo(Guid packageid, Guid packagenightid, Guid packageNightsValidityId, int hotelRoomTypeId)
        {
            var viewModel = new TourPackageDetailViewModel();
            var packageDetail = await this.packageService.GetTourPackageAsync(packageid);
            if (packageDetail != null && packageDetail.TourPackageNights != null)
            {
                viewModel = this.Mapper.Map<TourPackageDetailViewModel>(packageDetail);
                if (viewModel != null)
                {
                    viewModel.TourPackageNights = viewModel.TourPackageNights.Where(x => x.Id == packagenightid).ToList();

                    foreach (var packageNight in viewModel.TourPackageNights)
                    {
                        if (packageNight.TourPackageNightsValidity != null)
                        {
                            packageNight.TourPackageNightsValidity = packageNight.TourPackageNightsValidity.Where(x => x.HotelRoomTypeId == hotelRoomTypeId).OrderBy(x => x.RateValidFrom).ToList();
                            foreach (var item in packageNight.TourPackageNightsValidity)
                            {
                                item.BookedOnDates = await this.packageService.GetPackageBNightAvailabilityAsync(item.Id);
                            }
                        }
                    }
                }
            }

            return viewModel;
        }

        private async Task<TourPackageDetailViewModel> GetPackageInfo(Guid packageid, Guid packagenightid, Guid packagenightvalidityid)
        {
            var viewModel = new TourPackageDetailViewModel();
            var packageDetail = await this.packageService.GetTourPackageAsync(packageid);
            if (packageDetail != null && packageDetail.TourPackageNights != null)
            {
                viewModel = this.Mapper.Map<TourPackageDetailViewModel>(packageDetail);
            }

            return viewModel;
        }

        /////// <summary>
        /////// Adds the passenger.
        /////// </summary>
        /////// <param name="personCount">The person count.</param>
        /////// <param name="personType">Type of the person.</param>
        /////// <param name="passengers">The passengers.</param>
        /////// <returns>AddPassenger</returns>
        ////private async Task<List<PassengerDetailsViewModel>> AddPassenger(int personCount, Enums.PersonType personType, List<PassengerDetailsViewModel> passengers)
        ////{
        ////    var userid = await this.userDetailService.GetUserIdAsync(this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value);
        ////    var userDeatils = await this.userDetailService.GetByIdAsync(userid);

        ////    passengers = passengers ?? new List<PassengerDetailsViewModel>();

        ////    for (int i = 0; i < personCount; i++)
        ////    {
        ////        var passengerDetail = new PassengerDetailsViewModel()
        ////        {
        ////            PersonType = (int)personType,
        ////            Count = i + 1
        ////        };
        ////        if (passengerDetail.PersonDetails == null)
        ////        {
        ////            passengerDetail.PersonDetails = new List<HotelBookingPersonDetailViewModel>();
        ////        }

        ////        var personDetail = new HotelBookingPersonDetailViewModel()
        ////        {
        ////            RoomNo = Convert.ToInt16(i + 1),
        ////            PersonType = personType.ToString()
        ////        };
        ////        if (i == 0 && userDeatils != null)
        ////        {
        ////            personDetail.FirstName = userDeatils.FirstName;
        ////            personDetail.LastName = userDeatils.LastName;
        ////            if (userDeatils.DateOfBirth != null && userDeatils.DateOfBirth != DateTime.MinValue)
        ////            {
        ////                personDetail.DateOfBirth = Convert.ToDateTime(userDeatils.DateOfBirth);
        ////            }
        ////        }

        ////        passengerDetail.PersonDetails.Add(personDetail);

        ////        passengers.Add(passengerDetail);
        ////    }

        ////    return passengers;
        ////}

        private async Task<List<PassengerDetailsViewModel>> AddPassengerDetail(List<PackageRoomDetailViewModel> roomdetail, List<PassengerDetailsViewModel> passengers)
        {
            passengers = passengers ?? new List<PassengerDetailsViewModel>();

            foreach (var item in roomdetail)
            {
                var personDetail = new List<HotelBookingPersonDetailViewModel>();
                for (int i = 0; i < item.Adult; i++)
                {
                    personDetail.Add(new HotelBookingPersonDetailViewModel
                    {
                        RoomNo = item.RoomNo,
                        PersonType = Enums.PersonType.Adult.ToString()
                    });
                }

                for (int i = 0; i < item.Child; i++)
                {
                    personDetail.Add(new HotelBookingPersonDetailViewModel
                    {
                        RoomNo = item.RoomNo,
                        PersonType = Enums.PersonType.Child.ToString()
                    });
                }

                var passengerDetail = new PassengerDetailsViewModel()
                {
                    Count = item.Adult + item.Child,
                    RoomNumber = item.RoomNo,
                    PersonDetails = personDetail,
                };

                passengers.Add(passengerDetail);
            }

            return await Task.Run(() => passengers);
        }

        private Enums.PersonType GetPersonTypeByDateOfBirth(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age = age - 1;
            }

            var personType = Enums.PersonType.Adult;

            if (age > 12)
            {
                personType = Enums.PersonType.Adult;
            }
            else if (age > 2 && age < 12)
            {
                personType = Enums.PersonType.Child;
            }
            else if (age <= 2)
            {
                personType = Enums.PersonType.Infant;
            }

            return personType;
        }
    }
}