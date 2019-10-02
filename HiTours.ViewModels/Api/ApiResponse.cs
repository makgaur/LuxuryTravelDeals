// <copyright file="ApiResponse.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HiTours.ViewModels.Api;
    using HiTours.ViewModels.FlightApi;

    /// <summary>
    /// ApiResponse
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the response status.
        /// </summary>
        /// <value>
        /// The response status.
        /// </value>
        public int ResponseStatus { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public ApiError Error { get; set; }

        /// <summary>
        /// Gets or sets the trace identifier.
        /// </summary>
        /// <value>
        /// The trace identifier.
        /// </value>
        public string TraceId { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public List<List<ApiSearchResponseResult>> Results { get; set; }
    }
}
