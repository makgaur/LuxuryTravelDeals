// <copyright file="BookingController.cs" company="Luxury Travel Deals">
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
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// StateController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class BookingController : AdminController
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly IBookingService bookingService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingController" /> class.
        /// </summary>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="bookingService">Booking Service</param>
        /// <param name="configuration">Configuration</param>
        public BookingController(IHostingEnvironment hostingEnvironment, IMasterService masterService, IMapper mapper, IBookingService bookingService, IConfiguration configuration)
            : base(mapper, configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.masterService = masterService;
            this.bookingService = bookingService;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">Model Data Table</param>
        /// <param name="type">View Mode</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int type)
        {
            this.ViewBag.Type = type;
            if (this.IsAjaxRequest())
            {
                var result = await this.bookingService.GetAllBookingsByType(model, type);
                return this.Json(result);
            }

            return this.View();
        }

        /////// <summary>
        /////// Indexes the specified model.
        /////// </summary>
        /////// <param name="id">Visa Id</param>
        /////// <param name="mode">Curation Mode</param>
        /////// <returns>View</returns>
        ////public async Task<IActionResult> Manage(int? id, string mode)
        ////{
        ////    this.ViewBag.Mode = mode;
        ////    BlogPostAddViewModel model = new BlogPostAddViewModel();
        ////    if (mode == "feature")
        ////    {
        ////        model.IsFeatured = true;
        ////    }

        ////    if (mode == "sub")
        ////    {
        ////        model.IsFeatured = false;
        ////    }

        ////    if (id > 0)
        ////    {
        ////        model = this.Mapper.Map<BlogPostAddViewModel>(await this.blogService.GetBlogsByIdAsync(Convert.ToInt32(id)));
        ////    }
        ////    else
        ////    {
        ////        model.Id = 0;
        ////        model.IsActive = true;
        ////    }

        ////    model.Mode = mode;
        ////    return this.View(model);
        ////}

        /////// <summary>
        /////// Uploads the images.
        /////// </summary>
        /////// <param name="model">The files.</param>
        /////// <returns>
        /////// Imaghes
        /////// </returns>
        ////[HttpPost]
        ////public async Task<IActionResult> Manage(BlogPostAddViewModel model)
        ////{
        ////    if (this.ModelState.IsValid)
        ////    {
        ////        var data = this.Mapper.Map<BlogPostsModel>(model);
        ////        if (model.Id == 0)
        ////        {
        ////            if (model.ImageFile != null)
        ////            {
        ////                model = await this.UploadOnly(Path.Combine(this.hostingEnvironment.WebRootPath, "Blogs"), model);
        ////            }

        ////            var record = this.Mapper.Map<BlogPostsModel>(model);
        ////            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
        ////            await this.blogService.InsertAsync(record);
        ////            this.ShowMessage(Messages.SavedSuccessfully);
        ////        }
        ////        else
        ////        {
        ////            if (model.ImageFile != null)
        ////            {
        ////                model = await this.UploadOnly(Path.Combine(this.hostingEnvironment.WebRootPath, "Blogs"), model);
        ////                data.Image = model.Image;
        ////            }

        ////            data.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
        ////            await this.blogService.UpdateAsync(data);
        ////            this.ShowMessage(Messages.UpdateSuccessfully);
        ////        }

        ////        return this.RedirectToAction("index", "blog", new { @mode=model.Mode });
        ////    }

        ////    return this.View(model);
        ////}

        ////private async Task<BlogPostAddViewModel> UploadOnly(string path, BlogPostAddViewModel model)
        ////{
        ////    if (model.ImageFile != null && model.ImageFile.Length > 0)
        ////    {
        ////        try
        ////        {
        ////            if (!Directory.Exists(path))
        ////            {
        ////                DirectoryInfo di = Directory.CreateDirectory(path);
        ////            }

        ////            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.ImageFile.FileName)}";
        ////            var filePath = Path.Combine(path, fileName);
        ////            using (var fileStream = new FileStream(filePath, FileMode.Create))
        ////            {
        ////                await model.ImageFile.CopyToAsync(fileStream);
        ////            }

        ////            model.Image = fileName;
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            var msg = ex.ToString();
        ////        }
        ////    }

        ////    return model;
        ////}
    }
}
