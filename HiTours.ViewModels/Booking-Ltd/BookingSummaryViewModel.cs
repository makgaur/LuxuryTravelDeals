// <copyright file="BookingSummaryViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BookingPayment
    /// </summary>
    public class BookingSummaryViewModel
    {
        /// <summary>
        /// Gets or sets the Type of Deal.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public int Nights { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal TotalTax { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal TotalServiceFee { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal TaxPercentage { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal RoomPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public bool IsVisa { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public bool IsFlight { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public List<BookingSummaryVisaInformationViewModel> VisaInformation { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal FlightPrice { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal FlightService { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal FlightTax { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public BookingSummaryPassengerBreakdownViewModel PassengerBreakDown { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public List<BookingHotelRoomViewModel> BookingHotelRooms { get; set; }
    }
}