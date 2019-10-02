// <copyright file="VendorController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
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
    /// UploadBannersController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class VendorController : AdminController
    {
        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IVendorService vendorService;
        private readonly IMasterService masterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VendorController" /> class.
        /// </summary>
        /// <param name="configuration">Confg</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="vendorService">Vendor Service</param>
        /// <param name="masterService">Master Service</param>
        public VendorController(IConfiguration configuration, IMapper mapper, IVendorService vendorService, IMasterService masterService)
           : base(mapper, configuration)
        {
            this.masterService = masterService;
            this.vendorService = vendorService;
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
                var result = await this.vendorService.GetAllVendorsAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Tours the package creation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>viewModel</returns>
        public async Task<IActionResult> Creation(int id)
        {
            VendorInformationViewModel model = new VendorInformationViewModel
            {
                Id = id
            };
            if (id != 0)
            {
                var record = await this.vendorService.GetVendorById(id);
                model.Name = record.Name;
            }

            return this.View(model);
        }

        /// <summary>
        /// Tours the package creation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>viewModel</returns>
        public async Task<IActionResult> Manage(int id)
        {
            VendorInformationViewModel model = new VendorInformationViewModel
            {
                Id = id
            };
            if (id > 0)
            {
                model = this.Mapper.Map<VendorInformationViewModel>(await this.vendorService.GetVendorById(id));
                model.ServiceTypes = await this.vendorService.GetServiceTypesByVendorId(id);
            }

            model.CurrencyItems = model.Currency == 0 ? new List<SelectListItem>() : (await this.vendorService.GetCurrencyDropDownListAsync(string.Empty, 1, model.Currency)).ToSelectList();
            model.VendorGroupItems = model.Group == 0 || model.Group == null ? new List<SelectListItem>() : (await this.vendorService.GetVendorGroupDropDownListAsync(string.Empty, 1, model.Group)).ToSelectList();
            model.CategoryItems = model.Category == 0 ? new List<SelectListItem>() : (await this.vendorService.GetCategoryDropDownListAsync(string.Empty, 1, model.Category)).ToSelectList();
            model.CountryItems = model.Country == 0 ? new List<SelectListItem>() : (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, model.Country)).ToSelectList();
            model.StateItems = model.State == 0 || model.State == null ? new List<SelectListItem>() : (await this.masterService.GetTourPackageStatesByCountrId(string.Empty, 1, model.Country, (short)model.State)).ToSelectList();
            model.CityItems = model.City == 0 ? new List<SelectListItem>() : (await this.masterService.GetTourPackageCityByCounryIdorStateIdAsync(string.Empty, 1, model.Country, model.State == 0 || model.State == null ? (short)0 : (short)model.State, (short)model.City)).ToSelectList();
            model.ServiceTypeItems = (await this.vendorService.GetAllVendorServiceTypeItems()).ToSelectList();
            return this.PartialView("ManageVendorInfo", model);
        }

        /// <summary>
        /// Manages the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="nextview">Next View</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> Manage(VendorInformationViewModel model, string nextview)
        {
            if (this.ModelState.IsValid)
            {
                int? newId = 0;
                if (!string.IsNullOrEmpty(nextview))
                {
                    this.TempData["nextview"] = nextview;
                }

                try
                {
                    var record = this.Mapper.Map<VendorInformationModel>(model);
                    if (record.Id != 0)
                    {
                        record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        await this.vendorService.UpdateVendorInfoAsync(record);
                        newId = record.Id;
                        await this.vendorService.DeleteVendorServicesById(Convert.ToInt32(newId));
                        foreach (var item in model.ServiceTypes)
                        {
                            await this.vendorService.InsertVendorServiceRecord(Convert.ToInt32(newId), item);
                        }

                        this.ShowMessage(Messages.SavedSuccessfully);
                    }
                    else
                    {
                        record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                        record.IsActive = true;
                        newId = await this.vendorService.AddVendorInfoAsync(record);
                        foreach (var item in model.ServiceTypes)
                        {
                            await this.vendorService.InsertVendorServiceRecord(Convert.ToInt32(newId), item);
                        }

                        this.ShowMessage(Messages.SavedSuccessfully);
                    }
                }
                catch (Exception ex)
                {
                    var str = ex.ToString();
                    this.ShowMessage(Messages.InsertFailed);
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "vendor", action = "index", area = Constants.AreaAdmin });
                }

                if (model.CommandButton != null && model.CommandButton == "SaveandReload")
                {
                    return this.RedirectToAction("Creation", "Vendor", new { @area = Constants.AreaAdmin, @id = newId });
                }
                else if (model.CommandButton != null && model.CommandButton == "SubmitAndNext")
                {
                    this.TempData["nextview"] = "#vendor-contact";
                    return this.RedirectToAction("Creation", "Vendor", new { @area = Constants.AreaAdmin, @id = newId });
                }
                else if (model.CommandButton != null && model.CommandButton == "SubmitAndClose")
                {
                    return this.RedirectToAction("Index", "Vendor", new { @area = Constants.AreaAdmin });
                }
                else
                {
                    return this.RedirectToAction("Index", "Vendor", new { @area = Constants.AreaAdmin });
                }
            }
            else
            {
                this.ShowMessage(Messages.InsertFailed);
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "vendor", action = "index", area = Constants.AreaAdmin });
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpGet]
        public ActionResult ManageGroup(int id)
        {
            VendorGroupViewModel groupModel = new VendorGroupViewModel
            {
                Id = 0,
                IsActive = true
            };
            groupModel.CountryItems = groupModel.StateItems = groupModel.CityItems = new List<SelectListItem>();
            return this.PartialView("_ManageVendorGroup", groupModel);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="modal">The identifier.</param>
        /// <param name="full_phone">Personal Phone Full</param>
        /// <param name="full_phone_work">Work Phone Full</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> ManageGroup(VendorGroupViewModel modal, string full_phone, string full_phone_work)
        {
            var record = this.Mapper.Map<VendorGroupModel>(modal);
            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
            record.IsActive = true;
            try
            {
                await this.vendorService.AddGroupAsync(record);
                return this.Content("success");
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return this.Content("failure");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <returns>Manage</returns>
        /// <param name="model">The identifier.</param>
        public async Task<ActionResult> Group([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.vendorService.GetAllVendorGroupsAsync(model);

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
        public async Task<ActionResult> AddGroup(int id)
        {
            VendorGroupViewModel groupModel = new VendorGroupViewModel
            {
                Id = 0,
                IsActive = true
            };
            if (id > 0)
            {
                groupModel = this.Mapper.Map<VendorGroupViewModel>(await this.vendorService.GetVendorGroupById(id));
            }

            groupModel.DesignationItems = string.IsNullOrEmpty(groupModel.Designation) ? new List<SelectListItem>() : (await this.masterService.GetDesignationMaster(groupModel.Designation, 1)).ToSelectList();
            groupModel.SalutationItems = string.IsNullOrEmpty(groupModel.Salutation) ? new List<SelectListItem>() : (await this.masterService.GetSalutationMaster(groupModel.Salutation, 1)).ToSelectList();
            groupModel.CountryItems = groupModel.CountryId == 0 ? new List<SelectListItem>() : (await this.masterService.GetPackageCountryListAsync(string.Empty, 1, groupModel.CountryId)).ToSelectList();
            groupModel.StateItems = groupModel.StateId == 0 || groupModel.StateId == null ? new List<SelectListItem>() : (await this.masterService.GetTourPackageStatesByCountrId(string.Empty, 1, groupModel.CountryId, (short)groupModel.StateId)).ToSelectList();
            groupModel.CityItems = groupModel.CityId == 0 ? new List<SelectListItem>() : (await this.masterService.GetTourPackageCityByCounryIdorStateIdAsync(string.Empty, 1, groupModel.CountryId, groupModel.StateId == 0 || groupModel.StateId == null ? (short)0 : (short)groupModel.StateId, (short)groupModel.CityId)).ToSelectList();
            return this.PartialView("AddGroup", groupModel);
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="modal">The identifier.</param>
        /// <returns>Manage</returns>
        [HttpPost]
        public async Task<ActionResult> AddGroup(VendorGroupViewModel modal)
        {
            try
            {
                var record = this.Mapper.Map<VendorGroupModel>(modal);
                if (record.Id > 0)
                {
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.vendorService.UpdateGroupAsync(record);
                    this.ShowMessage("Updated Successfully");
                }
                else
                {
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    record.IsActive = true;
                    await this.vendorService.AddGroupAsync(record);
                    this.ShowMessage("Successfully Inserted");
                }

                return this.RedirectToAction("Group");
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                this.ShowMessage("Failed");
                return this.RedirectToAction("Group");
            }
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var record = await this.vendorService.GetVendorById(id);
                record.IsActive = false;
                record.IsDeleted = true;
                await this.vendorService.UpdateVendorInfoAsync(record);
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
        public async Task<ActionResult> DeleteVendorGroup(int id)
        {
            try
            {
                var record = await this.vendorService.GetVendorGroupById(id);
                record.IsDeleted = !record.IsDeleted;
                await this.vendorService.UpdateGroupAsync(record);
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
        public async Task<ActionResult> DeleteContact(int id)
        {
            try
            {
                await this.vendorService.DeleteContactAsync(id);
                this.ShowMessage(Messages.DeletedSuccessfully);
                this.TempData["nextview"] = "#vendor-contact";
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
                var vendor = await this.vendorService.GetVendorById(id);
                if (vendor == null)
                {
                    return this.NotFound();
                }

                vendor.IsActive = !vendor.IsActive;
                await this.vendorService.UpdateVendorInfoAsync(vendor);

                if (this.IsAjaxRequest())
                {
                    return this.Json(new { Status = true });
                }
                else
                {
                    return this.RedirectToRoute(Constants.RouteArea, new { controller = "Vendor", action = "index", area = Constants.AreaAdmin });
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return this.Json(new { Status = false });
            }
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="id">the identifier.</param>
        /// <returns>viewmodel</returns>
        public IActionResult Contacts(int id)
        {
            this.ViewBag.VendorId = id;
            return this.PartialView();
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="id">the identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>viewmodel</returns>
        [HttpGet]
        public async Task<IActionResult> ContactAdd(int id, int vendorId)
        {
            this.ViewBag.VendorId = id;
            VendorContactViewModel model = new VendorContactViewModel
            {
                Id = 0,
                VendorId = vendorId
            };
            if (id > 0)
            {
                model = this.Mapper.Map<VendorContactViewModel>(await this.vendorService.GetVendorContactsByIdentifierAsync(id));
            }

            model.DesignationItems = string.IsNullOrEmpty(model.Designation) ? new List<SelectListItem>() : (await this.masterService.GetDesignationMaster(model.Designation, 1)).ToSelectList();
            model.SalutationItems = string.IsNullOrEmpty(model.Salutation) ? new List<SelectListItem>() : (await this.masterService.GetSalutationMaster(model.Salutation, 1)).ToSelectList();
            return this.PartialView("_ContactAdd", model);
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="model">the identifier.</param>
        /// <returns>viewmodel</returns>
        [HttpPost]
        public async Task<IActionResult> ContactAdd(VendorContactViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<VendorContactModel>(model);
                record.Mobile = string.IsNullOrEmpty(record.Mobile) ? string.Empty : Regex.Replace(record.Mobile, @"\s+", string.Empty);
                record.Alt_Mobile = string.IsNullOrEmpty(record.Alt_Mobile) ? string.Empty : Regex.Replace(record.Alt_Mobile, @"\s+", string.Empty);
                record.WorkPhone = string.IsNullOrEmpty(record.WorkPhone) ? string.Empty : Regex.Replace(record.WorkPhone, @"\s+", string.Empty);
                record.Alt_WorkPhone = string.IsNullOrEmpty(record.Alt_WorkPhone) ? string.Empty : Regex.Replace(record.Alt_WorkPhone, @"\s+", string.Empty);
                if (model.Id > 0)
                {
                    ////Update
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.vendorService.UpdateVendorContactAsync(record);
                    this.ShowMessage("Updated Successfully");
                    this.TempData["nextview"] = "#vendor-contact";
                }
                else
                {
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.vendorService.AddVendorContactAsync(record);
                    this.ShowMessage("Added Successfully");
                    this.TempData["nextview"] = "#vendor-contact";
                }

                return this.Json("success");
            }

            return this.Json("failure");
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="model">Data Table Model</param>
        /// <param name="id">the identifier.</param>
        /// <returns>viewmodel</returns>
        public async Task<IActionResult> GetContactGrid([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int id)
        {
            this.ViewBag.VendorId = id;
            if (this.IsAjaxRequest())
            {
                var result = await this.vendorService.GetVendorContactsByIdAsync(model, id);
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteContract(int id)
        {
            try
            {
                await this.vendorService.DeleteContractAsync(id);
                this.ShowMessage(Messages.DeletedSuccessfully);
                this.TempData["nextview"] = "#vendor-contract";
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
        /// tours the package creation.
        /// </summary>
        /// <param name="id">the identifier.</param>
        /// <returns>viewmodel</returns>
        public IActionResult Contracts(int id)
        {
            this.ViewBag.VendorId = id;
            return this.PartialView();
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="id">the identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>viewmodel</returns>
        [HttpGet]
        public async Task<IActionResult> ContractAdd(int id, int vendorId)
        {
            this.ViewBag.VendorId = id;
            VendorContractViewModel model = new VendorContractViewModel
            {
                Id = 0,
                VendorId = vendorId
            };
            if (id > 0)
            {
                model = this.Mapper.Map<VendorContractViewModel>(await this.vendorService.GetVendorContractsByIdentifierAsync(id));
            }

            model.MarginTypeItems = model.MarginType == 0 ? new List<SelectListItem>() : (await this.masterService.GetMarginTypeMaster(string.Empty, 1, model.MarginType)).ToSelectList();
            return this.PartialView("_ContractAdd", model);
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="model">the identifier.</param>
        /// <returns>viewmodel</returns>
        [HttpPost]
        public async Task<IActionResult> ContractAdd(VendorContractViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<VendorContractModel>(model);
                if (model.Id > 0)
                {
                    ////Update
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.vendorService.UpdateVendorContractAsync(record);
                    this.ShowMessage("Updated Successfully");
                    this.TempData["nextview"] = "#vendor-contract";
                }
                else
                {
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.vendorService.AddVendorContractAsync(record);
                    this.ShowMessage("Added Successfully");
                    this.TempData["nextview"] = "#vendor-contract";
                }

                return this.Json("success");
            }

            return this.Json("failure");
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="model">Data Table Model</param>
        /// <param name="id">the identifier.</param>
        /// <returns>viewmodel</returns>
        public async Task<IActionResult> GetContractGrid([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int id)
        {
            this.ViewBag.VendorId = id;
            if (this.IsAjaxRequest())
            {
                var result = await this.vendorService.GetVendorContractsByIdAsync(model, id);
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="id">the identifier.</param>
        /// <returns>viewmodel</returns>
        public IActionResult BankDetails(int id)
        {
            this.ViewBag.VendorId = id;
            return this.PartialView();
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="model">Data Table Model</param>
        /// <param name="id">the identifier.</param>
        /// <returns>viewmodel</returns>
        public async Task<IActionResult> GetBankDetailGrid([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model, int id)
        {
            this.ViewBag.VendorId = id;
            if (this.IsAjaxRequest())
            {
                var result = await this.vendorService.GetVendorBankDetailByIdAsync(model, id);
                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="id">the identifier.</param>
        /// <param name="vendorId">Vendor Id</param>
        /// <returns>viewmodel</returns>
        [HttpGet]
        public async Task<IActionResult> BankDetailAdd(int id, int vendorId)
        {
            this.ViewBag.VendorId = id;
            VendorBankDetailsViewModel model = new VendorBankDetailsViewModel
            {
                Id = 0,
                VendorId = vendorId
            };
            if (id > 0)
            {
                model = this.Mapper.Map<VendorBankDetailsViewModel>(await this.vendorService.GetVendorBankDetailByIdentifierAsync(id));
            }

            return this.PartialView("_BankDetailAdd", model);
        }

        /// <summary>
        /// tours the package creation.
        /// </summary>
        /// <param name="model">the identifier.</param>
        /// <returns>viewmodel</returns>
        [HttpPost]
        public async Task<IActionResult> BankDetailAdd(VendorBankDetailsViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var record = this.Mapper.Map<VendorBankDetailModel>(model);
                record.AccountNumber = string.IsNullOrEmpty(record.AccountNumber) ? string.Empty : record.AccountNumber.ToUpper();
                record.PAN = string.IsNullOrEmpty(record.PAN) ? string.Empty : record.PAN.ToUpper();
                record.GST = string.IsNullOrEmpty(record.GST) ? string.Empty : record.GST.ToUpper();
                if (model.Id > 0)
                {
                    ////Update
                    record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.vendorService.UpdateVendorBankDetailtAsync(record);
                    this.ShowMessage("Updated Successfully");
                }
                else
                {
                    record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
                    await this.vendorService.AddVendorBankDetailAsync(record);
                    this.ShowMessage("Added Successfully");
                }

                this.TempData["nextview"] = "#vendor-bank";
                return this.Json("success");
            }

            return this.Json("failure");
        }

        /// <summary>
        /// Manages the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<ActionResult> DeleteBankDetail(int id)
        {
            try
            {
                await this.vendorService.DeleteBankDetailAsync(id);
                this.ShowMessage(Messages.DeletedSuccessfully);
                this.TempData["nextview"] = "#vendor-bank";
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
        /// <param name="name">Name</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Manage</returns>
        public async Task<JsonResult> IsVendorAvailable(string name, int id)
        {
            return this.Json(await this.vendorService.IsDuplicateVendor(name, id));
        }
    }
}