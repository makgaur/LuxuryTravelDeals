// <copyright file="DealsFlightViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.TBO.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Flights Cabin
    /// </summary>
    public enum CabinClass
    {
        /// <summary>All == 1</summary>
        All = 1,

        /// <summary>
        /// Economy = 2
        /// </summary>
        Economy = 2,

        /// <summary>
        /// PremiumEconomy = 3,
        /// </summary>
        PremiumEconomy = 3,

        /// <summary>
        /// Business = 4,
        /// </summary>
        Business = 4,

        /// <summary>
        /// PremiumBusiness = 5,
        /// </summary>
        PremiumBusiness = 5,

        /// <summary>
        /// First = 6
        /// </summary>
        First = 6
    }

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class DealsFlightViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int FlightId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int InclusionId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int ItenaryId { get; set; }

        /// <summary>
        /// Gets or sets the CountryId.
        /// </summary>
        /// <value>
        /// The CountryId.
        /// </value>
        [Required]
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the CountryId.
        /// </summary>
        /// <value>
        /// The CountryId.
        /// </value>
        public string OriginName { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        [Required]
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the CountryId.
        /// </summary>
        /// <value>
        /// The CountryId.
        /// </value>
        public string DestinationName { get; set; }

        /// <summary>
        /// Gets or sets the AdultPrice.
        /// </summary>
        /// <value>
        /// The AdultPrice.
        /// </value>
        ////[Required]
        [Display(Name = "Cabin Class")]
        public string CabinClass { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        ////[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        ////[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Days.
        /// </value>
        ////[Required]
        [Display(Name = "Days")]
        ////[Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public bool AllDay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Days.
        /// </value>
        ////[Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        [Display(Name = "Day")]
        public int? Days { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public int TotalDays { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        [Display(Name = "Vendor")]
        public int? VendorId { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public ICollection<SelectListItem> VendorItems { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public ICollection<SelectListItem> OriginItems { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public ICollection<SelectListItem> DestinationItems { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public AirSearchResponse GetAirSearchResponse { get; set; }
    }
}
