// <copyright file="ApiBalance.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HiTours.ViewModels.Api;

    /// <summary>
    /// ApiBalance
    /// </summary>
    public class ApiBalance
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ApiStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the agency.
        /// </summary>
        /// <value>
        /// The type of the agency.
        /// </value>
        public ApiAgencyType AgencyType { get; set; }

        /// <summary>
        /// Gets or sets the cash balance.
        /// </summary>
        /// <value>
        /// The cash balance.
        /// </value>
        public decimal CashBalance { get; set; }

        /// <summary>
        /// Gets or sets the credit balance.
        /// </summary>
        /// <value>
        /// The credit balance.
        /// </value>
        public decimal CreditBalance { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public ApiError Error { get; set; }
    }
}
