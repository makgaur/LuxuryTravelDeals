// <copyright file="BlogController.cs" company="Luxury Travel Deals">
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
    public class BlogController : AdminController
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly IBlogService blogService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController" /> class.
        /// </summary>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="blogService">Blog Service</param>
        /// <param name="configuration">Configuration</param>
        public BlogController(IHostingEnvironment hostingEnvironment, IMasterService masterService, IMapper mapper, IBlogService blogService, IConfiguration configuration)
            : base(mapper, configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.masterService = masterService;
            this.blogService = blogService;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">Model Data Table</param>
        /// <param name="mode">View Mode</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, string mode)
        {
            this.ViewBag.Mode = mode;
            if (this.IsAjaxRequest())
            {
                var result = await this.blogService.GetAllBlogsAsync(model, mode);
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="id">Visa Id</param>
        /// <param name="mode">Curation Mode</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Manage(int? id, string mode)
        {
            this.ViewBag.Mode = mode;
            BlogPostAddViewModel model = new BlogPostAddViewModel();
            if (mode == "feature")
            {
                model.IsFeatured = true;
            }

            if (mode == "sub")
            {
                model.IsFeatured = false;
            }

            if (id > 0)
            {
                model = this.Mapper.Map<BlogPostAddViewModel>(await this.blogService.GetBlogsByIdAsync(Convert.ToInt32(id)));
            }
            else
            {
                model.Id = 0;
                model.IsActive = true;
            }

            model.Mode = mode;
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
        public async Task<IActionResult> Manage(BlogPostAddViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var data = this.Mapper.Map<BlogPostsModel>(model);
                if (model.Id == 0)
                {
                    if (model.ImageFile != null)
                    {
                        model = await this.UploadOnly("Blogs", model);
                    }

                    var record = this.Mapper.Map<BlogPostsModel>(model);
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.blogService.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    if (model.ImageFile != null)
                    {
                        model = await this.UploadOnly("Blogs", model);
                        data.Image = model.Image;
                    }

                    data.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.blogService.UpdateAsync(data);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToAction("index", "blog", new { @mode=model.Mode });
            }

            return this.View(model);
        }

        private async Task<BlogPostAddViewModel> UploadOnly(string folder, BlogPostAddViewModel model)
        {
            model.Image = await this.UploadImageBlobStorage(folder, model.ImageFile);
            return model;
        }
    }
}
