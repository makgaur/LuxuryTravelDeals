// <copyright file="HotelBookingViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// HotelBookingViewModel
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class HotelBookingViewModel : BaseModel
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Display(Name = "User")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the booking date.
        /// </summary>
        /// <value>
        /// The booking date.
        /// </value>
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Gets or sets the block date.
        /// </summary>
        /// <value>
        /// The block date.
        /// </value>
        [Display(Name = "Block Date")]
        public DateTime BlockDate { get; set; }

        /// <summary>
        /// Gets or sets the book from date.
        /// </summary>
        /// <value>
        /// The book from date.
        /// </value>
        [Display(Name = "Book From Date")]
        public DateTime BookFromDate { get; set; }

        /// <summary>
        /// Gets or sets the book to date.
        /// </summary>
        /// <value>
        /// The book to date.
        /// </value>
        [Display(Name = "Book To Date")]
        public DateTime BookToDate { get; set; }

        /// <summary>
        /// Gets or sets the packages identifier.
        /// </summary>
        /// <value>
        /// The packages identifier.
        /// </value>
        [Display(Name = "Packages")]
        public Guid PackagesId { get; set; }

        /// <summary>
        /// Gets or sets the hotel room night target identifier.
        /// </summary>
        /// <value>
        /// The hotel room night target identifier.
        /// </value>
        [Display(Name = "Hotel Room Night Target")]
        public Guid HotelRoomNightTargetId { get; set; }

        /// <summary>
        /// Gets or sets the room count.
        /// </summary>
        /// <value>
        /// The room count.
        /// </value>
        [Display(Name = "Room Count")]
        public short RoomCount { get; set; }

        /// <summary>
        /// Gets or sets the room price.
        /// </summary>
        /// <value>
        /// The room price.
        /// </value>
        [Display(Name = "Room Price")]
        public decimal RoomPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage.
        /// </summary>
        /// <value>
        /// The discount percentage.
        /// </value>
        [Display(Name = "Discount Percentage")]
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        /// <value>
        /// The discount amount.
        /// </value>
        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancelled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cancelled; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Cancelled")]
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unblocked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is unblocked; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Unblocked")]
        public bool IsUnblocked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is confirmed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is confirmed; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Confirmed")]
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>
        /// The salutation.
        /// </value>
        [Required]
        [StringLength(15)]
        [Display(Name = "Salutation")]
        public string Salutation { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        [StringLength(75)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        [StringLength(75)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        [Required]
        [StringLength(20, ErrorMessage = "Mobile should be 20 digits")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address.
        /// </value>
        [StringLength(4000)]
        [Display(Name = "Address")]
        public string BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        /// <value>
        /// The pin code.
        /// </value>
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Post code should be 6 characters")]
        ////[Required]
        [Display(Name = "Post code")]
        public string PinCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [StringLength(75)]
        [Display(Name = "Town / city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        ////[Required]
        [Display(Name = "Town / city")]
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        ////[Required]
        [Display(Name = "Country")]
        public short CountryId { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        public IEnumerable<SelectListItem> Countries { get; set; }

        /// <summary>
        /// Gets or sets the citites.
        /// </summary>
        /// <value>
        /// The citites.
        /// </value>
        public IEnumerable<SelectListItem> Citites { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the booking status.
        /// </summary>
        /// <value>
        /// The booking status.
        /// </value>
        public string BookingStatus { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the roomtype.
        /// </summary>
        /// <value>
        /// The roomtype.
        /// </value>
        public string Roomtype { get; set; }

        /// <summary>
        /// Gets or sets the GST amount.
        /// </summary>
        /// <value>
        /// The GST amount.
        /// </value>
        public decimal GstAmount { get; set; }

        /// <summary>
        /// Gets or sets the holiday price.
        /// </summary>
        /// <value>
        /// The holiday price.
        /// </value>
        public decimal HolidayPrice { get; set; }

        /// <summary>
        /// Gets or sets the deal code.
        /// </summary>
        /// <value>
        /// The deal code.
        /// </value>
        public string DealCode { get; set; }

        /// <summary>
        /// Gets or sets the hotel address.
        /// </summary>
        /// <value>
        /// The hotel address.
        /// </value>
        public string HotelAddress { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the total adults.
        /// </summary>
        /// <value>
        /// The total adults.
        /// </value>
        public int TotalAdults { get; set; }

        /// <summary>
        /// Gets or sets the contact person.
        /// </summary>
        /// <value>
        /// The contact person.
        /// </value>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Gets or sets the type of the booking.
        /// </summary>
        /// <value>
        /// The type of the booking.
        /// </value>
        public Enums.CurrentDealType BookingType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [update information].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [update information]; otherwise, <c>false</c>.
        /// </value>
        public bool AuotUpdateInfo { get; set; }

        /// <summary>
        /// Gets or sets the booking identifier.
        /// </summary>
        /// <value>
        /// The booking identifier.
        /// </value>
        public long BookingId { get; set; }
    }
}