// <copyright file="ApiSearchResponseResult.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ApiSearchResponse
    /// </summary>
    public class ApiSearchResponseResult
    {
        /// <summary>
        /// Gets or sets the index of the result.
        /// </summary>
        /// <value>
        /// The index of the result.
        /// </value>
        public string ResultIndex { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is LCC.
        /// </summary>
        /// <value>
        /// {D255958A-8513-4226-94B9-080D98F904A1}  <c>true</c> if this instance is LCC; otherwise, <c>false</c>.
        /// </value>
        public bool IsLCC { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is refundable.
        /// </summary>
        /// <value>
        /// {D255958A-8513-4226-94B9-080D98F904A1}  <c>true</c> if this instance is refundable; otherwise, <c>false</c>.
        /// </value>
        public bool IsRefundable { get; set; }

        /// <summary>
        /// Gets or sets the airline remarks.
        /// </summary>
        /// <value>
        /// The airline remarks.
        /// </value>
        public string AirlineRemarks { get; set; }

        /// <summary>
        /// Gets or sets the fare breakdown.
        /// </summary>
        /// <value>
        /// The fare breakdown.
        /// </value>
        public ApiFare Fare { get; set; }

        /// <summary>
        /// Gets or sets the fare breakdown.
        /// </summary>
        /// <value>
        /// The fare breakdown.
        /// </value>
        public List<ApiFareBreakdown> FareBreakdown { get; set; }

        /// <summary>
        /// Gets or sets the Segments.
        /// </summary>
        /// <value>
        /// The Segments.
        /// </value>
        public List<List<ApiSearchResponseSegments>> Segments { get; set; }

        /// <summary>
        /// Gets or sets the last ticket date.
        /// </summary>
        /// <value>
        /// The last ticket date.
        /// </value>
        public DateTime LastTicketDate { get; set; }

        /// <summary>
        /// Gets or sets the ticket advisory.
        /// </summary>
        /// <value>
        /// The ticket advisory.
        /// </value>
        public string TicketAdvisory { get; set; }

        /// <summary>
        /// Gets or sets the fare rules.
        /// </summary>
        /// <value>
        /// The fare rules.
        /// </value>
        public List<ApiSearchResponseFareRules> FareRules { get; set; }
    }
}