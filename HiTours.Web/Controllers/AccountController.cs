// <copyright file="AccountController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Account controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class AccountController : BaseController
    {
        /// <summary>
        /// The user detail service
        /// </summary>
        private readonly IUserDetailService userDetailService;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// The application user service
        /// </summary>
        private readonly IApplicationUserService applicationUserService;

        private readonly IHotelBookingService hotelBookingService;

        private readonly IFlightBookingService flightBookingService;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly ICityService cityService;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly ICountryService countryService;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="stateService">State Service</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="countryService">The Country Service.</param>
        /// <param name="cityService">The City Service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userDetailService">The user detail service.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="applicationUserService">The application user service.</param>
        /// <param name="hotelBookingService">The hotel booking service.</param>
        /// <param name="flightBookingService">The flight booking service.</param>
        public AccountController(IHomePageService homePageService, IStateService stateService, IConfiguration configuration, ICountryService countryService, ICityService cityService, IMapper mapper, IUserDetailService userDetailService, IMasterService masterService, IServiceProvider serviceProvider, IApplicationUserService applicationUserService, IHotelBookingService hotelBookingService, IFlightBookingService flightBookingService)
            : base(mapper, homePageService, cityService, countryService, configuration, stateService)
        {
            this.configuration = configuration;
            this.cityService = cityService;
            this.countryService = countryService;
            this.userDetailService = userDetailService;
            this.masterService = masterService;
            this.serviceProvider = serviceProvider;
            this.applicationUserService = applicationUserService;
            this.hotelBookingService = hotelBookingService;
            this.flightBookingService = flightBookingService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>my account index view</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Users the profile.
        /// </summary>
        /// <returns>
        /// MyAccount
        /// </returns>
        public IActionResult MyInformation()
        {
            return this.View();
        }

        /// <summary>
        /// Mies the information.
        /// </summary>
        /// <returns>
        /// GetMyInformation
        /// </returns>
        [HttpPost]
        public async Task<PartialViewResult> GetMyInformation()
        {
            var emailId = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;
            var result = await this.userDetailService.GetUserProfileByEmailId(emailId);

            await this.BindSelectList(result);

            return this.PartialView("_MyInformation", result);
        }

        /// <summary>
        /// Updates my information.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// UpdateMyInformation
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> UpdateMyInformation(MyInformationViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userDeatils = await this.userDetailService.GetByIdAsync(model.Id);
                if (userDeatils != null)
                {
                    userDeatils.FirstName = model.FirstName;
                    userDeatils.LastName = model.LastName;
                    userDeatils.MobileNo = model.MobileNo;
                    userDeatils.Gender = model.Gender;
                    userDeatils.DateOfBirth = model.DateOfBirth;
                    userDeatils.NationalityId = model.NationalityId;
                    userDeatils.Address = model.Address;
                    userDeatils.ZipCode = model.ZipCode;
                    userDeatils.City = model.City;
                    userDeatils.CountryId = model.CountryId;
                    userDeatils.PhoneNumber = model.PhoneNumber;
                    userDeatils.PassportNo = model.PassportNo;
                    userDeatils.CountryofIssueId = model.CountryofIssueId;
                    userDeatils.ExpiryDate = model.ExpiryDate;
                    await this.userDetailService.UpdateAsync(userDeatils);
                }

                return this.Json(new { Status = true, Message = Messages.UpdateSuccessfully, RedirectUrl = "/Account/MyInformation" });
            }

            return this.Json(new { Status = false, Message = "Invalid validation error show" });
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnPath">The return path.</param>
        /// <returns>
        /// ChangePassword
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> UserChangePassword(ApplicationUserChangePasswordViewModel model, string returnPath)
        {
            var userRecord = false;
            var emailid = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;
            if (this.User.Identities.FirstOrDefault().AuthenticationType == Constants.ApplicationCookies)
            {
                userRecord = await this.userDetailService.UserChangePassword(emailid, model.OldPassword, model.NewPassword);
            }
            else
            {
                userRecord = await this.userDetailService.UserSetPassword(emailid, model.NewPassword);
            }

            if (userRecord)
            {
                return this.Json(new { Status = true, Message = Messages.PasswordChangeSuccess });
            }
            else
            {
                return this.Json(new { Status = false, Message = Messages.InvalidOldPassword });
            }
        }

        /// <summary>
        /// Mies the account.
        /// </summary>
        /// <returns>MyAccount</returns>
        public IActionResult HelpMyAccount()
        {
            return this.View();
        }

        /// <summary>
        /// Whats the next.
        /// </summary>
        /// <returns>WhatNext</returns>
        public IActionResult HelpWhatNext()
        {
            return this.View();
        }

        /// <summary>
        /// Wants to book.
        /// </summary>
        /// <returns>WantToBook</returns>
        public IActionResult HelpWantToBook()
        {
            return this.View();
        }

        /// <summary>
        /// Mies the booking.
        /// </summary>
        /// <returns>MyBooking</returns>
        public async Task<IActionResult> MyBooking()
        {
            var emailid = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;

            ////var userid = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Sid).Value;
            var record = await this.hotelBookingService.GetBookingDetails(emailid);
            return this.View(record);
        }

        /// <summary>
        /// Flights the bookings.
        /// </summary>
        /// <returns>FlightBookings</returns>
        public async Task<IActionResult> FlightBookings()
        {
            var userid = await this.userDetailService.GetUserIdAsync(this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value);
            var records = await this.flightBookingService.GetAllByUserIdAsync(userid);
            return this.View(records);
        }

        /// <summary>
        /// Gets the booking details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>GetBookingDetails</returns>
        public async Task<PartialViewResult> GetFlightDetails(Guid id)
        {
            var bookingDetail = await this.flightBookingService.GetByIdAsync(id);
            return this.PartialView("GetFlightDetails", bookingDetail);
        }

        /// <summary>
        /// Binds the select list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Bind SelectList</returns>
        private async Task BindSelectList(MyInformationViewModel model)
        {
            if (model.CountryId != null)
            {
                model.Country = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, (short)model.CountryId)).ToSelectList();
            }

            if (model.NationalityId != null)
            {
                model.Nationality = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, (short)model.NationalityId)).ToSelectList();
            }

            if (model.CountryofIssueId != null)
            {
                model.CountryIssue = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, (short)model.CountryofIssueId)).ToSelectList();
            }

            if (short.TryParse(model.City, out short cityid))
            {
                model.Cities = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, cityid, 0)).ToSelectList();
            }
        }
    }
}