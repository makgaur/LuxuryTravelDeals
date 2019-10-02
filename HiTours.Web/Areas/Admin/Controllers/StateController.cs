// <copyright file="StateController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.IO;
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
    /// StateController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class StateController : AdminController
    {
        private readonly IStateService state;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateController" /> class.
        /// </summary>
        /// <param name="configuration">Config</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="state">The country.</param>
        /// <param name="masterService">The master service.</param>
        public StateController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IMapper mapper, IStateService state, IMasterService masterService)
            : base(mapper, configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.state = state;
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
                var result = await this.state.GetAllAsync(model);

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
            var model = new PackageStateViewModel();
            if (id > 0)
            {
                var record = await this.state.GetByIdAsync(id);

                if (record != null)
                {
                    model = this.Mapper.Map<PackageStateViewModel>(record);
                    model.Coutries = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, record.CountryId)).ToSelectList();
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageStateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    model = await this.UploadOnly("images/State", model);
                }

                var record = this.Mapper.Map<PackageStateModel>(model);
                if (model.Id == 0)
                {
                    record.SetAuditInfo(0);
                    await this.state.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    record.UpdateAuditInfo(0);
                    await this.state.UpdateAsync(record);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "state", action = "index", area = Constants.AreaAdmin });
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
            return this.Json(await this.state.IsDuplicateAsync(name, id));
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
                var state = await this.state.GetByIdAsync(id ?? 0);
                if (state == null)
                {
                    return this.NotFound();
                }

                await this.state.DeleteAsync(state);
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

        private async Task<PackageStateViewModel> UploadOnly(string folder, PackageStateViewModel model)
        {
            model.Image = await this.UploadImageBlobStorage(folder, model.ImageFile);
            return model;
        }
    }
}
