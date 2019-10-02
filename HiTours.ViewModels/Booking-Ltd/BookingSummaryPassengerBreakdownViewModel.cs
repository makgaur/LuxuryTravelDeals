// <copyright file="BookingSummaryPassengerBreakdownViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;

    /// <summary>
    /// BookingPayment
    /// </summary>
    public class BookingSummaryPassengerBreakdownViewModel
    {
        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public int Adults { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public int Childs { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public int Infants { get; set; }
    }
}