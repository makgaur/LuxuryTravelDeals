// <copyright file="BookingPassengerModel.cs" company="Luxury Travel Deals">
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
    public class BookingPassengerModel
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
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int BookingId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public bool IsLead { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public int? PassengerType { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public DateTime? PassportExpiryDate { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public DateTime? DOB { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public BookingInformationModel BookingInformationModel { get; set; }
    }
}
