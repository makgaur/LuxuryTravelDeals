// <copyright file="ApplicationUserService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// ApplicationUserService
    /// </summary>
    /// <seealso cref="HiTours.Services.IApplicationUserService" />
    /// <seealso cref="IApplicationUserService" />
    public class ApplicationUserService : IApplicationUserService
    {
        /// <summary>
        /// The application user service repository
        /// </summary>
        private readonly IRepository<ApplicationUserModel> applicationUserServiceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserService"/> class.
        /// </summary>
        /// <param name="applicationUserServiceRepository">The application user service repository.</param>
        public ApplicationUserService(IRepository<ApplicationUserModel> applicationUserServiceRepository)
        {
            this.applicationUserServiceRepository = applicationUserServiceRepository;
        }

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>record</returns>
        public async Task<ApplicationUserModel> LoginAsync(string userId, string password)
        {
            try
            {
                var record = await this.applicationUserServiceRepository.Table.FirstOrDefaultAsync(x => x.UserId.ToLower() == userId.ToLower() && x.Password == password);
                return record == null ? null : ((record.Password == password) ? record : null);
            }
            catch (Exception ex)
            {
                var messg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="name">Name of Updating User</param>
        /// <returns>ChangePassword</returns>
        public async Task<bool> ChangePassword(string userId, string password, string newPassword, string name)
        {
            var record = await this.applicationUserServiceRepository.Table.FirstOrDefaultAsync(x => x.Id.ToString() == userId.ToString() && x.Password == password);
            if (record == null)
            {
                return false;
            }
            else
            {
                record.Password = newPassword;
                record.UpdatedDate = DateTime.Now;
                record.UpdatedBy = new Guid(userId);
                await this.applicationUserServiceRepository.UpdateAsync(record);
                return true;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllCmsUsersAsync(DataTableParameter model)
        {
            var query = this.applicationUserServiceRepository.Table;

            return await this.applicationUserServiceRepository.ToPagedListAsync(query, model);
        }

        /// <summary>
        /// Check for Duplicate Username.
        /// </summary>
        /// <param name="username">The Username.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<bool> CheckDuplicateUsername(string username)
        {
            var record = await this.applicationUserServiceRepository.Table.FirstOrDefaultAsync(x => x.UserId == username);
            if (record == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get By Id.
        /// </summary>
        /// <param name="id">The Identifier.</param>
        /// <returns>Application User Record</returns>
        public async Task<ApplicationUserModel> GetByIdAsync(Guid id)
        {
            try
            {
                var record = await this.applicationUserServiceRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
                return record;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get By Id.
        /// </summary>
        /// <param name="id">The Identifier.</param>
        /// <returns>Application User Record</returns>
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var record = await this.applicationUserServiceRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }

            await this.applicationUserServiceRepository.DeleteAsync(record);
            return true;
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="record">The Record.</param>
        /// <returns>ChangePassword</returns>
        public async Task<bool> SaveCmsUser(ApplicationUserModel record)
        {
            try
            {
                await this.applicationUserServiceRepository.InsertAsync(record);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="record">The Record.</param>
        /// <returns>ChangePassword</returns>
        public async Task<bool> UpdateCmsUser(ApplicationUserModel record)
        {
            try
            {
                await this.applicationUserServiceRepository.UpdateAsync(record);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return false;
            }
        }
    }
}