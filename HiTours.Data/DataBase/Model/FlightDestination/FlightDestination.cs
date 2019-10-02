// <copyright file="FlightDestination.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// FlightDestination
    /// </summary>
    public class FlightDestination
    {
        /// <summary>
        /// Gets or sets the row no.
        /// </summary>
        /// <value>
        /// The row no.
        /// </value>
        [Key]
        public int RowNo { get; set; }

        /// <summary>
        /// Gets or sets the city code.
        /// </summary>
        /// <value>
        /// The city code.
        /// </value>
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the short detail.
        /// </summary>
        /// <value>
        /// The short detail.
        /// </value>
        public string ShortDetail { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the search in.
        /// </summary>
        /// <value>
        /// The search in.
        /// </value>
        [NotMapped]
        public string SearchIn { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [NotMapped]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [NotMapped]
        [JsonProperty(PropertyName = "text")]
        public string Name { get; set; }
    }
}