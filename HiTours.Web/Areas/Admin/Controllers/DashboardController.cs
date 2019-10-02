// <copyright file="DashboardController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// DashboardController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class DashboardController : AdminController
    {
        /// <summary>
        /// The hotelbooking
        /// </summary>
        private readonly IHotelBookingService hotelbooking;

        /// <summary>
        /// The user detail
        /// </summary>
        private readonly IUserDetailService userDetail;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="hotelbooking">The hotelbooking.</param>
        /// <param name="userDetail">The user detail.</param>
        public DashboardController(IConfiguration configuration, IMapper mapper, IHotelBookingService hotelbooking, IUserDetailService userDetail)
            : base(mapper, configuration)
        {
            this.hotelbooking = hotelbooking;
            this.userDetail = userDetail;
        }

        /// <summary>Dashboard
        /// Indexes this instance.
        /// </summary>
        /// <returns>Dashboard view</returns>
        public async Task<ActionResult> Index()
        {
            this.ViewBag.TotalUsers = await this.userDetail.CountAsync();
            this.ViewBag.TotalNewBookings = await this.hotelbooking.CountCurrentBookingAsync();
            return this.View();
        }
    }
}