// <copyright file="TourPackageDetailModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TourPackageDetailModel
    /// </summary>
    public class TourPackageDetailModel
    {
        /// <summary>
        /// Gets or sets the tour package.
        /// </summary>
        /// <value>
        /// The tour package.
        /// </value>
        public TourPackageModel TourPackage { get; set; }

        /// <summary>
        /// Gets or sets the tour package image.
        /// </summary>
        /// <value>
        /// The tour package image.
        /// </value>
        public IEnumerable<TourPackageImageModel> TourPackageImages { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights.
        /// </summary>
        /// <value>
        /// The tour package nights.
        /// </value>
        public IEnumerable<TourPackageNightModel> TourPackageNights { get; set; }

        /// <summary>
        /// Gets or sets the total booked.
        /// </summary>
        /// <value>
        /// The total booked.
        /// </value>
        public int TotalBooked { get; set; }

        /// <summary>
        /// Gets or sets the tour package book date.
        /// </summary>
        /// <value>
        /// The tour package book date.
        /// </value>
        public TourPackageBookDateModel TourPackageBookDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is up comming deal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is up comming deal; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpCommingDeal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deal started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deal started; otherwise, <c>false</c>.
        /// </value>
        public bool IsDealStarted { get; set; }

        /// <summary>
        /// Gets or sets the valid to.
        /// </summary>
        /// <value>
        /// The valid to.
        /// </value>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the valid from.
        /// </summary>
        /// <value>
        /// The valid from.
        /// </value>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public string DealType { get; set; }
    }
}