// <copyright file="UsersController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// UserController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class UsersController : AdminController
    {
        private readonly IUserDetailService user;

        /// <summary>
        /// The hotelbooking
        /// </summary>
        private readonly IHotelBookingService hotelbooking;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="configuration">Configu</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="user">The user.</param>
        /// <param name="hotelbooking">The hotelbooking.</param>
        public UsersController(IConfiguration configuration, IMapper mapper, IUserDetailService user, IHotelBookingService hotelbooking)
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
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.user.GetAllAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Index
        /// </returns>
        public async Task<IActionResult> BookingReport([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
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
        /// <returns>BookingDetail</returns>
        public async Task<PartialViewResult> GetUserDeatils(int id)
        {
            var result = await this.user.GetByIdAsync(id);

            return this.PartialView("_UserDetails", result);
        }

        /// <summary>
        /// Changes the active status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChangeActiveStatus</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            var category = await this.user.GetByIdAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            category.IsActive = !category.IsActive;
            await this.user.UpdateAsync(category);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "users", action = "index", area = Constants.AreaAdmin });
            }
        }
    }
}
