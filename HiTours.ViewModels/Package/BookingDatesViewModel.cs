// <copyright file="BookingDatesViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Hotel Booking Dates View Model
    /// </summary>
    public class BookingDatesViewModel
    {
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the requested rooms.
        /// </summary>
        /// <value>
        /// The requested rooms.
        /// </value>
        public int RequestedRooms { get; set; }

        /// <summary>
        /// Gets or sets the room per day.
        /// </summary>
        /// <value>
        /// The room per day.
        /// </value>
        public int? RoomPerDay { get; set; }

        /// <summary>
        /// Gets or sets the total available room.
        /// </summary>
        /// <value>
        /// The total available room.
        /// </value>
        public int? TotalAvailableRoom { get; set; }

        /// <summary>
        /// Gets or sets the black out date range.
        /// </summary>
        /// <value>
        /// The black out date range.
        /// </value>
        public List<DateRangeViewModel> BlackOutDateRange { get; set; }

        /// <summary>
        /// Gets or sets the nights.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        public string Nights { get; set; }

        /// <summary>
        /// Gets or sets the discount cost.
        /// </summary>
        /// <value>
        /// The discount cost.
        /// </value>
        public decimal DiscountCost { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        public bool IsExtraNight { get; set; }

        /// <summary>
        /// Gets or sets the booked on dates.
        /// </summary>
        /// <value>
        /// The booked on dates.
        /// </value>
        public List<RoomAvailabilityViewModel> BookedOnDates { get; set; }
    }
}