// <copyright file="TourPackageController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    /// <summary>
    /// CategoryController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class TourPackageController : AdminController
    {
        /// <summary>
        /// The environment
        /// </summary>
        private readonly IHostingEnvironment environment;

        /// <summary>
        /// The category service
        /// </summary>
        private readonly ITourPackageService tourPackageService;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// The package nights service
        /// </summary>
        private readonly IPackageNightsService packageNightsService;

        private readonly IHotelBookingService hotelBookingService;
        ////private readonly IVendorService vendorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourPackageController" /> class.
        /// </summary>
        /// <param name="configuration">Config</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="tourPackageService">The category service.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="packageNightsService">The package nights service.</param>
        /// <param name="hotelBookingService">The hotel booking service.</param>
        public TourPackageController(IConfiguration configuration, IMapper mapper, IHostingEnvironment environment, ITourPackageService tourPackageService, IMasterService masterService, IPackageNightsService packageNightsService, IHotelBookingService hotelBookingService)
            : base(mapper, configuration)
        {
            this.environment = environment;
            this.tourPackageService = tourPackageService;
            this.masterService = masterService;
            this.packageNightsService = packageNightsService;
            this.hotelBookingService = hotelBookingService;
        }

        /// <summary>
        /// Tours the package creation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>viewModel</returns>
        public async Task<IActionResult> TourPackageCreation(string id)
        {
            var viewModel = new TourPackageViewModel
            {
                TourPackageType = 1
            };
            if (id != string.Empty)
            {
                viewModel.IsHotelOnly = (await this.tourPackageService.GetByIdAsync(new Guid(id))).IsHotelOnly;
            }

            return this.View(viewModel);
        }

        /// <summary>
        /// Tours the package creation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>viewModel</returns>
        public IActionResult GetPackageAddOns(Guid id)
        {
            this.ViewBag.TourPackageId = id;
            return this.PartialView("_PackageAddOns");
        }

        /// <summary>
        /// Tours the package creation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// TourPackageCreation
        /// </returns>
        public IActionResult HotelCreation(string id)
        {
            var viewModel = new TourPackageViewModel()
            {
                TourPackageType = 2
            };
            if (id != string.Empty)
            {
                // get record for edit
            }

            return this.View("TourPackageCreation", viewModel);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.tourPackageService.GetAllAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> PackagehotelList([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.tourPackageService.GetAllHotelListAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="packageId">Package ID</param>
        /// <param name="isHotelOnly">Is Hotel</param>
        /// <returns>
        /// DataTable Pagging
        /// </returns>
        public async Task<IActionResult> BlockBookingDates(Guid packageId, bool isHotelOnly)
        {
            if (packageId == Guid.Empty)
            {
                return this.PartialView("_BlockDatesPartial", new BlockBookingViewModel { DataIncomplete = true });
            }

            try
            {
                var model = await this.tourPackageService.GetPackageBlockedDatesAsync(packageId, isHotelOnly);
                return this.PartialView("_BlockDatesPartial", model);
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return this.PartialView("_BlockDatesPartial", new BlockBookingViewModel { DataIncomplete = true });
            }
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>
        /// DataTable Pagging
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> BlockBookingDates(BlockBookingViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var data = await this.tourPackageService.GetPackageBlockedDatesAsync(model.BB_PackageId, model.IsHotel);
                    if (data.Dates != string.Empty)
                    {
                        List<DateTime> existingDates = data.Dates.Split(',').ToList().Select(x => Convert.ToDateTime(x)).ToList();
                        List<DateTime> newDates = model.Dates.Split(',').ToList().Select(x => Convert.ToDateTime(x)).ToList();
                        List<DateTime> datesToAdd = newDates.Where(nd => existingDates.All(ed => ed != nd)).ToList();
                        List<DateTime> datesToRemove = existingDates.Where(ed => newDates.All(nd => nd != ed)).ToList();
                        if (datesToAdd != null && datesToAdd.Count != 0)
                        {
                            foreach (var item in datesToAdd)
                            {
                                BlockBookingModel addModel = new BlockBookingModel
                                {
                                    BB_Date = item,
                                };
                                addModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                if (model.IsHotel)
                                {
                                    addModel.BB_HotelId = model.BB_PackageId;
                                }
                                else
                                {
                                    addModel.BB_PackageId = model.BB_PackageId;
                                }

                                await this.tourPackageService.AddPackageBlockedDatesAsync(addModel);
                            }
                        }

                        if (datesToRemove != null && datesToRemove.Count != 0)
                        {
                            foreach (var item in datesToRemove)
                            {
                                var itemToRemove = await this.tourPackageService.GetBlockBookingDateEntryAsync(model.BB_PackageId, item, model.IsHotel);

                                await this.tourPackageService.DeletePackageBlockedDatesAsync(itemToRemove);
                            }
                        }
                    }
                    else
                    {
                        List<DateTime> newDates = model.Dates.Split(',').ToList().Select(x => Convert.ToDateTime(x)).ToList();
                        foreach (var item in newDates)
                        {
                            BlockBookingModel addModel = new BlockBookingModel
                            {
                                BB_Date = item,
                            };
                            addModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value));
                            if (model.IsHotel)
                            {
                                addModel.BB_HotelId = model.BB_PackageId;
                            }
                            else
                            {
                                addModel.BB_PackageId = model.BB_PackageId;
                            }

                            await this.tourPackageService.AddPackageBlockedDatesAsync(addModel);
                        }
                    }

                    return this.Json("success");
                }
                catch (Exception ex)
                {
                    var msg = ex.ToString();
                    return this.Json("failed");
                }
            }

            return this.PartialView("_BlockDatesPartial", model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// DataTable Pagging
        /// </returns>
        public async Task<IActionResult> PackageNightList(Guid id)
        {
            var model = new List<TourPackageNightViewModel>();

            if (this.IsAjaxRequest())
            {
                var package = await this.tourPackageService.GetByIdAsync(id);
                if (package != null)
                {
                    this.ViewBag.PackageName = package.PackageName;
                }

                var result = await this.tourPackageService.GetNightsByPackageAsync(id);
                model = this.Mapper.Map<List<TourPackageNightViewModel>>(result);
                return this.PartialView("PackageNightList", model);
            }

            if (this.IsAjaxRequest())
            {
                return this.PartialView("PackageNightList", model);
            }

            return this.View(model);
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// DataTable Pagging
        /// </returns>
        public async Task<IActionResult> PackageNightListForRate(Guid id)
        {
            var model = new List<TourPackageNightViewModel>();

            if (this.IsAjaxRequest())
            {
                var package = await this.tourPackageService.GetByIdAsync(id);
                if (package != null)
                {
                    this.ViewBag.PackageName = package.PackageName;
                }

                var result = await this.tourPackageService.GetNightsByPackageAsync(id);
                model = this.Mapper.Map<List<TourPackageNightViewModel>>(result);
                return this.PartialView("PackageNightListForRate", model);
            }

            if (this.IsAjaxRequest())
            {
                return this.PartialView("PackageNightListForRate", model);
            }

            return this.View(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Manage
        /// </returns>
        public async Task<ActionResult> Manage(Guid id, byte type)
        {
            var model = this.TempData.Get<TourPackageViewModel>("Model") ?? new TourPackageViewModel
            {
                TourPackageType = type
            };
            if (model.VendorId == 0 || model.VendorId == null)
            {
                model.VendorListItems = new List<SelectListItem>();
            }

            if (model.TourPackageType == 2 && model.HotelId.HasValue)
            {
                model.HotelList = (await this.masterService.GetPackageHotelListAsync(string.Empty, 1, model.HotelId.Value, 0)).ToSelectList();
            }

            if (model.TourPackageCity != null)
            {
                foreach (var item in model.TourPackageCity)
                {
                    item.RegionList = (await this.masterService.GetPackageRegionListAsync(string.Empty, 1, item.RegionId)).ToSelectList();
                    item.CountyList = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, short.Parse(item.CountryId.ToString()), short.Parse(item.RegionId.ToString()))).ToSelectList();
                    item.StateList = (await this.masterService.GetPackageStateListAsync(string.Empty, 1, item.StateId, short.Parse(item.CountryId.ToString()))).ToSelectList();

                    item.CityList = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, item.CityId, item.StateId == null ? short.Parse("0") : short.Parse(item.StateId.ToString()))).ToSelectList();
                }
            }

            ////this.ViewBag.TravelStyle = (await this.masterService.GetPackageTravelStyleListAsync(string.Empty, 1, 0)).ToSelectList();
            this.ViewBag.TravelStyle = (await this.masterService.GetAllPackageTravelStyleListAsync()).ToSelectList();
            model.Deals = (await this.masterService.GetPackageDealTypeListAsync(string.Empty, 1, 0)).ToSelectList();

            if (this.IsAjaxRequest())
            {
                return this.PartialView("Manage", model);
            }

            return this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="nextview">The nextview.</param>
        /// <returns>
        /// Manage
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Manage(TourPackageViewModel model, string nextview)
        {
            if (this.ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(nextview))
                {
                    this.TempData["nextview"] = nextview;
                }

                this.TempData["Error"] = null;
                this.TempData["Duplicate"] = null;
                var existing = await this.tourPackageService.GetByIdAsync(model.Id);
                if (existing != null)
                {
                    if (model.PackageValidFrom < existing.PackageValidFrom || model.PackageValidFrom > model.PackageValidTo)
                    {
                        this.TempData["Error"] = "Please select PackageValidFrom date after  " + existing.PackageValidFrom.ToString("dd/MM/yyyy") + " or Before " + model.PackageValidTo.Value.ToString("dd/MM/yyyy");
                        model.TourPackageImage = model.TourPackageImage.Where(x => x.TourPackageId != Guid.Empty).ToList();
                        this.TempData.Put("Model", model);
                        if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                        {
                            this.TempData["nextview"] = "#package-night";
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                        }

                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                    }
                    else if (model.PackageValidTo.Value.Date < existing.PackageValidTo.Date)
                    {
                        this.TempData["Error"] = "Please select PackageValidTo date after  " + existing.PackageValidTo.ToString("dd/MM/yyyy");
                        model.TourPackageImage = model.TourPackageImage.Where(x => x.TourPackageId != Guid.Empty).ToList();
                        this.TempData.Put("Model", model);

                        if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                        {
                            this.TempData["nextview"] = "#package-night";
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                        }

                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                    }
                }

                if (model.TourPackageCity.Count > 0)
                {
                    var duplicateCityRegion = model.TourPackageCity.GroupBy(x => new { x.RegionId, x.CountryId, x.CityId }).Where(i => i.Count() > 1).ToList();
                    if (duplicateCityRegion.Count > 0)
                    {
                        this.TempData["Duplicate"] = "Please select different types of Region , Country and City !";
                        model.TourPackageImage = model.TourPackageImage.Where(x => x.TourPackageId != Guid.Empty).ToList();
                        this.TempData.Put("Model", model);
                        if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                        {
                            this.TempData["nextview"] = "#package-night";
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                        }

                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                    }
                }

                var bookingFromDate = model.TourPackageBookDate.Select(x => x.BookingValidFrom).FirstOrDefault();
                var bookingToDate = model.TourPackageBookDate.Select(x => x.BookingValidTo).FirstOrDefault();
                if (bookingFromDate < model.PackageValidTo)
                {
                    if (bookingToDate <= model.PackageValidTo && bookingToDate > bookingFromDate)
                    {
                        ////
                    }
                    else
                    {
                        this.TempData["Error"] = "Please select booking date before or Between  Date = " + " { " + model.PackageValidFrom.Value.ToString("dd/MM/yyyy") + " To " + model.PackageValidTo.Value.ToString("dd/MM/yyyy") + " }";
                        model.TourPackageImage = model.TourPackageImage.Where(x => x.TourPackageId != Guid.Empty).ToList();
                        this.TempData.Put("Model", model);
                        if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                        {
                            this.TempData["nextview"] = "#package-night";
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                        }
                        else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                        {
                            return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                        }

                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                    }
                }
                else
                {
                    this.TempData["Error"] = "Please select booking date before or Between  Date = " + " { " + model.PackageValidFrom.Value.ToString("dd/MM/yyyy") + " To " + model.PackageValidTo.Value.ToString("dd/MM/yyyy") + " }";
                    model.TourPackageImage = model.TourPackageImage.Where(x => x.TourPackageId != Guid.Empty).ToList();
                    this.TempData.Put("Model", model);
                    if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                    {
                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                    }
                    else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                    {
                        this.TempData["nextview"] = "#package-night";
                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                    }
                    else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                    {
                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                    }

                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = model.Id != Guid.Empty ? model.Id.ToString() : string.Empty });
                }

                model.PackageValidTo = Convert.ToDateTime(model.PackageValidTo.Value.ToString("dd/MM/yyyy")).AddHours(23).AddMinutes(59).AddSeconds(59);

                if (model.TourPackageBookDate != null && model.TourPackageBookDate.Count > 0)
                {
                    foreach (var validty in model.TourPackageBookDate)
                    {
                        validty.BookingValidTo = Convert.ToDateTime(validty.BookingValidTo.Value.ToString("dd/MM/yyyy")).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
                }

                model.TourPackageTravelStyle = model.TourPackageTravelStyle ?? new List<TourPackageTravelStyleViewModel>();
                model.TravelStyle = model.TravelStyle ?? new List<string>().ToArray();
                model.DealTypeId = string.Join(",", model.DealType);
                var newaddstyles = model.TravelStyle.Except(model.TourPackageTravelStyle.Select(x => x.TravelStyleId.ToString()).ToArray()).ToArray();
                var deletedstyles = model.TourPackageTravelStyle.Select(x => x.TravelStyleId.ToString()).ToArray().Except(model.TravelStyle).ToArray();
                foreach (var travelstyle in newaddstyles)
                {
                    model.TourPackageTravelStyle.Add(new TourPackageTravelStyleViewModel { TourPackageId = model.Id, TravelStyleId = int.Parse(travelstyle) });
                }

                foreach (var item in deletedstyles)
                {
                    var tpts = model.TourPackageTravelStyle.FirstOrDefault(x => x.TravelStyleId.ToString().Equals(item));
                    model.TourPackageTravelStyle.Remove(tpts);
                }

                var record = this.Mapper.Map<TourPackageModel>(model);

                if (model.Id == Guid.Empty)
                {
                    record.DealCode = (await this.tourPackageService.GetMaxDealCodeAsync()) + 1;

                    record.SetAuditInfo(0);
                    record.TourPackageImage = await this.UploadPackageImages(model.TourPackageImage, record.Id);
                    record.IsActive = false;
                    await this.tourPackageService.InsertAsync(record);

                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    ////var existing = await this.tourPackageService.GetByIdAsync(model.Id);
                    existing.Visitors = model.Visitors;

                    ///// Add or Update Packge Date
                    ////var child = new List<TourPackageBookDateModel>();
                    var existingChild = existing.TourPackageBookDate.Where(x => model.TourPackageBookDate.Any(m => m.Id == x.Id)).ToList();
                    foreach (var item in existingChild)
                    {
                        var update = model.TourPackageBookDate.FirstOrDefault(x => x.Id == item.Id);
                        item.Id = update.Id;
                        item.TourPackageId = update.TourPackageId;
                        item.BookingValidFrom = update.BookingValidFrom ?? DateTime.Now;
                        item.BookingValidTo = update.BookingValidTo ?? DateTime.Now;
                        item.UpdatedBy = update.UpdatedBy;
                        item.UpdatedDate = update.UpdatedDate;
                        item.ObjectState = EntityState.Modified;
                        ////child.Add(item);
                    }

                    var addChild = model.TourPackageBookDate.Where(x => !existingChild.Any(m => m.Id == x.Id)).ToList();
                    foreach (var item in addChild)
                    {
                        var childmapped = this.Mapper.Map<TourPackageBookDateModel>(item);
                        childmapped.ObjectState = EntityState.Added;
                        existing.TourPackageBookDate.Add(childmapped);
                    }

                    var deletedChild = existing.TourPackageBookDate.Where(x => !model.TourPackageBookDate.Any(m => m.Id == x.Id)).ToList();
                    deletedChild.ForEach(item =>
                    {
                        ////x.ObjectState = EntityState.Deleted;
                        this.tourPackageService.RemoveTourPackageBookDateModelEntity(item);
                    });
                    ////foreach (var item in deletedChild)
                    ////{
                    ////    item.ObjectState = EntityState.Deleted;
                    ////    child.Add(item);

                    ////    this.tourPackageService.RemoveTourPackageBookDateModelEntity(item);
                    ////}

                    //////// Add or Update Travel style

                    var exstingTravelStyle = existing.TourPackageTravelStyle.Where(x => model.TourPackageTravelStyle.Any(m => m.Id == x.Id)).ToList();
                    ////var travelstyleChild = new List<TourPackageTravelStyleModel>();
                    foreach (var travelstyle in exstingTravelStyle)
                    {
                        var update = model.TourPackageTravelStyle.FirstOrDefault(x => x.TravelStyleId == travelstyle.TravelStyleId);
                        travelstyle.Id = update.Id;
                        travelstyle.TourPackageId = update.TourPackageId;
                        travelstyle.TravelStyleId = update.TravelStyleId;
                        travelstyle.UpdatedBy = update.UpdatedBy;
                        travelstyle.UpdatedDate = update.UpdatedDate;
                        travelstyle.ObjectState = EntityState.Modified;
                        ////travelstyleChild.Add(travelstyle);
                    }

                    var addtravelStyleChild = model.TourPackageTravelStyle.Where(x => !exstingTravelStyle.Any(m => m.TravelStyleId == x.TravelStyleId)).ToList();
                    foreach (var travelstylechlid in addtravelStyleChild)
                    {
                        var childmapped = this.Mapper.Map<TourPackageTravelStyleModel>(travelstylechlid);
                        childmapped.ObjectState = EntityState.Added;
                        existing.TourPackageTravelStyle.Add(childmapped);
                    }

                    var deletedTSChild = existing.TourPackageTravelStyle.Where(x => !model.TourPackageTravelStyle.Any(m => m.TravelStyleId == x.TravelStyleId)).ToList();

                    deletedTSChild.ForEach(item =>
                    {
                        ////x.ObjectState = EntityState.Deleted;
                        this.tourPackageService.RemoveTourPackageTravelStyleModelEntity(item);
                    });

                    var exstingTPCity = existing.TourPackageCity.Where(x => model.TourPackageCity.Any(m => m.Id == x.Id)).ToList();
                    foreach (var item in exstingTPCity)
                    {
                        var update = model.TourPackageCity.FirstOrDefault(x => x.Id == item.Id);
                        item.Id = update.Id;
                        item.TourPackageId = update.TourPackageId;
                        item.RegionId = update.RegionId;
                        item.CountryId = update.CountryId;
                        item.StateId = update.StateId;
                        item.CityId = update.CityId;
                        item.CityDescription = update.CityDescription;
                        item.UpdatedBy = update.UpdatedBy;
                        item.UpdatedDate = update.UpdatedDate;
                        item.ObjectState = EntityState.Modified;
                    }

                    var addtpCityChild = model.TourPackageCity.Where(x => !exstingTPCity.Any(m => m.Id == x.Id)).ToList();
                    addtpCityChild.ForEach(item =>
                    {
                        item.TourPackageId = existing.Id;
                        existing.TourPackageCity.Add(this.Mapper.Map<TourPackageCityModel>(item));
                    });

                    var deletedtpCityChild = existing.TourPackageCity.Where(x => !model.TourPackageCity.Any(m => m.Id == x.Id)).ToList();
                    deletedtpCityChild.ForEach(item =>
                    {
                        ////x.ObjectState = EntityState.Deleted;
                        this.tourPackageService.RemoveTourpackageCityEntity(item);
                    });

                    var existtpimg = existing.TourPackageImage.Where(x => model.TourPackageImage.Any(m => m.Id == x.Id)).ToList();
                    foreach (var item in existtpimg)
                    {
                        var update = model.TourPackageImage.FirstOrDefault(x => x.Id == item.Id);
                        item.ImageDescription = update.ImageDescription;
                        item.AltTag = update.AltTag;
                        item.SequenceNo = update.SequenceNo;
                        item.UpdatedDate = DateTime.Now;
                        item.ObjectState = EntityState.Modified;
                    }

                    var deletetptpimgChild = existing.TourPackageImage.Where(x => !model.TourPackageImage.Any(m => m.Id == x.Id)).ToList();
                    deletetptpimgChild.ForEach(item =>
                    {
                        ////x.ObjectState = EntityState.Deleted;
                        this.tourPackageService.RemoveTourPackageImageEntity(item);
                    });

                    var tourPackageImage = await this.UploadPackageImages(model.TourPackageImage, record.Id);
                    tourPackageImage.ForEach(item =>
                    {
                        item.TourPackageId = existing.Id;
                        existing.TourPackageImage.Add(this.Mapper.Map<TourPackageImageModel>(item));
                    });

                    this.UpdateTourPackage(existing, model);
                    await this.tourPackageService.UpdateAsync(existing);

                    this.ShowMessage(Messages.UpdateSuccessfully);
                    if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                    {
                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = record.Id != Guid.Empty ? record.Id.ToString() : string.Empty });
                    }
                    else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                    {
                        this.TempData["nextview"] = "#package-night";

                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = record.Id != Guid.Empty ? record.Id.ToString() : string.Empty });
                    }
                    else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                    {
                        return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                    }

                    ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = "TourPackageCreation", area = Constants.AreaAdmin, id = $"{existing.Id}" });
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                }

                if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = record.Id != Guid.Empty ? record.Id.ToString() : string.Empty });
                }
                else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                {
                    this.TempData["nextview"] = "#package-night";

                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = record.Id != Guid.Empty ? record.Id.ToString() : string.Empty });
                }
                else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? "packagehotellist" : "Index", area = Constants.AreaAdmin });
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = model.TourPackageType == 2 ? Constants.HotelCreation : Constants.TourPackageCreation, area = Constants.AreaAdmin, id = record.Id != Guid.Empty ? record.Id.ToString() : string.Empty });
            }

            return this.View(model);
        }

        /// <summary>
        /// PackageCity
        /// </summary>
        /// <returns>_PackageCity</returns>
        public IActionResult PackageCity()
        {
            return this.PartialView("_PackageCity", new TourPackageCityViewModel());
        }

        /// <summary>
        /// Gets the tour package nights.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packagename">The packagename.</param>
        /// <param name="tourpackagetype">The tourpackagetype.</param>
        /// <returns>
        /// GetTourPackageNights
        /// </returns>
        public ActionResult GetTourPackageNights(Guid id, string packagename, string tourpackagetype)
        {
            this.ViewBag.TourPackageId = id;
            this.ViewBag.TourPackageType = tourpackagetype;
            return this.PartialView("_PackageNightSection");
        }

        /// <summary>
        /// Gets the tour package nights.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Get Package Promotion View
        /// </returns>
        public ActionResult GetPackagePromotion(Guid id)
        {
            this.ViewBag.TourPackageId = id;
            return this.PartialView("_PackagePromotionMaster");
        }

        /// <summary>
        /// Gets the tour package nights.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Get Package Promotion View
        /// </returns>
        public ActionResult GetPackageCancellationPolicy(Guid id)
        {
            this.ViewBag.TourPackageId = id;
            return this.PartialView("_PackageCancellationPolicyMaster");
        }

        /// <summary>
        /// Gets the tour package nights.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packagename">The packagename.</param>
        /// <param name="tourpackagetype">The tourpackagetype.</param>
        /// <returns>
        /// GetTourPackageNights
        /// </returns>
        public ActionResult GetTourPackageNightsForRate(Guid id, string packagename, string tourpackagetype)
        {
            this.ViewBag.TourPackageId = id;
            this.ViewBag.TourPackageType = tourpackagetype;
            return this.PartialView("_PackageNightSectionForRate");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nextview">The nextview.</param>
        /// <returns>
        /// Manage
        /// </returns>
        public async Task<ActionResult> PackageNights(Guid id, string nextview)
        {
            var model = new TourPackageNightViewModel
            {
                TourPackageId = id
            };

            if (id != Guid.Empty)
            {
                var record = await this.tourPackageService.GetTourPackageNightBytouPackageIdAsync(id);
                if (record != null)
                {
                    model = this.Mapper.Map<TourPackageNightViewModel>(record);
                    if (model.TourPackageNightsValidity != null)
                    {
                        foreach (var item in model.TourPackageNightsValidity)
                        {
                            var roomTypes = await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, item.HotelRoomTypeId);
                            if (roomTypes != null && roomTypes.Count > 0)
                            {
                                item.RoomTypeName = roomTypes.FirstOrDefault().Name;
                            }
                        }
                    }

                    model.Cities = model.TourPackageNightsDepartCity.Select(x => x.DepartCityId.ToString()).ToArray();

                    return this.PartialView("PackageNights", model);
                }

                ////model.CityList = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, record.TourPackageNightsDepartCity.FirstOrDefault().DepartCityId)).ToSelectList();
            }

            if (this.IsAjaxRequest())
            {
                return this.PartialView("PackageNights", model);
            }

            return this.View(model);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Delete Record
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> DeletePackageNights(Guid id)
        {
            var model = await this.tourPackageService.GetNightsByPackageNightIDAsync(id);
            if (model == null)
            {
                return this.NotFound();
            }

            if (model.TourPackageNightsValidity.Count > 0)
            {
                foreach (var item in model.TourPackageNightsValidity.ToList())
                {
                    await this.tourPackageService.TourPackageValidityDeleteAsync(item);
                }
            }

            await this.packageNightsService.DeleteAsync(model);
            ////await this.tourPackageService.DeleteAsync(category);
            ////this.ShowMessage(Messages.DeletedSuccessfully);
            return this.Json("success");
        }

        /// <summary>
        /// Gets the booking detail.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// BookingDetail
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> PackageNights(TourPackageNightViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (model.TourPackageNightsValidity != null && model.TourPackageNightsValidity.Count > 0)
                {
                    foreach (var addhour in model.TourPackageNightsValidity)
                    {
                        addhour.RateTypeApplied = (int)Enums.RateTypeApplied.Double;
                        addhour.RateValidTo = Convert.ToDateTime(addhour.RateValidTo.ToString("dd/MM/yyyy")).AddHours(23).AddMinutes(59).AddSeconds(59);
                    }
                }

                model.TourPackageNightsDepartCity = model.TourPackageNightsDepartCity ?? new List<TourPackageNightsDepartCityViewModel>();
                model.Cities = model.Cities ?? new List<string>().ToArray();
                foreach (var packagecities in model.Cities)
                {
                    model.TourPackageNightsDepartCity.Add(new TourPackageNightsDepartCityViewModel { Id = model.Id, DepartCityId = int.Parse(packagecities) });
                }

                var record = this.Mapper.Map<TourPackageNightModel>(model);

                if (model.Id == Guid.Empty)
                {
                    //// record.SetAuditInfo(0);
                    await this.tourPackageService.SavePackageNights(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    var existing = await this.tourPackageService.GetTourPackageNightBytouPackageIdAsync(model.Id);

                    ///// Add or Update Packge Date
                    ////var child = new List<TourPackageNightsValidityModel>();
                    var existingChild = existing.TourPackageNightsValidity.Where(x => model.TourPackageNightsValidity.Any(m => m.Id == x.Id)).ToList();
                    foreach (var item in existingChild)
                    {
                        var update = model.TourPackageNightsValidity.FirstOrDefault(x => x.Id == item.Id);
                        item.Id = update.Id;
                        item.TourPackageNightsId = update.TourPackageNightsId;
                        item.HotelRoomTypeId = update.HotelRoomTypeId;
                        item.RateValidFrom = update.RateValidFrom == DateTime.MinValue ? DateTime.Now : update.RateValidFrom;
                        item.RateValidTo = update.RateValidTo == DateTime.MinValue ? DateTime.Now : update.RateValidTo;
                        item.TripleRateWeekDays = update.TripleRateWeekDays;
                        item.TwinRateWeekDays = update.TwinRateWeekDays;
                        item.SingleRateWeekDays = update.SingleRateWeekDays;
                        item.ChildWithBedRateWeekDays = update.ChildWithBedRateWeekDays;
                        item.ChildWithoutBedRateWeekDays = update.ChildWithoutBedRateWeekDays;
                        item.TripleRateWeekend = update.TripleRateWeekend;
                        item.TwinRateWeekend = update.TwinRateWeekend;
                        item.SingleRateWeekend = update.SingleRateWeekend;
                        item.ChildWithBedRateWeekend = update.ChildWithBedRateWeekend;
                        item.ChildWithoutBedRateWeekend = update.ChildWithoutBedRateWeekend;
                        item.DepartCityId = update.DepartCityId;
                        item.RateTypeApplied = (int)Enums.RateTypeApplied.Double; ////update.RateTypeApplied;
                        item.IsActive = update.IsActive;
                        item.UpdatedBy = update.UpdatedBy;
                        item.UpdatedDate = update.UpdatedDate;
                        item.ObjectState = EntityState.Modified;
                        ////child.Add(item);
                    }

                    var deletedChild = existing.TourPackageNightsValidity.Where(x => !model.TourPackageNightsValidity.Any(m => m.Id == x.Id)).ToList();
                    foreach (var item in deletedChild)
                    {
                        item.ObjectState = EntityState.Deleted;
                        this.tourPackageService.RemoveTourPackageNightsValidityModelEntity(item);
                        ////child.Add(item);
                    }

                    var addChild = model.TourPackageNightsValidity.Where(x => !existingChild.Any(m => m.Id == x.Id)).ToList();
                    foreach (var item in addChild)
                    {
                        item.RateTypeApplied = (int)Enums.RateTypeApplied.Double;

                        var childmapped = this.Mapper.Map<TourPackageNightsValidityModel>(item);
                        childmapped.ObjectState = EntityState.Added;
                        childmapped.TourPackageNightsId = model.Id;
                        existing.TourPackageNightsValidity.Add(childmapped);
                        ////child.Add(childmapped);
                    }

                    ////var mapped = this.Mapper.Map<TourPackageNightModel>(model);
                    ////mapped.ObjectState = EntityState.Modified;

                    existing.PackagePrice = model.PackagePrice;
                    existing.DepositAmount = model.DepositAmount;
                    existing.NoOfNights = model.NoOfNights;
                    existing.IsExtraNight = model.IsExtraNight;
                    existing.PackageDiscountPrice = model.PackageDiscountPrice;

                    //// existing.TourPackageNightsValidity = child;

                    await this.tourPackageService.UpdateTourPackageNightAsync(existing);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                if (model.TourPackageType == 2)
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = "packagehotellist", area = Constants.AreaAdmin });
                }
                else
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "tourpackage", action = "index", area = Constants.AreaAdmin });
                }
            }

            return this.PartialView("_RateValidty", model);
        }

        /// <summary>
        /// Gets the booking detail.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// BookingDetail
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> PackageNightsPrice(TourPackageNightViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var nights = await this.tourPackageService.GetAllTourPackageNightsBytourPackageIdAsync(model.TourPackageId, model.NoOfNights);
                var record = this.Mapper.Map<TourPackageNightModel>(model);
                if (model.Id == Guid.Empty)
                {
                    if (nights.Count == 0)
                    {
                        //// record.SetAuditInfo(0);
                        await this.tourPackageService.SavePackageNights(record);
                        return this.Content("success");
                    }

                    return this.Content("failure");
                }
                else
                {
                    ////var records = await this.tourPackageService.GetTourPackageNightBytouPackageIdAsync(model.Id);
                    if (nights.Count == 1 && nights.Select(x => x.Id).FirstOrDefault() == model.Id)
                    {
                        await this.tourPackageService.UpdateTourPackageNightRatesAsync(record);
                        return this.Content("update");
                    }
                }
            }

            var errorFields = this.ModelState.Where(ms => ms.Value.Errors.Any())
                                          .Select(x => new
                                          {
                                              key = x.Key,
                                              error = string.Join("; ", x.Value.Errors.Select(y => y.ErrorMessage))
                                          });

            return this.Content(JsonConvert.SerializeObject(errorFields));
        }

        /// <summary>
        /// Gets the booking detail.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="update">if set to <c>true</c> [update].</param>
        /// <param name="rowidentifier">The rowidentifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>
        /// BookingDetail
        /// </returns>
        public async Task<ActionResult> PackageRateValidty(TourPackageNightsValidityViewModel model, bool update, string rowidentifier, string from, string to)
        {
            model.Id = (model.Id == Guid.Empty) ? Guid.NewGuid() : model.Id;
            if (update && model.Id != Guid.Empty)
            {
                // get from DataBase
            }

            model.RowIdentifier = rowidentifier;
            model.CityList = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, model.DepartCityId, 0)).ToSelectList();
            model.HotelList = (await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, 0)).ToSelectList();
            if (!string.IsNullOrEmpty(from) && DateTime.TryParse(from, out DateTime dtFrom) &&
                !string.IsNullOrEmpty(to) && DateTime.TryParse(to, out DateTime dtTo))
            {
                model.RateValidFrom = dtFrom;
                model.RateValidTo = dtTo;
            }

            return this.PartialView("_RateValidty", model);
        }

        /// <summary>
        /// PackageAddRateValidity
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>_AddRateNightValidity</returns>
        public async Task<IActionResult> PackageAddRateValidity(Guid id)
        {
            TourPackageNightsValiditys model = new TourPackageNightsValiditys();
            var packageNightValiditydates = await this.tourPackageService.GetTourPackagesNightsValidityByTourpackagesnightsIDAsync(id);
            var packageNight = await this.packageNightsService.GetByIdAsync(id);
            if (packageNight != null)
            {
                var packageDates = await this.tourPackageService.GetByIdAsync(packageNight.TourPackageId);
                model.PackageValidFrom = packageDates.PackageValidFrom;
                model.PackageValidTo = packageDates.PackageValidTo;
                model.PackagePrice = packageNight.PackageDiscountPrice;
            }

            model.RateValidFrom = model.PackageValidFrom;
            model.RateValidTo = model.PackageValidTo;
                ////var packageNightValidityMap = new TourPackageNightsValidityViewModel();
                ////packageNightValidityMap.HotelRoomTypeId = packageNightValiditydates.HotelRoomTypeId;
                ////packageNightValidityMap.MaxAdult = packageNightValiditydates.MaxAdult;
                ////packageNightValidityMap.MaxChild = packageNightValiditydates.MaxChild;
                ////packageNightValidityMap.TwinRateWeekDays = packageNightValiditydates.TwinRateWeekDays;
                ////packageNightValidityMap.TwinRateWeekend = packageNightValiditydates.TwinRateWeekend;
                ////packageNightValidityMap.DepartCityId = packageNightValiditydates.DepartCityId;
                ////packageNightValidityMap.RoomCapacity = packageNightValiditydates.RoomCapacity;
                ////packageNightValidityMap.Descriptions = packageNightValiditydates.Descriptions;
                ////model.TourPackageNightsValidity = new List<TourPackageNightsValidityViewModel>();
                ////packageNightValidityMap.HotelList = (await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, packageNightValidityMap.HotelRoomTypeId)).ToSelectList();
                ////if (packageNightValidityMap.HotelList.Count() > 0)
                ////{
                ////    packageNightValidityMap.RoomTypeName = packageNightValidityMap.HotelList.First().Text;
                ////}

                ////packageNightValidityMap.CityList = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, packageNightValidityMap.DepartCityId, 0)).ToSelectList();
                ////if (packageNightValidityMap.CityList.Count() > 0)
                ////{
                ////    packageNightValidityMap.DepartCityName = packageNightValidityMap.CityList.First().Text;
                ////}

                ////model.TourPackageNightsValidity.Add(packageNightValidityMap);

                var list = await this.tourPackageService.GetTourPackagesNightsValidityListByTourpackagesnightsIDAsync(id);
                var lastdefault = list.LastOrDefault(x => x.TourPackageNightsId == id);
                list = list.Where(x => x.RateValidFrom == lastdefault.RateValidFrom && x.RateValidTo == lastdefault.RateValidTo).ToList();
                var models = this.Mapper.Map<List<TourPackageNightsValidityViewModel>>(list);
                foreach (var item in models)
                {
                    item.Id = Guid.Empty;
                    item.TourPackageNightsId = Guid.Empty;
                    item.CityList = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, item.DepartCityId, 0)).ToSelectList();
                    if (item.CityList.Count() > 0)
                    {
                        item.DepartCityName = item.CityList.First().Text;
                    }

                    item.HotelList = (await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, item.HotelRoomTypeId)).ToSelectList();
                    if (item.HotelList.Count() > 0)
                    {
                        item.RoomTypeName = item.HotelList.First().Text;
                    }

                model.TourPackageNightsValidity = models;
                ////model.RateValidFrom = dateTimefrom;
                ////model.RateValidTo = dateTimeto;
                ////model.PackagePrice = packageNightValue.PackageDiscountPrice;
            }

            model.TourPackageNightsId = id;
            model.Id = Guid.Empty;
            return this.PartialView("_RateValidtyForNight", model);
        }

        /// <summary>
        /// PackageEditRateValidity
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        /// <returns>_AddRateNightValidity</returns>
        public async Task<IActionResult> PackageEditRateValidity(Guid id, string from, string to)
        {
            if (id != Guid.Empty)
            {
                var packageNightValue = await this.packageNightsService.GetByIdAsync(id);
                DateTime dateTimefrom = Convert.ToDateTime(from);
                DateTime dateTimeto = Convert.ToDateTime(to);
                var list = await this.tourPackageService.GetTourPackagesNightsValidityListByAsync(id, dateTimefrom, dateTimeto);
                var models = this.Mapper.Map<List<TourPackageNightsValidityViewModel>>(list);
                foreach (var item in models)
                {
                    item.CityList = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, item.DepartCityId, 0)).ToSelectList();
                    if (item.CityList.Count() > 0)
                    {
                        item.DepartCityName = item.CityList.First().Text;
                    }

                    item.HotelList = (await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, item.HotelRoomTypeId)).ToSelectList();
                    if (item.HotelList.Count() > 0)
                    {
                        item.RoomTypeName = item.HotelList.First().Text;
                    }
                }

                var model = new TourPackageNightsValiditys
                {
                    TourPackageNightsId = id,
                    Id = id,
                    TourPackageNightsValidity = models,
                    RateValidFrom = dateTimefrom,
                    RateValidTo = dateTimeto,
                    PackagePrice = packageNightValue.PackageDiscountPrice
                };
                return this.PartialView("_RateValidtyForNight", model);
            }

            return this.View();
        }

        /// <summary>
        /// PackageAddRateValidity
        /// </summary>
        /// <returns>_AddRateNightValidity</returns>
        public IActionResult AddNewRow()
        {
            return this.PartialView("_AddRateNightValidity", new TourPackageNightsValidityViewModel());
        }

        /// <summary>
        /// Gets the booking detail.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="update">if set to <c>true</c> [update].</param>
        /// <param name="rowidentifier">The rowidentifier.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns>
        /// BookingDetail
        /// </returns>
        public async Task<ActionResult> PackageRateValidtyFor(TourPackageNightsValidityViewModel model, bool update, string rowidentifier, string from, string to)
        {
            model.Id = (model.Id == Guid.Empty) ? Guid.NewGuid() : model.Id;
            if (update && model.Id != Guid.Empty)
            {
                // get from DataBase
            }

            model.RowIdentifier = rowidentifier;
            model.CityList = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, model.DepartCityId, 0)).ToSelectList();
            model.HotelList = (await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, 0)).ToSelectList();
            if (!string.IsNullOrEmpty(from) && DateTime.TryParse(from, out DateTime dtFrom) &&
                !string.IsNullOrEmpty(to) && DateTime.TryParse(to, out DateTime dtTo))
            {
                model.RateValidFrom = dtFrom;
                model.RateValidTo = dtTo;
            }

            return this.PartialView("_RateValidtyForNight", model);
        }

        /// <summary>
        /// Adds the Rate.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="rowidentifier">The rowidentifier.</param>
        /// <returns>
        /// Add Rates Success messages
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddRateNightsValidity(TourPackageNightsValiditys model, string rowidentifier)
        {
            ////Caculation for Rate dates
            var packageNightValiditydates = await this.tourPackageService.GetTourPackagesNightsValidityByTourpackagesnightsIDAsync(model.TourPackageNightsId);
            var packageNight = await this.packageNightsService.GetByIdAsync(model.TourPackageNightsId);
            var packagesDates = await this.tourPackageService.GetByIdAsync(packageNight.TourPackageId);
            if (packageNightValiditydates != null)
            {
                if (model.RateValidFrom > packagesDates.PackageValidTo)
                {
                    return this.Content("Rooms are add Only between Package From  Date { " + packagesDates.PackageValidFrom + " }  To  Date { " + packagesDates.PackageValidTo + " }  ");
                }

                if (model.RateValidTo <= model.RateValidFrom || model.RateValidTo > packagesDates.PackageValidTo)
                {
                    return this.Content("Please select RateValidTo date alwayz greater from RateValidFrom Date = " + " { " + model.RateValidFrom + " }");
                }
            }
            else
            {
                if (model.RateValidTo <= packagesDates.PackageValidTo && model.RateValidTo > model.RateValidFrom)
                {
                    if (packageNight != null)
                    {
                        var atleartTodate = model.RateValidFrom.AddDays(packageNight.NoOfNights);
                        if (model.RateValidTo < atleartTodate)
                        {
                            return this.Content("Please select RateValidTo date is always greater then or eqaul to RateValidFrom date including with No of Nights... !");
                        }
                    }
                }
                else
                {
                    return this.Content("Please select RateValidTo date between package valid date = " + " { " + packagesDates.PackageValidFrom + " To " + packagesDates.PackageValidTo + " }");
                }
            }

            var roomtypelist = model.TourPackageNightsValidity.ToList();
            var duplicatesRoomType = roomtypelist.GroupBy(s => s.HotelRoomTypeId).Where(i => i.Count() > 1).Select(x => x.Select(m => m.RoomTypeName).FirstOrDefault()).ToList();
            ////var duplicatesCityList = roomtypelist.GroupBy(s => s.DepartCityId).Where(i => i.Count() > 1).Select(x => x.Select(m => m.DepartCityName).FirstOrDefault()).ToList();
            if (duplicatesRoomType.Count > 0)
            {
                var roomName = string.Empty;
                if (duplicatesRoomType.Count > 0)
                {
                    roomName = "Room Types are = ";
                    foreach (var item in duplicatesRoomType.ToList())
                    {
                        roomName += item + " ,";
                    }

                    roomName = roomName.TrimEnd(',');
                }

                return this.Content("You are selected duplicate " + " { " + roomName + "..! " + "  } ");
            }
            else
            {
                var fromDate = Convert.ToDateTime(model.RateValidFrom).ToString("dd-MM-yyyy");
                var toDate = Convert.ToDateTime(model.RateValidTo).ToString("dd-MM-yyyy");
                var exisingList = await this.tourPackageService.GetTourPackagesNightsValidityListByTourpackagesnightsIDAsync(model.TourPackageNightsId);
                var existing = await this.tourPackageService.GetTourPackagesNightsValidityListByAsync(model.TourPackageNightsId, model.RateValidFrom, model.RateValidTo);
                if (existing.Count > 0 || existing != null)
                {
                    var deletedChild = existing.Where(x => !model.TourPackageNightsValidity.Any(m => m.Id == x.Id)).ToList();
                    foreach (var items in deletedChild)
                    {
                        var count = await this.hotelBookingService.IsTourPackageNightValidityUsed(items.Id);
                        if (count == 0)
                        {
                            items.ObjectState = EntityState.Deleted;
                            this.tourPackageService.RemoveTourPackageNightsValidityModelEntity(items);
                        }

                        ////child.Add(item);
                    }

                    foreach (var item in model.TourPackageNightsValidity.ToList())
                    {
                        item.TourPackageNightsId = model.TourPackageNightsId;
                        item.RateValidFrom = model.RateValidFrom;
                        item.RateValidTo = model.RateValidTo;
                        var record = this.Mapper.Map<TourPackageNightsValidityModel>(item);

                        record.SetAuditInfo(0);
                        if (item.Id != Guid.Empty)
                        {
                            var existingChild = existing.Where(x => model.TourPackageNightsValidity.Any(m => m.Id == x.Id)).ToList();
                            var firstValue = existingChild.Where(x => x.Id == item.Id).ToList();
                            foreach (var itm in firstValue)
                            {
                                ////itm.RateValidFrom = item.RateValidFrom;
                                ////itm.RateValidTo = item.RateValidTo;
                                itm.HotelRoomTypeId = item.HotelRoomTypeId;
                                itm.DepartCityId = item.DepartCityId;
                                itm.TwinRateWeekend = item.TwinRateWeekend;
                                itm.TwinRateWeekDays = item.TwinRateWeekDays;
                                itm.RateTypeApplied = (int)Enums.RateTypeApplied.Double;
                                itm.MaxAdult = item.MaxAdult;
                                itm.MaxChild = item.MaxChild ?? (short)0;
                                itm.RoomCapacity = item.RoomCapacity;
                                itm.Descriptions = item.Descriptions;
                                await this.tourPackageService.UpdateTourPackageNightValidityAsync(itm);
                            }
                        }
                        else
                        {
                            await this.tourPackageService.InsertTourPackagesnightsValidityAsync(record);
                        }

                        var matchRecord = exisingList.Where(x => x.Id != item.Id && x.TourPackageNightsId == item.TourPackageNightsId && x.HotelRoomTypeId == item.HotelRoomTypeId);
                        foreach (var match in matchRecord)
                        {
                            match.MaxAdult = item.MaxAdult;
                            match.MaxChild = item.MaxChild ?? (short)0;
                            match.RoomCapacity = item.RoomCapacity;
                            await this.tourPackageService.UpdateTourPackageNightValidityAsync(match);
                        }
                    }

                    return this.Content("success");
                }

                ////foreach (var item in model.TourPackageNightsValidity.ToList())
                ////{
                ////    item.TourPackageNightsId = model.TourPackageNightsId;
                ////    item.RateValidFrom = model.RateValidFrom;
                ////    item.RateValidTo = model.RateValidTo;
                ////    var record = this.Mapper.Map<TourPackageNightsValidityModel>(item);

                ////    record.SetAuditInfo(0);
                ////    if (item.Id != Guid.Empty)
                ////    {
                ////        if (existing.Count > 0 || existing != null)
                ////        {
                ////            var existingChild = existing.Where(x => model.TourPackageNightsValidity.Any(m => m.Id == x.Id)).ToList();
                ////            var deletedChild = existing.Where(x => !model.TourPackageNightsValidity.Any(m => m.Id == x.Id)).ToList();
                ////            foreach (var items in deletedChild)
                ////            {
                ////                items.ObjectState = EntityState.Deleted;
                ////                this.tourPackageService.RemoveTourPackageNightsValidityModelEntity(items);
                ////                ////child.Add(item);
                ////            }

                ////            var firstValue = existingChild.Where(x => x.Id == item.Id).ToList();
                ////            foreach (var itm in firstValue)
                ////            {
                ////                ////itm.RateValidFrom = item.RateValidFrom;
                ////                ////itm.RateValidTo = item.RateValidTo;
                ////                itm.HotelRoomTypeId = item.HotelRoomTypeId;
                ////                itm.DepartCityId = item.DepartCityId;
                ////                itm.TwinRateWeekend = item.TwinRateWeekend;
                ////                itm.TwinRateWeekDays = item.TwinRateWeekDays;
                ////                itm.RateTypeApplied = (int)Enums.RateTypeApplied.Double;
                ////                await this.tourPackageService.UpdateTourPackageNightValidityAsync(itm);
                ////            }
                ////        }
                ////    }

                ////    ////return this.Content("updated");
                ////    else
                ////    {
                ////        await this.tourPackageService.InsertTourPackagesnightsValidityAsync(record);
                ////    }
                ////}
            }

            return this.Content("success");
        }

        /// <summary>
        /// Adds the Rate.
        /// </summary>
        /// <param name="id">The rowidentifier.</param>
        /// <returns>
        /// Add Rates Success messages
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetpackageNightsValidityListByPackageNightsID(Guid id)
        {
            if (id != Guid.Empty)
            {
                var list = await this.tourPackageService.GetTourPackagesNightsValidityListByTourpackagesnightsIDAsync(id);
                var model = this.Mapper.Map<List<TourPackageNightsValidityViewModel>>(list);
                var lastdefault = model.LastOrDefault(x => x.TourPackageNightsId == id);
                var modelist = model.GroupBy(x => new { x.RateValidFrom, x.RateValidTo }).ToList();
                TourPackageNightsValiditys modal = new TourPackageNightsValiditys();
                foreach (var items in model)
                {
                    var roomtype = await this.masterService.GetHotelRomeTypeByTypeIDAsync(items.HotelRoomTypeId);
                    items.RoomTypeName = roomtype.Name;
                }

                modal.TourPackageNightsValidity = model;
                modal.TourPackageNightsId = id;
                return this.PartialView("_GroupRateValidityForNigts", modal);
            }

            return this.View();
        }

        /// <summary>
        /// Adds the Rate.
        /// </summary>
        /// <param name="id">The rowidentifier.</param>
        /// <param name="from">The from.</param>
        /// <param name="to">The to.</param>
        /// <returns>
        /// Add Rates Success messages
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> DeletepackageNightsValidityByPackageNightsID(Guid id, string from, string to)
        {
            if (id != Guid.Empty)
            {
                DateTime dateTimefrom = Convert.ToDateTime(from);
                DateTime dateTimeto = Convert.ToDateTime(to);
                //// var fromDate = from.ToString("MM/dd/yyyy HH:mm:ss");
                //// var toDate = to.ToString("MM/dd/yyyy HH:mm:ss");
                var list = await this.tourPackageService.GetTourPackagesNightsValidityListByAsync(id, dateTimefrom, dateTimeto);
                ////var model = this.Mapper.Map<List<TourPackageNightsValidityViewModel>>(list);
                ////var modelist = model.GroupBy(x => x.RateValidFrom).ToList();
                ////TourPackageNightsValiditys modal = new TourPackageNightsValiditys();

                foreach (var items in list)
                {
                    ////var validfrom = items.RateValidFrom.ToString();
                    ////var validto = items.RateValidTo.ToString();
                    ////if (validfrom == from && validto == to)
                    ////{
                    await this.tourPackageService.TourPackageValidityDeleteAsync(items);
                    ////}
                }

                ////modal.TourPackageNightsValidity = model;
                return this.Content("success");
            }

            return this.View();
        }

        /// <summary>
        /// Adds the contact.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="rowidentifier">The rowidentifier.</param>
        /// <returns>
        /// Add Contact in List
        /// </returns>
        [HttpPost]
        public IActionResult AddRateValidity(TourPackageNightsValidityViewModel model, string rowidentifier)
        {
            var fromDate = Convert.ToDateTime(model.RateValidFrom).ToString("dd-MM-yyyy");
            var toDate = Convert.ToDateTime(model.RateValidTo).ToString("dd-MM-yyyy");

            if (!string.IsNullOrEmpty(model.RowId))
            {
                return this.PartialView("_RateValidityRow", model);
            }

            if (!string.IsNullOrEmpty(model.RowIdentifier))
            {
                try
                {
                    foreach (var item in model.RowIdentifier.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (item.Split(':')[0] == fromDate && item.Split(':')[1] == toDate)
                        {
                            model.TrIndentifier = item.Split(':')[2];
                            return this.PartialView("_RateValidityRow", model);
                        }
                    }
                }
                catch
                {
                }
            }

            var grpModel = new TourPackageNightsValidityGroupedViewModel()
            {
                FromDate = Convert.ToDateTime(model.RateValidFrom),
                ToDate = Convert.ToDateTime(model.RateValidTo),
                Records = new List<TourPackageNightsValidityViewModel> { model }
            };

            ////if (this.IsAjaxRequest())
            ////{
            ////    return this.Json(new { success = true });
            ////}

            return this.PartialView("_GroupRateValidity", grpModel);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Record</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await this.tourPackageService.GetByIdAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            await this.tourPackageService.DeleteAsync(category);
            this.ShowMessage(Messages.DeletedSuccessfully);
            return this.Json("success");
        }

        /// <summary>
        /// Adds the new row.
        /// </summary>
        /// <returns>
        /// json result
        /// </returns>
        public IActionResult AddNewCity()
        {
            var model = new TourPackageCityViewModel();
            return this.PartialView("_TourPackageCity", model);
        }

        /// <summary>
        /// News the package image.
        /// </summary>
        /// <param name="seq">The seq.</param>
        /// <returns>Tour package image</returns>
        public IActionResult NewPackageImage(short seq)
        {
            seq += 1;
            var model = new TourPackageImageViewModel
            {
                SequenceNo = seq
            };
            model.Id = Guid.NewGuid();
            return this.PartialView("_TourPackageImage", model);
        }

        /// <summary>
        /// Adds the new row.
        /// </summary>
        /// <returns>
        /// json result
        /// </returns>
        public IActionResult AddNewBookingDate()
        {
            TourPackageBookDateViewModel model = new TourPackageBookDateViewModel();
            return this.PartialView("_TourPackageBookDate", model);
        }

        /// <summary>
        /// Udpates the order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>UdpateOrder</returns>
        [HttpPost]
        public async Task<IActionResult> UdpateOrder(Guid id, short index)
        {
            var result = await this.tourPackageService.GetTourPackageImageByIdAsync(id);
            result.SequenceNo = index;
            await this.tourPackageService.TourPackageImageUpdateAsync(result);
            return this.Json(new { Status = true });
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Record</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteImage(string id)
        {
            var wwwrootPath = this.environment.WebRootPath;
            var ids = id.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in ids)
            {
                Guid imageid;
                if (Guid.TryParse(item, out imageid))
                {
                    var packageImage = await this.tourPackageService.GetTourPackageImageByIdAsync(imageid);
                    if (packageImage == null)
                    {
                        return this.NotFound();
                    }

                    await this.tourPackageService.DeleteTourPackageImageAsync(packageImage);

                    string fullPath = wwwrootPath + "//packages//" + packageImage.ImageName;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            }

            return this.Json(new { Status = true, Message = Messages.DeletedSuccessfully });
        }

        /// <summary>
        /// Changes the active status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChangeActiveStatus</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(Guid id)
        {
            var category = await this.tourPackageService.GetTourPackageByIdAsyn(id);
            if (category == null)
            {
                return this.NotFound();
            }

            category.IsActive = !category.IsActive;
            await this.tourPackageService.UpdateAsync(category);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "TourPackage", action = "index", area = Constants.AreaAdmin });
            }
        }

        /// <summary>
        /// Duplicates the category.
        /// </summary>
        /// <param name="noofnights">The noofnights.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Get Duplicate Category
        /// </returns>
        public async Task<JsonResult> IsDuplicate(int noofnights, Guid id)
        {
            return this.Json(await this.tourPackageService.IsDuplicateAsync(noofnights, id));
        }

        /// <summary>
        /// Determines whether [is duplicate URL] [the specified urltitle].
        /// </summary>
        /// <param name="urltitle">The urltitle.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>IsDuplicateUrl</returns>
        public async Task<JsonResult> IsDuplicateUrl(string urltitle, Guid id)
        {
            return this.Json(await this.tourPackageService.IsDuplicateUrl(urltitle, id));
        }

        private void UpdateTourPackage(TourPackageModel source, TourPackageViewModel model)
        {
            source.PackageName = model.PackageName;
            source.TourPackageType = model.TourPackageType;
            source.Prefix = model.Prefix;
            ////source.DealCode = model.DealCode;
            source.Suffix = model.Suffix;
            source.PackageName = model.PackageName;
            source.UrlTitle = model.UrlTitle.Trim();
            source.DealTypeId = model.DealTypeId;
            source.PackageValidFrom = model.PackageValidFrom ?? DateTime.Now;
            source.PackageValidTo = model.PackageValidTo ?? DateTime.Now;
            source.HotelId = model.HotelId;
            source.IsFlightIncluded = model.IsFlightIncluded;
            source.IsHotelOnly = model.IsHotelOnly;
            source.PackageDescription = model.PackageDescription;
            source.Quote = model.Quote;
            source.HighLights = model.HighLights;
            source.Programs = model.Programs;
            source.HotelDescription = model.HotelDescription;
            source.HotelReview = model.HotelReview;
            source.MapScript = model.MapScript;
            ////source.IsActive = model.IsActive;
            source.UpdatedBy = model.UpdatedBy;
            source.UpdateAuditInfo(model.UpdatedBy);
        }

        /// <summary>
        /// Uploads the package images.
        /// </summary>
        /// <param name="tourPackageImage">The tour package image.</param>
        /// <param name="packageid">The packageid.</param>
        /// <returns>
        /// UploadPackageImages
        /// </returns>
        private async Task<List<TourPackageImageModel>> UploadPackageImages(ICollection<TourPackageImageViewModel> tourPackageImage, Guid packageid)
        {
            var packageImages = new List<TourPackageImageModel>();
            foreach (var item in tourPackageImage)
            {
                if (item.File != null)
                {
                    var uploads = Path.Combine(this.environment.WebRootPath, "packages");
                    if (!Directory.Exists(uploads))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(uploads);
                    }

                    if (item.File.Length > 0)
                    {
                        var fileName = item.Id == Guid.Empty ? item.ImageName : $"{Guid.NewGuid().ToString()}{Path.GetExtension(item.File.FileName)}";
                        var filePath = Path.Combine(uploads, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            if (item.Id != Guid.Empty)
                            {
                                var pacakgeImage = new TourPackageImageModel() { TourPackageId = packageid, ImageName = fileName, AltTag = item.AltTag, ImageDescription = item.ImageDescription, SequenceNo = item.SequenceNo };
                                ////pacakgeImage.SetAuditInfo(Guid.Empty.ToString());
                                packageImages.Add(pacakgeImage);
                            }

                            await item.File.CopyToAsync(fileStream);
                        }
                    }
                }
            }

            return packageImages;
        }
    }
}