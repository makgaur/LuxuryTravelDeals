// <copyright file="BlogController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// BlogController1    /// </summary>
    /// <seealso cref="HiTours.Web.BaseController" />
    public class BlogController : BaseController
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index</returns>
        public IActionResult Index()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }

        /// <summary>
        /// Destinations this instance.
        /// </summary>
        /// <returns>Destination</returns>
        public IActionResult Destination()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }

        /// <summary>
        /// Mores the deatils.
        /// </summary>
        /// <returns>MoreDeatils</returns>
        public IActionResult MoreDeatils()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }
    }
}
