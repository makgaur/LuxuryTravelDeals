// <copyright file="CurrencyController.cs" company="Luxury Travel Deals">
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
    using HiTours.Data.DataBase.Model;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CurrencyController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CurrencyController : AdminController
    {
        private IMasterService masterService;
        private ICurrencyService currencyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyController"/> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="masterService">Master Service</param>
        /// <param name="currencyService">Currency Service</param>
        public CurrencyController(IConfiguration configuration, IMapper mapper, IMasterService masterService, ICurrencyService currencyService)
             : base(mapper, configuration)
        {
            this.masterService = masterService;
            this.currencyService = currencyService;
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <returns>DataTable Pagging</returns>
        /// <param name="model">Date Table Model</param>
        public async Task<IActionResult> Index([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.currencyService.GetAllAsync(model);

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
            var model = new PackageCurrencyViewModel();
            if (id > 0)
            {
                model = this.Mapper.Map<PackageCurrencyViewModel>(await this.currencyService.GetByIdAsync(id));
            }

            model.CountryList = (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, model.Country)).ToSelectList();
            return this.View(model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(PackageCurrencyViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<CurrencyModel>(model);
                if (record.Id != 0)
                {
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.currencyService.UpdateAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }
                else
                {
                    record.IsActive = true;
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.currencyService.AddAsync(record);
                    this.ShowMessage(Messages.SavedSuccessfully);
                }

                return this.RedirectToRoute(Constants.RouteArea, new { controller = "currency", action = "index", area = Constants.AreaAdmin });
            }

            return this.View(model);
        }

        /// <summary>
        /// Changes the active status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChangeActiveStatus</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(int id)
        {
            var currency = await this.currencyService.GetByIdAsync(id);
            if (currency == null)
            {
                return this.NotFound();
            }

            currency.IsActive = !currency.IsActive;

            await this.currencyService.UpdateAsync(this.Mapper.Map<CurrencyModel>(currency));

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Country", action = "index", area = Constants.AreaAdmin });
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
            try
            {
                var currency = await this.currencyService.GetByIdAsync(id ?? 0);
                if (currency == null)
                {
                    return this.NotFound();
                }

                await this.currencyService.DeleteAsync(this.Mapper.Map<CurrencyModel>(currency));
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