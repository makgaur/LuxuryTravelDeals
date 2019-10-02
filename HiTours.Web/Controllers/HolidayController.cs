// <copyright file="HolidayController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Holiday Controller
    /// </summary>
    /// <seealso cref="HiTours.Web.BaseController" />
    public class HolidayController : BaseController
    {
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayController"/> class.
        /// </summary>
        /// <param name="masterService">The master service.</param>
        public HolidayController(IMasterService masterService)
        {
            this.masterService = masterService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The identifier.</param>
        /// <returns>
        /// view
        /// </returns>
        [Route("holiday/{parent}/{child}")]
        public async Task<IActionResult> Index(string parent, string child)
        {
            var holiday = await this.masterService.GetPackageHolidayMenuListAsync(parent);

            ////var holiday = CountryCity.List().SelectMany(x => x.Value).Where(x => x.Key == child);
            var exists = holiday.FirstOrDefault().Description.ToLower().Contains(child.ToLower());
            this.ViewBag.TitleName = child;
            if (holiday != null && holiday.Count() > 0 && exists)
            {
                ////is region
                if (holiday.FirstOrDefault().Id == "1")
                {
                    this.ViewBag.group = "country";
                    this.ViewBag.CityName = child;
                }
                else
                {
                    this.ViewBag.group = "city";
                    this.ViewBag.CityName = child.ToUpper() + ", " + parent;
                }

                this.ViewBag.PageType = Enums.SeoPageType.Holiday;

                this.ViewBag.CityId = child;
                return this.View();
            }

            ////holiday.FirstOrDefault().Value;

            return this.LocalRedirect("/");
        }
    }
}