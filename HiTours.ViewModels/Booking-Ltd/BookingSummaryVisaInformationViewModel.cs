// <copyright file="BookingSummaryVisaInformationViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;

    /// <summary>
    /// BookingPayment
    /// </summary>
    public class BookingSummaryVisaInformationViewModel
    {
        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public int VisaId { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal ServiceFee { get; set; }

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
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public decimal TotalPriceITax { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public int Count { get; set; }
    }
}