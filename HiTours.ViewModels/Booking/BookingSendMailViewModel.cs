// <copyright file="BookingSendMailViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using HiTours.Core;

    /// <summary>
    /// Booking Send Mail View Model
    /// </summary>
    public class BookingSendMailViewModel
    {
        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public string DealName { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the hotel city.
        /// </summary>
        /// <value>
        /// The hotel city.
        /// </value>
        public string HotelCity { get; set; }

        /// <summary>
        /// Gets or sets the hotel city code.
        /// </summary>
        /// <value>
        /// Prop Using in Api Data.
        /// </value>
        public string HotelCityCode { get; set; }

        /// <summary>
        /// Gets or sets the hotel country.
        /// </summary>
        /// <value>
        /// The hotel country.
        /// </value>
        public string HotelCountry { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the Booking Date.
        /// </summary>
        /// <value>
        /// The Booking Date.
        /// </value>
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Gets or sets the deal code.
        /// </summary>
        /// <value>
        /// The deal code.
        /// </value>
        public string DealCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string RoomType { get; set; }

        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>
        /// The salutation.
        /// </value>
        public string Salutation { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the pincode.
        /// </summary>
        /// <value>
        /// The pincode.
        /// </value>
        public string PinCode { get; set; }

        /// <summary>
        /// Gets or sets the room cost.
        /// </summary>
        /// <value>
        /// The room cost.
        /// </value>
        public decimal RoomCost { get; set; }

        /// <summary>
        /// Gets or sets the GST amount.
        /// </summary>
        /// <value>
        /// The GST amount.
        /// </value>
        public decimal GstAmount { get; set; }

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
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>
        /// The participants.
        /// </value>
        public string Participants { get; set; }

        /// <summary>
        /// Gets or sets the person detail.
        /// </summary>
        /// <value>
        /// The person detail.
        /// </value>
        public List<HotelBookingPersonDetailViewModel> PersonDetail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is contact detail.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is contact detail; otherwise, <c>false</c>.
        /// </value>
        public bool IsContactDetail { get; set; }

        /// <summary>
        /// Gets or sets the type of the booking.
        /// </summary>
        /// <value>
        /// The type of the booking.
        /// </value>
        public Enums.CurrentDealType BookingType { get; set; }

        /// <summary>
        /// Gets or sets the paid amount.
        /// </summary>
        /// <value>
        /// The paid amount.
        /// </value>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Gets or sets the hotel price identifier.
        /// </summary>
        /// <value>
        /// The hotel price identifier.
        /// </value>
        public Guid HotelPriceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the depart city.
        /// </summary>
        /// <value>
        /// The name of the depart city.
        /// </value>
        public string DepartCityName { get; set; }

        /// <summary>
        /// Gets or sets the depart city code.
        /// </summary>
        /// <value>
        /// User Property for Api Result.
        /// </value>
        public string DepartCityCode { get; set; }

        /// <summary>
        /// Gets or sets the booking from.
        /// </summary>
        /// <value>
        /// The booking from.
        /// </value>
        public DateTime BookingFrom { get; set; }

        /// <summary>
        /// Gets or sets the total participant.
        /// </summary>
        /// <value>
        /// The total participant.
        /// </value>
        public int TotalParticipant { get; set; }

        /// <summary>
        /// Gets or sets the booking identifier.
        /// </summary>
        /// <value>
        /// The booking identifier.
        /// </value>
        public long BookingId { get; set; }
    }
}