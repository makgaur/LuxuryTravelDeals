// <copyright file="UpComingDealsViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using HiTours.Core;

    /// <summary>
    /// UpComingDealsViewModel
    /// </summary>
    public class UpComingDealsViewModel
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reminded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is reminded; otherwise, <c>false</c>.
        /// </value>
        public bool IsReminded { get; set; }

        /// <summary>
        /// Gets or sets the name of the images.
        /// </summary>
        /// <value>
        /// The name of the images.
        /// </value>
        public string ImagesName { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the deal quotes.
        /// </summary>
        /// <value>
        /// The deal quotes.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the deal start date.
        /// </summary>
        /// <value>
        /// The deal start date.
        /// </value>
        public DateTime DealStartDate { get; set; }

        /// <summary>
        /// Gets or sets the user emails.
        /// </summary>
        /// <value>
        /// The user emails.
        /// </value>
        public IList<string> UserEmails { get; set; }

        /// <summary>
        /// Gets or sets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets the total booked.
        /// </summary>
        /// <value>
        /// The total booked.
        /// </value>
        public int TotalBooked { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Enums.CurrentDealType Type { get; set; }
    }
}