// <copyright file="RecentViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using HiTours.Core;

    /// <summary>
    /// Recent View Model
    /// </summary>
    public class RecentViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the URL title.
        /// </summary>
        /// <value>
        /// The URL title.
        /// </value>
        public string UrlTitle { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Enums.CurrentDealType Type { get; set; }
    }
}