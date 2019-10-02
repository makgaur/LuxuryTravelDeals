// <copyright file="TourPackageModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using HiTours.Models;

    /// <summary>
    /// TourPackageModel
    /// </summary>
    public class TourPackageModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the tour package.
        /// </summary>
        /// <value>
        /// The type of the tour package.
        /// </value>
        public byte TourPackageType { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the deal code.
        /// </summary>
        /// <value>
        /// The deal code.
        /// </value>
        public int DealCode { get; set; }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>
        /// The suffix.
        /// </value>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the visitors.
        /// </summary>
        /// <value>
        /// The visitors.
        /// </value>
        public int? Visitors { get; set; }

        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the URL title.
        /// </summary>
        /// <value>
        /// The URL title.
        /// </value>
        public string UrlTitle { get; set; }

        /// <summary>
        /// Gets or sets the deal type identifier.
        /// </summary>
        /// <value>
        /// The deal type identifier.
        /// </value>
        public string DealTypeId { get; set; }

        /// <summary>
        /// Gets or sets the package valid from.
        /// </summary>
        /// <value>
        /// The package valid from.
        /// </value>
        public DateTime PackageValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the package valid to.
        /// </summary>
        /// <value>
        /// The package valid to.
        /// </value>
        public DateTime PackageValidTo { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int? HotelId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is flight included.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is flight included; otherwise, <c>false</c>.
        /// </value>
        public bool IsFlightIncluded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Hotel Only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel only; otherwise, <c>false</c>.
        /// </value>
        public bool IsHotelOnly { get; set; }

        /// <summary>
        /// Gets or sets the package description.
        /// </summary>
        /// <value>
        /// The package description.
        /// </value>
        public string PackageDescription { get; set; }

        /// <summary>
        /// Gets or sets the quote.
        /// </summary>
        /// <value>
        /// The quote.
        /// </value>
        public string Quote { get; set; }

        /// <summary>
        /// Gets or sets the high lights.
        /// </summary>
        /// <value>
        /// The high lights.
        /// </value>
        public string HighLights { get; set; }

        /// <summary>
        /// Gets or sets the programs.
        /// </summary>
        /// <value>
        /// The programs.
        /// </value>
        public string Programs { get; set; }

        /// <summary>
        /// Gets or sets the hotel description.
        /// </summary>
        /// <value>
        /// The hotel description.
        /// </value>
        public string HotelDescription { get; set; }

        /// <summary>
        /// Gets or sets the hotel review.
        /// </summary>
        /// <value>
        /// The hotel review.
        /// </value>
        public string HotelReview { get; set; }

        /// <summary>
        /// Gets or sets the map script.
        /// </summary>
        /// <value>
        /// The map script.
        /// </value>
        public string MapScript { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public ICollection<TourPackageCityModel> TourPackageCity { get; set; }

        /// <summary>
        /// Gets or sets the tour package book date.
        /// </summary>
        /// <value>
        /// The tour package book date.
        /// </value>
        public ICollection<TourPackageBookDateModel> TourPackageBookDate { get; set; }

        /// <summary>
        /// Gets or sets the package travel style.
        /// </summary>
        /// <value>
        /// The package travel style.
        /// </value>
        public ICollection<TourPackageTravelStyleModel> TourPackageTravelStyle { get; set; }

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public ICollection<TourPackageNightModel> TourPackageNight { get; set; }

        /// <summary>
        /// Gets or sets the tour package image.
        /// </summary>
        /// <value>
        /// The tour package image.
        /// </value>
        public ICollection<TourPackageImageModel> TourPackageImage { get; set; } = new List<TourPackageImageModel>();

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public ICollection<DestinationModel> DestinationModels { get; set; }
    }
}