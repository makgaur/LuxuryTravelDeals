// <copyright file="ApiSearchRequest.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    /// <summary>
    /// Search Request
    /// </summary>
    public class ApiSearchRequest
    {
        /// <summary>
        /// Gets or sets the adult count.
        /// </summary>
        /// <value>
        /// The adult count.
        /// </value>
        public int AdultCount { get; set; }

        /// <summary>
        /// Gets or sets the child count.
        /// </summary>
        /// <value>
        /// The child count.
        /// </value>
        public int ChildCount { get; set; }

        /// <summary>
        /// Gets or sets the infant count.
        /// </summary>
        /// <value>
        /// The infant count.
        /// </value>
        public int InfantCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [direct flight].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [direct flight]; otherwise, <c>false</c>.
        /// </value>
        public bool DirectFlight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [one stop flight].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [one stop flight]; otherwise, <c>false</c>.
        /// </value>
        public bool OneStopFlight { get; set; }

        /// <summary>
        /// Gets or sets the type of the journey.
        /// </summary>
        /// <value>
        /// The type of the journey.
        /// </value>
        public ApiJourneyType JourneyType { get; set; }

        /// <summary>
        /// Gets or sets the preferred airlines.
        /// </summary>
        /// <value>
        /// The preferred airlines.
        /// </value>
        public string[] PreferredAirlines { get; set; }

        /// <summary>
        /// Gets or sets the segments.
        /// </summary>
        /// <value>
        /// The segments.
        /// </value>
        public ApiSegments Segments { get; set; }

        /// <summary>
        /// Gets or sets the sources.
        /// </summary>
        /// <value>
        /// The sources.
        /// </value>
        public string[] Sources { get; set; }
    }
}