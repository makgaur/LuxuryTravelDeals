// <copyright file="ISelectService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;

    /// <summary>
    /// SelectList Service
    /// </summary>
    public interface ISelectService
    {
        /// <summary>
        /// Gets the drop down list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// Get DropDown List Async
        /// </returns>
        Task<IList<Dropdown>> GetDropDownListAsync(string search, short page, params object[] param);
    }
}