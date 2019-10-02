// <copyright file="BookingPassengerViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels.CustomAttributes;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class BookingPassengerViewModel
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
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int BookingId { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public int? PassengerType { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether gets or sets gets or set Hotelier Info view model
        /// </summary>
        public bool PassportRequired { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        [Required(ErrorMessage = "Passport Number Required")]
        [MinLength(6, ErrorMessage = "Invalid Passport Number")]
        public string PassportNumber { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        [Required(ErrorMessage = "Passport Expiry Date Required")]
        public DateTime? PassportExpiryDate { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        [Required(ErrorMessage = "First Name Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alphabets Only")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string FirstName { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        [Required(ErrorMessage = "Last Name Required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alphabets Only")]
        [NotEqual("FirstName", ErrorMessage = "First Name should be different than Last Name")]
        [MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string LastName { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        [Required(ErrorMessage = "Date of Birth Required")]
        public DateTime? DOB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public bool IsLead { get; set; }
    }
}
