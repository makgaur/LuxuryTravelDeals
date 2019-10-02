// <copyright file="PackageDeatilsViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// PackageFrontEndViewModel    /// </summary>
    public class PackageDeatilsViewModel
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
        public string PackageName { get; set; }

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
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the single cost.
        /// </summary>
        /// <value>
        /// The single cost.
        /// </value>
        public decimal DoubleCost { get; set; }

        /// <summary>
        /// Gets or sets the single cost.
        /// </summary>
        /// <value>
        /// The single cost.
        /// </value>
        public decimal DiscountCost { get; set; }

        /// <summary>
        /// Gets or sets the fit double cost.
        /// </summary>
        /// <value>
        /// The fit double cost.
        /// </value>
        public double? FitDoubleCost { get; set; }

        /// <summary>
        /// Gets or sets the package images.
        /// </summary>
        /// <value>
        /// The package images.
        /// </value>
        public List<PackageImageViewModel> PackageImages { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the high lights.
        /// </summary>
        /// <value>
        /// The high lights.
        /// </value>
        public string HighLights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [deal started].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [deal started]; otherwise, <c>false</c>.
        /// </value>
        public bool IsDealStarted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is up comming deal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is up comming deal; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpCommingDeal { get; set; }

        /// <summary>
        /// Gets or sets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        public bool IsExtraNight { get; set; }

        /// <summary>
        /// Gets or sets the extra bed cost.
        /// </summary>
        /// <value>
        /// The extra bed cost.
        /// </value>
        public decimal ExtraBedCost { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }

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
        /// Gets or sets the hotel review.
        /// </summary>
        /// <value>
        /// The hotel review.
        /// </value>
        public string HotelReview { get; set; }

        /// <summary>
        /// Gets or sets the type of the hotel room.
        /// </summary>
        /// <value>
        /// The type of the hotel room.
        /// </value>
        public IList<HotelRoomTypeViewModel> HotelRoomTypes { get; set; }

        /// <summary>
        /// Gets or sets the total booked.
        /// </summary>
        /// <value>
        /// The total booked.
        /// </value>
        public int TotalBooked { get; set; }
    }
}