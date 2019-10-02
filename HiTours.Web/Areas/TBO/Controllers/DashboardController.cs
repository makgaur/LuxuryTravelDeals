// <copyright file="DashboardController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.TBO.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Services;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// DashboardController
    /// </summary>
    public class DashboardController : TBOController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="domainSetting">The service urls.</param>
        public DashboardController(IOptions<DomainSetting> domainSetting)
            : base(domainSetting)
        {
        }

        /// <summary>Dashboard
        /// Indexes this instance.
        /// </summary>
        /// <returns>Dashboard view</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}