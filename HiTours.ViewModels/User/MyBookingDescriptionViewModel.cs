// <copyright file="MyBookingDescriptionViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Data.DataBase.Model;
    using HiTours.TBO.Models;
    using HiTours.ViewModels.Deals.Product;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// MyBookingDescriptionViewModel
    /// </summary>
    public class MyBookingDescriptionViewModel
    {
        /// <summary>
        /// Gets or sets productBanner
        /// </summary>
        public ProductBannerViewModel productBanner { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public DealsPackageViewModel dealPackageViewModel { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public DealsContentViewModel dealContentViewModel { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public List<DealsImageViewModel> dealsImageViewModels { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public BookingInformationViewModel bookingInformationViewModel { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public List<BookingHotelRoomViewModel> bookingHotelViewModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Booking Information View Model
        /// </summary>
        public bool visaAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Booking Information View Model
        /// </summary>
        public bool visaRequired { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public List<BookingVisaViewModel> bookingVisaViewModel { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public List<string> LocationNames { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Booking Information View Model
        /// </summary>
        public bool flightAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Booking Information View Model
        /// </summary>
        public bool flightRequired { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public List<BookingFlightViewModel> bookingFlightViewModel { get; set; }

        /// <summary>
        /// Gets or sets Booking Information View Model
        /// </summary>
        public List<TicketLCCResponse> ticketsViewModel { get; set; }
    }
}