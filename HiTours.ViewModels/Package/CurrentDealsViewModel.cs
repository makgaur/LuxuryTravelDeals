// <copyright file="CurrentDealsViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using HiTours.Core;

    /// <summary>
    /// Current Deals View Model
    /// </summary>
    public class CurrentDealsViewModel
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
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the fit double cost.
        /// </summary>
        /// <value>
        /// The fit double cost.
        /// </value>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the valid to.
        /// </summary>
        /// <value>
        /// The valid to.
        /// </value>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the nights.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        public string Nights { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets IsHotelOnly.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public bool IsHotelOnly { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the deal quotes.
        /// </summary>
        /// <value>
        /// The deal quotes.
        /// </value>
        public string DealQuotes { get; set; }

        /// <summary>
        /// Gets or sets the total booked.
        /// </summary>
        /// <value>
        /// The total booked.
        /// </value>
        public int TotalBooked { get; set; }

        /// <summary>
        /// Gets or sets the strike price.
        /// </summary>
        /// <value>
        /// The strike price.
        /// </value>
        public decimal StrikePrice { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Enums.CurrentDealType Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string[] CountryNameList { get; set; }

        /// <summary>
        /// Gets or sets the city list.
        /// </summary>
        /// <value>
        /// The city list.
        /// </value>
        public string[] CityList { get; set; }

        /// <summary>
        /// Gets or sets the hotel validity identifier.
        /// </summary>
        /// <value>
        /// The hotel validity identifier.
        /// </value>
        public Guid? HotelValidityId { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public string DealType { get; set; }

        /// <summary>
        /// Gets or sets the visitors.
        /// </summary>
        /// <value>
        /// The visitors.
        /// </value>
        public int? Visitors { get; set; }

        /// <summary>
        /// Gets or sets the travel style.
        /// </summary>
        /// <value>
        /// The travel style.
        /// </value>
        public List<int> TravelStyle { get; set; }

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
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string[] StateNameList { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>
        /// The name of the region.
        /// </value>
        public string[] RegionNameList { get; set; }

        ///// <summary>
        ///// Gets or sets the created date.
        ///// </summary>
        ///// <value>
        ///// The created date.
        ///// </value>
        ////public DateTime CreatedDate { get; set; }
    }
}