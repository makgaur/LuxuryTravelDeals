// <copyright file="HotelBookingModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HiTours.Core;

    /// <summary>
    /// HotelBookingModel
    /// </summary>
    public class HotelBookingModel : BaseModel
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the booking date.
        /// </summary>
        /// <value>
        /// The booking date.
        /// </value>
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// Gets or sets the block date.
        /// </summary>
        /// <value>
        /// The block date.
        /// </value>
        public DateTime? BlockDate { get; set; }

        /// <summary>
        /// Gets or sets the book from date.
        /// </summary>
        /// <value>
        /// The book from date.
        /// </value>
        public DateTime BookFromDate { get; set; }

        /// <summary>
        /// Gets or sets the book to date.
        /// </summary>
        /// <value>
        /// The book to date.
        /// </value>
        public DateTime BookToDate { get; set; }

        /// <summary>
        /// Gets or sets the packages identifier.
        /// </summary>
        /// <value>
        /// The packages identifier.
        /// </value>
        public Guid PackagesId { get; set; }

        /// <summary>
        /// Gets or sets the hotel room night target identifier.
        /// </summary>
        /// <value>
        /// The hotel room night target identifier.
        /// </value>
        public Guid HotelPriceId { get; set; }

        /// <summary>
        /// Gets or sets the room count.
        /// </summary>
        /// <value>
        /// The room count.
        /// </value>
        public short RoomCount { get; set; }

        /// <summary>
        /// Gets or sets the room price.
        /// </summary>
        /// <value>
        /// The room price.
        /// </value>
        public decimal RoomPrice { get; set; }

        /// <summary>
        /// Gets or sets the GST amount.
        /// </summary>
        /// <value>
        /// The GST amount.
        /// </value>
        public decimal GstAmount { get; set; }

        /// <summary>
        /// Gets or sets the GST percent.
        /// </summary>
        /// <value>
        /// The GST percent.
        /// </value>
        public decimal GstPercent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rate increase by per.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rate increase by per; otherwise, <c>false</c>.
        /// </value>
        public bool IsRateIncreaseByPer { get; set; }

        /// <summary>
        /// Gets or sets the rate increase.
        /// </summary>
        /// <value>
        /// The rate increase.
        /// </value>
        public decimal? RateIncrease { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancelled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cancelled; otherwise, <c>false</c>.
        /// </value>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unblocked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is unblocked; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnblocked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is confirmed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is confirmed; otherwise, <c>false</c>.
        /// </value>
        public bool IsConfirmed { get; set; }

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
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
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
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the packages.
        /// </summary>
        /// <value>
        /// The packages.
        /// </value>
        public PackageModel Packages { get; set; }

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
        /// Gets or sets the room remark.
        /// </summary>
        /// <value>
        /// The room remark.
        /// </value>
        public string RoomRemark { get; set; }

        /// <summary>
        /// Gets or sets the type of the booking package.
        /// </summary>
        /// <value>
        /// The type of the booking package.
        /// </value>
        public string BookingPackageType { get; set; }

        /// <summary>
        /// Gets or sets the paid amount.
        /// </summary>
        /// <value>
        /// The paid amount.
        /// </value>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>
        /// The participants.
        /// </value>
        public int Participants { get; set; }
    }
}