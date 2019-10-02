// <copyright file="SeoDetailController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Seo Detail controller
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class SeoDetailController : AdminController
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        private readonly ISeoDetailServices seoDetailServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeoDetailController" /> class.
        /// </summary>
        /// <param name="configuration">Config</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="seoDetailServices">The seo detail services.</param>
        public SeoDetailController(IConfiguration configuration, IMapper mapper, IMasterService masterService, ISeoDetailServices seoDetailServices)
            : base(mapper, configuration)
        {
            this.masterService = masterService;
            this.seoDetailServices = seoDetailServices;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// view
        /// </returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Manages this instance.
        /// </summary>
        /// <param name="pageType">Type of the page.</param>
        /// <returns>
        /// view
        /// </returns>
        public IActionResult Manage(Enums.SeoPageType pageType)
        {
            var model = new SeoPageTypeDetail
            {
                SeoPageTypeName = pageType.GetDisplayName(),
                PageType = pageType.ToString()
            };

            switch (pageType)
            {
                case Enums.SeoPageType.Static:
                    model.PageIdTitle = "Page Name";
                    model.PageIdOptionUrl = "/selectlist/StaticPageMasters";
                    break;

                case Enums.SeoPageType.Package:
                    model.PageIdTitle = "Package";
                    model.PageIdOptionUrl = "/selectlist/tourpackages";
                    break;

                case Enums.SeoPageType.Hotel:
                    model.PageIdTitle = "Hotel";
                    model.PageIdOptionUrl = "/selectlist/packages";
                    break;
                case Enums.SeoPageType.Style:
                    model.PageIdTitle = "Style";
                    model.PageIdOptionUrl = "/selectlist/getpackagetravelstylelist";
                    break;

                case Enums.SeoPageType.Holiday:
                    model.PageIdTitle = "Holiday";
                    model.PageIdOptionUrl = "/selectlist/getmenucountrycity";
                    break;

                default:
                    model.PageIdTitle = "Page Name";
                    model.PageIdOptionUrl = "/selectlist/StaticPageMasters";
                    break;
            }

            return this.View(model);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="pageType">Type of the page.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// return seo detail partial
        /// </returns>
        [HttpGet]
        public async Task<PartialViewResult> GetDetail(string pageType, string pageId)
        {
            var model = new SeoDetailViewModel();

            var result = await this.seoDetailServices.GetByIdAsync(pageType, pageId);

            if (result != null)
            {
                model = this.Mapper.Map<SeoDetailViewModel>(result);
            }

            model.PageId = pageId;
            model.PageType = pageType;

            return this.PartialView("_SeoDetail", model);
        }

        /// <summary>
        /// Manages this instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// view
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Manage(SeoDetailViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<SeoDetailModel>(model);
                if (model.Id == 0)
                {
                    record.SetAuditInfo(0);
                    await this.seoDetailServices.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    record.UpdateAuditInfo(0);
                    await this.seoDetailServices.UpdateAsync(record);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "seodetail", action = "manage", area = Constants.AreaAdmin, pageType = model.PageType });
            }

            return this.View(model);
        }
    }
}