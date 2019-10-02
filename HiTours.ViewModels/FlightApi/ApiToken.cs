// <copyright file="ApiToken.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HiTours.ViewModels.Api;

    /// <summary>
    /// ApiToken
    /// </summary>
    public class ApiToken
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ApiStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the token identifier.
        /// </summary>
        /// <value>
        /// The token identifier.
        /// </value>
        public string TokenId { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public ApiError Error { get; set; }

        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        /// <value>
        /// The member.
        /// </value>
        public ApiMember Member { get; set; }
    }
}
