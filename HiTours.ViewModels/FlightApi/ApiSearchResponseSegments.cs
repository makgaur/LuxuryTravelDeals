// <copyright file="ApiSearchResponseSegments.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;

    /// <summary>
    /// ApiSearchResponceSegments
    /// </summary>
    public class ApiSearchResponseSegments
    {
        /// <summary>
        /// Gets or sets the trip indicator.
        /// </summary>
        /// <value>
        /// The trip indicator.
        /// </value>
        public int TripIndicator { get; set; }

        /// <summary>
        /// Gets or sets the segment indicator.
        /// </summary>
        /// <value>
        /// The segment indicator.
        /// </value>
        public int SegmentIndicator { get; set; }

        /// <summary>
        /// Gets or sets the search response airline.
        /// </summary>
        /// <value>
        /// The search response airline.
        /// </value>
        public ApiSearchResponseSegmentsAirline Airline { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public ApiSearchResponseSegmentsOrigin Origin { get; set; }

        /// <summary>
        /// Gets or sets the dep time.
        /// </summary>
        /// <value>
        /// The dep time.
        /// </value>
        public DateTime DepTime { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public ApiSearchResponseSegmentsDestination Destination { get; set; }

        /// <summary>
        /// Gets or sets the arr time.
        /// </summary>
        /// <value>
        /// The arr time.
        /// </value>
        public DateTime ArrTime { get; set; }

        /// <summary>
        /// Gets or sets the accumulated durati.
        /// </summary>
        /// <value>
        /// The accumulated durati.
        /// </value>
        public int AccumulatedDuration { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the ground time.
        /// </summary>
        /// <value>
        /// The ground time.
        /// </value>
        public int GroundTime { get; set; }

        /// <summary>
        /// Gets or sets the mile.
        /// </summary>
        /// <value>
        /// The mile.
        /// </value>
        public string Mile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [stop over].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [stop over]; otherwise, <c>false</c>.
        /// </value>
        public bool StopOver { get; set; }

        /// <summary>
        /// Gets or sets the stop point.
        /// </summary>
        /// <value>
        /// The stop point.
        /// </value>
        public string StopPoint { get; set; }

        /// <summary>
        /// Gets or sets the stop point arrival time.
        /// </summary>
        /// <value>
        /// The stop point arrival time.
        /// </value>
        public DateTime StopPointArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the stop point departure time.
        /// </summary>
        /// <value>
        /// The stop point departure time.
        /// </value>
        public DateTime StopPointDepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the craft.
        /// </summary>
        /// <value>
        /// The craft.
        /// </value>
        public string Craft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is e ticket eligible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is e ticket eligible; otherwise, <c>false</c>.
        /// </value>
        public bool IsETicketEligible { get; set; }

        /// <summary>
        /// Gets or sets the flight status.
        /// </summary>
        /// <value>
        /// The flight status.
        /// </value>
        public string FlightStatus { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
    }
}