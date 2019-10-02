// <copyright file="MarketModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;

    /// <summary>
    /// MarketModel
    /// </summary>
    public class MarketModel
    {
        /// <summary>
        /// Gets or sets the market identifier.
        /// </summary>
        /// <value>
        /// The market identifier.
        /// </value>
        public Guid MarketID { get; set; }

        /// <summary>
        /// Gets or sets the name of the market.
        /// </summary>
        /// <value>
        /// The name of the market.
        /// </value>
        public string MarketName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public short SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }
    }
}