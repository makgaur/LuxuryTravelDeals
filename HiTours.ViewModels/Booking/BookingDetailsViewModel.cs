// <copyright file="BookingDetailsViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using HiTours.Core;

    /// <summary>
    /// BookingDetailsViewModel
    /// </summary>
    public class BookingDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the name of the hote.
        /// </summary>
        /// <value>
        /// The name of the hote.
        /// </value>
        public string DealName { get; set; }

        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string Country { get; set; }

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
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>
        /// The participants.
        /// </value>
        public int Participants { get; set; }

        /// <summary>
        /// Gets or sets the quotes.
        /// </summary>
        /// <value>
        /// The quotes.
        /// </value>
        public string Quotes { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the room price.
        /// </summary>
        /// <value>
        /// The room price.
        /// </value>
        public decimal HolidayPrice { get; set; }

        /// <summary>
        /// Gets or sets the weekend price.
        /// </summary>
        /// <value>
        /// The weekend price.
        /// </value>
        public decimal WeekendPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the room count.
        /// </summary>
        /// <value>
        /// The room count.
        /// </value>
        public short RoomCount { get; set; }

        /// <summary>
        /// Gets or sets the booking date.
        /// </summary>
        /// <value>
        /// The booking date.
        /// </value>
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the hote.
        /// </summary>
        /// <value>
        /// The name of the hote.
        /// </value>
        public string HoteName { get; set; }

        /// <summary>
        /// Gets or sets the total no of booking.
        /// </summary>
        /// <value>
        /// The total no of booking.
        /// </value>
        public int TotalNoOfBooking { get; set; }

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the GST amount.
        /// </summary>
        /// <value>
        /// The GST amount.
        /// </value>
        public decimal GstAmount { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string RoomType { get; set; }

        /// <summary>
        /// Gets or sets the booking identifier.
        /// </summary>
        /// <value>
        /// The booking identifier.
        /// </value>
        public long BookingId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

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
    }
}