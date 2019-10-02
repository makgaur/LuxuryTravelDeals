// <copyright file="VisaController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// VisaController
    /// </summary>
    /// <seealso cref="HiTours.Web.AdminController" />
    public class VisaController : AdminController
    {
        private readonly IVisaService visaServices;
        private readonly IDealService dealsServices;
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisaController" /> class.
        /// </summary>
        /// <param name="dealsServices">Deal Service</param>
        /// <param name="configuration">Config</param>
        /// <param name="visaServices">visa Service</param>
        /// <param name="mapper">mapper</param>
        /// <param name="masterService">master service</param>
        public VisaController(IDealService dealsServices, IConfiguration configuration, IMasterService masterService, IVisaService visaServices, IMapper mapper)
            : base(mapper, configuration)
        {
            this.dealsServices = dealsServices;
            this.masterService = masterService;
            this.visaServices = visaServices;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>DataTable Pagging</returns>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            try
            {
                if (this.IsAjaxRequest())
                {
                    var result = await this.visaServices.GetAllVisaMasterAsync(model);

                    return this.Json(result);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage("Model Load Failed", Enums.MessageType.Error);
                return this.Json("failure");
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">modelID</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public async Task<ActionResult> Manage(int id)
        {
            var model = new AddVisaMasterViewModel
            {
                IsActive = true,
                Id = id
            };
            if (id > 0)
            {
                model = this.Mapper.Map<AddVisaMasterViewModel>(await this.visaServices.GetVisaMasterByIdAsyn(id));
                model.VendorItems = model.VendorID == 0 ? new List<SelectListItem>() : (await this.visaServices.GetVendorVisaDropDownListAsync(string.Empty, 0, model.VendorID)).ToSelectList();
                model.CountryItems = model.CountryId == 0 ? new List<SelectListItem>() : (await this.masterService.GetPackageCountryListAsync(string.Empty, 0, model.CountryId, 0)).ToSelectList();
            }

            return this.View(model);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteMaster(int id)
        {
            try
            {
                await this.visaServices.DeleteMasterAsync(id);
                this.ShowMessage(Messages.DeletedSuccessfully);
                return this.Json("success");
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                this.ShowMessage("Delete Failed", Enums.MessageType.Error);
                return this.Json("failure");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            try
            {
                var visa = await this.visaServices.GetVisaMasterByIdAsyn(id);
                if (visa == null)
                {
                    return this.NotFound();
                }

                visa.IsActive = !visa.IsActive;
                await this.visaServices.UpdateVisaMasterAsync(visa);

                if (this.IsAjaxRequest())
                {
                    return this.Json(new { Status = true });
                }
                else
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Visa", action = "index", area = Constants.AreaAdmin });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return this.Json(new { Status = false });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(AddVisaMasterViewModel model)
        {
            try
            {
                var record = this.Mapper.Map<VisaModel>(model);
                if (record.Id != 0)
                {
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.visaServices.UpdateVisaMasterAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    record.IsActive = true;
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    int? newId = await this.visaServices.AddVisaMasterAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }

                List<DealVisaModel> dealVisaModels = await this.dealsServices.GetAllVisaFromActiveDealsFromCountryId(model.CountryId);
                if (dealVisaModels != null && dealVisaModels.Count > 0)
                {
                    foreach (var item in dealVisaModels)
                    {
                        item.AdultPrice = model.AdultPrice;
                        ////item.BufferDays = model.BufferDays;
                        item.ChildPrice = model.ChildPrice;
                        item.DocumentsRequired = model.DocumentsRequired;
                        item.GeneralPolicy = model.GeneralPolicy;
                        item.Markup = model.Markup;
                        item.PhotoSpecification = model.PhotoSpecification;
                        item.ProcessingTime = model.ProcessingTime;
                        item.VendorId = model.VendorID;
                        item.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.dealsServices.UpdateDealPackageVisa(item);
                    }
                }
            }
            catch (Exception ex)
            {
                var str = ex.ToString();
                this.ShowMessage(Messages.InsertFailed, Enums.MessageType.Error);
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Visa", action = "Index", area = Constants.AreaAdmin });
            }

            return this.RedirectToRoute(Constants.RouteArea, new { controller = "Visa", action = "Index", area = Constants.AreaAdmin });
        }
    }
}