// <copyright file="CurationController.cs" company="Luxury Travel Deals">
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
    public class CurationController : AdminController
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly ICurationsService curationService;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurationController" /> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="curationService">Curation Service</param>
        public CurationController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IMasterService masterService, IMapper mapper, ICurationsService curationService)
            : base(mapper, configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.masterService = masterService;
            this.curationService = curationService;
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
                var result = await this.curationService.GetAllCurationsAsync(model, mode);
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
            CurationsAddViewModel model = new CurationsAddViewModel();
            if (mode == "1x1")
            {
                model.OneXOne = true;
                model.OneXTwo = false;
                model.TwoXTwo = false;
            }

            if (mode == "1x2")
            {
                model.OneXOne = false;
                model.OneXTwo = true;
                model.TwoXTwo = false;
            }

            if (mode == "2x2")
            {
                model.OneXOne = false;
                model.OneXTwo = false;
                model.TwoXTwo = true;
            }

            if (id > 0)
            {
                model = this.Mapper.Map<CurationsAddViewModel>(await this.curationService.GetCurationByIdAsync(Convert.ToInt32(id)));
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
        public async Task<IActionResult> Manage(CurationsAddViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var data = this.Mapper.Map<CurationsModel>(model);
                if (model.Id == 0)
                {
                    if (model.ImageFile != null)
                    {
                        model = await this.UploadOnly("CurationImages", model);
                    }

                    var record = this.Mapper.Map<CurationsModel>(model);
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.curationService.InsertAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    if (model.ImageFile != null)
                    {
                        model = await this.UploadOnly("CurationImages", model);
                        data.Image = model.Image;
                    }

                    data.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.curationService.UpdateAsync(data);
                    this.ShowMessage(Messages.UpdateSuccessfully);
                }

                return this.RedirectToAction("index", "curation", new { @mode=model.Mode });
            }

            return this.View(model);
        }

        private async Task<CurationsAddViewModel> UploadOnly(string folder, CurationsAddViewModel model)
        {
            model.Image = await this.UploadImageBlobStorage(folder, model.ImageFile);
            return model;
        }
    }
}
