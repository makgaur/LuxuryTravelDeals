// <copyright file="LayoutMenu.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Layout Menu
    /// </summary>
    public class LayoutMenu : ViewComponent
    {
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutMenu" /> class.
        /// </summary>
        /// <param name="masterService">The master service.</param>
        /// <param name="holidayMenuService">The holiday menu service.</param>
        public LayoutMenu(IMasterService masterService, IHolidayMenuService holidayMenuService)
        {
            this.masterService = masterService;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>
        /// Render View Component
        /// </returns>
        public async Task<IViewComponentResult> InvokeAsync(Enums.ViewComponentMenu component)
        {
            var response = await this.ViewComponent(component);

            return this.View(response);
        }

        /// <summary>
        /// Views the component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>
        /// ViewComponent
        /// </returns>
        private async Task<string> ViewComponent(Enums.ViewComponentMenu component)
        {
            string viewName = string.Empty;
            switch (component)
            {
                case Enums.ViewComponentMenu.Style:
                    this.ViewBag.TravelStyleMenu = await this.masterService.GetPackageTravelStyleListAsync();
                    viewName = "_StyleMenu"; break;

                case Enums.ViewComponentMenu.Holiday:
                    var list = await this.masterService.GetPackageHolidayMenuListAsync();
                    var india = list.Where(x => x.Name == "India").FirstOrDefault();
                    if (india != null)
                    {
                        var oldindex = list.IndexOf(india);
                        list.Remove(india);
                        list.Insert(0, india);
                    }

                    var dictionary = new Dictionary<string, List<KeyValuePair<string, string>>>();
                    foreach (var item in list)
                    {
                        var keypairlist = new List<KeyValuePair<string, string>>();
                        var childmenulist = item.Description.Split(",");
                        foreach (var child in childmenulist)
                        {
                            keypairlist.Add(new KeyValuePair<string, string>(child.ToLower(), child.ToLower()));
                        }

                        dictionary.Add(item.Name.ToLower(), keypairlist);
                    }

                    this.ViewBag.CountryCityList = dictionary; ////await this.masterService.GetPackageHolidayMenuListAsync(); ////CountryCity.List();
                    viewName = "_HolidayMenu"; break;
            }

            return viewName;
        }
    }
}