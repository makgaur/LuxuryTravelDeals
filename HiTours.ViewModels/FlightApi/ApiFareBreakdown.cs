// <copyright file="ApiFareBreakdown.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ApiFareBreakdown
    /// </summary>
    public class ApiFareBreakdown
    {
        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the type of the passenger.
        /// </summary>
        /// <value>
        /// The type of the passenger.
        /// </value>
        public string PassengerType { get; set; }

        /// <summary>
        /// Gets or sets the passenger count.
        /// </summary>
        /// <value>
        /// The passenger count.
        /// </value>
        public int PassengerCount { get; set; }

        /// <summary>
        /// Gets or sets the base fare.
        /// </summary>
        /// <value>
        /// The base fare.
        /// </value>
        public decimal BaseFare { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets the yq tax.
        /// </summary>
        /// <value>
        /// The yq tax.
        /// </value>
        public decimal YQTax { get; set; }

        /// <summary>
        /// Gets or sets the additional TXN fee o.
        /// </summary>
        /// <value>
        /// The additional TXN fee o.
        /// </value>
        public decimal AdditionalTxnFeeOfrd { get; set; }

        /// <summary>
        /// Gets or sets the additional TXN fee p.
        /// </summary>
        /// <value>
        /// The additional TXN fee p.
        /// </value>
        public decimal AdditionalTxnFeePub { get; set; }
    }
}
