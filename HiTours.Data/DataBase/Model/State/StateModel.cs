// <copyright file="StateModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// StateModel
    /// </summary>
    public class StateModel
    {
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        [Key]
        public Guid StateId { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string StateName { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets the luxury tax.
        /// </summary>
        /// <value>
        /// The luxury tax.
        /// </value>
        public double LuxuryTax { get; set; }

        /// <summary>
        /// Gets or sets the hotelvat.
        /// </summary>
        /// <value>
        /// The hotelvat.
        /// </value>
        public double Hotelvat { get; set; }

        /// <summary>
        /// Gets or sets the meal service charges.
        /// </summary>
        /// <value>
        /// The meal service charges.
        /// </value>
        public double MealServiceCharges { get; set; }

        /// <summary>
        /// Gets or sets the mealvat.
        /// </summary>
        /// <value>
        /// The mealvat.
        /// </value>
        public double Mealvat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hotel tax on publish rate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel tax on publish rate; otherwise, <c>false</c>.
        /// </value>
        public bool IsHotelTaxOnPublishRate { get; set; }

        /// <summary>
        /// Gets or sets the dvat.
        /// </summary>
        /// <value>
        /// The dvat.
        /// </value>
        public double DVAT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not indian hotel.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is not indian hotel; otherwise, <c>false</c>.
        /// </value>
        public bool IsNotIndianHotel { get; set; }

        /// <summary>
        /// Gets or sets the transport tax.
        /// </summary>
        /// <value>
        /// The transport tax.
        /// </value>
        public double TransportTax { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the hotel other tax.
        /// </summary>
        /// <value>
        /// The hotel other tax.
        /// </value>
        public double HotelOtherTax { get; set; }

        /// <summary>
        /// Gets or sets the meal other tax.
        /// </summary>
        /// <value>
        /// The meal other tax.
        /// </value>
        public double MealOtherTax { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public byte Code { get; set; }
    }
}