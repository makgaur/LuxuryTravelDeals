// <copyright file="ApiSearchResponseSegmentsOriginAirport.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    /// <summary>
    /// ApiSearchResponseSegmentsOriginAirport
    /// </summary>
    public class ApiSearchResponseSegmentsOriginAirport
    {
        /// <summary>
        /// Gets or sets the airport code.
        /// </summary>
        /// <value>
        /// The airport code.
        /// </value>
        public string AirportCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the airport.
        /// </summary>
        /// <value>
        /// The name of the airport.
        /// </value>
        public string AirportName { get; set; }

        /// <summary>
        /// Gets or sets the terminal.
        /// </summary>
        /// <value>
        /// The terminal.
        /// </value>
        public string Terminal { get; set; }

        /// <summary>
        /// Gets or sets the city code.
        /// </summary>
        /// <value>
        /// The city code.
        /// </value>
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }
    }
}