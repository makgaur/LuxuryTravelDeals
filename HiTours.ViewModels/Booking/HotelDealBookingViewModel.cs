// <copyright file="HotelDealBookingViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// HotelDealBookingViewModel
    /// </summary>
    public class HotelDealBookingViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the package.
        /// </summary>
        /// <value>
        /// The package.
        /// </value>
        public PackageDeatilsViewModel Package { get; set; }

        /// <summary>
        /// Gets or sets the tour package.
        /// </summary>
        /// <value>
        /// The tour package.
        /// </value>
        public TourPackageDetailViewModel TourPackageDetail { get; set; }

        /// <summary>
        /// Gets or sets the nights.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        public int Nights { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>
        /// The days.
        /// </value>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        public int Rooms { get; set; }

        /// <summary>
        /// Gets or sets the room price.
        /// </summary>
        /// <value>
        /// The room price.
        /// </value>
        public decimal RoomPrice { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>
        /// The participants.
        /// </value>
        public int TotalAdults { get; set; }

        /// <summary>
        /// Gets or sets the participant details.
        /// </summary>
        /// <value>
        /// The participant details.
        /// </value>
        public string AdultDescriptions { get; set; }

        /// <summary>
        /// Gets or sets the check in date.
        /// </summary>
        /// <value>
        /// The check in date.
        /// </value>
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// Gets or sets the check out date.
        /// </summary>
        /// <value>
        /// The check out date.
        /// </value>
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// Gets or sets the booking fees.
        /// </summary>
        /// <value>
        /// The booking fees.
        /// </value>
        public decimal BookingFees { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public decimal GstAmount { get; set; }

        /// <summary>
        /// Gets or sets the GST percent.
        /// </summary>
        /// <value>
        /// The GST percent.
        /// </value>
        public decimal GstPercent { get; set; }

        /// <summary>
        /// Gets or sets the booking price.
        /// </summary>
        /// <value>
        /// The booking price.
        /// </value>
        public decimal BookingPrice { get; set; }

        /// <summary>
        /// Gets or sets the weekend price.
        /// </summary>
        /// <value>
        /// The weekend price.
        /// </value>
        public decimal WeekendPrice { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking.
        /// </summary>
        /// <value>
        /// The hotel booking.
        /// </value>
        public HotelBookingViewModel HotelBooking { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking person details.
        /// </summary>
        /// <value>
        /// The hotel booking person details.
        /// </value>
        public List<PassengerDetailsViewModel> PassengerDetails { get; set; }

        /// <summary>
        /// Gets or sets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets the hotel price identifier.
        /// </summary>
        /// <value>
        /// The hotel price identifier.
        /// </value>
        public Guid HotelPriceId { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        public Guid RoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [terms and conditions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [terms and conditions]; otherwise, <c>false</c>.
        /// </value>
        public bool TermsAndConditions { get; set; } = true;

        /// <summary>
        /// Gets or sets the booked on dates.
        /// </summary>
        /// <value>
        /// The booked on dates.
        /// </value>
        public List<RoomAvailabilityViewModel> BookedOnDates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rate increase by per.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rate increase by per; otherwise, <c>false</c>.
        /// </value>
        public bool IsRateIncreaseByPer { get; set; }

        /// <summary>
        /// Gets or sets the rate increase.
        /// </summary>
        /// <value>
        /// The rate increase.
        /// </value>
        public decimal? RateIncrease { get; set; }

        /// <summary>
        /// Gets or sets the package night identifier.
        /// </summary>
        /// <value>
        /// The package night identifier.
        /// </value>
        public Guid PackageNightId { get; set; }

        /// <summary>
        /// Gets or sets the package nights validity identifier.
        /// </summary>
        /// <value>
        /// The package nights validity identifier.
        /// </value>
        public Guid PackageNightsValidityId { get; set; }

        /// <summary>
        /// Gets or sets the type of the booking package.
        /// </summary>
        /// <value>
        /// The type of the booking package.
        /// </value>
        public string BookingPackageType { get; set; }

        /// <summary>
        /// Gets or sets the pay amount.
        /// </summary>
        /// <value>
        /// The pay amount.
        /// </value>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Gets or sets the deposite amount.
        /// </summary>
        /// <value>
        /// The deposite amount.
        /// </value>
        public decimal DepositeAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is add flight.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is add flight; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Do you want to book Flight?")]
        public bool IsAddFlight { get; set; }

        /// <summary>
        /// Gets or sets the URL title.
        /// </summary>
        /// <value>
        /// The URL title.
        /// </value>
        public string UrlTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is flight process.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is flight process; otherwise, <c>false</c>.
        /// </value>
        public bool IsFlightProcess { get; set; }

        /// <summary>
        /// Gets or sets the hotel room type identifier.
        /// </summary>
        /// <value>
        /// The hotel room type identifier.
        /// </value>
        public int HotelRoomTypeId { get; set; }
    }
}