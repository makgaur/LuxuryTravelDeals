// <copyright file="ApiSegments.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// List of segments
    /// </summary>
    public class ApiSegments
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
        /// Gets or sets the flight cabin class.
        /// </summary>
        /// <value>
        /// The flight cabin class.
        /// </value>
        public FlightCabinClass FlightCabinClass { get; set; }

        /// <summary>
        /// Gets or sets the preferred departure time.
        /// </summary>
        /// <value>
        /// The preferred departure time.
        /// </value>
        public DateTime PreferredDepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the preferred arrival time.
        /// </summary>
        /// <value>
        /// The preferred arrival time.
        /// </value>
        public DateTime PreferredArrivalTime { get; set; }
    }
}
