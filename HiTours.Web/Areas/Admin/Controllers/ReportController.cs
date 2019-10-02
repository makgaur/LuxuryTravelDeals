// <copyright file="ReportController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// ReportController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class ReportController : AdminController
    {
        private readonly IUserDetailService user;

        /// <summary>
        /// The hotelbooking
        /// </summary>
        private readonly IHotelBookingService hotelbooking;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="user">The user.</param>
        /// <param name="hotelbooking">The hotelbooking.</param>
        public ReportController(IConfiguration configuration, IMapper mapper, IUserDetailService user, IHotelBookingService hotelbooking)
           : base(mapper, configuration)
        {
            this.user = user;
            this.hotelbooking = hotelbooking;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Index
        /// </returns>
        public async Task<IActionResult> Booking([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.hotelbooking.BookingReport(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Gets the booking detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// BookingDetail
        /// </returns>
        public async Task<PartialViewResult> GetBookingDetail(Guid id, Enums.CurrentDealType type)
        {
            var bookingDetail = new BookingSendMailViewModel();

            if (type == Enums.CurrentDealType.Package)
            {
                bookingDetail = await this.hotelbooking.GetTourBookingSendMailDetailAsync(id);
            }
            else
            {
                bookingDetail = await this.hotelbooking.GetBookingSendMailDetailAsync(id);
            }

            return this.PartialView("_BookingDetails", bookingDetail);
        }

        /////// <summary>
        /////// Cuurents the booking.
        /////// </summary>
        /////// <param name="model">The model.</param>
        /////// <returns>CuurentBooking</returns>
        ////public async Task<IActionResult> CuurentBooking([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        ////{
        ////    if (this.IsAjaxRequest())
        ////    {
        ////        var result = await this.hotelbooking.CurrentBookingReport(model);

        ////        return this.Json(result);
        ////    }

        ////    return this.View();
        ////}
    }
}