// <copyright file="ApiSearchResponseFareRules.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System.Collections.Generic;

    /// <summary>
    /// ApiSearchResponseFareRules
    /// </summary>
    public class ApiSearchResponseFareRules
    {
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
        /// Gets or sets the airline.
        /// </summary>
        /// <value>
        /// The airline.
        /// </value>
        public string Airline { get; set; }

        /// <summary>
        /// Gets or sets the fare basis code.
        /// </summary>
        /// <value>
        /// The fare basis code.
        /// </value>
        public string FareBasisCode { get; set; }

        /// <summary>
        /// Gets or sets the fare rule detail.
        /// </summary>
        /// <value>
        /// The fare rule detail.
        /// </value>
        public List<string> FareRuleDetail { get; set; }

        /// <summary>
        /// Gets or sets the fare restriction.
        /// </summary>
        /// <value>
        /// The fare restriction.
        /// </value>
        public string FareRestriction { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the validating airline.
        /// </summary>
        /// <value>
        /// The validating airline.
        /// </value>
        public string ValidatingAirline { get; set; }
    }
}