// <copyright file="ApiChargeBU.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ApiChargeBU
    /// </summary>
    public class ApiChargeBU
    {
        /// <summary>
        /// Gets or sets the tbo mark up.
        /// </summary>
        /// <value>
        /// The tbo mark up.
        /// </value>
        public decimal TBOMarkUp { get; set; }

        /// <summary>
        /// Gets or sets the convenience charge.
        /// </summary>
        /// <value>
        /// The convenience charge.
        /// </value>
        public decimal ConvenienceCharge { get; set; }

        /// <summary>
        /// Gets or sets the other charge.
        /// </summary>
        /// <value>
        /// The other charge.
        /// </value>
        public decimal OtherCharge { get; set; }
    }
}
