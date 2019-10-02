// <copyright file="HotelBookingPersonDetailModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HiTours.Core;

    /// <summary>
    /// HotelBookingPersonDetailModel
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class HotelBookingPersonDetailModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the row no.
        /// </summary>
        /// <value>
        /// The row no.
        /// </value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RowNo { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking identifier.
        /// </summary>
        /// <value>
        /// The hotel booking identifier.
        /// </value>
        public Guid HotelBookingId { get; set; }

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
        /// Gets or sets the type of the person.
        /// </summary>
        /// <value>
        /// The type of the person.
        /// </value>
        public string PersonType { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public byte Age { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancelled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cancelled; otherwise, <c>false</c>.
        /// </value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the room no.
        /// </summary>
        /// <value>
        /// The room no.
        /// </value>
        public short RoomNo { get; set; }

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
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address.
        /// </value>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address.
        /// </value>
        public string BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        /// <value>
        /// The pin code.
        /// </value>
        public string PinCode { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public string CityCode { get; set; }

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
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
    }
}