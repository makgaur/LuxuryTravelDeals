// <copyright file="HotelValidityModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Hotel Validity Model
    /// </summary>
    public class HotelValidityModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Gets or sets the validity date from.
        /// </summary>
        /// <value>
        /// The validity date from.
        /// </value>
        public DateTime ValidityDateFrom { get; set; }

        /// <summary>
        /// Gets or sets the validity date to.
        /// </summary>
        /// <value>
        /// The validity date to.
        /// </value>
        public DateTime ValidityDateTo { get; set; }
    }
}