// <copyright file="HelpController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// HelpController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HelpController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Mies the account.
        /// </summary>
        /// <returns>MyAccount</returns>
        public IActionResult MyAccount()
        {
            return this.View();
        }

        /// <summary>
        /// Whats the next.
        /// </summary>
        /// <returns>WhatNext</returns>
        public IActionResult WhatNext()
        {
            return this.View();
        }

        /// <summary>
        /// Wants to book.
        /// </summary>
        /// <returns>WantToBook</returns>
        public IActionResult WantToBook()
        {
            return this.View();
        }
    }
}
