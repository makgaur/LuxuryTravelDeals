// <copyright file="TBOService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using static HiTours.Core.Enums;

    /// <summary>
    /// PackageService
    /// </summary>
    /// <seealso cref="HiTours.Services.ITBOService" />
    public class TBOService : ITBOService
    {
        private readonly IRepository<LoginModel> loginRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TBOService" /> class.
        /// </summary>
        /// <param name="loginRepository">The TBO Login Repository.</param>
        public TBOService(IRepository<LoginModel> loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="loginModel">The Login Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<int> InsertLoginRecordAsync(LoginModel loginModel)
        {
            try
            {
                return await this.loginRepository.InsertAsync(loginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<string> GetTodayTokenIdAsync()
        {
            try
            {
                var record = await this.loginRepository.Table.Where(x => x.GeneratedDate.Date == DateTime.Now.Date).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                return record != null ? record.Token : null;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }
    }
}