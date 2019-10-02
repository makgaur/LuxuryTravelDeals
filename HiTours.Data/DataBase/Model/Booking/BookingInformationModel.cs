// <copyright file="BookingInformationModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class BookingInformationModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int DealId { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Url.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int NightId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public DateTime Checkin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public DateTime Checkout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Star Rating
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public DateTime BookedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Property Type
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Open Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string LeadFullName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Open Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Open Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Active.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Status.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool IsHold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Sub Status.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public DateTime? HoldFromDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Lat.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public DateTime? HoldToDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Long.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public decimal PackagePrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Long.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public decimal ServiceFees { get; set; }

        /// <summary>
        /// Gets or sets a value to Hotelier Property Type Model
        /// </summary>
        public string DiscountCoupon { get; set; }

        /// <summary>
        /// Gets or sets a value to Hotelier Property Type Model
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public int? DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal GST { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public int Currency { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool FlightRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool FlightSuccessful { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string FlightTraceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool FlightOptionAvailable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool VisaOptionAvailable { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string BookingSummarySerialized { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public DealsPackageModel DealsPackageModel { get; set; }

        /// <summary>
        /// Gets or sets collection of Itinerary Model
        /// </summary>
        public ICollection<BookingHotelRoomModel> BookingHotelRoomModels { get; set; }

        /// <summary>
        /// Gets or sets collection of Itinerary Model
        /// </summary>
        public ICollection<BookingFlightModel> BookingFlightModels { get; set; }

        /// <summary>
        /// Gets or sets collection of Itinerary Model
        /// </summary>
        public ICollection<BookingPassengerModel> BookingPassengerModels { get; set; }

        /// <summary>
        /// Gets or sets collection of Itinerary Model
        /// </summary>
        public ICollection<BookingVisaModel> BookingVisaModels { get; set; }
    }
}
