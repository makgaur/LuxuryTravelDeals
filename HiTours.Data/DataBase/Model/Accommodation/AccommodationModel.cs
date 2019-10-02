// <copyright file="AccommodationModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// AccommodationModel
    /// </summary>
    public class AccommodationModel
    {
        /// <summary>
        /// Gets or sets the accommodation identifier.
        /// </summary>
        /// <value>
        /// The accommodation identifier.
        /// </value>
        [Key]
        public Guid AccommodationID { get; set; }

        /// <summary>
        /// Gets or sets the accommodation group identifier.
        /// </summary>
        /// <value>
        /// The accommodation group identifier.
        /// </value>
        public Guid AccommodationGroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public Guid CityID { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is group rate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is group rate; otherwise, <c>false</c>.
        /// </value>
        public bool IsGroupRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is luxury tax taken.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is luxury tax taken; otherwise, <c>false</c>.
        /// </value>
        public bool IsLuxuryTaxTaken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is vat taken.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vat taken; otherwise, <c>false</c>.
        /// </value>
        public bool IsVATTaken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is service tax taken.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is service tax taken; otherwise, <c>false</c>.
        /// </value>
        public bool IsServiceTaxTaken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is meal vat applicaple.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is meal vat applicaple; otherwise, <c>false</c>.
        /// </value>
        public bool IsMealVATApplicaple { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is service charge applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is service charge applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsServiceChargeApplicable { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public Guid CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccommodationModel"/> is monday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if monday; otherwise, <c>false</c>.
        /// </value>
        public bool Monday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccommodationModel"/> is tuesday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if tuesday; otherwise, <c>false</c>.
        /// </value>
        public bool Tuesday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccommodationModel"/> is wednesday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if wednesday; otherwise, <c>false</c>.
        /// </value>
        public bool Wednesday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccommodationModel"/> is thursday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if thursday; otherwise, <c>false</c>.
        /// </value>
        public bool Thursday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccommodationModel"/> is friday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if friday; otherwise, <c>false</c>.
        /// </value>
        public bool Friday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccommodationModel"/> is saterday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if saterday; otherwise, <c>false</c>.
        /// </value>
        public bool Saterday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccommodationModel"/> is sunday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sunday; otherwise, <c>false</c>.
        /// </value>
        public bool Sunday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is recommended.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is recommended; otherwise, <c>false</c>.
        /// </value>
        public bool IsRecommended { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        /// <value>
        /// The name of the location.
        /// </value>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is preferred.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is preferred; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferred { get; set; }
    }
}