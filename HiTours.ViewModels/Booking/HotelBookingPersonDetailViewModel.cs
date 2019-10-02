// <copyright file="HotelBookingPersonDetailViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// HotelBookingPersonDetailViewModel
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class HotelBookingPersonDetailViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the row no.
        /// </summary>
        /// <value>
        /// The row no.
        /// </value>
        [Display(Name = "Row No")]
        public long RowNo { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking identifier.
        /// </summary>
        /// <value>
        /// The hotel booking identifier.
        /// </value>
        [Display(Name = "Hotel Booking Id")]
        public Guid HotelBookingId { get; set; }

        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>
        /// The salutation.
        /// </value>
        [StringLength(15)]
        [Display(Name = "Salutation")]
        public string Salutation { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [StringLength(75)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [StringLength(75)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the type of the person.
        /// </summary>
        /// <value>
        /// The type of the person.
        /// </value>
        [StringLength(10)]
        [Display(Name = "Person Type")]
        public string PersonType { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        [Display(Name = "Age")]
        public byte Age { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancelled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cancelled; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Cancelled")]
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the room no.
        /// </summary>
        /// <value>
        /// The room no.
        /// </value>
        public short RoomNo { get; set; }

        /// <summary>
        /// Gets or sets the person no.
        /// </summary>
        /// <value>
        /// The person no.
        /// </value>
        public int Adults { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [Display(Name = "Gender")]
        [StringLength(11)]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Display(Name = "Email")]
        [StringLength(125)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address.
        /// </value>
        [Display(Name = "Mobile")]
        [StringLength(10)]
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address.
        /// </value>
        [Display(Name = "Billing Address")]
        [StringLength(4000)]
        public string BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        /// <value>
        /// The pin code.
        /// </value>
        [Display(Name = "Pin Code")]
        [StringLength(6)]
        public string PinCode { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        [Display(Name = "City")]
        [StringLength(6)]
        public string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Display(Name = "City")]
        [StringLength(150)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Display(Name = "Country")]
        [StringLength(6)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Display(Name = "Country")]
        [StringLength(150)]
        public string Country { get; set; }
    }
}