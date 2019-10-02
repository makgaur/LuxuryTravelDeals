// <copyright file="AccountController.cs" company="Luxury Travel Deals">
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
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// AccountController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    ////[ServiceFilter(typeof(IPCheckFilter))]
    public class AccountController : AdminController
    {
        /// <summary>
        /// The application user service
        /// </summary>
        private readonly IApplicationUserService applicationUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="applicationUserService">The application user service.</param>
        /// <param name="configuration">Configuration</param>
        public AccountController(IMapper mapper, IApplicationUserService applicationUserService, IConfiguration configuration)
             : base(mapper, configuration)
        {
            this.applicationUserService = applicationUserService;
        }

        /// <summary>
        /// Indexes the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// ReturnUrl
        /// </returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            return this.View();
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns>ChangePassword</returns>
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        /// <summary>
        /// CRM User List.
        /// </summary>
        /// <returns>CRM User List</returns>
        public IActionResult CmsUser()
        {
            return this.View();
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// ChangePassword
        /// </returns>
        public async Task<IActionResult> CrmUserList([ModelBinder(typeof(DataTableModelBinder))]DataTableParameter model)
        {
            if (this.IsAjaxRequest())
            {
                var result = await this.applicationUserService.GetAllCmsUsersAsync(model);

                return this.Json(result);
            }

            return this.View();
        }

        /// <summary>
        /// Add Crm User
        /// </summary>
        /// <returns>View</returns>
        public IActionResult AddCmsUser()
        {
            return this.View();
        }

        /// <summary>
        /// Add Crm User
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<IActionResult> AddCmsUser(ApplicationUserAddViewModel model)
        {
            if (!await this.applicationUserService.CheckDuplicateUsername(model.UserId))
            {
                this.ShowMessage(string.Format(Messages.AlreadyExists, model.UserId), Enums.MessageType.Warning);
                return this.View(model);
            }

            var record = this.Mapper.Map<ApplicationUserModel>(model);
            record.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
            record.Id = Guid.NewGuid();
            record.IsActive = true;

            var status = await this.applicationUserService.SaveCmsUser(record);
            if (status)
            {
                this.ShowMessage(Messages.SavedSuccessfully, Enums.MessageType.Success);
            }
            else
            {
                this.ShowMessage(Messages.InsertFailed, Enums.MessageType.Error);
            }

            return this.RedirectToRoute(Constants.RouteArea, new { controller = "Account", action = "CmsUser", area = Constants.AreaAdmin });
        }

        /// <summary>
        /// Changes the active status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChangeActiveStatus</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeActiveStatus(Guid id)
        {
            var record = await this.applicationUserService.GetByIdAsync(id);
            if (record == null)
            {
                return this.NotFound();
            }

            record.IsActive = !record.IsActive;
            record.UpdateAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
            await this.applicationUserService.UpdateCmsUser(record);

            if (this.IsAjaxRequest())
            {
                return this.Json(new { Status = true });
            }
            else
            {
                return this.RedirectToRoute(Constants.RouteArea, new { controller = "Account", action = "CmsUserList", area = Constants.AreaAdmin });
            }
        }

        /// <summary>
        /// Changes the active status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChangeActiveStatus</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteCrmUser(Guid id)
        {
            await this.applicationUserService.DeleteByIdAsync(id);
            this.ShowMessage(Messages.DeletedSuccessfully);
            return this.Json("Success");
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnPath">The return path.</param>
        /// <returns>
        /// ChangePassword
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ApplicationUserChangePasswordViewModel model, string returnPath)
        {
            var userId = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
            var name = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var userRecord = await this.applicationUserService.ChangePassword(userId, model.OldPassword, model.NewPassword, name);
            if (userRecord)
            {
                this.ShowMessage(Messages.PasswordChangeSuccess, Enums.MessageType.Success);
            }
            else
            {
                this.ShowMessage(Messages.InvalidOldPassword, Enums.MessageType.Error);
            }

            return this.View();
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return path.</param>
        /// <returns>
        /// Login User Async
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ApplicationUserViewModels model, string returnUrl)
        {
            var userRecord = await this.applicationUserService.LoginAsync(model.UserId, model.Password);
            if (userRecord == null)
            {
                this.ViewBag.LoginError = Messages.LoginFailed;
                return this.View();
            }

            var claimIdentity = new ClaimsIdentity(Constants.ApplicationAdminCookies, ClaimTypes.Name, ClaimTypes.Role);
            claimIdentity.AddClaim(new Claim(ClaimTypes.Sid, userRecord.Id.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userRecord.UserId));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, Constants.AdminRole));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, userRecord.Name));

            await this.HttpContext.SignInAsync(Constants.ApplicationAdminCookies, new ClaimsPrincipal(claimIdentity));

            if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/admin/account/logout?index=login")
            {
                return this.LocalRedirect(returnUrl);
            }

            return this.RedirectToRoute(Constants.RouteArea, new { controller = "dashboard", action = "index", area = Constants.AreaAdmin });
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>Logout User Async</returns>
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(Constants.ApplicationAdminCookies);
            return this.RedirectToRoute(Constants.RouteArea, new { controller = "account", index = "login", area = Constants.AreaAdmin });
        }
    }
}