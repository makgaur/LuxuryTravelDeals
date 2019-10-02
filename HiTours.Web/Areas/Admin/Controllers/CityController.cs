// <copyright file="CityController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CityController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class CityController : AdminController
    {
        private readonly ICityService city;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="hostingEnvironment">HOsting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="city">The country.</param>
        /// <param name="masterService">The master service.</param>
        public CityController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IMapper mapper, ICityService city, IMasterService masterService)
            : base(mapper, configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.city = city;
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
                var result = await this.city.GetAllAsync(model);

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
            var model = new PackageCityViewModel();
            if (id > 0)
            {
                var record = await this.city.GetByIdAsync(id);
                //// model.States = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, model.CountryId, model.CountryId.ToString())).ToSelectList();
                model = this.Mapper.Map<PackageCityViewModel>(record);
                model.Coutries = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, record.CountryId)).ToSelectList();
                model.States = (await this.masterService.GetPackageStateListAsync(string.Empty, 1, record.StateId)).ToSelectList();
            }

            if (this.IsAjaxRequest())
            {
                return this.View("_city", model);
            }

            return this.View(model);
           ////return this.IsAjaxRequest()
           ////   ? this.View("_city", model)
           ////   : this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageCityViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<PackageCityModel>(model);
                if (model.Id == 0)
                {
                    if (model.ImageFile != null)
                    {
                        model = await this.UploadOnly("images/City", model);
                    }

                    record.Image = model.Image;
                    record.SetAuditInfo(0);
                    await this.city.InsertAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.SavedSuccessfully);
                    }
                }
                else
                {
                    if (model.ImageFile != null)
                    {
                        model = await this.UploadOnly("images/City", model);
                    }

                    record.Image = model.Image;
                    record.UpdateAuditInfo(0);
                    await this.city.UpdateAsync(record);
                    if (!this.IsAjaxRequest())
                    {
                        this.ShowMessage(Messages.UpdateSuccessfully);
                    }
                }

                if (this.IsAjaxRequest())
                {
                    return this.Json(new { success = true, Name = model.Name, Id = record.Id.ToString(), CityDescription=model.CityDescription });
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "city", action = "index", area = Constants.AreaAdmin });
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
            return this.Json(await this.city.IsDuplicateAsync(name, id));
        }

        /// <summary>
        /// Duplicates the category.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Get Duplicate Category
        /// </returns>
        public async Task<JsonResult> IsDuplicateCityCode(string code = "", int id = 0)
        {
            return this.Json(await this.city.IsDuplicateCityCodeAsync(code, id));
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
                var city = await this.city.GetByIdAsync(id ?? 0);
                if (city == null)
                {
                    return this.NotFound();
                }

                await this.city.DeleteAsync(city);
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
            var city = await this.city.GetByIdAsync(id);
            if (city == null)
            {
                return this.NotFound();
            }

            city.IsActive = !city.IsActive;
            await this.city.UpdateAsync(city);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "City", action = "index", area = Constants.AreaAdmin });
            }
        }

        private async Task<PackageCityViewModel> UploadOnly(string folder, PackageCityViewModel model)
        {
            ////if (model.ImageFile != null && model.ImageFile.Length > 0)
            ////{
            ////    try
            ////    {
            ////        if (!Directory.Exists(path))
            ////        {
            ////            DirectoryInfo di = Directory.CreateDirectory(path);
            ////        }

            ////        var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.ImageFile.FileName)}";
            ////        var filePath = Path.Combine(path, fileName);
            ////        using (var fileStream = new FileStream(filePath, FileMode.Create))
            ////        {
            ////            await model.ImageFile.CopyToAsync(fileStream);
            ////        }

            ////        model.Image = fileName;
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        var msg = ex.ToString();
            ////    }
            ////}
            model.Image = await this.UploadImageBlobStorage(folder, model.ImageFile);
            return model;
        }
    }
}
