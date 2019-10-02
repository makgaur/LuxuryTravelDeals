// <copyright file="TourProductViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.Deals.Product.Tour
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels.Deals.Product.Hotel;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///  Vendor Model
    /// </summary>
    public class TourProductViewModel
    {
        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public bool IsFixedDeparture { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<int> NightIds { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<int> InclusionIds { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int FlightInclusionId { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Name { get; set; }

        /// Test Start
        /// <summary>
        /// Gets or sets Latitudes and Longitutdes of all the Hotels in Tour
        /// </summary>
        public List<Tuple<decimal, decimal>> LatLong { get; set; }

        /// <summary>
        /// Gets or sets Full Address of all the Hotels in Tour
        /// </summary>
        public List<string> FullAddress { get; set; }

        /// Test End
        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<string> City { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string AboutDestination { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int MinimumLOS { get; set; }

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
        public string LogoImage { get; set; }

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

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<DealsNightViewModel> DealsNightsViewModels { get; set; }
    }
}