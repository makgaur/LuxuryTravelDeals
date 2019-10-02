// <copyright file="HotelProductViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.Deals.Product.Hotel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///  Vendor Model
    /// </summary>
    public class HotelProductViewModel
    {
        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int NightId { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int InclusionId { get; set; }

        /// <summary>
        /// Gets or sets full address of the hotel
        /// </summary>
        public string FullAddress { get; set; }

        /// <summary>
        /// Gets or sets latitude of hotel
        /// </summary>
        public decimal? Lat { get; set; }

        /// <summary>
        /// Gets or sets longitute of the hotel
        /// </summary>
        public decimal? Long { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string AboutHotel { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string HotelLogo { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string AboutImage { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int MinimumLOS { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string TripadvisorLogo { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string TripadvisorLink { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public decimal Overallrating { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public decimal Cleanlinessrating { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public decimal Locationrating { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public decimal Valuerating { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string CardImage { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int ReviewCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public bool FlightIncluded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public decimal PriceWithoutFlight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public decimal RackPriceWithoutFlight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public decimal PriceWithFlight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public decimal RackPriceWithFlight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public Tuple<int, string> TravelStyle { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public ProductBannerViewModel BannerViewModel { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<HotelHighlightsViewModel> HighlightsViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<DealsRatePlanViewModel> RatePlanViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<DealVisaViewModel> DealVisaViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<HotelAmenetiesViewModel> HotelAmenetiesViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<ProductReviewViewModel> ProductReviewViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<ProductReviewViewModel> AllReviewViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public DealsFlightViewModel DealsFlightViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public ImageGalleryViewModel ImageGalleryViewModel { get; set; }
    }
}