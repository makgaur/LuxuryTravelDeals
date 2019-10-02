// <copyright file="HotelierController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CityController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class HotelierController : AdminController
    {
        private readonly IHotelierService hotelierServices;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly IVendorService vendorService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ICancellationService cancellationService;
        private readonly IPromotionService promotionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelierController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="promotionService">Promotion Service</param>
        /// <param name="cancellationService">Cancellation Service</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="hotel">Type of the hoteroom.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="vendorService">Vendor Service</param>
        public HotelierController(IConfiguration configuration, IPromotionService promotionService, ICancellationService cancellationService, IHostingEnvironment hostingEnvironment, IMapper mapper, IHotelierService hotel, IMasterService masterService, IVendorService vendorService)
            : base(mapper, configuration)
        {
            this.promotionService = promotionService;
            this.cancellationService = cancellationService;
            this.hostingEnvironment = hostingEnvironment;
            this.vendorService = vendorService;
            this.hotelierServices = hotel;
            this.masterService = masterService;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> List([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.hotelierServices.GetHotelsAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="infoId">The identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult Manage(int infoId, int vendorId)
        {
            this.ViewBag.HotelId = infoId;
            this.ViewBag.VendorId = vendorId;
            return this.View();
        }

        /// <summary>
        /// Gets Hotel Info
        /// </summary>
        /// <param name="id"> Identifier</param>
        /// <param name="vendorId">Vendor Identifier</param>
        /// <returns>Hotel Info model view</returns>
        public async Task<ActionResult> Information(int id, int vendorId)
        {
            var model = new HotelierInfoViewModel
            {
                VendorInformationViewModel = new VendorInformationViewModel(),
                VendorId = vendorId,
                IsActive = true,
                IsDeleted = false,
                Id = id
            };
            if (id > 0)
            {
                var result = await this.hotelierServices.GetHotelierInfoAsync(id);
                model = this.Mapper.Map<HotelierInfoViewModel>(result);
                model.VendorInformationViewModel = this.Mapper.Map<VendorInformationViewModel>(result.VendorInformationModel);

                DateTime time = DateTime.Today.Add((TimeSpan)result.CheckIn);
                model.CheckInTime = time.ToString("hh:mm tt");
                time = DateTime.Today.Add((TimeSpan)result.CheckOut);
                model.CheckOutTime = time.ToString("hh:mm tt");
            }
            else
            {
                if (vendorId > 0)
                {
                    model.VendorInformationViewModel = this.Mapper.Map<VendorInformationViewModel>(await this.vendorService.GetVendorById(vendorId));
                }
            }

            model.PropertyTypeItems = model.PropertyType == 0 ? new List<SelectListItem>() : (await this.hotelierServices.GetHotelierPropertyTypeDropDownListAsync(string.Empty, 1, model.PropertyType)).ToSelectList();
            model.VendorInformationViewModel.CurrencyItems = model.VendorInformationViewModel.Currency == 0 ? new List<SelectListItem>() : (await this.vendorService.GetCurrencyDropDownListAsync(string.Empty, 1, model.VendorInformationViewModel.Currency)).ToSelectList();
            model.VendorInformationViewModel.VendorGroupItems = model.VendorInformationViewModel.Group == 0 || model.VendorInformationViewModel.Group == null ? new List<SelectListItem>() : (await this.vendorService.GetVendorGroupDropDownListAsync(string.Empty, 1, model.VendorInformationViewModel.Group)).ToSelectList();
            model.VendorInformationViewModel.CategoryItems = model.VendorInformationViewModel.Category == 0 ? new List<SelectListItem>() : (await this.vendorService.GetCategoryDropDownListAsync(string.Empty, 1, model.VendorInformationViewModel.Category)).ToSelectList();
            model.VendorItems = model.VendorId == 0 ? new List<SelectListItem>() : (await this.vendorService.GetVendorDropDownListAsync(string.Empty, 1, model.VendorId)).ToSelectList();
            model.CountryItems = model.Country == 0 ? new List<SelectListItem>() : (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, model.Country)).ToSelectList();
            model.StateItems = model.State == 0 || model.State == null ? new List<SelectListItem>() : (await this.masterService.GetTourPackageStatesByCountrId(string.Empty, 1, model.Country, (short)model.State)).ToSelectList();
            model.CityItems = model.City == 0 || model.City == null ? new List<SelectListItem>() : (await this.masterService.GetTourPackageCityByCounryIdorStateIdAsync(string.Empty, 1, model.Country, model.State == 0 || model.State == null ? (short)0 : (short)model.State, (short)model.City)).ToSelectList();
            return this.PartialView(model);
        }

        /// <summary>
        /// Gets Hotel Info
        /// </summary>
        /// <param name="model"> Model</param>
        /// <param name="nextview">Next View</param>
        /// <returns>Hotel Info model view</returns>
        [HttpPost]
        public async Task<ActionResult> Information(HotelierInfoViewModel model, string nextview)
        {
            try
            {
                var recordHotel = this.Mapper.Map<HotelierInformationModel>(model);
                if (model.IsOpenCheckIn)
                {
                    recordHotel.CheckIn = TimeSpan.Zero;
                    recordHotel.CheckOut = TimeSpan.Zero;
                }
                else
                {
                    recordHotel.CheckIn = string.IsNullOrEmpty(model.CheckInTime) ? TimeSpan.MinValue : DateTime.ParseExact(model.CheckInTime, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    recordHotel.CheckOut = string.IsNullOrEmpty(model.CheckOutTime) ? TimeSpan.MinValue : DateTime.ParseExact(model.CheckOutTime, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                }

                if (model.Id > 0)
                {
                    //// Update Hotelier
                    recordHotel.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    model.Id = await this.hotelierServices.UpdateHotelierInformationAsync(recordHotel);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }
                else
                {
                    ////Add Hotelier
                    recordHotel.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    model.Id = await this.hotelierServices.AddHotelierInformationAsync(recordHotel);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }

                if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "Manage", area = Constants.AreaAdmin, infoId = model.Id, vendorId = model.VendorId });
                }
                else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                {
                    this.TempData["nextview"] = "#vendor-contact";
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "Manage", area = Constants.AreaAdmin, infoId = model.Id, vendorId = model.VendorId });
                }
                else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "List", area = Constants.AreaAdmin });
                }
                else
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "List", area = Constants.AreaAdmin });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage(Messages.InsertFailed);
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "List", area = Constants.AreaAdmin });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult Rooms(int id)
        {
            this.ViewBag.HotelId = id;
            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelRoomId">The Hotel Room ConfigId.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> RoomsAdd(int hotelRoomId, int hotelId)
        {
            HotelierRoomConfigurationViewModel model = new HotelierRoomConfigurationViewModel
            {
                Id = 0,
                HotelId = hotelId,
                IsActive = true
            };
            if (hotelRoomId > 0)
            {
                model = this.Mapper.Map<HotelierRoomConfigurationViewModel>(await this.hotelierServices.GetHotelierRoomConfigByIdAsync(hotelRoomId));
            }

            model.RoomTypeItems = model.RoomTypeId > 0 ? (await this.masterService.GetPackageHotelRoomTypeListAsync(string.Empty, 1, model.RoomTypeId)).ToSelectList() : new List<SelectListItem>();
            this.ViewBag.HotelId = hotelId;
            return this.PartialView("_RoomsAdd", model);
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="model">the identifier.</param>
        /// <returns>viewmodel</returns>
        [HttpPost]
        public async Task<IActionResult> RoomsAdd(HotelierRoomConfigurationViewModel model)
        {
            try
            {
                var record = this.Mapper.Map<HotelierRoomConfigurationModel>(model);
                if (model.Id > 0)
                {
                    ////Update
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.hotelierServices.UpdateHotelierRoomConfigAsync(record);
                    return this.Json("update");
                }
                else
                {
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.hotelierServices.AddHotelierRoomConfigAsync(record);
                    return this.Json("success");
                }
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
        /// <param name="hotelId">The Hotel Id.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult RoomsGrid(int hotelId)
        {
            this.ViewBag.HotelId = hotelId;
            return this.PartialView("_RoomsGrid");
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> RoomGridData([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int hotelId)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.hotelierServices.GetHotelierRoomCofigGrid(model, hotelId);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteRoomConfig(int id)
        {
            try
            {
                await this.hotelierServices.DeleteHotelierRoomConfigAsync(id);
                this.ShowMessage(Messages.DeletedSuccessfully);
                this.TempData["nextview"] = "#rooms";
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
        /// <param name="hotelId">The identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Content(int hotelId, int vendorId)
        {
            HotelierContentViewModel model = await this.hotelierServices.GetHotelierContentByHotelIdAsync(hotelId);
            if (model.HotelierRoomConfigurations.Count > 0)
            {
                foreach (var item in model.HotelierRoomConfigurations)
                {
                    item.AmenetiesItems = item.Ameneties != null ? (await this.hotelierServices.GetAmenitiesListAsync(string.Empty, 1, item.Ameneties)).ToSelectList() : new List<SelectListItem>();
                }
            }

            model.HotelAmenetiesItem = model.HotelAmeneties != null ? (await this.hotelierServices.GetHotelierAmenitiesListAsync(string.Empty, 1, model.HotelAmeneties, "all")).ToSelectList() : new List<SelectListItem>();
            model.VendorId = vendorId;
            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The Model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Content(HotelierContentViewModel model)
        {
            model = await this.HotelContentImageOperation(model);
            var record = this.Mapper.Map<HotelierContentModel>(model);
            if (model.Id > 0)
            {
                ////Update Vendor
                await this.hotelierServices.DeleteAllHotelierAmetiesByHotelId(record.HotelId);
                record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.hotelierServices.UpdateHotelierContent(record);
                this.ShowMessage(Messages.UpdateSuccessfully);
            }
            else
            {
                //// Add Vendor
                record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                await this.hotelierServices.AddHotelierContent(record);
                this.ShowMessage(Messages.SavedSuccessfully);
            }

            foreach (var item in model.HotelAmeneties)
            {
                HotelierAmenitiesModel hotelAmenetieModel = new HotelierAmenitiesModel
                {
                    AmentieId = item,
                    HotelId = model.HotelId
                };
                await this.hotelierServices.AddHotelierAmenity(hotelAmenetieModel);
            }

            if (model.HotelierRoomConfigurations != null)
            {
                foreach (var items in model.HotelierRoomConfigurations)
                {
                    var recordHotelRoomConfig = await this.hotelierServices.GetHotelierRoomConfigByIdAsync(items.Id);
                    recordHotelRoomConfig.Description = items.Description;
                    if (items.CardImgFile != null)
                    {
                        recordHotelRoomConfig.CardImg = await this.UploadOnly("DealImages", items.CardImgFile);
                    }

                    recordHotelRoomConfig.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.hotelierServices.UpdateHotelierRoomConfigAsync(recordHotelRoomConfig);

                    await this.hotelierServices.DeleteAllHotelierRoomAmetiesByRoomConfigId(items.Id);
                    if (items.Ameneties != null)
                    {
                        foreach (var amenity in items.Ameneties)
                        {
                            HotelierRoomAmenetiesModel roomAmenityModel = new HotelierRoomAmenetiesModel
                            {
                                AmenetieId = amenity,
                                RoomConfigId = items.Id
                            };
                            await this.hotelierServices.AddHotelierRoomAmenity(roomAmenityModel);
                        }
                    }
                }
            }

            this.TempData["nextview"] = "#content";
            return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "Manage", area = Constants.AreaAdmin, infoId = model.HotelId, vendorId = model.VendorId });
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Images(int hotelId, int vendorId)
        {
            HotelierImageMasterViewModel model = new HotelierImageMasterViewModel
            {
                VendorId = vendorId,
                HotelId = hotelId,
                HotelierImageViewModels = new List<HotelierImageViewModel>(),
                HotelierRoomImageViewModels = new List<HotelierRoomImageViewModel>(),
                HotelierRoomConfigurationViewModels = new List<HotelierRoomConfigurationViewModel>()
            };
            var hotelierImage = await this.hotelierServices.GetHotelierImageByHotelId(hotelId);
            foreach (var items in hotelierImage)
            {
                model.HotelierImageViewModels.Add(this.Mapper.Map<HotelierImageViewModel>(items));
            }

            var hotelierRoomImages = await this.hotelierServices.GetHotelierRoomImagesByHotelId(hotelId);
            foreach (var item in hotelierRoomImages)
            {
                model.HotelierRoomImageViewModels.Add(this.Mapper.Map<HotelierRoomImageViewModel>(item));
            }

            var hotelierRoomConfig = await this.hotelierServices.GetAllHotelierRoomConfigByHotelIdAsync(hotelId);
            foreach (var item in hotelierRoomConfig)
            {
                var hotelierRommConfigItem = this.Mapper.Map<HotelierRoomConfigurationViewModel>(item);
                hotelierRommConfigItem.PackageHotelRoomTypeViewModel = this.Mapper.Map<PackageHotelRoomTypeViewModel>(item.PackageHotelRoomTypeModel);
                model.HotelierRoomConfigurationViewModels.Add(hotelierRommConfigItem);
            }

            ////int i = 0;
            ////foreach (var items in hotelierRoomConfig)
            ////{
            ////    var configItem = this.Mapper.Map<HotelierRoomConfigurationViewModel>(items);
            ////    configItem.PackageHotelRoomTypeViewModel = this.Mapper.Map<PackageHotelRoomTypeViewModel>(items.PackageHotelRoomTypeModel);
            ////    configItem.HotelierRoomImageViewModels = new List<HotelierRoomImageViewModel>();
            ////    foreach (var image in items.HotelierRoomImageModels)
            ////    {
            ////        var hotelRoomImage = this.Mapper.Map<HotelierRoomImageViewModel>(image);
            ////        configItem.HotelierRoomImageViewModels.Add(hotelRoomImage);
            ////    }

            ////    model.HotelierRoomConfigurationViewModel.Add(configItem);
            ////    i++;
            ////}

            return this.PartialView(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult Reviews(int hotelId, int vendorId)
        {
            this.ViewBag.HotelId = hotelId;
            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult ReviewsGrid(int hotelId, int vendorId)
        {
            this.ViewBag.HotelId = hotelId;
            return this.PartialView("_ReviewsGrid");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Data Table Model</param>
        /// <param name="hotelId">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ReviewsGridData([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int hotelId)
        {
            this.ViewBag.HotelId = hotelId;
            if (this.IsAjaxRequest())
            {
                var result = await this.hotelierServices.GetAllHotelReviewsByHotelId(model, hotelId);
                return this.Json(result);
            }

            return this.PartialView();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">Review Identifier</param>
        /// <param name="hotelId">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ReviewAdd(int id, int hotelId)
        {
            this.ViewBag.HotelId = hotelId;
            HotelierReviewViewModel model = new HotelierReviewViewModel
            {
                Id = 0,
                IsActive = true,
                HotelId = hotelId
            };

            if (id > 0)
            {
                model = this.Mapper.Map<HotelierReviewViewModel>(await this.hotelierServices.GetHotelReviewsById(id));
            }

            return this.PartialView("_ReviewAdd", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">Review Identifier</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> ReviewAdd(HotelierReviewViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<HotelierReviewModel>(model);
                if (model.Id > 0)
                {
                    ////Update
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.hotelierServices.UpdateHotelierReviewAsync(record);
                    return this.Json("update");
                }
                else
                {
                    ////Add
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.hotelierServices.AddHotelierReviewAsync(record);
                    return this.Json("success");
                }
            }

            return this.Json("failure");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Images(HotelierImageMasterViewModel model)
        {
            try
            {
                if (model.HotelierImageViewModels != null)
                {
                    foreach (var item in model.HotelierImageViewModels)
                    {
                        var record = this.Mapper.Map<HotelierImageModel>(item);
                        if (item.ImageFile != null)
                        {
                            record.Image = await this.UploadOnly("DealImages", item.ImageFile);
                        }

                        if (item.IsDeleted)
                        {
                            if (item.Id > 0)
                            {
                                await this.hotelierServices.DeleteHotelierImageAsync(item.Id);
                            }
                        }
                        else if (record.Id > 0)
                        {
                            ////Update Hotel Image
                            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.hotelierServices.UpdateHotelierImageAsync(record);
                        }
                        else
                        {
                            ////Add Hotel Image
                            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.hotelierServices.AddHotelierImageAsync(record);
                        }
                    }
                }

                if (model.HotelierRoomImageViewModels != null)
                {
                    foreach (var item in model.HotelierRoomImageViewModels)
                    {
                        var record = this.Mapper.Map<HotelierRoomImageModel>(item);
                        if (item.ImageFile != null)
                        {
                            record.Image = await this.UploadOnly("DealImages", item.ImageFile);
                        }

                        if (item.IsDeleted)
                        {
                            if (item.Id > 0)
                            {
                                await this.hotelierServices.DeleteHotelierRoomImageAsync(item.Id);
                            }
                        }
                        else if (record.Id > 0)
                        {
                            ////Update Hotel Room Image
                            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.hotelierServices.UpdateHotelierRoomImageAsync(record);
                        }
                        else
                        {
                            ////Add Hotel Room Image
                            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.hotelierServices.AddHotelierRoomImageAsync(record);
                        }
                    }
                }

                this.TempData["nextview"] = "#images";
                this.ShowMessage(Messages.SavedSuccessfully, Enums.MessageType.Success);
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "Manage", area = Constants.AreaAdmin, infoId = model.HotelId, vendorId = model.VendorId });
            }
            catch (Exception ex)
            {
                this.ShowMessage(Messages.InsertFailed, Enums.MessageType.Error);
                string msg = ex.ToString();
                this.TempData["nextview"] = "#images";
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Hotelier", action = "Manage", area = Constants.AreaAdmin, infoId = model.HotelId, vendorId = model.VendorId });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The hotelid.</param>
        /// <returns>Manage</returns>
        public ActionResult HotelImagePartial(int hotelId)
        {
            HotelierImageViewModel model = new HotelierImageViewModel
            {
                HotelId = hotelId,
                Id = 0,
                IsDeleted = false,
            };

            return this.PartialView("_HotelierImage", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="file">The file Id.</param>
        /// <param name="identifer">The hotelId Id.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> HotelImageUpload(IFormFile file, int identifer)
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

            HotelierImageViewModel model = new HotelierImageViewModel
            {
                HotelId = identifer,
                Id = 0,
                Image = file.FileName,
                Caption = Path.GetFileNameWithoutExtension(fileName).Replace(sortOrder.ToString(), string.Empty),
                IsDeleted = false,
                SortOrder = sortOrder
            };

            if (file != null)
            {
                model.Image = await this.UploadOnly("DealImages", file);
            }

            ////record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
            ////await this.hotelierServices.AddHotelierImageAsync(record);

            return this.PartialView("_HotelierImage", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="roomConfigId">The roomConfiig Id.</param>
        /// <returns>Manage</returns>
        public ActionResult HotelRoomImagePartial(int roomConfigId)
        {
            HotelierRoomImageViewModel model = new HotelierRoomImageViewModel
            {
                Id = 0,
                RoomConfigId = roomConfigId,
                IsDeleted = false
            };

            return this.PartialView("_HotelierRoomImage", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="file">The file Id.</param>
        /// <param name="identifer">The roomConfig Id.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> HotelRoomImageUpload(IFormFile file, int identifer)
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

            HotelierRoomImageViewModel model = new HotelierRoomImageViewModel
            {
                Id = 0,
                RoomConfigId = identifer,
                Image = file.FileName,
                Caption = Path.GetFileNameWithoutExtension(file.FileName).Replace(sortOrder.ToString(), string.Empty),
                IsDeleted = false,
                SortOrder = sortOrder
            };

            if (file != null)
            {
                model.Image = await this.UploadOnly("DealImages", file);
            }

            ////record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
            ////await this.hotelierServices.AddHotelierRoomImageAsync(record);

            return this.PartialView("_HotelierRoomImage", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteReview(int id)
        {
            try
            {
                await this.hotelierServices.DeleteHotelierReviewAsync(id);
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
                var record = await this.hotelierServices.GetHotelReviewsById(id);
                if (record == null)
                {
                    return this.NotFound();
                }

                record.IsActive = !record.IsActive;
                await this.hotelierServices.UpdateHotelierReviewAsync(record);

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
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ChangeRoomConfigActiveStatus(int id)
        {
            try
            {
                var record = await this.hotelierServices.GetHotelierRoomConfigByIdAsync(id);
                if (record == null)
                {
                    return this.NotFound();
                }

                record.IsActive = !record.IsActive;
                await this.hotelierServices.UpdateHotelierRoomConfigAsync(record);

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
        /// <param name="hotelId">The identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> CancellationPolicy(int hotelId, int vendorId)
        {
            List<HotelierCancellationPolicyViewModel> model = await this.cancellationService.GetHotelierCancellationPolicyByHotelId(hotelId);
            foreach (var item in model)
            {
                item.MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList();
            }

            this.ViewBag.HotelId = hotelId;
            this.ViewBag.VendorId = vendorId;
            return this.PartialView("_CancellationPolicy", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> CancellationPolicies(List<HotelierCancellationPolicyViewModel> model)
        {
            if (this.ModelState.IsValid)
            {
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        if (item.IsDeleted)
                        {
                            if (item.Id > 0)
                            {
                                var record = await this.cancellationService.GetHotelierCancellationPolicyById(item.Id);
                                await this.cancellationService.DeleteHotelierCancellationPolicy(record);
                            }
                        }
                        else if (item.Id > 0)
                        {
                            ////Update
                            var record = this.Mapper.Map<HotelierCancellationPolicyModel>(item);
                            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.cancellationService.UpdateHotelierCancellationPolicy(record);
                        }
                        else
                        {
                            var record = this.Mapper.Map<HotelierCancellationPolicyModel>(item);
                            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                            await this.cancellationService.AddHotelierCancellationPolicy(record);
                        }
                    }

                    this.TempData["nextview"] = "#cancellation";
                    this.ShowMessage("Successfully Updated", Enums.MessageType.Success);
                    return this.Json("success");
                }
            }

            this.TempData["nextview"] = "#cancellation";
            this.ShowMessage("Failed", Enums.MessageType.Warning);
            return this.Json("success");
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="hotelId">HotelId</param>
        public async Task<IActionResult> AddCancellationPolicyRow(int hotelId)
        {
            HotelierCancellationPolicyViewModel model = new HotelierCancellationPolicyViewModel
            {
                Id = 0,
                IsDeleted = false,
                HotelId = hotelId,
                MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList()
            };
            return this.PartialView("_CancellationPolicyRow", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult Promotion(int hotelId, int vendorId)
        {
            this.ViewBag.HotelId = hotelId;
            this.ViewBag.VendorId = vendorId;
            return this.PartialView("Promotion");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="hotelId">The identifier.</param>
        /// <param name="vendorId">VendorID</param>
        /// <returns>Manage</returns>
        public ActionResult PromotionGrid(int hotelId, int vendorId)
        {
            this.ViewBag.HotelId = hotelId;
            this.ViewBag.VendorId = vendorId;
            return this.PartialView("_PromotionGrid");
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> PromotionsGridData([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int hotelId)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.hotelierServices.GetAllHotelierPromotionAsync(model, hotelId);
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">Review Identifier</param>
        /// <param name="hotelId">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> PromotionAdd(int id, int hotelId)
        {
            this.ViewBag.HotelId = hotelId;
            HotelierPromotionViewModel model = new HotelierPromotionViewModel
            {
                Id = 0,
                IsActive = true,
                IsDeleted = false,
                HotelId = hotelId
            };

            if (id > 0)
            {
                model = this.Mapper.Map<HotelierPromotionViewModel>(await this.hotelierServices.GetHotelPromotionById(id));
            }

            model.PromotionTypeItems = (await this.promotionService.GetPromotionTypeItems()).ToSelectList();
            model.MarginTypeItems = (await this.cancellationService.GetMarginTypeItems()).ToSelectList();
            return this.PartialView("_PromotionAdd", model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="promotionId">Review Identifier</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ChangePromotionActiveStatus(int promotionId)
        {
            var record = await this.hotelierServices.GetHotelPromotionById(promotionId);
            record.IsActive = !record.IsActive;
            await this.hotelierServices.UpdateHotelierPromotionAsync(record);
            return this.Json(new { Status = true });
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeletePromotion(int id)
        {
            try
            {
                var record = await this.hotelierServices.GetHotelPromotionById(id);
                record.IsDeleted = !record.IsDeleted;
                await this.hotelierServices.UpdateHotelierPromotionAsync(record);
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
        /// <param name="model">The Model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> PromotionAdd(HotelierPromotionViewModel model)
        {
            this.ViewBag.HotelId = model.HotelId;
            try
            {
                var record = this.Mapper.Map<HotelierPromotionModel>(model);
                if (model.Id > 0)
                {
                    ////Update
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.hotelierServices.UpdateHotelierPromotionAsync(record);
                    this.TempData["nextview"] = "#promotions";
                    this.ShowMessage("Successfully Updated");
                    return this.Json("update");
                }
                else
                {
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.hotelierServices.AddHotelierPromotionAsync(record);
                    this.TempData["nextview"] = "#promotions";
                    this.ShowMessage("Successfully Inserted");
                    return this.Json("success");
                }
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
        /// <returns>Manage</returns>
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var record = await this.hotelierServices.GetHotelierInfoAsync(id);
                record.IsDeleted = !record.IsDeleted;
                await this.hotelierServices.UpdateHotelierInformationAsync(record);
                this.ShowMessage(Messages.DeletedSuccessfully);
                this.TempData["nextview"] = "#rooms";
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
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            try
            {
                var record = await this.hotelierServices.GetHotelierInfoAsync(id);
                if (record == null)
                {
                    return this.NotFound();
                }

                record.IsActive = !record.IsActive;
                await this.hotelierServices.UpdateHotelierInformationAsync(record);

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

        private async Task<HotelierContentViewModel> HotelContentImageOperation(HotelierContentViewModel model)
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

        private async Task<string> UploadOnly(string folderName, IFormFile file)
        {
            ////if (file != null && file.Length > 0)
            ////{
            ////    try
            ////    {
            ////        if (!Directory.Exists(path))
            ////        {
            ////            DirectoryInfo di = Directory.CreateDirectory(path);
            ////        }

            ////        var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            ////        var filePath = Path.Combine(path, fileName);
            ////        using (var fileStream = new FileStream(filePath, FileMode.Create))
            ////        {
            ////            await file.CopyToAsync(fileStream);
            ////        }

            ////        return fileName;
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        var msg = ex.ToString();
            ////        return existingFileName;
            ////    }
            ////}

            return await this.UploadImageBlobStorage(folderName, file);
        }
    }
}