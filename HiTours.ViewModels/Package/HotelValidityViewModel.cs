// <copyright file="HotelValidityViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;

    /// <summary>
    /// Hotel Validity View Model
    /// </summary>
    public class HotelValidityViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
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