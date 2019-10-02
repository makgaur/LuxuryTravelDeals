// <copyright file="HolidayMenuController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Holiday Menu Controller
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class HolidayMenuController : AdminController
    {
        private readonly IHolidayMenuService holidayService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayMenuController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="holidayService">The holiday service.</param>
        public HolidayMenuController(IConfiguration configuration, IMapper mapper, IHolidayMenuService holidayService)
            : base(mapper, configuration)
        {
            this.holidayService = holidayService;
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
                var result = await this.holidayService.GetAllAsync(model);

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
            var model = new PackageHolidayMenuViewModel();
            if (id > 0)
            {
                var record = await this.holidayService.GetByIdAsync(id);
                model = this.Mapper.Map<PackageHolidayMenuViewModel>(record);
                model.MenuList = model.ChildMenu.Split(",");
                model.ChildMenuList = model.MenuList.Select(x => new SelectListItem
                {
                    Text = x,
                    Value = x,
                });

                model.NameList = new List<SelectListItem> { new SelectListItem { Text = model.Name, Value = model.Name } };
            }

            return this.IsAjaxRequest()
               ? this.View("_holidayService", model)
               : this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Manage
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageHolidayMenuViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var exsts = await this.holidayService.IsDuplicateAsync(model.Name, model.Id);
                if (exsts)
                {
                    model.ChildMenu = string.Join(",", model.MenuList);
                    var record = this.Mapper.Map<PackageHolidayMenuModel>(model);
                    if (model.Id == 0)
                    {
                        record.SetAuditInfo(0);
                        await this.holidayService.InsertAsync(record);
                        this.ShowMessage(Messages.SavedSuccessfully);
                    }
                    else
                    {
                        record.UpdateAuditInfo(0);
                        await this.holidayService.UpdateAsync(record);
                        this.ShowMessage(Messages.UpdateSuccessfully);
                    }

                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "holidaymenu", action = "index", area = Constants.AreaAdmin });
                }
                else
                {
                    model.ChildMenuList = model.MenuList.Select(x => new SelectListItem
                    {
                        Text = x,
                        Value = x,
                    });

                    model.NameList = new List<SelectListItem> { new SelectListItem { Text = model.Name, Value = model.Name } };
                    this.ModelState.AddModelError("Name", "Already Exists");
                }
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
            return this.Json(await this.holidayService.IsDuplicateAsync(name, id));
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
                var category = await this.holidayService.GetByIdAsync(id ?? 0);
                if (category == null)
                {
                    return this.NotFound();
                }

                await this.holidayService.DeleteAsync(category);
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