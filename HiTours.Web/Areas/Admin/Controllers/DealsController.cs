// <copyright file="DealsController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// UploadBannersController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class DealsController : AdminController
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IDealService dealService;
        private readonly IHotelierService hotelierService;
        private readonly IConfiguration configurationService;
        private readonly IVendorService vendorService;
        private readonly IPromotionService promotionService;
        private readonly IVisaService visaService;
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly ICancellationService cancellationService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealsController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cityService">City Repository</param>
        /// <param name="countryService">Country Repository</param>
        /// <param name="dealService">Deal Service</param>
        /// <param name="vendorService">Vendor Service</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="hostingEnvironment">Hosting Service</param>
        /// <param name="promotionService">Promotion Service</param>
        /// <param name="hotelierService">Hotelier Service</param>
        /// <param name="visaService">Visa Service</param>
        /// <param name="cancellationService">Cancellation Service</param>
        public DealsController(
            IConfiguration configuration,
            IMapper mapper,
            ICityService cityService,
            ICountryService countryService,
            IDealService dealService,
            IVendorService vendorService,
            IMasterService masterService,
            ICancellationService cancellationService,
            IHostingEnvironment hostingEnvironment,
            IPromotionService promotionService,
            IHotelierService hotelierService,
            IVisaService visaService)
           : base(mapper, configuration)
        {
            this.cityService = cityService;
            this.countryService = countryService;
            this.configurationService = configuration;
            this.vendorService = vendorService;
            this.visaService = visaService;
            this.hotelierService = hotelierService;
            this.promotionService = promotionService;
            this.dealService = dealService;
            this.masterService = masterService;
            this.cancellationService = cancellationService;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="type">Deal Type</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> List([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int type)
        {
            this.ViewBag.DealType = type;
            if (this.IsAjaxRequest())
            {
                var result = await this.dealService.GetDealsAsync(model, type);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ChangeDealActiveStatus(int id)
        {
            try
            {
                var record = await this.dealService.GetDealPackageAsync(id);
                record.IsActive = !record.IsActive;
                await this.dealService.UpdateDealPackageInfoAsync(record);
                return this.Json("success");
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return this.Json("failure");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageType">PackageType</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteDeal(int id, int packageType)
        {
            try
            {
                var record = await this.dealService.GetDealPackageAsync(id);
                record.IsDeleted = !record.IsDeleted;
                await this.dealService.UpdateDealPackageInfoAsync(record);
                this.ShowMessage("Deleted Successfully");
                return this.RedirectToAction("List", new { @type = packageType });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage("Delete Failed", Enums.MessageType.Error);
                return this.RedirectToAction("List", new { @type = packageType });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult Manage(int id, int packageTypeId)
        {
            this.ViewBag.PackageId = id;
            this.ViewBag.PackageTypeId = packageTypeId;
            this.ViewBag.PackageTypeName = this.GetPackageTypeName(packageTypeId);
            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">packageTypeId</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> Itinerary(int id, int packageTypeId)
        {
            this.ViewBag.PackageId = id;
            this.ViewBag.packageTypeId = packageTypeId;
            var result = await this.dealService.GetNightsAsync(id);
            List<DealsNightViewModel> dealNightModels = this.Mapper.Map<List<DealsNightViewModel>>(result);
            foreach (DealsNightViewModel item in dealNightModels)
            {
                item.DealsItineraryViewModels = this.Mapper.Map<List<DealsItineraryViewModel>>(await this.dealService.GetItinerariesAsync(item.Id));
            }

            return this.PartialView(dealNightModels);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> HotelRooms(int id)
        {
            this.ViewBag.PackageId = id;
            DealsHotelInfoViewModel model = await this.dealService.GetDealPackageHotelInfoByPackageIdAsync(id);
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> RoomsPackage(int packageId)
        {
            this.TempData["packageId"] = this.ViewBag.PackageId = packageId;
            List<DealRoomConfigViewModel> model = await this.dealService.GetDealPackageRoomConfigByPackageIdAsync(packageId);
            return this.PartialView("_DealRoom", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The Model.</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> RoomsPackage(List<DealRoomConfigViewModel> model)
        {
            ////List<DealRoomConfigViewModel> model = await this.dealService.GetDealPackageRoomConfigByPackageIdAsync(packageId);
            try
            {
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        if (item.IsDeleted)
                        {
                            await this.dealService.DealDeletePackageRoomConfig(item.Id);
                        }
                        else if (item.Id > 0)
                        {
                            var record = this.Mapper.Map<DealRoomConfigurationModel>(item);
                            await this.dealService.UpdateDealPackageRoomConfig(record);
                        }
                    }
                }

                this.ShowMessage("Updated Successfully");
                this.TempData["nextview"] = "#rooms";
                return this.RedirectToAction("Manage", new { @packageTypeId = 1, @id = this.TempData["packageId"] });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage("Update  Failed", Enums.MessageType.Error);
                this.TempData["nextview"] = "#rooms";
                return this.RedirectToAction("Manage", new { @packageTypeId = 1, @id = this.TempData["packageId"] });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The Hotelier identifier.</param>
        /// <param name="packageId">The Package identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> RoomsMaster(int hotelId, int packageId)
        {
            this.ViewBag.PackageId = packageId;
            var hotelierRoomConfig = await this.hotelierService.GetAllHotelierRoomConfigByHotelIdAsync(hotelId);
            List<HotelierRoomConfigurationViewModel> model = new List<HotelierRoomConfigurationViewModel>();
            foreach (var item in hotelierRoomConfig)
            {
                var hotelierRommConfigItem = this.Mapper.Map<HotelierRoomConfigurationViewModel>(item);
                hotelierRommConfigItem.PackageHotelRoomTypeViewModel = this.Mapper.Map<PackageHotelRoomTypeViewModel>(item.PackageHotelRoomTypeModel);
                model.Add(hotelierRommConfigItem);
            }

            this.TempData["packageId"] = packageId;
            return this.PartialView("_DealRoomsMaster", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The Hotelier identifier.</param>
        /// <param name="packageId">The Package identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> GetHotelRoomsTour(int hotelId, int packageId)
        {
            this.ViewBag.PackageId = packageId;
            var hotelierRoomConfig = await this.hotelierService.GetAllHotelierRoomConfigByHotelIdAsync(hotelId);
            List<HotelierRoomConfigurationViewModel> model = new List<HotelierRoomConfigurationViewModel>();
            foreach (var item in hotelierRoomConfig)
            {
                var hotelierRoomConfigItem = this.Mapper.Map<HotelierRoomConfigurationViewModel>(item);
                hotelierRoomConfigItem.PackageHotelRoomTypeViewModel = this.Mapper.Map<PackageHotelRoomTypeViewModel>(item.PackageHotelRoomTypeModel);
                model.Add(hotelierRoomConfigItem);
            }

            this.TempData["packageId"] = packageId;
            return this.PartialView("_AddHotelRoomsForTours", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Hotelier Room Config Model</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> RoomsMaster(List<HotelierRoomConfigurationViewModel> model)
        {
            this.ViewBag.PackageId = this.TempData["packageId"];
            this.TempData["nextview"] = "#rooms";
            try
            {
                foreach (var item in model)
                {
                    if (item.IsSelected)
                    {
                        var record = this.dealService.GetInclusionRecordForHotelFromPackageId(Convert.ToInt32(this.TempData["packageId"]));
                        DealRoomConfigurationModel dealRoomModel = new DealRoomConfigurationModel
                        {
                            Adult = item.Adult,
                            AdultAge = item.AdultAge,
                            CardImg = item.CardImg,
                            Child = item.Child,
                            ChildAge = item.ChildAge,
                            Description = item.Description,
                            FreeChild = item.FreeChild,
                            FreeInfant = item.FreeInfant,
                            Id = 0,
                            Infant = item.Infant,
                            InfantAge = item.InfantAge,
                            Max = item.Max,
                            IsActive = true,
                            InclusionId = record.Id,
                            RoomTypeId = (short)item.RoomTypeId
                        };
                        dealRoomModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealService.AddDealPackageHotelRoomConfiguration(dealRoomModel);
                    }
                }

                this.ShowMessage("Moved Successfully");
                return this.RedirectToAction("Manage", new { @packageTypeId = 1, @id = this.TempData["packageId"] });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage("Moved Failed", Enums.MessageType.Error);
                return this.RedirectToAction("Manage", new { @packageTypeId = 1, @id = this.TempData["packageId"] });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Hotelier Room Config Model</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> AddInclusionHotelRoomConfig(DealsInclusionViewModel model)
        {
            try
            {
                DealsInclusionModel dealInclusionHotelForTour = new DealsInclusionModel
                {
                    TypeId = model.TypeId,
                    ItineraryId = model.ItineraryId,
                    VendorInfoId = model.VendorId,
                    IsChargeable = model.IsChargeable
                };
                dealInclusionHotelForTour.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                int inclusionId = await this.dealService.AddDealsInclusionAsync(dealInclusionHotelForTour);
                if (inclusionId != 0)
                {
                    foreach (var item in model.RoomConfigurations)
                    {
                        if (item.IsSelected)
                        {
                            DealRoomConfigurationModel dealRoomModel = new DealRoomConfigurationModel
                            {
                                Adult = item.Adult,
                                AdultAge = item.AdultAge,
                                CardImg = item.CardImg,
                                Child = item.Child,
                                ChildAge = item.ChildAge,
                                Description = item.Description,
                                FreeChild = item.FreeChild,
                                FreeInfant = item.FreeInfant,
                                Id = 0,
                                Infant = item.Infant,
                                InfantAge = item.InfantAge,
                                Max = item.Max,
                                IsActive = true,
                                InclusionId = inclusionId,
                                RoomTypeId = (short)item.RoomTypeId
                            };
                            dealRoomModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.AddDealPackageHotelRoomConfiguration(dealRoomModel);
                        }
                    }
                }

                ////this.TempData["nextview"] = "#itinerary";
                ////this.ShowMessage("Inclusion Added");
                return this.Json("Success");
            }
            catch (Exception ex)
            {
                this.TempData["nextview"] = "#itinerary";
                return this.Json("Failure => " + ex);
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The Deal ID</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> Flights(int id)
        {
            this.ViewBag.PackageId = id;
            var inclusionRecord = this.dealService.GetInclusionRecordForHotelFromPackageId(id);
            var flightRecord = this.dealService.GetHotelFlightRecordFromInclusionId(inclusionRecord.Id);
            DealsFlightViewModel model = new DealsFlightViewModel
            {
                Id = 0,
                InclusionId = inclusionRecord.Id,
                FlightId = 0
            };

            if (flightRecord != null && flightRecord.Id > 0)
            {
                model = this.Mapper.Map<DealsFlightViewModel>(flightRecord);
                model.FlightId = model.Id;
                model.OriginItems = string.IsNullOrEmpty(model.Origin) ? new List<SelectListItem>() : (await this.dealService.GetAirportsCodesDropdownAsync(string.Empty, 1, model.Origin)).ToSelectList();
                model.DestinationItems = string.IsNullOrEmpty(model.Destination) ? new List<SelectListItem>() : (await this.dealService.GetAirportsCodesDropdownAsync(string.Empty, 1, model.Destination)).ToSelectList();
            }

            this.TempData["packageId"] = id;
            return this.PartialView("AddFlightInclusionForTour", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The MOdel</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> Flights(DealsFlightViewModel model)
        {
            this.ViewBag.PackageId = this.TempData["packageId"];
            model.Id = model.FlightId;
            var record = this.Mapper.Map<DealsFlightModel>(model);
            if (record.Id > 0)
            {
                //// Update Record
                record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.UpdateDealFlightAsync(record);
                this.ShowMessage("Successfully Updated");
            }
            else
            {
                //// Insert Record
                record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.AddDealsFlightAsync(record);
                this.ShowMessage("Successfully Inserted");
            }

            this.TempData["nextview"] = "#flights";
            return this.RedirectToAction("Manage", new { @packageTypeId = 1, @id = this.TempData["packageId"] });
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public ActionResult Routes(int id)
        {
            this.ViewBag.PackageId = id;
            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">Package type id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> Visa(int id, int packageTypeId)
        {
            this.ViewBag.PackageId = this.TempData["packageId"] = id;
            this.ViewBag.PackageType = this.TempData["packageTypeId"] = packageTypeId;
            var visaItems = await this.dealService.GetVisaItemsByPackageId(id);

            if (visaItems.Count > 0)
            {
                foreach (var item in visaItems)
                {
                    item.VendorItems = item.VendorId == 0 ? new List<SelectListItem>() : (await this.visaService.GetVendorVisaDropDownListAsync(string.Empty, 0, item.VendorId)).ToSelectList();
                    item.CountriesItems = item.CountryId == 0 ? new List<SelectListItem>() : (await this.masterService.GetPackageCountryListAsync(string.Empty, 0, item.CountryId, 0)).ToSelectList();
                }
            }

            return this.PartialView(visaItems);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">Package id</param>
        /// <param name="from">from</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> ImportVisaFromDestination(int packageId, string from)
        {
            if (from == "Destination")
            {
                this.ViewBag.PackageId = this.TempData["packageId"];
                this.ViewBag.PackageType = this.TempData["packageTypeId"];
                int flag = await this.dealService.MoveVisaItemsByPackageDestinations(packageId);
                if (flag == 1)
                {
                    this.TempData["nextview"] = "#visa";
                    this.ShowMessage("Successfully Imported");
                    return this.Json("Success");
                }
                else if (flag == 0)
                {
                    return this.Json("nodata");
                }
                else
                {
                    this.TempData["nextview"] = "#visa";
                    this.ShowMessage("Internal Server Error", Enums.MessageType.Error);
                    return this.Json("error");
                }
            }
            else
            {
                var visaRecord = await this.dealService.GetVisaByCountryId((short)packageId);

                return this.Json(visaRecord);
                ////if (flag == 1)
                ////{
                ////    this.TempData["nextview"] = "#visa";
                ////    this.ShowMessage("Successfully Imported");
                ////    return this.Json("Success");
                ////}
                ////else if (flag == 0)
                ////{
                ////    return this.Json("nodata");
                ////}
                ////else
                ////{
                ////    this.TempData["nextview"] = "#visa";
                ////    this.ShowMessage("Internal Server Error", Enums.MessageType.Error);
                ////    return this.Json("error");
                ////}
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The Model.</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> Visa(List<DealVisaViewModel> model)
        {
            this.ViewBag.PackageId = this.TempData["packageId"];
            this.ViewBag.PackageTypeId = this.TempData["packageTypeId"];
            this.TempData["nextview"] = "#visa";
            try
            {
                foreach (var item in model)
                {
                    if (item.IsDeleted)
                    {
                        if (item.Id > 0)
                        {
                            await this.dealService.DeletePackageVisaById(item.Id);
                        }
                    }
                    else
                    {
                        var record = this.Mapper.Map<DealVisaModel>(item);
                        var visaRecord = await this.dealService.GetVisaByCountryId(record.CountryId);
                        if (visaRecord != null)
                        {
                            record.DocumentsRequired = visaRecord.DocumentsRequired;
                            record.GeneralPolicy = visaRecord.GeneralPolicy;
                            record.Markup = visaRecord.Markup;
                            record.PhotoSpecification = visaRecord.PhotoSpecification;
                            record.ProcessingTime = visaRecord.ProcessingTime;
                        }

                        if (record.Id > 0)
                        {
                            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.UpdateDealPackageVisa(record);
                        }
                        else
                        {
                            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.AddDealPackageVisa(record);
                        }
                    }
                }

                this.ShowMessage("Successfully Updated");
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id = this.ViewBag.PackageId, packageTypeId = this.ViewBag.PackageTypeId });
            }
            catch (Exception ex)
            {
                this.ShowMessage("Updated Failed", Enums.MessageType.Error);
                string msg = ex.ToString();
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id = this.ViewBag.PackageId, packageTypeId = this.ViewBag.PackageTypeId });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The Package Id.</param>
        /// <param name="rowcount">The rowcount.</param>
        /// <returns>Itinerary</returns>
        public ActionResult AddVisaRow(int packageId, int rowcount)
        {
            this.ViewBag.PackageId = packageId;
            DealVisaViewModel model = new DealVisaViewModel
            {
                Id = 0,
                IsActive = true,
                PackageId = packageId,
                IsDeleted = false,
                CountriesItems = new List<SelectListItem>(),
                VendorItems = new List<SelectListItem>(),
                RowCount = rowcount
            };
            return this.PartialView("_AddVisaRow", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public ActionResult Insurance(int id)
        {
            this.ViewBag.PackageId = id;
            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public ActionResult Promotion(int id)
        {
            this.ViewBag.PackageId = id;
            return this.PartialView("_PromotionGrid");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Itinerary</returns>
        public ActionResult PromotionGrid(int id)
        {
            this.ViewBag.PackageId = id;
            return this.PartialView("_PromotionGrid");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Grid Model</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Itinerary</returns>
        public async Task<ActionResult> PromotionGridData([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int id)
        {
            this.ViewBag.PackageId = id;
            if (this.IsAjaxRequest())
            {
                var result = await this.dealService.GetDealsPromotionsAsync(model, id);

                return this.Json(result);
            }

            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="promotionId">Promotion Id.</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>Itinerary</returns>
        public async Task<ActionResult> PromotionAdd(int promotionId, int packageId)
        {
            this.ViewBag.PackageId = packageId;
            DealsPromotionViewModel model = new DealsPromotionViewModel
            {
                Id = 0,
                IsActive = true,
                IsDeleted = false,
                PackageId = packageId
            };
            var roomIds = this.dealService.GetAllRoomTypeIdsFromPackageId(packageId);
            this.ViewBag.RoomIds = string.Join(',', roomIds);
            if (promotionId > 0)
            {
                var record = await this.dealService.GetDealPromotionById(promotionId);
                model = this.Mapper.Map<DealsPromotionViewModel>(record);
                if (record.DealPromotionRoomTypeModel != null && record.DealPromotionRoomTypeModel.Count > 0)
                {
                    model.RoomTypeItems = (await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, Convert.ToInt32(record.DealPromotionRoomTypeModel.Select(x => x.RoomTypeId).FirstOrDefault()))).ToSelectList();
                }
                else
                {
                    model.RoomTypeItems = new List<SelectListItem>();
                }
            }

            model.PromotionTypeItems = (await this.promotionService.GetPromotionTypeItems()).ToSelectList();
            model.MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList();
            return this.PartialView("_PromotionAdd", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Promotion View Model</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> PromotionAdd(DealsPromotionViewModel model)
        {
            this.ViewBag.PackageId = model.PackageId;
            if (this.ModelState.IsValid)
            {
                try
                {
                    var record = this.Mapper.Map<DealsPromotionModel>(model);
                    if (model.Id > 0)
                    {
                        ////Update
                        record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealService.UpdateDealsPromotionAsync(record);
                        var promoRoomTypeRecord = await this.dealService.GetDealPromotionRoomTypeRecord(record.Id);
                        if (promoRoomTypeRecord != null)
                        {
                            if (model.RoomType != null)
                            {
                                promoRoomTypeRecord.RoomTypeId = (short)model.RoomType;
                                await this.dealService.UpdateDealPromotionRoomTypeRecord(promoRoomTypeRecord);
                            }
                            else
                            {
                                await this.dealService.DeletePromotionRoomTypeRecord(promoRoomTypeRecord);
                            }
                        }
                        else if (model.RoomType != null)
                        {
                            DealsPromotion_RoomType promoRoomTypeModel = new DealsPromotion_RoomType
                            {
                                Id = 0,
                                PromotionId = model.Id,
                                RoomTypeId = (short)model.RoomType
                            };
                            await this.dealService.AddDealPromotionRoomTypeRecord(promoRoomTypeModel);
                        }

                        this.TempData["nextview"] = "#promotions";
                        this.ShowMessage("Updated Successfully");
                        return this.Json("update");
                    }
                    else
                    {
                        ////Add
                        record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        var recordID = await this.dealService.AddDealsPromotionAsync(record);
                        if (model.RoomType != null && model.RoomType != 0)
                        {
                            DealsPromotion_RoomType promoRoomTypeModel = new DealsPromotion_RoomType
                            {
                                Id = 0,
                                PromotionId = recordID,
                                RoomTypeId = (short)model.RoomType
                            };
                            await this.dealService.AddDealPromotionRoomTypeRecord(promoRoomTypeModel);
                        }

                        this.TempData["nextview"] = "#promotions";
                        this.ShowMessage("Added Successfully");
                        return this.Json("success");
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                    return this.Json("success");
                }
            }

            return this.PartialView("_PromotionAdd");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">Promotion View Model</param>
        /// <returns>Itinerary</returns>
        public async Task<ActionResult> DeletePromotion(int id)
        {
            try
            {
                var record = await this.dealService.GetDealPromotionById(id);
                record.IsDeleted = !record.IsDeleted;
                await this.dealService.UpdateDealsPromotionAsync(record);
                this.ShowMessage(Messages.DeletedSuccessfully);
                this.TempData["nextview"] = "#promotions";
                return this.Json("success");
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage("Delete Failed", Enums.MessageType.Error);
                return this.Json("failure");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ChangePromotionActiveStatus(int id)
        {
            try
            {
                var record = await this.dealService.GetDealPromotionById(id);
                if (record == null)
                {
                    return this.NotFound();
                }

                record.IsActive = !record.IsActive;
                await this.dealService.UpdateDealsPromotionAsync(record);

                if (this.IsAjaxRequest())
                {
                    return this.Json(new { Status = true });
                }
                else
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Vendor", action = "index", area = Constants.AreaAdmin });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return this.Json(new { Status = false });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">Promotion View Model</param>
        /// <returns>Itinerary</returns>
        public async Task<ActionResult> DeleteReview(int id)
        {
            try
            {
                await this.dealService.DeleteDealsReviewAsync(id);
                this.ShowMessage(Messages.DeletedSuccessfully);
                this.TempData["nextview"] = "#reviews";
                return this.Json("success");
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage("Delete Failed", Enums.MessageType.Error);
                return this.Json("failure");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ChangeReviewActiveStatus(int id)
        {
            try
            {
                var record = await this.dealService.GetDealsReviewsById(id);
                if (record == null)
                {
                    return this.NotFound();
                }

                record.IsActive = !record.IsActive;
                await this.dealService.UpdateDealsReviewAsync(record);

                if (this.IsAjaxRequest())
                {
                    return this.Json(new { Status = true });
                }
                else
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Vendor", action = "index", area = Constants.AreaAdmin });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return this.Json(new { Status = false });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="nightId">The identifier.</param>
        /// <param name="packageTypeId">packageTypeId</param>
        /// <param name="packageId">packageId</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> ManageItinerary(int nightId, int packageTypeId, int packageId)
        {
            List<DealsItineraryViewModel> itineraryModels = new List<DealsItineraryViewModel>();
            var result = await this.dealService.GetItinerariesAsync(nightId);
            ////itineraryModels = this.Mapper.Map<List<DealsItineraryViewModel>>(result);
            try
            {
                for (int i = 0; i < result.Count; i++)
                {
                    itineraryModels.Add(this.Mapper.Map<DealsItineraryViewModel>(result[i]));
                    itineraryModels[i].InclusionModels = new List<DealsInclusionViewModel>();
                    for (int k = 0; k < result[i].DealsInclusionModels.Count; k++)
                    {
                        DealsInclusionViewModel inclusionViewModel = this.Mapper.Map<DealsInclusionViewModel>(result[i].DealsInclusionModels[k]);
                        if (inclusionViewModel.TypeId == 1)
                        {
                            inclusionViewModel.HotelName = (await this.hotelierService.GetHotelierInfoAsync(result[i].DealsInclusionModels[k].VendorInfoId != null ? Convert.ToInt32(result[i].DealsInclusionModels[k].VendorInfoId) : 0)).Name;
                        }

                        itineraryModels[i].InclusionModels.Add(inclusionViewModel);
                        itineraryModels[i].InclusionModels[k].RoomConfigurations = new List<DealRoomConfigViewModel>();
                        for (int m = 0; m < result[i].DealsInclusionModels[k].DealRoomConfigurationModels.Count; m++)
                        {
                            DealRoomConfigViewModel roomConfigViewModel = this.Mapper.Map<DealRoomConfigViewModel>(result[i].DealsInclusionModels[k].DealRoomConfigurationModels[m]);
                            roomConfigViewModel.RoomName = result[i].DealsInclusionModels[k].DealRoomConfigurationModels[m].PackageHotelRoomTypeModel.Name;

                            itineraryModels[i].InclusionModels[k].RoomConfigurations.Add(roomConfigViewModel);
                        }

                        itineraryModels[i].InclusionModels[k].FlightViewModels = new List<DealsFlightViewModel>();
                        for (int n = 0; n < result[i].DealsInclusionModels[k].DealsFlightModels.Count; n++)
                        {
                            DealsFlightViewModel flightViewModel = this.Mapper.Map<DealsFlightViewModel>(result[i].DealsInclusionModels[k].DealsFlightModels[n]);
                            FlightDestination airportDetails = await this.dealService.GetAirportDetailsByCode(result[i].DealsInclusionModels[k].DealsFlightModels[n].Origin);
                            flightViewModel.OriginName = airportDetails.ShortDetail + "(" + airportDetails.CityCode + "), " + airportDetails.CityName + ", " + airportDetails.CountryName;
                            airportDetails = await this.dealService.GetAirportDetailsByCode(result[i].DealsInclusionModels[k].DealsFlightModels[n].Destination);
                            flightViewModel.DestinationName = airportDetails.ShortDetail + "(" + airportDetails.CityCode + "), " + airportDetails.CityName + ", " + airportDetails.CountryName;
                            itineraryModels[i].InclusionModels[k].FlightViewModels.Add(flightViewModel);
                        }

                        itineraryModels[i].InclusionModels[k].AddOnViewModels = new List<DealsAddOnViewModel>();
                        for (int n = 0; n < result[i].DealsInclusionModels[k].DealsAddOnModels.Count; n++)
                        {
                            DealsAddOnViewModel addOnViewModel = this.Mapper.Map<DealsAddOnViewModel>(result[i].DealsInclusionModels[k].DealsAddOnModels[n]);
                            itineraryModels[i].InclusionModels[k].AddOnViewModels.Add(addOnViewModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            var nightModels = await this.dealService.GetNightsAsync(packageId);

            this.ViewBag.FirstNightId = nightModels.ToList().OrderBy(x => x.Value).Select(x => x.Id).FirstOrDefault();
            this.ViewBag.nightId = nightId;
            this.ViewBag.TotalNights = (await this.dealService.GetNightByNightId(nightId)).Value + 1;
            this.ViewBag.packageTypeId = packageTypeId;
            this.ViewBag.packageId = packageId;
            this.ViewBag.IsFixedDeparture = (await this.dealService.GetDealPackageAsync(packageId)).IsFixedDeparture;
            itineraryModels = itineraryModels != null ? itineraryModels.OrderBy(x => x.StartDay).ToList() : new List<DealsItineraryViewModel>();
            return this.PartialView(itineraryModels);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="nightId">nightId</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> AddItinearyPart(int id, int nightId)
        {
            var model = new DealsItineraryViewModel
            {
                IsActive = true,
                Id = id,
                NightId = nightId
            };
            if (id > 0)
            {
                model = this.Mapper.Map<DealsItineraryViewModel>(await this.dealService.GetItineraryByIdAsync(id));
            }

            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="fromNightId">from night id</param>
        /// <param name="toNightId">to night id</param>
        /// <param name="packageId">nightId</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> CopyItinerary(int fromNightId, int toNightId, int packageId)
        {
            int result = await this.dealService.CopyItinerary(fromNightId, toNightId, packageId);
            if (result == 1)
            {
                ////this.ShowMessage("Clonning Successfull!", Enums.MessageType.Success);
                ////this.TempData["nextview"] = "#itinerary";
                return this.Json(new { Status = true, Message = "Clonning Successfull!" });
            }
            else if (result == 2)
            {
                ////this.ShowMessage("Data Already Exists! Clonning Failed!", Enums.MessageType.Warning);
                ////this.TempData["nextview"] = "#itinerary";
                return this.Json(new { Status = false, Message = "Data Already Exists! Clonning Failed!" });
            }
            else
            {
                ////this.ShowMessage("Internal Server Error", Enums.MessageType.Error);
                ////this.TempData["nextview"] = "#itinerary";
                return this.Json(new { Status = false, Message = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="nightId">nightId</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> DepartureDates(int nightId)
        {
            var model = await this.dealService.GetDealDepartureByNightIdAsync(nightId);
            return this.PartialView("_DepartureDates", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">nightId</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> DepartureDates(DealsDepartureDateViewModel model)
        {
            try
            {
                await this.dealService.DeleteAllDealsDeparture(model.NightId);
                List<DateTime> dates = model.Dates.Split(',').ToList().Select(x => Convert.ToDateTime(x)).ToList();
                if (dates != null)
                {
                    foreach (var item in dates)
                    {
                        DealsDepartureDatesModel record = new DealsDepartureDatesModel
                        {
                            Id = 0,
                            Date = item,
                            NightId = model.NightId
                        };
                        record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealService.AddDealsDepartureAsync(record);
                    }
                }

                this.ShowMessage("Updated Successfully");
                this.TempData["nextview"] = "#itinerary";
                return this.Json("success");
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                this.ShowMessage("Updated Failed", Enums.MessageType.Error);
                this.TempData["nextview"] = "#itinerary";
                return this.Json("success");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="inclusionId">inclusionId</param>
        /// <param name="packageId">packageId</param>
        /// <param name="itineraryId">itineraryId</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> AddHotelRoomConfigurationForTours(int inclusionId, int packageId, int itineraryId)
        {
            this.ViewBag.packageId = packageId;
            this.ViewBag.itineraryId = itineraryId;
            DealsInclusionViewModel model = new DealsInclusionViewModel();
            if (inclusionId == 0)
            {
                model.TypeId = 1;
                model.ItineraryId = itineraryId;
            }
            else
            {
                model = this.Mapper.Map<DealsInclusionViewModel>(this.dealService.GetDealsInclusion(inclusionId));
            }

            var destinations = await this.dealService.GetDestinationsAsync(packageId);
            this.ViewBag.DestinationCities = destinations != null ? string.Join(",", destinations.Select(x => x.City).ToArray()) : null;
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="inclusionId">inclusionId</param>
        /// <param name="packageId">packageId</param>
        /// <param name="itineraryId">itineraryId</param>
        /// <param name="totalDays">Number of days</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> AddFlightForItenary(int inclusionId, int packageId, int itineraryId, int totalDays)
        {
            this.ViewBag.packageId = packageId;
            this.ViewBag.itineraryId = itineraryId;
            DealsFlightViewModel model = new DealsFlightViewModel();
            if (inclusionId == 0)
            {
                model.TypeId = 2; //// Ie: Flight
                model.InclusionId = inclusionId;
                model.ItenaryId = itineraryId;
            }
            else
            {
                model = await this.dealService.GetFlightsFromInclusion(inclusionId);
                model.OriginItems = string.IsNullOrEmpty(model.Origin) ? new List<SelectListItem>() : (await this.dealService.GetAirportsCodesDropdownAsync(string.Empty, 1, model.Origin)).ToSelectList();
                model.DestinationItems = string.IsNullOrEmpty(model.Destination) ? new List<SelectListItem>() : (await this.dealService.GetAirportsCodesDropdownAsync(string.Empty, 1, model.Destination)).ToSelectList();
            }

            model.TotalDays = totalDays;

            return this.PartialView("AddFlightInclusionForTour", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">inclusionId</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> AddFlightForItenary(DealsFlightViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    ////Update Logic Here
                    var inclusionRecord = await this.dealService.GetDealsInclusion(model.InclusionId);
                    inclusionRecord.Day = model.Days;
                    inclusionRecord.VendorInfoId = model.VendorId;
                    inclusionRecord.ItineraryId = model.ItenaryId;
                    inclusionRecord.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealsInclusionAsync(inclusionRecord);

                    var flightRecord = await this.dealService.GetDealFlightAsync(model.Id);
                    flightRecord.Origin = model.Origin;
                    flightRecord.CabinClass = model.CabinClass;
                    flightRecord.Destination = model.Destination;
                    flightRecord.EndTime = model.EndTime;
                    flightRecord.StartTime = model.StartTime;
                    await this.dealService.UpdateDealFlightAsync(flightRecord);

                    ////this.TempData["nextview"] = "#itinerary";
                    ////this.ShowMessage("Flights Updated");
                    return this.Json("Success");
                }
                else
                {
                    DealsInclusionModel inclusionModel = new DealsInclusionModel
                    {
                        Day = model.Days,
                        Id = 0,
                        IsChargeable = false,
                        TypeId = 2,
                        VendorInfoId = model.VendorId,
                        ItineraryId = model.ItenaryId
                    };
                    inclusionModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    model.InclusionId = await this.dealService.AddDealsInclusionAsync(inclusionModel);
                    DealsFlightModel flightModel = new DealsFlightModel
                    {
                        Id = 0,
                        Origin = model.Origin,
                        InclusionId = model.InclusionId,
                        AllDay = false,
                        CabinClass = model.CabinClass,
                        Destination = model.Destination,
                        EndTime = model.EndTime,
                        StartTime = model.StartTime
                    };
                    flightModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.AddDealsFlightAsync(flightModel);
                    ////this.TempData["nextview"] = "#itinerary";
                    ////this.ShowMessage("Flights Added");
                    return this.Json("Success");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                ////this.TempData["nextview"] = "#itinerary";
                ////this.ShowMessage("Flights Adding Failure", Enums.MessageType.Error);
                return this.Json("Success");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="inclusionId">inclusionId</param>
        /// <param name="packageId">packageId</param>
        /// <param name="itineraryId">itineraryId</param>
        /// <param name="totalDays">TOtal Days</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> AddActivityInclusionForTour(int inclusionId, int packageId, int itineraryId, int totalDays)
        {
            this.ViewBag.packageId = packageId;
            this.ViewBag.itineraryId = itineraryId;
            DealsAddOnViewModel model = new DealsAddOnViewModel
            {
                InclusionId = inclusionId,
                ItenaryId = itineraryId
            };
            if (inclusionId > 0)
            {
                model = await this.dealService.GetDealActivitiesByInclusionId(inclusionId);
            }

            model.TotalDays = totalDays;
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> AddActivityInclusionForTour(DealsAddOnViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    ////Update Logic Here
                    var inclusionRecord = await this.dealService.GetDealsInclusion(model.InclusionId);
                    inclusionRecord.Day = model.Day;
                    inclusionRecord.VendorInfoId = model.VendorId;
                    inclusionRecord.ItineraryId = model.ItenaryId;
                    inclusionRecord.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealsInclusionAsync(inclusionRecord);

                    var addOnRecord = await this.dealService.GetDealAddOnAsync(model.Id);
                    addOnRecord.AdultCharge = model.AdultCharge;
                    addOnRecord.AdultMinimumAge = model.AdultMinimumAge;
                    addOnRecord.ChildCharge = model.ChildCharge;
                    addOnRecord.ChildMinimumAge = model.ChildMinimumAge;
                    addOnRecord.Description = model.Description;
                    addOnRecord.Image = model.Image;
                    addOnRecord.InclusionId = model.InclusionId;
                    addOnRecord.InfantCharge = model.InfantCharge;
                    addOnRecord.IsChargeable = model.IsChargeable;
                    addOnRecord.IsIncluded = model.IsIncluded;
                    addOnRecord.Name = model.Name;
                    addOnRecord.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealAddOnAsync(addOnRecord);

                    ////this.TempData["nextview"] = "#itinerary";
                    ////this.ShowMessage("Activity Updated");
                    return this.Json("Success");
                }
                else
                {
                    DealsInclusionModel inclusionModel = new DealsInclusionModel
                    {
                        Day = model.Day,
                        Id = 0,
                        IsChargeable = false,
                        TypeId = 5,
                        VendorInfoId = model.VendorId,
                        ItineraryId = model.ItenaryId
                    };
                    inclusionModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    model.InclusionId = await this.dealService.AddDealsInclusionAsync(inclusionModel);
                    DealsAddOnModel addOnModel = new DealsAddOnModel
                    {
                        Id = 0,
                        InclusionId = model.InclusionId,
                        AdultCharge = model.AdultCharge,
                        AdultMinimumAge = model.AdultMinimumAge,
                        ChildCharge = model.ChildCharge,
                        ChildMinimumAge = model.ChildMinimumAge,
                        Description = model.Description,
                        Image = model.Image,
                        InfantCharge = model.InfantCharge,
                        IsChargeable = model.IsChargeable,
                        IsIncluded = model.IsIncluded,
                        Name = model.Name
                    };
                    addOnModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.AddDealsAddOnAsync(addOnModel);
                    ////this.TempData["nextview"] = "#itinerary";
                    ////this.ShowMessage("Activity Added");
                    return this.Json("Success");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                ////this.TempData["nextview"] = "#itinerary";
                ////this.ShowMessage("Activity Adding Failure", Enums.MessageType.Error);
                return this.Json("Success");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Review Identifier</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> AddItinearyPart(DealsItineraryViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this.TempData["SubView"] = model.SubView;
                    if (model.Id > 0)
                    {
                        ////Update
                        var record = this.Mapper.Map<DealsItineraryModel>(model);
                        record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealService.UpdateDealsItenaryAsync(record);
                        ////this.TempData["nextview"] = "#itinerary";
                        ////this.ShowMessage("Successfully Updated");
                        return this.Json("success");
                    }
                    else
                    {
                        var record = this.Mapper.Map<DealsItineraryModel>(model);
                        record.IsActive = true;
                        record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealService.AddDealsItenaryAsync(record);
                        ////this.TempData["nextview"] = "#itinerary";
                        ////this.ShowMessage("Successfully Added");
                        return this.Json("success");
                    }
                }
                catch (Exception ex)
                {
                    var str = ex.ToString();
                    return this.Json(str);
                }
            }

            return this.Json("failure");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="itineraryId">The identifier.</param>
        /// <param name="packageTypeId">packageTypeID</param>
        /// <param name="packageId">packageId</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteItineary(int itineraryId, int packageTypeId, int packageId)
        {
            try
            {
                await this.dealService.DeleteItineraryAsync(itineraryId);
                ////this.ShowMessage(Messages.DeletedSuccessfully);
                ////this.TempData["nextview"] = "#itinerary";
                int id = packageId;
               //// return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
                return this.Json(new { Status = true, Message = "Successfully Deleted" });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                int id = packageId;
                return this.Json(new { Status = false, Message = "Error Occured, Try Deleting Inner Item First." });
                ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The identifier.</param>
        /// <param name="packageTypeId">packageTypeID</param>
        /// <param name="packageId">packageId</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteHotelInclusion(int hotelId, int packageTypeId, int packageId)
        {
            try
            {
                await this.dealService.DeleteHotelInclusion(hotelId);
                int id = packageId;
                return this.Json(new { Status = true, Message = "Successfully Deleted" });
                ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                int id = packageId;
                return this.Json(new { Status = false, Message = "Error Occured" });
                ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="inclusionId">The identifier.</param>
        /// <param name="packageTypeId">packageTypeID</param>
        /// <param name="packageId">packageId</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteFlightInclusion(int inclusionId, int packageTypeId, int packageId)
        {
            try
            {
                await this.dealService.DeleteFlightInclusion(inclusionId);
                ////this.ShowMessage(Messages.DeletedSuccessfully);
                ////this.TempData["nextview"] = "#itinerary";
                int id = packageId;
                return this.Json(new { Status = true, Message = "Successfully Deleted" });
                ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                int id = packageId;
                return this.Json(new { Status = false, Message = "Error Occured" });
                ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="roomTypeId">The identifier.</param>
        /// <param name="packageTypeId">packageTypeID</param>
        /// <param name="packageId">packageId</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteRoomTypeInclusion(int roomTypeId, int packageTypeId, int packageId)
        {
            try
            {
                await this.dealService.DeleteRoomTypeInclusion(roomTypeId);
                ////this.ShowMessage(Messages.DeletedSuccessfully);
                ////this.TempData["nextview"] = "#itinerary";
                int id = packageId;
                ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
                return this.Json(new { Status = true, Message="Successfully Deleted" });
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                int id = packageId;
                return this.Json(new { Status = false, Message = "Error Occured" });
                ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id, packageTypeId });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> HotelRatePlan(int id, int packageTypeId)
        {
            this.ViewBag.PackageId = id;
            this.TempData["packageType"] = packageTypeId;
            List<DealsHotelRatePlanViewModel> model = await this.dealService.GetRoomConfigsForHotelRatePlans(id);
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> AddHotelRatePlan(int id, int packageTypeId, int ratePlanId)
        {
            this.ViewBag.PackageId = id;
            this.TempData["packageType"] = packageTypeId;
            DealsRatePlanViewModel viewModel = new DealsRatePlanViewModel
            {
                Id = 0,
                NightId = (await this.dealService.GetNightsAsync(id)).Select(x => x.Id).FirstOrDefault(),
                IsActive = true,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(1),
                RatePlanId = 0
            };
            if (ratePlanId > 0)
            {
                viewModel = this.Mapper.Map<DealsRatePlanViewModel>(await this.dealService.GetDealRatePlanById(ratePlanId));
                viewModel.RatePlanId = viewModel.Id;
                viewModel.RoomConfigurationItems = (await this.dealService.GetRoomConfigDropDownListForRatePlanAsync(string.Empty, 0, viewModel.RoomConfigId, id)).ToSelectList();
                viewModel.CurrencyItems = viewModel.Currency != null ? (await this.vendorService.GetCurrencyDropDownListAsync(string.Empty, 1, Convert.ToInt32(viewModel.Currency))).ToSelectList() : new List<SelectListItem>();
            }

            return this.PartialView("_AddHotelRatePlan", viewModel);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> AddHotelRatePlan(DealsRatePlanViewModel model)
        {
            try
            {
                this.ViewBag.PackageId = model.Id;
                this.TempData["nextview"] = "#hotelratePlans";
                var submitModel = this.Mapper.Map<DealsRatePlanModel>(model);
                submitModel.Id = model.RatePlanId;
                submitModel.NightId = (await this.dealService.GetNightsAsync(model.Id)).Select(x => x.Id).FirstOrDefault();
                if (submitModel.Id > 0)
                {
                    submitModel.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealRatePlan(submitModel);
                    this.ShowMessage(Messages.SavedSuccessfully);
                    await this.dealService.UpdateMinPriceForPackage(model.Id);
                    return this.Json("update");
                }
                else
                {
                    submitModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    int ratePlanId = await this.dealService.AddDealRatePlan(submitModel);
                    await this.dealService.UpdateMinPriceForPackage(model.Id);
                    this.ShowMessage(Messages.SavedSuccessfully);
                    return this.Json("success");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return this.Json("failed");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> DeleteRatePlan(int id, int packageTypeId, int ratePlanId)
        {
            this.ViewBag.PackageId = id;
            this.TempData["packageType"] = packageTypeId;
            this.TempData["nextview"] = "#hotelratePlans";
            try
            {
                await this.dealService.DeleteRatePlanAsync(ratePlanId);
                this.ShowMessage("Deleted");
                return this.Json("success");
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return this.Json("failure");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="roomConfigId">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> GetRoomConfigRatePlan(int roomConfigId, int packageTypeId)
        {
            this.TempData["packageType"] = packageTypeId;
            var viewModel = await this.dealService.GetDealRoomConfigAllRatePlans(roomConfigId);
            return this.PartialView("_RoomConfigRatePlan", viewModel);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> RatePlan(int id, int packageTypeId)
        {
            this.ViewBag.PackageId = id;
            this.TempData["packageType"] = packageTypeId;
            var result = await this.dealService.GetNightsAsync(id);
            List<DealsNightViewModel> dealNightModels = this.Mapper.Map<List<DealsNightViewModel>>(result);
            foreach (DealsNightViewModel item in dealNightModels)
            {
                item.DealsItineraryViewModels = this.Mapper.Map<List<DealsItineraryViewModel>>(await this.dealService.GetItinerariesAsync(item.Id));
            }

            return this.PartialView(dealNightModels);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="nightId">The identifier.</param>
        /// <param name="packageType">The package Type</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> ManageRatePlan(int nightId, int packageType, int packageId)
        {
            this.TempData["packageType"] = packageType;
            this.TempData["packageId"] = packageId;
            this.ViewBag.NightId = nightId;
            List<DealsRatePlanViewModel> ratePlanModels = new List<DealsRatePlanViewModel>();
            ratePlanModels = this.Mapper.Map<List<DealsRatePlanViewModel>>(await this.dealService.GetratePlansAsync(nightId));
            if (ratePlanModels != null)
            {
                foreach (var item in ratePlanModels)
                {
                    var currency = item.Currency != null ? await this.masterService.GetCurrencyByIdAsync(Convert.ToInt32(item.Currency)) : new CurrencyModel { };
                    if (currency.Id > 0)
                    {
                        item.CurrencyName = currency.Name;
                        item.CurrencyCode = currency.Code.ToUpper();
                    }
                }
            }

            this.ViewBag.IsFixedDeparture = (await this.dealService.GetDealPackageAsync(packageId)).IsFixedDeparture;
            return this.PartialView(ratePlanModels);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="ratePlanId">The Rate Plan dentifier.</param>
        /// <param name="type">Record Type 1-Regular Departure, 2 - Fixed Departure</param>
        /// <param name="nightId">Night Id</param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> RatePlanInventory(int ratePlanId, int type, int nightId)
        {
            try
            {
                var ratePlanRecord = await this.dealService.GetDealRatePlanById(ratePlanId);
                List<DealInventoryModel> inventories = (await this.dealService.GetInventoryByRatePlanId(ratePlanId)).OrderBy(x => x.Date).ToList();
                if (type == 1) //// Regular Departure
                {
                    if (inventories == null || inventories.Count == 0)
                    {
                        for (DateTime i = ratePlanRecord.ValidFrom; i <= ratePlanRecord.ValidTo; i = i.AddDays(1))
                        {
                            DealInventoryModel inventoryRecord = new DealInventoryModel
                            {
                                Id = 0,
                                BlackOut = false,
                                Booking = 0,
                                Date = i,
                                Day = (int)i.DayOfWeek,
                                Inventory = 0,
                                Surgcharge = 0,
                                ExtraAdult = ratePlanRecord.ExtraAdult,
                                ExtraChild_NB = ratePlanRecord.ExtraChild_NB,
                                ExtraChild_WB = ratePlanRecord.ExtraChild_WB,
                                ExtraInfant = ratePlanRecord.ExtraInfant,
                                Price = ratePlanRecord.Price,
                                SingleSupplement = ratePlanRecord.SingleSupplement,
                                IsActive = true,
                                RatePlanId = ratePlanId
                            };
                            inventoryRecord.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.AddDealInventoryRecordAsync(inventoryRecord);
                        }

                        return this.PartialView("_RatePlanInventory", this.Mapper.Map<List<DealInventoryViewModel>>(await this.dealService.GetInventoryByRatePlanId(ratePlanId)));
                    }
                    else
                    {
                        for (DateTime i = ratePlanRecord.ValidFrom; i <= ratePlanRecord.ValidTo; i = i.AddDays(1))
                        {
                            if (!inventories.Select(x => x.Date).Contains(i))
                            {
                                DealInventoryModel inventoryRecord = new DealInventoryModel
                                {
                                    Id = 0,
                                    BlackOut = false,
                                    Booking = 0,
                                    Date = i,
                                    Day = (int)i.DayOfWeek,
                                    Inventory = 0,
                                    Surgcharge = 0,
                                    ExtraAdult = ratePlanRecord.ExtraAdult,
                                    ExtraChild_NB = ratePlanRecord.ExtraChild_NB,
                                    ExtraChild_WB = ratePlanRecord.ExtraChild_WB,
                                    ExtraInfant = ratePlanRecord.ExtraInfant,
                                    Price = ratePlanRecord.Price,
                                    SingleSupplement = ratePlanRecord.SingleSupplement,
                                    IsActive = true,
                                    RatePlanId = ratePlanId,
                                };
                                inventoryRecord.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.AddDealInventoryRecordAsync(inventoryRecord);
                            }
                        }

                        var deleteInventoryRecords = inventories.Where(x => x.Date < ratePlanRecord.ValidFrom || x.Date > ratePlanRecord.ValidTo).ToList();
                        if (deleteInventoryRecords.Count > 0)
                        {
                            foreach (var item in deleteInventoryRecords)
                            {
                                item.IsActive = false;
                                item.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.UpdateDealInventoryRecordAsync(item);
                            }
                        }

                        inventories = (await this.dealService.GetInventoryByRatePlanId(ratePlanId)).OrderBy(x => x.Date).ToList();
                        return this.PartialView("_RatePlanInventory", this.Mapper.Map<List<DealInventoryViewModel>>(inventories));
                    }
                }
                else //// type == 2 Fixed Departure
                {
                    List<DealsDepartureDatesModel> departureDates = await this.dealService.GetAllDealDepartureRecordsByNightIdAsync(nightId);
                    if (departureDates.Count > 0)
                    {
                        departureDates = departureDates.Where(x => x.Date >= ratePlanRecord.ValidFrom && x.Date <= ratePlanRecord.ValidTo).ToList();
                        if (inventories == null || inventories.Count == 0)
                        {
                            for (DateTime i = ratePlanRecord.ValidFrom; i <= ratePlanRecord.ValidTo; i = i.AddDays(1))
                            {
                                if (departureDates.Select(x => x.Date).Contains(i))
                                {
                                    DealInventoryModel inventoryRecord = new DealInventoryModel
                                    {
                                        Id = 0,
                                        BlackOut = false,
                                        Booking = 0,
                                        Date = i,
                                        Day = (int)i.DayOfWeek,
                                        Inventory = 0,
                                        Surgcharge = 0,
                                        ExtraAdult = ratePlanRecord.ExtraAdult,
                                        ExtraChild_NB = ratePlanRecord.ExtraChild_NB,
                                        ExtraChild_WB = ratePlanRecord.ExtraChild_WB,
                                        ExtraInfant = ratePlanRecord.ExtraInfant,
                                        Price = ratePlanRecord.Price,
                                        SingleSupplement = ratePlanRecord.SingleSupplement,
                                        IsActive = true,
                                        RatePlanId = ratePlanId
                                    };
                                    inventoryRecord.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                    await this.dealService.AddDealInventoryRecordAsync(inventoryRecord);
                                }
                            }

                            return this.PartialView("_RatePlanInventory", this.Mapper.Map<List<DealInventoryViewModel>>(await this.dealService.GetInventoryByRatePlanId(ratePlanId)));
                        }
                        else
                        {
                            var leftOutDepartureDates = departureDates.Where(x => !inventories.Select(y => y.Date).Contains(x.Date)).ToList();
                            if (leftOutDepartureDates.Count > 0)
                            {
                                foreach (var item in leftOutDepartureDates)
                                {
                                    DealInventoryModel inventoryRecord = new DealInventoryModel
                                    {
                                        Id = 0,
                                        BlackOut = false,
                                        Booking = 0,
                                        Date = item.Date,
                                        Day = (int)item.Date.DayOfWeek,
                                        Inventory = 0,
                                        Surgcharge = 0,
                                        ExtraAdult = ratePlanRecord.ExtraAdult,
                                        ExtraChild_NB = ratePlanRecord.ExtraChild_NB,
                                        ExtraChild_WB = ratePlanRecord.ExtraChild_WB,
                                        ExtraInfant = ratePlanRecord.ExtraInfant,
                                        Price = ratePlanRecord.Price,
                                        SingleSupplement = ratePlanRecord.SingleSupplement,
                                        IsActive = true,
                                        RatePlanId = ratePlanId
                                    };
                                    inventoryRecord.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                    await this.dealService.AddDealInventoryRecordAsync(inventoryRecord);
                                }
                            }

                            var deleteInventoryRecords = inventories.Where(x => !departureDates.Select(y => y.Date).Contains(x.Date)).ToList();
                            if (deleteInventoryRecords.Count > 0)
                            {
                                foreach (var item in deleteInventoryRecords)
                                {
                                    item.IsActive = false;
                                    item.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                    await this.dealService.UpdateDealInventoryRecordAsync(item);
                                }
                            }

                            inventories = (await this.dealService.GetInventoryByRatePlanId(ratePlanId)).OrderBy(x => x.Date).ToList();
                            return this.PartialView("_RatePlanInventory", this.Mapper.Map<List<DealInventoryViewModel>>(inventories));
                        }
                    }
                    else
                    {
                        return this.PartialView("_RatePlanInventory", new List<DealInventoryViewModel>());
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return this.PartialView("_RatePlanInventory", new List<DealInventoryViewModel>());
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="startDate">The Start Date.</param>
        /// <param name="endDate">The End Date.</param>
        /// <param name="ratePlanId">The Rate Plan Id</param>
        /// <param name="days">Days of week</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> GetFilteredInventory(DateTime? startDate, DateTime? endDate, int ratePlanId, string[] days)
        {
            int[] convertedDays = null;
            if (days != null && days.Count() > 0)
            {
                if (!days.Any(x => x == null))
                {
                    convertedDays = days.Select(x => Convert.ToInt32(x)).ToArray();
                }
            }

            var result = this.Mapper.Map<List<DealInventoryViewModel>>(await this.dealService.GetFilteredInventoryByRatePlanId(ratePlanId, startDate, endDate, convertedDays));

            return this.PartialView("_FilteredRatePlanInventoryTable", result);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The identifier.</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> SaveRatePlanInventory(List<DealInventoryViewModel> model)
        {
            List<DealInventoryModel> records = this.Mapper.Map<List<DealInventoryModel>>(model);
            if (records.Count > 0)
            {
                for (int i = 0; i < records.Count; i++)
                {
                    records[i].UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealInventoryRecordAsync(records[i]);
                }

                try
                {
                    int ratePlanId = records.Select(x => x.RatePlanId).Distinct().FirstOrDefault();
                    List<DealInventoryModel> allInventoryRecords = (await this.dealService.GetInventoryByRatePlanId(ratePlanId)).Where(x => x.Date.Date >= DateTime.Now.Date).ToList();
                    DealsRatePlanModel thisRatePlan = await this.dealService.GetDealRatePlanById(ratePlanId);
                    thisRatePlan.Price = allInventoryRecords.Where(x => x.Price.HasValue).OrderBy(x => x.Price).Select(x => Convert.ToDecimal(x.Price)).FirstOrDefault();
                    thisRatePlan.SingleSupplement = allInventoryRecords.Where(x => x.SingleSupplement.HasValue).OrderBy(x => x.SingleSupplement).Select(x => Convert.ToDecimal(x.SingleSupplement)).FirstOrDefault();
                    thisRatePlan.ExtraAdult = allInventoryRecords.Where(x => x.ExtraAdult.HasValue).OrderBy(x => x.ExtraAdult).Select(x => Convert.ToDecimal(x.ExtraAdult)).FirstOrDefault();
                    thisRatePlan.ExtraChild_WB = allInventoryRecords.Where(x => x.ExtraChild_WB.HasValue).OrderBy(x => x.ExtraChild_WB).Select(x => Convert.ToDecimal(x.ExtraChild_WB)).FirstOrDefault();
                    thisRatePlan.ExtraChild_NB = allInventoryRecords.Where(x => x.ExtraChild_NB.HasValue).OrderBy(x => x.ExtraChild_NB).Select(x => Convert.ToDecimal(x.ExtraChild_NB)).FirstOrDefault();
                    thisRatePlan.ExtraInfant = allInventoryRecords.Where(x => x.ExtraInfant.HasValue).OrderBy(x => x.ExtraInfant).Select(x => Convert.ToDecimal(x.ExtraInfant)).FirstOrDefault();
                    thisRatePlan.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealRatePlan(thisRatePlan);
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                }
            }

            return this.Json(true);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="nightId">The identifier.</param>
        /// <param name="ratePlanId">Rate Plan Id</param>
        /// <param name="packageType">Package Tyope</param>
        /// <param name="packageId">PackageId </param>
        /// <returns>Itinerary</returns>
        [HttpGet]
        public async Task<ActionResult> AddTourRatePlan(int nightId, int ratePlanId, int packageType, int packageId)
        {
            this.TempData["packageType"] = packageType;
            this.TempData["packageId"] = packageId;
            this.ViewBag.NightId = nightId;
            DealsRatePlanViewModel model = new DealsRatePlanViewModel
            {
                Id = 0,
                IsActive = true,
                NightId = nightId,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(1)
            };

            if (ratePlanId > 0)
            {
                model = this.Mapper.Map<DealsRatePlanViewModel>(await this.dealService.GetDealRatePlanById(ratePlanId));
                model.CurrencyItems = model.Currency != null ? (await this.vendorService.GetCurrencyDropDownListAsync(string.Empty, 1, Convert.ToInt32(model.Currency))).ToSelectList() : new List<SelectListItem>();
                model.InclusionHotels = string.IsNullOrEmpty(model.Inclusions) ? null : model.Inclusions.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                model.InclusionItems = model.InclusionHotels != null ? (await this.dealService.GetInclusionHoteliersFromNightId(string.Empty, 0, nightId, model.InclusionHotels)).ToSelectList() : new List<SelectListItem>();
            }

            return this.PartialView("_AddTourRatePlan", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The Model.</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> AddTourRatePlan(DealsRatePlanViewModel model)
        {
            var record = this.Mapper.Map<DealsRatePlanModel>(model);
            record.Inclusions = string.Join(',', model.InclusionHotels);
            if (record.Id > 0)
            {
                ////Update
                record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.UpdateDealRatePlan(record);
                await this.dealService.UpdateMinPriceForPackage(Convert.ToInt32(this.TempData["packageId"]));
                return this.Json("Success");
            }
            else
            {
                record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.AddDealRatePlan(record);
                await this.dealService.UpdateMinPriceForPackage(Convert.ToInt32(this.TempData["packageId"]));
                return this.Json("Success");
            }

            ////this.TempData["nextview"] = "#ratePlans";
            ////this.ShowMessage(Messages.SavedSuccessfully, Enums.MessageType.Success);
            ////return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id = Convert.ToInt32(this.TempData["packageId"]), packageTypeId = Convert.ToInt32(this.TempData["packageType"]) });
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="rateplanId">The RajtePlan Id.</param>
        /// <returns>Itinerary</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteTourRatePlan(int rateplanId)
        {
            await this.dealService.DeleteRatePlanAsync(rateplanId);
            this.TempData["nextview"] = "#ratePlans";
            this.ShowMessage(Messages.DeletedSuccessfully, Enums.MessageType.Success);
            return this.Json("Success");
        }

        /// <summary>
        /// Gets deal package viewf for the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Package</returns>
        [HttpGet]
        public async Task<ActionResult> Package(int id, int packageTypeId)
        {
            this.ViewBag.TravelStyle = (await this.masterService.GetAllPackageTravelStyleListAsync()).ToSelectList();
            this.ViewBag.TravelCategories = (await this.masterService.GetPackageDealTypeListAsync(string.Empty, 1, 0)).ToSelectList();
            DealsPackageViewModel model = new DealsPackageViewModel
            {
                Type = packageTypeId,
                IsFixedDeparture = false,
                ViewCount = 0
            };
            if (id != 0)
            {
                var result = await this.dealService.GetDealPackageAsync(id);
                model = this.Mapper.Map<DealsPackageViewModel>(result);
                model.TravelStyles = !string.IsNullOrEmpty(result.TravelStyle) ? result.TravelStyle.Split(',').ToArray() : null;
                model.TravelCategories = !string.IsNullOrEmpty(result.TravelCategory) ? result.TravelCategory.Split(',').ToArray() : null;
                model.Nights = result.DealsNightModels != null || result.DealsNightModels.Count > 0 ? result.DealsNightModels.Select(x => x.Value).ToArray() : null;
                model.LengthOfStay = model.Type == 1 && model.Nights != null && model.Nights.Count() > 0 ? result.DealsNightModels.FirstOrDefault().Value : 0;
                model.DealsDestinationViewModels = this.Mapper.Map<List<DealsDestinationViewModel>>(await this.dealService.GetDestinationsAsync(id));
                if (model.DealsDestinationViewModels.Count > 0)
                {
                    foreach (var item in model.DealsDestinationViewModels)
                    {
                        item.CountryItems = item.Country == 0 ? new List<SelectListItem>() : (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, item.Country)).ToSelectList();
                        item.StateItems = item.State == 0 || item.State == null ? new List<SelectListItem>() : (await this.masterService.GetTourPackageStatesByCountrId(string.Empty, 1, item.Country, (short)item.State)).ToSelectList();
                        item.CityItems = item.City == 0 ? new List<SelectListItem>() : (await this.masterService.GetTourPackageCityByCounryIdorStateIdAsync(string.Empty, 1, item.Country, item.State == 0 || item.State == null ? (short)0 : (short)item.State, (short)item.City)).ToSelectList();
                        item.AreaItems = item.Area == 0 || item.Area == null ? new List<SelectListItem>() : (await this.masterService.GetAreaByCityIdAsync(string.Empty, 1, item.City, Convert.ToInt32(item.Area))).ToSelectList();
                    }
                }

                model.HotelierId = result.Type == 1 && result.DealsNightModels != null && result.DealsNightModels.Count > 0 ? Convert.ToInt32(await this.dealService.GetDealHotelierFromNightId(result.DealsNightModels.FirstOrDefault().Id)) : 0;
                model.HotelierItems = model.HotelierId > 0 && model.Type == 1 ? (await this.hotelierService.GetActiveHoteliersForDeals(string.Empty, 0, model.HotelierId)).ToSelectList() : new List<SelectListItem>();
                model.DealsBookingValidityViewModels = this.Mapper.Map<List<DealsBookingValidityViewModel>>(await this.dealService.GetBookingValiditiesAsync(id));
                if (model.Type != 1)
                {
                    model.GetDealsPaxCombinationViewModels = this.Mapper.Map<List<DealsPaxCombinationViewModel>>(await this.dealService.GetPaxCombinationsAsync(id));
                    this.ViewBag.MaxPassenger = model.GetDealsPaxCombinationViewModels.Count > 0 ? model.GetDealsPaxCombinationViewModels.Select(x => x.Total).Max() : 0;
                }
            }
            else
            {
                model.DealsDestinationViewModels = new List<DealsDestinationViewModel>();
                model.DealsNightViewModels = new List<DealsNightViewModel>();
                model.DealsBookingValidityViewModels = new List<DealsBookingValidityViewModel>();
                model.GetDealsPaxCombinationViewModels = new List<DealsPaxCombinationViewModel>();
            }

            this.ViewBag.PackageId = id;
            this.ViewBag.PackageTypeId = packageTypeId;
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <returns>Manage</returns>
        public ActionResult AddDestination(int packageId)
        {
            DealsDestinationViewModel model = new DealsDestinationViewModel
            {
                PackageId = packageId,
                Id = 0
            };
            return this.PartialView("_AddDestination", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The hotelid.</param>
        /// <param name="nextview">Next View</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Package(DealsPackageViewModel model, string nextview)
        {
            int recordId = 0;
            try
            {
                if (this.ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(nextview))
                    {
                        this.TempData["nextview"] = nextview;
                    }

                    ////Section Deal Package Record
                    var record = this.Mapper.Map<DealsPackageModel>(model);
                    record.TravelStyle = model.TravelStyles != null ? string.Join(",", model.TravelStyles.ToArray()) : string.Empty;
                    record.TravelCategory = model.TravelCategories != null ? string.Join(",", model.TravelCategories.ToArray()) : string.Empty;
                    if (model.Id > 0)
                    {
                        //// Update Deal Packages
                        record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        recordId = await this.dealService.UpdateDealPackageInfoAsync(record);
                    }
                    else
                    {
                        record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        recordId = await this.dealService.AddDealPackageInfoAsync(record);
                        var result = await this.dealService.GetDealPackageAsync(recordId);
                        string code = string.Empty;
                        switch (result.Type)
                        {
                            case 1: code = "HT" + recordId.ToString(); break;
                            case 2: code = "TR" + recordId.ToString(); break;
                            case 3: code = "CR" + recordId.ToString(); break;
                            case 4: code = "FL" + recordId.ToString(); break;
                            case 5: code = "BS" + recordId.ToString(); break;
                            default: code = string.Empty; break;
                        }

                        result.Code = code;
                        recordId = await this.dealService.UpdateDealPackageInfoAsync(record);
                    }

                    /*Booking Validity Starts*/
                    if (model.DealsBookingValidityViewModels != null)
                    {
                        foreach (var item in model.DealsBookingValidityViewModels)
                        {
                            if (item.IsDeleted)
                            {
                                if (item.Id > 0)
                                {
                                    await this.dealService.DeletePackageBookingValidityById(item.Id);
                                }
                            }
                            else if (item.Id > 0)
                            {
                                ////Update Record
                                var recordBookingValidity = this.Mapper.Map<DealsBookingValidityModel>(item);
                                recordBookingValidity.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.UpdateDealBookingValidty(recordBookingValidity);
                            }
                            else
                            {
                                ////Add Record
                                DealsBookingValidityModel validityModel = new DealsBookingValidityModel
                                {
                                    Id = 0,
                                    PackageId = recordId,
                                    ValidFrom = item.ValidFrom,
                                    ValidTo = item.ValidTo
                                };
                                validityModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.AddDealPackageBookingValidityAsync(validityModel);
                            }
                        }
                    }

                    /*Booking Validity Ends*/

                    /*Passenger Combinations Starts*/
                    if (model.GetDealsPaxCombinationViewModels != null)
                    {
                        foreach (var item in model.GetDealsPaxCombinationViewModels)
                        {
                            if (item.IsDeleted)
                            {
                                if (item.Id > 0)
                                {
                                    await this.dealService.DeletePackagePaxCombinationById(item.Id);
                                }
                            }
                            else if (item.Id > 0)
                            {
                                ////Update Record
                                var recordPackageCombination = this.Mapper.Map<DealsPaxCombinationModel>(item);
                                recordPackageCombination.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.UpdateDealPaxCombination(recordPackageCombination);
                            }
                            else
                            {
                                ////Add Record
                                DealsPaxCombinationModel dealPax = new DealsPaxCombinationModel
                                {
                                    Adult = item.Adult,
                                    AdultAge = item.AdultAge,
                                    Child = item.Child,
                                    ChildAge = item.ChildAge,
                                    Id = 0,
                                    PackageId = recordId,
                                    Infant = item.Infant,
                                    Total = item.Total
                                };
                                dealPax.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.AddDealPackagePaxCombinationAsync(dealPax);
                            }
                        }
                    }

                    /*Passenger Combinations Ends*/

                    /*Deals Destination Starts*/
                    if (model.DealsDestinationViewModels != null)
                    {
                        foreach (var item in model.DealsDestinationViewModels)
                        {
                            if (item.IsDeleted)
                            {
                                if (item.Id > 0)
                                {
                                    await this.dealService.DeleteDealsDestinationById(item.Id);
                                }
                            }
                            else if (item.Id > 0)
                            {
                                ////Update Record
                                var recordDealDestination = this.Mapper.Map<DealsDestinationModel>(item);
                                recordDealDestination.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.UpdateDealDestination(recordDealDestination);
                            }
                            else
                            {
                                ////Add Record
                                DealsDestinationModel destReco = new DealsDestinationModel
                                {
                                    Country = item.Country,
                                    Area = item.Area != null ? Convert.ToInt32(item.Area) : 0,
                                    City = item.City,
                                    State = item.State != null ? Convert.ToInt32(item.State) : 0,
                                    Id = 0,
                                    PackageId = recordId,
                                    VisaRequired = item.VisaRequired
                                };
                                destReco.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                await this.dealService.AddDealPackageDestinationAsync(destReco);
                            }
                        }
                    }

                    /*Deals Destination Ends*/
                    if (model.Type == 1) ////Hotel
                    {
                        if (model.Id > 0)
                        {
                            var recordNight = await this.dealService.GetDealNightHotelByPackageId(recordId);
                            recordNight.Value = model.LengthOfStay;
                            recordNight.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.UpdateDealNight(recordNight);

                            var recordItenary = await this.dealService.GetDealItenaryHotelByNightId(recordNight.Id);
                            recordItenary.StartDay = 1;
                            recordItenary.EndDay = model.LengthOfStay;
                            recordItenary.Days = model.LengthOfStay + 1;
                            recordItenary.Nights = model.LengthOfStay;
                            recordItenary.IsActive = true;
                            recordItenary.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.UpdateDealsItenaryAsync(recordItenary);

                            var recordInclusion = await this.dealService.GetDealInclusionHotelByItenaryId(recordItenary.Id);
                            recordInclusion.TypeId = 1;
                            recordInclusion.VendorInfoId = model.HotelierId;
                            recordInclusion.ItineraryId = recordItenary.Id;
                            recordInclusion.IsChargeable = recordInclusion.IsChargeable;
                            recordInclusion.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.UpdateDealsInclusionAsync(recordInclusion);
                        }
                        else
                        {
                            DealsNightModel nightModel = new DealsNightModel
                            {
                                Id = 0,
                                PackageId = recordId,
                                Value = model.LengthOfStay,
                                VisaRequired = false
                            };
                            nightModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            int nightId = await this.dealService.AddDealsNightAsync(nightModel);

                            DealsItineraryModel itenaryModel = new DealsItineraryModel
                            {
                                StartDay = 1,
                                EndDay = model.LengthOfStay,
                                Days = model.LengthOfStay + 1,
                                Nights = model.LengthOfStay,
                                NightId = nightId,
                                IsActive = true
                            };
                            itenaryModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            int itenaryId = await this.dealService.AddDealsItenaryAsync(itenaryModel);

                            DealsInclusionModel inclusionModel = new DealsInclusionModel
                            {
                                TypeId = 1,
                                ItineraryId = itenaryId,
                                VendorInfoId = model.HotelierId,
                                IsChargeable = false,
                                Id = 0
                            };
                            inclusionModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.AddDealsInclusionAsync(inclusionModel);
                        }

                        ////Save Lat Long Of Hotel
                        DealsPackageModel package = await this.dealService.GetDealPackageAsync(recordId);
                        HotelierInformationModel hotelModel = await this.hotelierService.GetHotelierInfoAsync(model.HotelierId);
                        if (hotelModel.Lat.HasValue && hotelModel.Long.HasValue && hotelModel.Lat.Value != 0 && hotelModel.Long.Value != 0)
                        {
                            package.Lat = hotelModel.Lat;
                            package.Long = hotelModel.Long;
                        }
                        else
                        {
                            Tuple<decimal?, decimal?> newLatLong = this.GetLatLong(hotelModel.Address1 + ", " + hotelModel.Address2 + ", " + (await this.cityService.GetByIdAsync(hotelModel.City.Value)).Name + ", " + (await this.countryService.GetByIdAsync(hotelModel.Country.Value)).Name, this.configurationService.GetValue<string>("DomainSetting:GoogleMapKey"));
                            package.Lat = newLatLong.Item1;
                            package.Long = newLatLong.Item2;
                        }

                        await this.dealService.UpdateDealPackageInfoAsync(package);
                    }

                    if (model.Type == 2) //// Tour
                    {
                        /*Package Nights Starts*/
                        //// Update/Insert/Delete Package Nights
                        /*Package Nights Ends*/
                        if (model.Id > 0)
                        {
                            List<DealsNightModel> nightRecords = await this.dealService.GetNightsAsync(model.Id);
                            List<int> existingNights = nightRecords.Select(x => x.Value).ToList();
                            if (!existingNights.SequenceEqual(model.Nights.ToList())) //// Ie: Nights Changed
                            {
                                List<int> nightsToAdd = model.Nights.Where(x => !existingNights.Contains(x)).ToList();
                                List<int> nightsToRemove = existingNights.Where(x => !model.Nights.Contains(x)).ToList();
                                if (nightsToAdd.Count > 0)
                                {
                                    foreach (var items in nightsToAdd)
                                    {
                                        DealsNightModel nightModel = new DealsNightModel
                                        {
                                            Id = 0,
                                            PackageId = recordId,
                                            Value = items,
                                            VisaRequired = false
                                        };
                                        nightModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                        int nightId = await this.dealService.AddDealsNightAsync(nightModel);
                                    }
                                }

                                if (nightsToRemove.Count > 0)
                                {
                                    ////Delete Nights Logic.
                                    foreach (var item in nightsToRemove)
                                    {
                                        DealsNightModel nightModel =
                                            await this.dealService.GetNightRecordByValueAndDealId(item, model.Id);
                                        int flag = await this.dealService.DeleteDealNightPackageTask(nightModel);
                                        if (flag != 1)
                                        {
                                            this.ShowMessage("Nights Not Updated!", Enums.MessageType.Error);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var items in model.Nights)
                            {
                                DealsNightModel nightModel = new DealsNightModel
                                {
                                    Id = 0,
                                    PackageId = recordId,
                                    Value = items,
                                    VisaRequired = false
                                };
                                nightModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                                int nightId = await this.dealService.AddDealsNightAsync(nightModel);
                            }
                        }

                        //// Save Lat Long Of Destination
                        DealsPackageModel package = await this.dealService.GetDealPackageAsync(recordId);
                        DealsDestinationModel destinationModel = (await this.dealService.GetDealDestinationByDealIdAsync(recordId)).FirstOrDefault();
                        Tuple<decimal?, decimal?> newLatLong = this.GetLatLong((await this.countryService.GetByIdAsync(destinationModel.Country)).Name, this.configurationService.GetValue<string>("DomainSetting:GoogleMapKey"));
                        package.Lat = newLatLong.Item1;
                        package.Long = newLatLong.Item2;
                        await this.dealService.UpdateDealPackageInfoAsync(package);
                    }

                    this.ShowMessage(Messages.SavedSuccessfully);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage(Messages.InsertFailed);
            }

            if (model.CommandButton != null && model.CommandButton == "SaveandReload")
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id = recordId, packageTypeId = model.Type });
            }
            else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
            {
                if (model.Type == 1)
                {
                    this.TempData["nextview"] = "#rooms";
                }

                if (model.Type == 2 || model.Type == 3)
                {
                    this.TempData["nextview"] = "#itinerary";
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id = recordId, packageTypeId = model.Type });
            }
            else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
            {
                return this.RedirectToAction("List", "Deals", new { @area = Constants.AreaAdmin, @type = model.Type });
            }
            else
            {
                return this.RedirectToAction("List", "Deals", new { @area = Constants.AreaAdmin, @type = model.Type });
            }
        }

        /// <summary>
        /// Gets Geo Code Lat Long From Address asynchronous.
        /// </summary>
        /// <param name="address">hotel address.</param>
        /// <param name="key">key.</param>
        /// <returns>
        /// Latitude  and Longitude
        /// </returns>
        public Tuple<decimal?, decimal?> GetLatLong(string address, string key)
        {
            try
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key=" + key, Uri.EscapeDataString(address));
                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());
                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                XElement locationElement = result.Element("geometry").Element("location");
                return new Tuple<decimal?, decimal?>(decimal.Parse(locationElement.Element("lat").Value), decimal.Parse(locationElement.Element("lng").Value));
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new Tuple<decimal?, decimal?>(null, null);
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <param name="lastDate">Last Date</param>
        /// <returns>Manage</returns>
        public ActionResult AddBookingValidity(int packageId, string lastDate)
        {
            DealsBookingValidityViewModel model = new DealsBookingValidityViewModel();
            model.PackageId = packageId;
            model.ValidTo = model.ValidFrom = !string.IsNullOrEmpty(lastDate) ? DateTime.ParseExact(lastDate, "dd/MM/yyyy", new CultureInfo("en-IN")).AddDays(1) : DateTime.Now;
            model.PackageId = packageId;
            model.ValidTo = model.ValidTo.AddDays(15);
            return this.PartialView("_AddBookingValidity", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <param name="maxPassenger">The Max Passenger.</param>
        /// <returns>Manage</returns>
        public ActionResult AddPaxCombination(int packageId, int maxPassenger)
        {
            DealsPaxCombinationViewModel model = new DealsPaxCombinationViewModel();
            model.PackageId = packageId;
            model.Total = maxPassenger;
            return this.PartialView("_AddPaxCombination", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <param name="packageTypeId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Content(int packageId, int packageTypeId)
        {
            this.TempData["packageId"] = packageId;
            this.TempData["packageType"] = packageTypeId;
            this.ViewBag.PackageId = packageId;
            this.ViewBag.PackageType = packageTypeId;
            DealsContentAndItineraryViewModel model = new DealsContentAndItineraryViewModel();
            var dealsContent = await this.dealService.GetDealsContentByPackageIdAsync(packageId);
            model.DealContent = dealsContent == null ? new DealsContentViewModel { PackageId = packageId } : dealsContent;
            model.DealsItineraries = new List<DealsItineraryViewModel>();
            model.DealsNights = this.Mapper.Map<List<DealsNightViewModel>>(await this.dealService.GetNightsAsync(packageId));
            foreach (DealsNightViewModel dealNight in model.DealsNights)
            {
                dealNight.DealsItineraryViewModels = this.Mapper.Map<List<DealsItineraryViewModel>>(await this.dealService.GetItinerariesAsync(dealNight.Id));
                ////model.DealsItineraries.ToList().AddRange(dealNight.DealsItineraryViewModels.ToList());
            }

            model.DealsHighlights = await this.dealService.GetAllDealHighlightsFromPackageIdAsync(packageId);
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The hotelid.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Content(DealsContentAndItineraryViewModel model)
        {
            int packageId = Convert.ToInt32(this.TempData["packageId"]);
            int packageType = Convert.ToInt32(this.TempData["packageType"]);
            model.DealContent = await this.DealsContentImageOperation(model.DealContent);
            var contentModel = this.Mapper.Map<DealsContentModel>(model.DealContent);
            if (contentModel.Id > 0)
            {
                contentModel.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.UpdateDealsContent(contentModel);
            }
            else
            {
                contentModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.AddDealsContent(contentModel);
            }

            if (model.DealsItineraries != null)
            {
                foreach (var item in model.DealsItineraries)
                {
                    var record = await this.dealService.GetItineraryByIdAsync(item.Id);
                    record.Description = item.Description;
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealsItenaryAsync(record);

                    if (item.DealsAddOnModels != null)
                    {
                        foreach (var addOnItem in item.DealsAddOnModels)
                        {
                            var recordAddOn = await this.dealService.GetDealAddOnAsync(addOnItem.Id);
                            if (addOnItem.ImageFile != null)
                            {
                                addOnItem.Image = await this.UploadOnly("DealImages", addOnItem.ImageFile);
                            }

                            recordAddOn.Image = addOnItem.Image;
                            recordAddOn.Description = addOnItem.Description;
                            recordAddOn.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));

                            await this.dealService.UpdateDealAddOnAsync(recordAddOn);
                        }
                    }
                }
            }

            if (model.DealsHighlights != null)
            {
                foreach (var item in model.DealsHighlights)
                {
                    if (item.IsDeleted)
                    {
                        if (item.Id > 0)
                        {
                            await this.dealService.DeleteDealHighlightByIdAsync(item.Id);
                        }
                    }
                    else if (item.Id > 0)
                    {
                        var highlightRecord = this.Mapper.Map<DealsHighlightModel>(item);
                        highlightRecord.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealService.UpdateDealHighlight(highlightRecord);
                    }
                    else
                    {
                        var highlightRecord = this.Mapper.Map<DealsHighlightModel>(item);
                        highlightRecord.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealService.AddDealHighlightAsync(highlightRecord);
                    }
                }
            }

            this.TempData["nextview"] = "#content";
            this.ShowMessage(Messages.SavedSuccessfully);
            return this.RedirectToAction("Manage", "Deals", new { @id = packageId, packageTypeId = packageType });
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> MoveHotelierImagesFromMaster(int packageId)
        {
            var inclusionRecord = this.dealService.GetInclusionRecordForHotelFromPackageId(packageId);
            var imageContents = await this.dealService.GetHotelierImagesByHotelIdAsync(Convert.ToInt32(inclusionRecord.VendorInfoId));
            if (imageContents.Count == 0)
            {
                return this.Json("nodata");
            }
            else
            {
                foreach (var item in imageContents)
                {
                    DealsImageModel imageModel = new DealsImageModel
                    {
                        Id = 0,
                        Caption = item.Caption,
                        Image = item.Image,
                        PackageId = packageId,
                        SortOrder = item.SortOrder
                    };
                    imageModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.AddDealsImageAsync(imageModel);
                }

                this.TempData["nextview"] = "#images";
                return this.Json("success");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> MoveHotelReviewsFromMaster(int packageId)
        {
            var inclusionRecord = this.dealService.GetInclusionRecordForHotelFromPackageId(packageId);
            var reviews = await this.dealService.GetAllHotelReviews(Convert.ToInt32(inclusionRecord.VendorInfoId));
            if (reviews.Count == 0)
            {
                return this.Json("nodata");
            }
            else
            {
                foreach (var item in reviews)
                {
                    DealsReviewModel reviewModel = new DealsReviewModel
                    {
                        Id = 0,
                        Comment = item.Comment,
                        FName = item.FName,
                        IsActive = true,
                        LName = item.LName,
                        Rating = item.Rating,
                        Rating_Cleanliness = item.Rating_Cleanliness,
                        PackageId = packageId,
                        Rating_Comfort = item.Rating_Comfort,
                        Rating_Location = item.Rating_Comfort,
                        Rating_Value = item.Rating_Value,
                        UserRecommend = item.UserRecommend
                    };
                    reviewModel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.AddDealsReviewAsync(reviewModel);
                }

                this.ShowMessage("Moved Successfully");
                this.TempData["nextview"] = "#reviews";
                return this.Json("success");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> MoveHotelierContentFromMaster(int packageId)
        {
            var inclusionRecord = this.dealService.GetInclusionRecordForHotelFromPackageId(packageId);
            var hotelContents = await this.dealService.GetHotelierContentByHotelIdAsync(Convert.ToInt32(inclusionRecord.VendorInfoId));
            if (hotelContents == null)
            {
                return this.Json("nodata");
            }
            else
            {
                var existingRecord = await this.dealService.GetDealsContentByPackageIdAsync(packageId);
                if (existingRecord.Id == 0)
                {
                    DealsContentModel model = new DealsContentModel
                    {
                        Id = 0,
                        About = hotelContents.About,
                        AboutImg = hotelContents.AboutImg,
                        BannerImg2x2_1 = hotelContents.BannerImg2x2_1,
                        BannerImg2x2_2 = hotelContents.BannerImg2x2_2,
                        BannerImg2x2_3 = hotelContents.BannerImg2x2_3,
                        BannerImg2x2_4 = hotelContents.BannerImg2x2_4,
                        BannerImg2x4 = hotelContents.BannerImg2x4,
                        BannerImg4x4 = hotelContents.BannerImg4x4,
                        CardImg = hotelContents.CardImg,
                        LogoImg = hotelContents.LogoImg,
                        OverallCleaninessRating = hotelContents.OverallCleaninessRating,
                        OverallComfortRating = hotelContents.OverallComfortRating,
                        OverallRating = hotelContents.OverallRating,
                        OverallValueRating = hotelContents.OverallValueRating,
                        PackageId = packageId,
                        TAUrl = hotelContents.TAUrl
                    };
                    model.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.AddDealsContent(model);
                }
                else
                {
                    existingRecord.About = hotelContents.About;
                    existingRecord.BannerImg2x2_1 = hotelContents.BannerImg2x2_1;
                    existingRecord.BannerImg2x2_2 = hotelContents.BannerImg2x2_2;
                    existingRecord.BannerImg2x2_3 = hotelContents.BannerImg2x2_3;
                    existingRecord.BannerImg2x2_4 = hotelContents.BannerImg2x2_4;
                    existingRecord.BannerImg2x4 = hotelContents.BannerImg2x4;
                    existingRecord.BannerImg4x4 = hotelContents.BannerImg4x4;
                    existingRecord.CardImg = hotelContents.CardImg;
                    existingRecord.LogoImg = hotelContents.LogoImg;
                    existingRecord.OverallCleaninessRating = hotelContents.OverallCleaninessRating;
                    existingRecord.OverallComfortRating = hotelContents.OverallComfortRating;
                    existingRecord.OverallRating = hotelContents.OverallRating;
                    existingRecord.OverallValueRating = hotelContents.OverallValueRating;
                    existingRecord.PackageId = packageId;
                    existingRecord.TAUrl = hotelContents.TAUrl;
                    var existingRecordModel = this.Mapper.Map<DealsContentModel>(existingRecord);
                    existingRecordModel.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealsContent(existingRecordModel);
                }

                return this.Json("success");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <returns>Manage</returns>
        public ActionResult AddHighLight(int packageId)
        {
            DealsHighlightViewModel model = new DealsHighlightViewModel
            {
                Id = 0,
                PackageId = packageId,
                IsDeleted = false,
                RandomIdentifier = Guid.NewGuid()
            };
            return this.PartialView("_HighlightRow", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <param name="packageTypeId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult AddOns(int packageId, int packageTypeId)
        {
            this.ViewBag.PackageId = packageId;
            return this.PartialView();
        }

        /// <summary>
        /// Manage Itinerary Content the specified identifier.
        /// </summary>
        /// <param name="nightId">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> ManageItineraryContent(int nightId)
        {
            List<DealsItineraryViewModel> model = new List<DealsItineraryViewModel>();
            var iteineraryContent = await this.dealService.GetItinerariesAsync(nightId);
            for (int i = 0; i < iteineraryContent.Count; i++)
            {
                DealsItineraryViewModel itenaryViewModel = new DealsItineraryViewModel();
                itenaryViewModel = this.Mapper.Map<DealsItineraryViewModel>(iteineraryContent[i]);
                itenaryViewModel.InclusionModels = new List<DealsInclusionViewModel>();
                for (int j = 0; j < iteineraryContent[i].DealsInclusionModels.Count; j++)
                {
                    DealsInclusionViewModel inclusionViewModel = this.Mapper.Map<DealsInclusionViewModel>(iteineraryContent[i].DealsInclusionModels[j]);
                    inclusionViewModel.AddOnViewModels = new List<DealsAddOnViewModel>();
                    for (int k = 0; k < iteineraryContent[i].DealsInclusionModels[j].DealsAddOnModels.Count; k++)
                    {
                        DealsAddOnViewModel addOnViewModel = this.Mapper.Map<DealsAddOnViewModel>(iteineraryContent[i].DealsInclusionModels[j].DealsAddOnModels[k]);
                        inclusionViewModel.AddOnViewModels.Add(addOnViewModel);
                    }

                    itenaryViewModel.InclusionModels.Add(inclusionViewModel);
                }

                model.Add(itenaryViewModel);
            }

            model = model != null ? model.OrderBy(x => x.StartDay).ToList() : new List<DealsItineraryViewModel>();
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <param name="packageTypeId">The package Type identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Images(int packageId, int packageTypeId)
        {
            this.ViewBag.PackageId = packageId;
            this.TempData["PackageId"] = packageId;
            this.TempData["PackageTypeId"] = packageTypeId;
            var result = await this.dealService.GetDealsImagesByPackageId(packageId);
            List<DealsImageViewModel> dealsImage = this.Mapper.Map<List<DealsImageViewModel>>(result);
            return this.PartialView(dealsImage);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="dealsImageViewModels">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Images(List<DealsImageViewModel> dealsImageViewModels)
        {
            try
            {
                foreach (DealsImageViewModel model in dealsImageViewModels)
                {
                    if (model.PackageId == 0)
                    {
                        model.PackageId = Convert.ToInt32(this.TempData["PackageId"]);
                    }

                    var record = this.Mapper.Map<DealsImageModel>(model);
                    if (model.IsDeleted)
                    {
                        if (model.Id > 0)
                        {
                            await this.dealService.DeletePackageImageById(model.Id);
                        }
                    }
                    else
                    {
                        if (model.ImageFile != null)
                        {
                            record.Image = await this.UploadOnly("DealImages", model.ImageFile);
                        }

                        if (model.IsDeleted)
                        {
                            if (model.Id > 0)
                            {
                                await this.dealService.DeleteDealTourImageAsync(model.Id);
                            }
                        }
                        else if (record.Id > 0)
                        {
                            ////Update Deal Image
                            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.UpdateDealsImageAsync(record);
                        }
                        else
                        {
                            ////Add Deal Image
                            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.dealService.AddDealsImageAsync(record);
                        }
                    }
                }

                    this.TempData["nextview"] = "#images";
                    this.ShowMessage(Messages.SavedSuccessfully, Enums.MessageType.Success);
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, id = Convert.ToInt32(this.TempData["PackageId"]), packageTypeId = Convert.ToInt32(this.TempData["PackageTypeId"]) });
            }
            catch (Exception ex)
            {
                this.ShowMessage(Messages.InsertFailed, Enums.MessageType.Error);
                string msg = ex.ToString();
                this.TempData["nextview"] = "#images";
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Deals", action = "Manage", area = Constants.AreaAdmin, packageId = this.TempData["PackageId"], packageTypeId = this.TempData["PackageTypeId"] });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The hotelid.</param>
        /// <returns>Manage</returns>
        public ActionResult AddImage(int packageId)
        {
            DealsImageViewModel model = new DealsImageViewModel
            {
                PackageId = packageId,
                Id = 0,
            };
            return this.PartialView("_AddImage", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="identifier">The hotelid.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DealImageUpload(IFormFile file, int identifier)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            int sortOrder = 0;
            if (!string.IsNullOrEmpty(fileName))
            {
                Regex regex;
                regex = new Regex(@"\d+", RegexOptions.IgnoreCase);
                var match = regex.Match(fileName);
                if (match.Success)
                {
                    sortOrder = Convert.ToInt32(match.Groups[0].ToString());
                }
            }

            DealsImageViewModel model = new DealsImageViewModel
            {
                PackageId = identifier,
                Id = 0,
                Image = file.FileName,
                Caption = Path.GetFileNameWithoutExtension(file.FileName).Replace(sortOrder.ToString(), string.Empty),
                SortOrder = sortOrder,
                IsDeleted = false,
            };
            if (file != null)
            {
                model.Image = await this.UploadOnly("DealImages", file);
            }

            return this.PartialView("_AddImage", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> CancellationPolicy(int packageId, int packageTypeId)
        {
            List<DealsCancellationPolicyViewModel> model = await this.cancellationService.GetDealsCancellationPolicyByPackageId(packageId, packageTypeId);
            foreach (var item in model)
            {
                item.MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList();
            }

            this.ViewBag.PackageId = packageId;
            return this.PartialView("CancellationPolicy", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> CancellationPolicies(List<DealsCancellationPolicyViewModel> model)
        {
            if (model.Count > 0)
            {
                foreach (var item in model)
                {
                    if (item.IsDeleted)
                    {
                        if (item.Id > 0)
                        {
                            var record = await this.cancellationService.GetDealsCancellationPolicyById(item.Id);
                            await this.cancellationService.DeleteDealsCancellationPolicy(record);
                        }
                    }
                    else if (item.Id > 0)
                    {
                        ////Update
                        var record = this.Mapper.Map<DealsCancellationPolicyModel>(item);
                        record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.cancellationService.UpdateDealsCancellationPolicy(record);
                    }
                    else
                    {
                        var record = this.Mapper.Map<DealsCancellationPolicyModel>(item);
                        record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.cancellationService.AddDealsCancellationPolicy(record);
                    }
                }

                this.TempData["nextview"] = "#cancellationPolicy";
                this.ShowMessage("Successfully Updated", Enums.MessageType.Success);
                return this.Json("success");
            }

            this.TempData["nextview"] = "#cancellationPolicy";
            this.ShowMessage("Failed", Enums.MessageType.Warning);
            return this.Json("success");
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="packageId">HotelId</param>
        public async Task<IActionResult> AddCancellationPolicy(int packageId)
        {
            DealsCancellationPolicyViewModel model = new DealsCancellationPolicyViewModel
            {
                Id = 0,
                IsDeleted = false,
                PackageId = packageId,
                MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList()
            };
            return this.PartialView("_AddCancellationPolicy", model);
        }

        /////// <summary>
        /////// Manages the specified identifier.
        /////// </summary>
        /////// <param name="packageId">The identifier.</param>
        /////// <returns>Manage</returns>
        ////[HttpGet]
        ////public ActionResult AddOnGrid(int packageId)
        ////{
        ////    this.ViewBag.PackageId = packageId;
        ////    return this.PartialView("_AddOnGrid");
        ////}

        /////// <summary>
        /////// Manages the specified identifier.
        /////// </summary>
        /////// <param name="model">Data Table Model</param>
        /////// <param name="packageId">The identifier.</param>
        /////// <returns>Manage</returns>
        ////public async Task<ActionResult> AddOnsGridData([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int packageId)
        ////{
        ////    this.ViewBag.PackageId = packageId;
        ////    if (this.IsAjaxRequest())
        ////    {
        ////        var result = await this.dealService.GetAllAddOnsByPackageId(model, packageId);
        ////        return this.Json(result);
        ////    }

        ////    return this.PartialView();
        ////}

        /////// <summary>
        /////// Manages the specified identifier.
        /////// </summary>
        /////// <param name="id">Review Identifier</param>
        /////// <param name="packageId">The identifier.</param>
        /////// <returns>Manage</returns>
        ////public async Task<ActionResult> AddOnAdd(int id, int packageId)
        ////{
        ////    this.ViewBag.PackageId = packageId;
        ////    DealsAddOnViewModel model = new DealsAddOnViewModel
        ////    {
        ////        Id = 0,
        ////        PackageId = packageId
        ////    };

        ////    if (id > 0)
        ////    {
        ////        model = this.Mapper.Map<DealsAddOnViewModel>(await this.dealService.AddDealsAddOnByIdAsync(id));
        ////    }

        ////    return this.PartialView("_AddAddOns", model);
        ////}

        /////// <summary>
        /////// Manages the specified identifier.
        /////// </summary>
        /////// <param name="model">Review Identifier</param>
        /////// <returns>Manage</returns>
        ////[HttpPost]
        ////public async Task<ActionResult> AddOnAdd(DealsAddOnViewModel model)
        ////{
        ////    if (this.ModelState.IsValid)
        ////    {
        ////        var record = this.Mapper.Map<DealsAddOnModel>(model);
        ////        if (model.Id > 0)
        ////        {
        ////            ////Update
        ////            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
        ////            await this.dealService.UpdateDealsAddOnAsync(record);
        ////            return this.Json("update");
        ////        }
        ////        else
        ////        {
        ////            ////Add
        ////            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
        ////            await this.dealService.AddDealsAddOnAsync(record);
        ////            return this.Json("success");
        ////        }
        ////    }

        ////    return this.Json("failure");
        ////}

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult Reviews(int packageId, int packageTypeId)
        {
            this.ViewBag.PackageTypeId = packageTypeId;
            this.ViewBag.PackageId = packageId;
            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult ReviewsGrid(int packageId)
        {
            this.ViewBag.PackageId = packageId;
            return this.PartialView("_ReviewsGrid");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Data Table Model</param>
        /// <param name="packageId">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ReviewsGridData([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int packageId)
        {
            this.ViewBag.PackageId = packageId;
            if (this.IsAjaxRequest())
            {
                var result = await this.dealService.GetAllDealsReviewsByPackageId(model, packageId);
                return this.Json(result);
            }

            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">Review Identifier</param>
        /// <param name="packageId">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ReviewAdd(int id, int packageId)
        {
            this.ViewBag.PackageId = packageId;
            DealsReviewViewModel model = new DealsReviewViewModel
            {
                Id = 0,
                IsActive = true,
                PackageId = packageId
            };

            if (id > 0)
            {
                model = this.Mapper.Map<DealsReviewViewModel>(await this.dealService.GetDealsReviewsById(id));
            }

            return this.PartialView("_AddReview", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Review Identifier</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> ReviewAdd(DealsReviewViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<DealsReviewModel>(model);
                if (model.Id > 0)
                {
                    ////Update
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.UpdateDealsReviewAsync(record);
                    return this.Json("update");
                }
                else
                {
                    ////Add
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.dealService.AddDealsReviewAsync(record);
                    return this.Json("success");
                }
            }

            return this.Json("failure");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="packageId">The identifier.</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Seo(int packageId, int packageTypeId)
        {
            this.ViewBag.PackageId = packageId;
            this.TempData["packageTypeId"] = packageTypeId;
            DealsSeoDetail model = await this.dealService.GetSeoDetail(packageId);
            if (model == null)
            {
                model = new DealsSeoDetail
                {
                    Id = 0,
                    DealId = packageId
                };
            }

            return this.PartialView("_SeoDetail", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Seo(DealsSeoDetail model)
        {
            this.ViewBag.PackageId = model.DealId;
            if (model.Id > 0)
            {
                //// Update Code
                model.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.UpdateDealSeo(model);
                this.ShowMessage("Updated Successfully");
            }
            else
            {
                //// Add Code
                model.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.dealService.AddDealSeo(model);
                this.ShowMessage("Successfully Inserted");
            }

            this.TempData["nextview"] = "#seo";
            return this.RedirectToAction("Manage", new { @packageTypeId = this.TempData["packageTypeId"], @id = model.DealId });
        }

        /// <summary>
        /// Get Package Type Name
        /// </summary>
        /// <param name="packageTypeId">Package type identifier</param>
        /// <returns> Package type name</returns>
        private string GetPackageTypeName(int packageTypeId)
        {
            string name = string.Empty;
            if (packageTypeId == 0)
            {
                name = string.Empty;
            }

            if (packageTypeId == 1)
            {
                name = "Hotel";
            }

            if (packageTypeId == 2)
            {
                name = "Tour";
            }

            if (packageTypeId == 3)
            {
                name = "Cruise";
            }

            if (packageTypeId == 4)
            {
                name = "Flight";
            }

            if (packageTypeId == 5)
            {
                name = "Bus";
            }

            return name;
        }

        private async Task<string> UploadOnly(string folder, IFormFile file)
        {
            return await this.UploadImageBlobStorage(folder, file);
        }

        private async Task<DealsContentViewModel> DealsContentImageOperation(DealsContentViewModel model)
        {
            if (model.CardImgFile != null)
            {
                model.CardImg = await this.UploadOnly("DealImages", model.CardImgFile);
            }

            if (model.LogoImgFile != null)
            {
                model.LogoImg = await this.UploadOnly("DealImages", model.LogoImgFile);
            }

            if (model.AboutImgFile != null)
            {
                model.AboutImg = await this.UploadOnly("DealImages", model.AboutImgFile);
            }

            if (model.BannerImg2x2_1File != null)
            {
                model.BannerImg2x2_1 = await this.UploadOnly("DealImages", model.BannerImg2x2_1File);
            }

            if (model.BannerImg2x2_2File != null)
            {
                model.BannerImg2x2_2 = await this.UploadOnly("DealImages", model.BannerImg2x2_2File);
            }

            if (model.BannerImg2x2_3File != null)
            {
                model.BannerImg2x2_3 = await this.UploadOnly("DealImages", model.BannerImg2x2_3File);
            }

            if (model.BannerImg2x2_4File != null)
            {
                model.BannerImg2x2_4 = await this.UploadOnly("DealImages", model.BannerImg2x2_4File);
            }

            if (model.BannerImg2x4File != null)
            {
                model.BannerImg2x4 = await this.UploadOnly("DealImages", model.BannerImg2x4File);
            }

            if (model.BannerImg4x4File != null)
            {
                model.BannerImg4x4 = await this.UploadOnly("DealImages", model.BannerImg4x4File);
            }

            return model;
        }
    }
}