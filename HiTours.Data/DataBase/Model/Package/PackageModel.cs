// <copyright file="PackageModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using HiTours.Core;

    /// <summary>
    /// Package Model
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class PackageModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the deal type identifier.
        /// </summary>
        /// <value>
        /// The deal type identifier.
        /// </value>
        public Guid DealTypeId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the valid from.
        /// </summary>
        /// <value>
        /// The valid from.
        /// </value>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the valid to.
        /// </summary>
        /// <value>
        /// The valid to.
        /// </value>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the deal quotes.
        /// </summary>
        /// <value>
        /// The deal quotes.
        /// </value>
        public string DealQuotes { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the nights.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        public string Nights { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public Guid CityId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Gets or sets the hotel validity identifier.
        /// </summary>
        /// <value>
        /// The hotel validity identifier.
        /// </value>
        public Guid? HotelValidityId { get; set; }

        /// <summary>
        /// Gets or sets the hotel price.
        /// </summary>
        /// <value>
        /// The hotel price.
        /// </value>
        public decimal HotelPrice { get; set; }

        /// <summary>
        /// Gets or sets the hight lights.
        /// </summary>
        /// <value>
        /// The hight lights.
        /// </value>
        public string HighLights { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        public bool IsExtraNight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delete; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Gets or sets the package images.
        /// </summary>
        /// <value>
        /// The package images.
        /// </value>
        public List<PackageImageModel> PackageImages { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the postfix.
        /// </summary>
        /// <value>
        /// The postfix.
        /// </value>
        public string Postfix { get; set; }

        /// <summary>
        /// Gets or sets the deal code.
        /// </summary>
        /// <value>
        /// The deal code.
        /// </value>
        public int DealCode { get; set; }

        /// <summary>
        /// Gets or sets the hotel review.
        /// </summary>
        /// <value>
        /// The hotel review.
        /// </value>
        public string HotelReview { get; set; }
    }
}