// <copyright file="StyleController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// StyleController
    /// </summary>
    /// <seealso cref="HiTours.Web.BaseController" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class StyleController : BaseController
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleController" /> class.
        /// </summary>
        /// <param name="masterService">The master service.</param>
        public StyleController(IMasterService masterService)
        {
            this.masterService = masterService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// view
        /// </returns>
        [Route("style/{name}/{id}")]
        public async Task<IActionResult> Index(string name, int id)
        {
            var style = (await this.masterService.GetPackageTravelStyleListAsync(string.Empty, 1, id)).ToSelectList();

            if (style != null && style.Count > 0 && SpecialChars.Remove(style[0].Text) == name)
            {
                this.ViewBag.PageType = Enums.SeoPageType.Style;
                this.ViewBag.StyleName = style[0].Text;
                this.ViewBag.StyleId = id;
                return this.View();
            }
            else
            {
                return this.RedirectToRoute("default");
            }
        }
    }
}