// <copyright file="UserDetailModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using HiTours.Core;

    /// <summary>
    /// User Details Model
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class UserDetailModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the mobile no.
        /// </summary>
        /// <value>
        /// The mobile no.
        /// </value>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the nationality identifier.
        /// </summary>
        /// <value>
        /// The nationality identifier.
        /// </value>
        public int? NationalityId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the passport no.
        /// </summary>
        /// <value>
        /// The passport no.
        /// </value>
        public string PassportNo { get; set; }

        /// <summary>
        /// Gets or sets the countryof issue identifier.
        /// </summary>
        /// <value>
        /// The countryof issue identifier.
        /// </value>
        public int? CountryofIssueId { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the data from.
        /// </summary>
        /// <value>
        /// The data from.
        /// </value>
        public string DetailType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delete; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is guest.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is guest; otherwise, <c>false</c>.
        /// </value>
        public bool IsGuest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating otp.
        /// </summary>
        /// /// <value>
        /// OTP.
        /// </value>
        public string OTP { get; set; }

        /// <summary>
        /// Gets or sets the OTP expiry datetime.
        /// </summary>
        /// <value>
        /// The OTP expiry datetime.
        /// </value>
        public DateTime? OtpExpiryDate { get; set; }
    }
}