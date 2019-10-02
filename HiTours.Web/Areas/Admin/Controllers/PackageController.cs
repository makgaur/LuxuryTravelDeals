// <copyright file="PackageController.cs" company="Luxury Travel Deals">
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
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Package controller
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class PackageController : AdminController
    {
        /// <summary>
        /// The package service
        /// </summary>
        private readonly IPackageService packageService;

        /// <summary>
        /// The package image service
        /// </summary>
        private readonly IPackageImageService packageImageService;

        /// <summary>
        /// The environment
        /// </summary>
        private readonly IHostingEnvironment environment;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// The hotel booking service
        /// </summary>
        private readonly IHotelBookingService hotelBookingService;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="packageService">The package service.</param>
        /// <param name="packageImageService">The package image service.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="hotelBookingService">The hotel booking service.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public PackageController(IConfiguration configuration, IMapper mapper, IPackageService packageService, IPackageImageService packageImageService, IMasterService masterService, IHostingEnvironment environment, IHotelBookingService hotelBookingService, IHostingEnvironment hostingEnvironment)
            : base(mapper, configuration)
        {
            this.packageService = packageService;
            this.packageImageService = packageImageService;
            this.masterService = masterService;
            this.environment = environment;
            this.hotelBookingService = hotelBookingService;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// index view
        /// </returns>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.packageService.GetAllAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Gets the hotel types.
        /// </summary>
        /// <param name="hotelvalidiytid">The hotelid.</param>
        /// <returns>
        /// Get Hotel Types
        /// </returns>
        public async Task<PartialViewResult> GetRoomTypePrice(Guid hotelvalidiytid)
        {
            var hotelRoomTypes = await this.hotelBookingService.GetHotelValidityRoomTypesAsync(hotelvalidiytid);
            return this.PartialView("_HotelRoomTypes", hotelRoomTypes);
        }

        /// <summary>
        /// Gets the price row.
        /// </summary>
        /// <returns>GetPriceRow</returns>
        public PartialViewResult GetPriceRow()
        {
            return this.PartialView("_SpecificPrices", new SpecificPriceViewModel());
        }

        /// <summary>
        /// Manages this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// manage view for create and update
        /// </returns>
        public async Task<IActionResult> Manage(Guid id)
        {
            var model = new PackageViewModel();

            if (id != Guid.Empty)
            {
                var result = await this.packageService.GetByIdAsync(id);
                model = this.Mapper.Map<PackageModel, PackageViewModel>(result);

                model.HotelRoomTypes = await this.hotelBookingService.GetHotelValidityRoomTypesAsync(model.HotelValidityId);

                model.SpecificPriceList = await this.packageService.GetSpecificDatePriceAsync(model.Id);

                await this.BindSelectList(model);
            }

            this.GenrateNights();

            return this.View(model);
        }

        /// <summary>
        /// Duplicates the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        /// Get Duplicate Category
        /// </returns>
        public async Task<JsonResult> IsDuplicate(Guid id, string name)
        {
            return this.Json(await this.packageService.IsDuplicateAsync(name, id));
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Details</returns>
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var record = await this.packageService.GetByIdAsync(id);
            if (record == null)
            {
                return this.NotFound();
            }

            this.GenrateNights();

            var model = this.Mapper.Map<PackageViewModel>(record);

            await this.BindSelectList(model);
            return this.View(model);
        }

        /// <summary>
        /// Changes the active status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChangeActiveStatus</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(Guid id)
        {
            var category = await this.packageService.GetByIdAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            category.IsActive = !category.IsActive;
            await this.packageService.UpdateAsync(category);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "category", action = "index", area = Constants.AreaAdmin });
            }
        }

        /// <summary>
        /// Uploads the images.
        /// </summary>
        /// <returns> UploadImages </returns>
        public IActionResult UploadImages()
        {
            return this.View(this.GetAllFiles());
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.ValidTo = model.ValidTo.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                var record = this.Mapper.Map<PackageModel>(model);
                ////record.Nights = string.Join(",", model.NightList);
                if (model.Id == Guid.Empty)
                {
                    record.Prefix = Constants.PrefixPackageDeal;
                    record.DealCode = (await this.packageService.GetMaxDealCodeAsync()) + 1;
                    record.Postfix = Constants.PostFixPackageDeal;
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.packageService.InsertAsync(record);
                    await this.UploadPackageImages(model.Files, record.Id);

                    await this.AddSpecificPriceList(model, record.Id);

                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    if (model.CountryId != Guid.Empty && model.CityId != Guid.Empty && model.HotelValidityId != Guid.Empty && model.HotelId != Guid.Empty)
                    {
                        record.Postfix = record.Postfix ?? string.Empty;
                        record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.UploadPackageImages(model.Files, record.Id);
                        await this.packageService.UpdateAsync(record);
                        await this.packageService.RemoveSpecificPriceByPackageAsync(record.Id);
                        await this.AddSpecificPriceList(model, record.Id);

                        this.ShowMessage(Messages.UpdateSuccessfully);
                    }
                    else
                    {
                        this.ShowMessage("Something is missing, Please try again.", Enums.MessageType.Error);
                        this.GenrateNights();
                        await this.BindSelectList(model);
                        return this.View(model);
                    }
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "package", action = "index", area = Constants.AreaAdmin });
            }

            this.GenrateNights();
            await this.BindSelectList(model);

            return this.View(model);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Record</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await this.packageService.GetByIdAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            await this.packageService.DeleteAsync(category);
            this.ShowMessage(Messages.DeletedSuccessfully);
            return this.Json("success");
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Record</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteImage(string id)
        {
            var wwwrootPath = this.hostingEnvironment.WebRootPath;
            var ids = id.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in ids)
            {
                Guid imageid;
                if (Guid.TryParse(item, out imageid))
                {
                    var packageImage = await this.packageImageService.GetByIdAsync(imageid);
                    if (packageImage == null)
                    {
                        return this.NotFound();
                    }

                    await this.packageImageService.DeleteAsync(packageImage);

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
        /// Udpates the order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="index">The index.</param>
        /// <returns>UdpateOrder</returns>
        [HttpPost]
        public async Task<IActionResult> UdpateOrder(Guid id, short index)
        {
            var result = await this.packageImageService.GetByIdAsync(id);
            result.SequenceNo = index;
            await this.packageImageService.UpdateAsync(result);
            return this.Json(new { Status = true });
        }

        /// <summary>
        /// Uploads the images.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns>
        /// Upload Multiple Images
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> UploadImages(IList<IFormFile> files)
        {
            if (files.Count > 0)
            {
                await this.UploadOnly(files, Path.Combine(this.environment.WebRootPath, "HotelImages"));
                this.ShowMessage(Messages.SavedSuccessfully);
            }

            return this.View(this.GetAllFiles());
        }

        /// <summary>
        /// Tests this instance.
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Test()
        {
            return this.View();
        }

        /// <summary>
        /// Binds the select list.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Bind SelectList</returns>
        private async Task BindSelectList(PackageViewModel model)
        {
            model.Categories = (await this.masterService.GetCategorySelectListAsync(string.Empty, 1, model.CategoryId.ToString())).ToSelectList();
            model.DealType = (await this.masterService.GetDealTypeSelectListAsync(string.Empty, 1, model.DealTypeId.ToString())).ToSelectList();
            model.Countries = (await this.masterService.GetCountrySelectListAsync(string.Empty, 1, model.CountryId.ToString())).ToSelectList();
            model.Cities = (await this.masterService.GetCitySelectListAsync(string.Empty, 1, model.CountryId, model.CityId.ToString())).ToSelectList();
            model.Hotels = (await this.masterService.GetHoteSelectListAsync(string.Empty, 0, model.CityId, model.HotelId.ToString())).ToSelectList();
            model.HotelValidities = (await this.masterService.GetHotelValditySelectListAsync(string.Empty, 0, model.HotelId, model.HotelValidityId.ToString())).ToSelectList();
        }

        /// <summary>
        /// Uploads the package images.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="packageid">The packageid.</param>
        /// <returns>UploadPackageImages</returns>
        private async Task UploadPackageImages(IList<IFormFile> files, Guid packageid)
        {
            if (files != null && files.Count > 0 && packageid != Guid.Empty)
            {
                var uploads = Path.Combine(this.environment.WebRootPath, "packages");
                if (!Directory.Exists(uploads))
                {
                    DirectoryInfo di = Directory.CreateDirectory(uploads);
                }

                var packageImages = new List<PackageImageModel>();
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(uploads, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            var pacakgeImage = new PackageImageModel() { PackageId = packageid, ImageName = fileName };
                            ////pacakgeImage.SetAuditInfo(Guid.Empty.ToString());
                            packageImages.Add(pacakgeImage);
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }

                await this.packageImageService.InsertAsync(packageImages);
            }
        }

        /// <summary>
        /// Uploads the images.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="path">The path.</param>
        /// <returns>
        /// UploadImages
        /// </returns>
        private async Task UploadOnly(IList<IFormFile> files, string path)
        {
            if (files != null && files.Count > 0)
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
            }
        }

        private void GenrateNights()
        {
            var nights = new List<SelectListItem>();

            for (int i = 1; i < 21; i++)
            {
                nights.Add(new SelectListItem() { Value = i.ToString(), Text = $"{i} Nights" });
            }

            this.ViewBag.Nights = nights;
        }

        private FileInfo[] GetAllFiles()
        {
            FileInfo[] files;
            var imagesLink = new List<string>();
            var wwwrootPath = this.hostingEnvironment.WebRootPath;
            var path = wwwrootPath + "/HotelImages";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            files = directoryInfo.GetFiles();
            return files;
        }

        private async Task AddSpecificPriceList(PackageViewModel model, Guid packageid)
        {
            var specificPriceList = this.Mapper.Map<List<SpecificPriceModel>>(model.SpecificPriceList);
            if (specificPriceList != null && specificPriceList.Count > 0)
            {
                foreach (var item in specificPriceList)
                {
                    item.PackageId = packageid;
                }

                await this.packageService.InsertSpecificPriceAsync(specificPriceList);
            }
        }
    }
}