// <copyright file="HotelRoomTypeController.cs" company="Luxury Travel Deals">
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
    public class HotelRoomTypeController : AdminController
    {
        private readonly IHotelRoomTypeService hoteroomType;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelRoomTypeController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="hoteroomType">Type of the hoteroom.</param>
        public HotelRoomTypeController(IConfiguration configuration, IMapper mapper, IHotelRoomTypeService hoteroomType)
            : base(mapper, configuration)
        {
            this.hoteroomType = hoteroomType;
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
                var result = await this.hoteroomType.GetAllAsync(model);

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
            var model = new PackageHotelRoomTypeViewModel();
            if (id > 0)
            {
                var record = await this.hoteroomType.GetByIdAsync(id);
                model = this.Mapper.Map<PackageHotelRoomTypeViewModel>(record);
            }

            return this.IsAjaxRequest()
                          ? this.View("_RoomType", model)
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
        public async Task<ActionResult> Manage(PackageHotelRoomTypeViewModel model, string method)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<PackageHotelRoomTypeModel>(model);
                if (model.Id == 0)
                {
                    record.SetAuditInfo(0);
                    await this.hoteroomType.InsertAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.SavedSuccessfully);
                    }
                }
                else
                {
                    record.UpdateAuditInfo(0);
                    await this.hoteroomType.UpdateAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.UpdateSuccessfully);
                    }
                }

                if (method == "ajax")
                {
                    return this.Json(new { success = true, Name = model.Name, Id = record.Id });
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "hotelroomtype", action = "index", area = Constants.AreaAdmin });
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
            return this.Json(await this.hoteroomType.IsDuplicateAsync(name, id));
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
                var hotelroomtype = await this.hoteroomType.GetByIdAsync(id ?? 0);
                if (hotelroomtype == null)
                {
                    return this.NotFound();
                }

                await this.hoteroomType.DeleteAsync(hotelroomtype);
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
            var category = await this.hoteroomType.GetByIdAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            category.IsActive = !category.IsActive;
            await this.hoteroomType.UpdateAsync(category);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "hotelroomtye", action = "index", area = Constants.AreaAdmin });
            }
        }
    }
}
