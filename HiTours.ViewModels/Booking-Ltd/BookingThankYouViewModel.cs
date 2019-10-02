// <copyright file="BookingThankYouViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BookingPayment
    /// </summary>
    public class BookingThankYouViewModel
    {
        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public string LeadName { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public string CardImage { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public string DealName { get; set; }

        /// <summary>
        /// Gets or sets the data key.
        /// </summary>
        /// <value>
        /// The data key.
        /// </value>
        public int DealType { get; set; }

        /// <summary>
        /// Gets or sets the name of the data.
        /// </summary>
        /// <value>
        /// The name of the data.
        /// </value>
        public List<string> Locations { get; set; }

        /// <summary>
        /// Gets or sets the data description.
        /// </summary>
        /// <value>
        /// The data description.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the data order identifier.
        /// </summary>
        /// <value>
        /// The data order identifier.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the data order identifier.
        /// </summary>
        /// <value>
        /// The data order identifier.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the data prefill.
        /// </summary>
        /// <value>
        /// The name of the data prefill.
        /// </value>
        public int Adults { get; set; }

        /// <summary>
        /// Gets or sets the name of the data prefill.
        /// </summary>
        /// <value>
        /// The name of the data prefill.
        /// </value>
        public int Childs { get; set; }

        /// <summary>
        /// Gets or sets the name of the data prefill.
        /// </summary>
        /// <value>
        /// The name of the data prefill.
        /// </value>
        public int Infants { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public bool BookingStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public bool FlightOpted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public bool VisaOpted { get; set; }

        /// <summary>
        /// Gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public string BookingStatusMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public bool PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public string PaymentStatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public int BookingId { get; set; }

        /// <summary>
        /// Gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public string BookingReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public List<string> Highlights { get; set; }

        /// <summary>
        /// Gets or sets the data prefill contact.
        /// </summary>
        /// <value>
        /// The data prefill contact.
        /// </value>
        public string BookingSummarySerialized { get; set; }
    }
}