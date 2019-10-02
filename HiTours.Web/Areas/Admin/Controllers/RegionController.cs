// <copyright file="RegionController.cs" company="Luxury Travel Deals">
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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CityController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class RegionController : AdminController
    {
        private readonly IRegionService region;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionController" /> class.
        /// </summary>
        /// <param name="configuration">Config</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="region">The travelstyle.</param>
        public RegionController(IConfiguration configuration, IMapper mapper, IRegionService region)
            : base(mapper, configuration)
        {
            this.region = region;
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
                var result = await this.region.GetAllAsync(model);

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
            var model = new PackageRegionViewModel();
            if (id > 0)
            {
                var record = await this.region.GetByIdAsync(id);
                model = this.Mapper.Map<PackageRegionViewModel>(record);
            }

            return this.IsAjaxRequest()
                          ? this.View("_Region", model)
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
        public async Task<ActionResult> Manage(PackageRegionViewModel model, string method)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<PackageRegionModel>(model);
                if (model.Id == 0)
                {
                    record.SetAuditInfo(0);
                    await this.region.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    record.UpdateAuditInfo(0);
                    await this.region.UpdateAsync(record);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                if (method == "ajax")
                {
                    return this.Json(new { success = true, Name = model.Name, Id = record.Id });
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "region", action = "index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }

        /// <summary>
        /// Duplicates the category.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Get Duplicate Category</returns>
        public async Task<JsonResult> IsDuplicate(string name = "", int id = 0)
        {
            return this.Json(await this.region.IsDuplicateAsync(name, id));
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Record</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                var category = await this.region.GetByIdAsync(id ?? 0);
                if (category == null)
                {
                    return this.NotFound();
                }

                await this.region.DeleteAsync(category);
                this.ShowMessage(Messages.DeletedSuccessfully);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                int i = ((System.Data.SqlClient.SqlException)ex.InnerException).Number;
                if (i == 547)
                {
                    this.ShowMessage("This data has been already used.");
                    return this.Json("Failier");
                }
            }

            return this.Json("Success");
        }
    }
}
