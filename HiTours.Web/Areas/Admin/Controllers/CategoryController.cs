// <copyright file="CategoryController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CategoryController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class CategoryController : AdminController
    {
        /// <summary>
        /// The category service
        /// </summary>
        private readonly ICategoryService categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="categoryService">The category service.</param>
        /// <param name="configuration">Configuration</param>
        public CategoryController(IMapper mapper, ICategoryService categoryService, IConfiguration configuration)
            : base(mapper, configuration)
        {
            this.categoryService = categoryService;
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
                var result = await this.categoryService.GetAllAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Duplicates the category.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Get Duplicate Category</returns>
        public async Task<JsonResult> IsDuplicate(string name = "", int id = 0)
        {
            return this.Json(await this.categoryService.IsDuplicateAsync(name, id));
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Manage(int? id)
        {
            var model = new CategoryViewModel();

            if (id != null && id > 0)
            {
                var record = await this.categoryService.GetByIdAsync(id ?? 0);
                model = this.Mapper.Map<CategoryViewModel>(record);
            }

            return this.View(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Details</returns>
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var record = await this.categoryService.GetByIdAsync(id);
            if (record == null)
            {
                return this.NotFound();
            }

            return this.View(this.Mapper.Map<CategoryViewModel>(record));
        }

        /// <summary>
        /// Changes the status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Change Active Status</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            var category = await this.categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            category.IsActive = !category.IsActive;
            await this.categoryService.UpdateAsync(category);

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
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(CategoryViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<CategoryModel>(model);
                if (model.ID == 0)
                {
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.categoryService.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.categoryService.UpdateAsync(record);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "category", action = "index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Record</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            var category = await this.categoryService.GetByIdAsync(id ?? 0);
            if (category == null)
            {
                return this.NotFound();
            }

            await this.categoryService.DeleteAsync(category);
            this.ShowMessage(Messages.DeletedSuccessfully);
            return this.Json("success");
        }
    }
}