// <copyright file="HomeBannersController.cs" company="Luxury Travel Deals">
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
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// UploadBannersController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeBannersController : AdminController
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        private readonly IHomeBannerService homeBanner;
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeBannersController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="homeBanner">The home banner.</param>
        public HomeBannersController(IConfiguration configuration, IMasterService masterService, IMapper mapper, IHostingEnvironment hostingEnvironment, IHomeBannerService homeBanner)
           : base(mapper, configuration)
        {
            this.masterService = masterService;
            this.hostingEnvironment = hostingEnvironment;
            this.homeBanner = homeBanner;
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
                var result = await this.homeBanner.GetAllAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> PopularDestination([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.homeBanner.GetAllPopularDestinationAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// view
        /// </returns>
        public async Task<IActionResult> PopularDestinationAdd(int id)
        {
            var model = new PopularDestinationViewModel();
            if (id > 0)
            {
                PopularDestinationModel data = await this.homeBanner.GetPopularDestinationByIdAsync(id);
                var result = this.Mapper.Map<PopularDestinationViewModel>(data);
                result.CountryItems = result.CountryId.HasValue && result.CountryId != 0 ? (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, result.CountryId.Value)).ToSelectList() : new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                result.StateItems = result.StateId.HasValue && result.StateId != 0 ? (await this.masterService.GetPackageStateListAsync(string.Empty, 1, result.StateId.Value)).ToSelectList() : new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                result.CityItems = result.CityId.HasValue && result.CityId != 0 ? (await this.masterService.GetPackageCityListAsync(string.Empty, 1, result.CityId.Value, 0)).ToSelectList() : new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                return this.View(result);
            }

            return this.View(model);
        }

        /// <summary>
        /// Uploads the images.
        /// </summary>
        /// <param name="model">The files.</param>
        /// <returns>
        /// Imaghes
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PopularDestinationAdd(PopularDestinationViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var data = this.Mapper.Map<PopularDestinationModel>(model);
                if (model.Id == 0)
                {
                    var record = this.Mapper.Map<PopularDestinationModel>(model);
                    if (model.ImageFile != null)
                    {
                        record.Image = await this.UploadOnly(model.ImageFile, "BannerImages");
                    }

                    if (record.CountryId.HasValue)
                    {
                        record.CountryName = (await this.masterService.GetPackageCountryByIdAsync(record.CountryId.Value)).Name;
                    }

                    if (record.StateId.HasValue)
                    {
                        record.StateName = (await this.masterService.GetPackageStateByIdAsync(record.StateId.Value)).Name;
                    }

                    if (record.CityId.HasValue)
                    {
                        record.CityName = (await this.masterService.GetPackageCityByIdAsync(Convert.ToInt16(record.CityId.Value))).Name;
                    }

                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.homeBanner.InsertPopularDestinationAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    if (model.ImageFile != null)
                    {
                        data.Image = await this.UploadOnly(model.ImageFile, "BannerImages");
                    }

                    if (data.CountryId.HasValue)
                    {
                        data.CountryName = (await this.masterService.GetPackageCountryByIdAsync(data.CountryId.Value)).Name;
                    }

                    if (data.StateId.HasValue)
                    {
                        data.StateName = (await this.masterService.GetPackageStateByIdAsync(data.StateId.Value)).Name;
                    }

                    if (data.CityId.HasValue)
                    {
                        data.CityName = (await this.masterService.GetPackageCityByIdAsync(Convert.ToInt16(data.CityId.Value))).Name;
                    }

                    data.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.homeBanner.UpdatePopularDestinationAsync(data);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToAction("PopularDestination", "HomeBanners");
            }

            return this.View(model);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// view
        /// </returns>
        public async Task<IActionResult> UploadImages(int id)
        {
            var model = new HomeBannerViewModel();
            if (id > 0)
            {
                var data = await this.homeBanner.GetByIdAsync(id);
                var result = this.Mapper.Map<HomeBannerViewModel>(data);
               return this.View(result);
            }

            return this.View();
        }

        /// <summary>
        /// Uploads the images.
        /// </summary>
        /// <param name="model">The files.</param>
        /// <returns>
        /// Imaghes
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> UploadImages(HomeBannerViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    if (model.ImageFile != null)
                    {
                        model.ImageName = await this.UploadOnly(model.ImageFile, "BannerImages");
                    }

                    if (model.ImageNameMobileLaptopFile != null)
                    {
                        model.ImageNameMobileLaptop = await this.UploadOnly(model.ImageNameMobileLaptopFile, "BannerImages");
                    }

                    if (model.ImageNameMobileLFile != null)
                    {
                        model.ImageNameMobileL = await this.UploadOnly(model.ImageNameMobileLFile, "BannerImages");
                    }

                    if (model.ImageNameMobileMFile != null)
                    {
                        model.ImageNameMobileM = await this.UploadOnly(model.ImageNameMobileMFile, "BannerImages");
                    }

                    if (model.ImageNameMobileSFile != null)
                    {
                        model.ImageNameMobileS = await this.UploadOnly(model.ImageNameMobileSFile, "BannerImages");
                    }

                    if (model.ImageNameMobileTFile != null)
                    {
                        model.ImageNameMobileT = await this.UploadOnly(model.ImageNameMobileTFile, "BannerImages");
                    }

                    var record = this.Mapper.Map<HomeBannerModel>(model);
                    await this.homeBanner.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    if (model.ImageFile != null)
                    {
                        model.ImageName = await this.UploadOnly(model.ImageFile,  "BannerImages");
                    }

                    if (model.ImageNameMobileLaptopFile != null)
                    {
                        model.ImageNameMobileLaptop = await this.UploadOnly(model.ImageNameMobileLaptopFile, "BannerImages");
                    }

                    if (model.ImageNameMobileLFile != null)
                    {
                        model.ImageNameMobileL = await this.UploadOnly(model.ImageNameMobileLFile, "BannerImages");
                    }

                    if (model.ImageNameMobileMFile != null)
                    {
                        model.ImageNameMobileM = await this.UploadOnly(model.ImageNameMobileMFile, "BannerImages");
                    }

                    if (model.ImageNameMobileSFile != null)
                    {
                        model.ImageNameMobileS = await this.UploadOnly(model.ImageNameMobileSFile, "BannerImages");
                    }

                    if (model.ImageNameMobileTFile != null)
                    {
                        model.ImageNameMobileT = await this.UploadOnly(model.ImageNameMobileTFile, "BannerImages");
                    }

                    var data = this.Mapper.Map<HomeBannerModel>(model);
                    await this.homeBanner.UpdateAsync(data);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "homebanners", action = "index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }

        /// <summary>
        /// Deletes the banners.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="imagename">Name of the image.</param>
        /// <returns>
        /// Image
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DeleteBanners(int id, string imagename)
        {
            ////var wwwrootPath = this.hostingEnvironment.WebRootPath;
            ////var bannerName = string.Empty;
            ////string fullPath = Path.Combine(wwwrootPath, "BannerImages", imagename);
            ////try
            ////{
            ////    if (System.IO.File.Exists(fullPath))
            ////    {
            ////        System.IO.File.Delete(fullPath);
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    var msg = ex.ToString();
            ////}

            var recored = await this.homeBanner.GetByIdAsync(id);
            await this.homeBanner.DeleteAsync(recored);

            return this.Json(Enums.MessageType.Success);
        }

        /// <summary>
        /// Deletes the banners.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="imagename">Name of the image.</param>
        /// <returns>
        /// Image
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DeletePopularDestination(int id, string imagename)
        {
            ////var wwwrootPath = this.hostingEnvironment.WebRootPath;
            ////var bannerName = string.Empty;
            ////string fullPath = Path.Combine(wwwrootPath, "BannerImages", imagename);
            ////try
            ////{
            ////    if (System.IO.File.Exists(fullPath))
            ////    {
            ////        System.IO.File.Delete(fullPath);
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    var msg = ex.ToString();
            ////}

            var recored = await this.homeBanner.GetPopularDestinationByIdAsync(id);
            await this.homeBanner.DeletePopularDestinationAsync(recored);

            return this.Json(Enums.MessageType.Success);
        }

        private FileInfo[] GetAllFiles()
        {
            FileInfo[] files;
            var imagesLink = new List<string>();
            var wwwrootPath = this.hostingEnvironment.WebRootPath;
            var path = wwwrootPath + "/BannerImages";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            files = directoryInfo.GetFiles();
            return files;
        }

        private async Task<string> UploadOnly(IFormFile files, string folder)
        {
            ////try
            ////{
            ////    if (files != null && files.Length > 0)
            ////    {
            ////        var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(files.FileName)}";
            ////        if (Directory.Exists(path))
            ////        {
            ////            DirectoryInfo di = Directory.CreateDirectory(path);
            ////            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            ////            {
            ////                await files.CopyToAsync(fileStream);
            ////            }
            ////        }

            ////        return fileName;
            ////    }

            ////    return string.Empty;
            ////}
            ////catch (Exception ex)
            ////{
            ////    var msg = ex.ToString();
            ////    return string.Empty;
            ////}
            return await this.UploadImageBlobStorage(folder, files);
        }
    }
}