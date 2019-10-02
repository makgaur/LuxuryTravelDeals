// <copyright file="IApplicationUserService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// IApplicationUserService
    /// </summary>
    public interface IApplicationUserService
    {
        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>ApplicationUserModel</returns>
        Task<ApplicationUserModel> LoginAsync(string userId, string password);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="name">Name of Updating User</param>
        /// <returns>ChangePassword</returns>
        Task<bool> ChangePassword(string userId, string password, string newPassword, string name);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllCmsUsersAsync(DataTableParameter model);

        /// <summary>
        /// Check for Duplicate Username.
        /// </summary>
        /// <param name="username">The Username.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<bool> CheckDuplicateUsername(string username);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="record">The Record.</param>
        /// <returns>ChangePassword</returns>
        Task<bool> SaveCmsUser(ApplicationUserModel record);

        /// <summary>
        /// Get By Id.
        /// </summary>
        /// <param name="id">The Identifier.</param>
        /// <returns>Application User Record</returns>
        Task<ApplicationUserModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="record">The Record.</param>
        /// <returns>ChangePassword</returns>
        Task<bool> UpdateCmsUser(ApplicationUserModel record);

        /// <summary>
        /// Get By Id.
        /// </summary>
        /// <param name="id">The Identifier.</param>
        /// <returns>Application User Record</returns>
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
