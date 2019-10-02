// <copyright file="HotelCategoryController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CityController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class HotelCategoryController : AdminController
    {
        private readonly IHotelCategoryService hotelcategory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelCategoryController" /> class.
        /// </summary>
        /// <param name="configuration">Configuratipn</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="hotelcategory">The travelstyle.</param>
        public HotelCategoryController(IConfiguration configuration, IMapper mapper, IHotelCategoryService hotelcategory)
            : base(mapper, configuration)
        {
            this.hotelcategory = hotelcategory;
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
                var result = await this.hotelcategory.GetAllAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Manage(int id)
        {
            var model = new PackageHotelCategoryViewModel();
            if (id > 0)
            {
                var record = await this.hotelcategory.GetByIdAsync(id);
                model = this.Mapper.Map<PackageHotelCategoryViewModel>(record);
            }

            return this.IsAjaxRequest()
                ? this.View("_HotelCategory", model)
                : this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="method">The method.</param>
        /// <returns>
        /// Manage
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageHotelCategoryViewModel model, string method)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<PackageHotelCategoryModel>(model);
                if (model.Id == 0)
                {
                    record.SetAuditInfo(0);
                    await this.hotelcategory.InsertAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.SavedSuccessfully);
                    }
                }
                else
                {
                    record.UpdateAuditInfo(0);
                    await this.hotelcategory.UpdateAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.UpdateSuccessfully);
                    }
                }

                if (method == "ajax")
                {
                    return this.Json(new { success = true, Name = model.Name, Id = record.Id.ToString(), Description = model.Description });
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "hotelcategory", action = "index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }

        /// <summary>
        /// Changes the status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Change Active Status</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            var category = await this.hotelcategory.GetByIdAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            category.IsActive = !category.IsActive;
            await this.hotelcategory.UpdateAsync(category);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "hotelcategory", action = "index", area = Constants.AreaAdmin });
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Record</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            var category = await this.hotelcategory.GetByIdAsync(id ?? 0);
            if (category == null)
            {
                return this.NotFound();
            }

            await this.hotelcategory.DeleteAsync(category);
            this.ShowMessage(Messages.DeletedSuccessfully);
            return this.Json("success");
        }

        /// <summary>
        /// Duplicates the category.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Get Duplicate Category</returns>
        public async Task<JsonResult> IsDuplicate(string name = "", int id = 0)
        {
            return this.Json(await this.hotelcategory.IsDuplicateAsync(name, id));
        }
    }
}