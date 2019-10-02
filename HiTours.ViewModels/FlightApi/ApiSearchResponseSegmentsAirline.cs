// <copyright file="ApiSearchResponseSegmentsAirline.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ApiSearchResponseAirline
    /// </summary>
    public class ApiSearchResponseSegmentsAirline
    {
        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the airline.
        /// </summary>
        /// <value>
        /// The name of the airline.
        /// </value>
        public string AirlineName { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the fare class.
        /// </summary>
        /// <value>
        /// The fare class.
        /// </value>
        public string FareClass { get; set; }

        /// <summary>
        /// Gets or sets the operating carrier.
        /// </summary>
        /// <value>
        /// The operating carrier.
        /// </value>
        public string OperatingCarrier { get; set; }
    }
}
