// <copyright file="ITBOService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;

    /// <summary>
    /// IPackageService
    /// </summary>
    public interface ITBOService
    {
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<string> GetTodayTokenIdAsync();

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="loginModel">The Login Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<int> InsertLoginRecordAsync(LoginModel loginModel);
    }
}