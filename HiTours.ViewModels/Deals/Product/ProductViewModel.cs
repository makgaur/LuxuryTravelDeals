// <copyright file="ProductViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels.Deals.Product.Hotel;
    using HiTours.ViewModels.Deals.Product.Tour;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class ProductViewModel : BookingViewModel
    {
        /// <summary>
        ///  Gets or sets the Key
        /// </summary>
        public string GoogleApiKey { get; set; }

        /// <summary>
        ///  Gets or sets the Type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        ///  Gets or sets the HotelProductViewModel
        /// </summary>
        public HotelProductViewModel HotelProductViewModel { get; set; }

        /// <summary>
        ///  Gets or sets the HotelProductViewModel
        /// </summary>
        public TourProductViewModel TourProductViewModel { get; set; }

        /// <summary>
        ///  Gets or sets the HotelProductViewModel
        /// </summary>
        public BookingSummaryViewModel BookingSummaryViewModel { get; set; }

        /// <summary>
        ///  Gets or sets the HotelProductViewModel
        /// </summary>
        public List<DealsPromotionViewModel> DealsPromotionViewModels { get; set; }

        /// <summary>
        ///  Gets or sets the HotelProductViewModel
        /// </summary>
        public string BookingSummaryViewModelString { get; set; }

        /// <summary>
        ///  Gets or sets the HotelProductViewModel
        /// </summary>
        public int TotalPassengers { get; set; }

        /// <summary>
        ///  Gets or sets the HotelProductViewModel
        /// </summary>
        public string ModelSerialized { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether gets or sets the HotelProductViewModel
        /// </summary>
        public bool FlightRequired { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether gets or sets the HotelProductViewModel
        /// </summary>
        public string FlightRender { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether gets or sets the HotelProductViewModel
        /// </summary>
        public string SummaryRendered { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether gets or sets the HotelProductViewModel
        /// </summary>
        public string razorpay_payment_id { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether gets or sets the HotelProductViewModel
        /// </summary>
        public string razorpay_order_id { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether gets or sets the HotelProductViewModel
        /// </summary>
        public string razorpay_signature { get; set; }
    }
}
