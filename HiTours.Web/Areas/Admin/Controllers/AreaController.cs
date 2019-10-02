// <copyright file="AreaController.cs" company="Luxury Travel Deals">
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
    public class AreaController : AdminController
    {
        private readonly IAreaService areaService;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaController" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="areaService">The Area Service.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="configuration">Configuration</param>
        public AreaController(IMapper mapper, IAreaService areaService, IMasterService masterService, IConfiguration configuration)
            : base(mapper, configuration)
        {
            this.areaService = areaService;
            this.masterService = masterService;
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
                var result = await this.areaService.GetAllAsync(model);

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
            var model = new PackageAreaViewModel { IsActive = true };
            if (id > 0)
            {
                var record = await this.areaService.GetByIdAsync(id);
                //// model.States = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, model.CountryId, model.CountryId.ToString())).ToSelectList();
                model = this.Mapper.Map<PackageAreaViewModel>(record);
                model.Countries = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, record.Country)).ToSelectList();
                model.States = (await this.masterService.GetPackageStateListAsync(string.Empty, 1, record.State)).ToSelectList();
                model.Cities = (await this.masterService.GetPackageCityListAsync(string.Empty, 1, record.City, 0)).ToSelectList();
            }

            if (this.IsAjaxRequest())
            {
                return this.View("_area", model);
            }

            return this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageAreaViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<PackageAreaModel>(model);
                if (model.Id == 0)
                {
                    await this.areaService.InsertAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.SavedSuccessfully);
                    }
                }
                else
                {
                    await this.areaService.UpdateAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.UpdateSuccessfully);
                    }
                }

                if (this.IsAjaxRequest())
                {
                    return this.Json(new { success = true, Name = model.Name, Id = record.Id.ToString() });
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "area", action = "index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }

        /////// <summary>
        /////// Duplicates the category.
        /////// </summary>
        /////// <param name="name">The name.</param>
        /////// <param name="id">The identifier.</param>
        /////// <returns>Get Duplicate Category</returns>
        ////public async Task<JsonResult> IsDuplicate(string name = "", int id = 0)
        ////{
        ////    return this.Json(await this.city.IsDuplicateAsync(name, id));
        ////}

        /////// <summary>
        /////// Duplicates the category.
        /////// </summary>
        /////// <param name="code">The code.</param>
        /////// <param name="id">The identifier.</param>
        /////// <returns>
        /////// Get Duplicate Category
        /////// </returns>
        ////public async Task<JsonResult> IsDuplicateCityCode(string code = "", int id = 0)
        ////{
        ////    return this.Json(await this.city.IsDuplicateCityCodeAsync(code, id));
        ////}

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
                var city = await this.areaService.GetByIdAsync(id ?? 0);
                if (city == null)
                {
                    return this.NotFound();
                }

                await this.areaService.DeleteAsync(city);
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

        /// <summary>
        /// Changes the status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Change Active Status</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            var city = await this.areaService.GetByIdAsync(id);
            if (city == null)
            {
                return this.NotFound();
            }

            city.IsActive = !city.IsActive;
            await this.areaService.UpdateAsync(city);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Area", action = "index", area = Constants.AreaAdmin });
            }
        }
    }
}
