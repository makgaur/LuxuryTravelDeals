// <copyright file="CityAreaModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// CityAreaModel
    /// </summary>
    public class CityAreaModel
    {
        /// <summary>
        /// Gets or sets the city area identifier.
        /// </summary>
        /// <value>
        /// The city area identifier.
        /// </value>
        [Key]
        public Guid CityAreaId { get; set; }

        /// <summary>
        /// Gets or sets the city area.
        /// </summary>
        /// <value>
        /// The city area.
        /// </value>
        public string CityArea { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public Guid CityId { get; set; }
    }
}