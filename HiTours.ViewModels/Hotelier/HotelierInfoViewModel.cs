// <copyright file="HotelierInfoViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// PackageHotelViewModel
    /// </summary>
    /// <seealso cref="BaseModel" />
    public class HotelierInfoViewModel : BaseModel
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
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets Url
        /// </summary>
        [Required]
        [Display(Name = "Website")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets Checkin
        /// </summary>
        [Required]
        [Display(Name = "Check In Time")]
        public string CheckInTime { get; set; }

        /// <summary>
        /// Gets or sets CheckOut
        /// </summary>
        [Required]
        [Display(Name = "Check Out Time")]
        public string CheckOutTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets ss open checking or not
        /// </summary>
        [Required]
        [Display(Name = "Is Open Check IN")]
        public bool IsOpenCheckIn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets ss open checking or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets ss open checking or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets StarRating
        /// </summary>
        [Required]
        [Display(Name = "Star Rating")]
        public int StarRating { get; set; }

        /// <summary>
        /// Gets or sets StarRating
        /// </summary>
        [Required]
        [Display(Name = "Property Type")]
        public int PropertyType { get; set; }

        /// <summary>
        /// Gets or sets Lat
        /// </summary>
        public decimal? Lat { get; set; }

        /// <summary>
        /// Gets or sets Long
        /// </summary>
        public decimal? Long { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets SubStatus
        /// </summary>
        public int? SubStatus { get; set; }

        /// <summary>
        /// Gets or sets get or sets vendor information model
        /// </summary>
        public VendorInformationViewModel VendorInformationViewModel { get; set; }

        /// <summary>
        /// Gets or sets get or sets vendor information model
        /// </summary>
        public ICollection<SelectListItem> PropertyTypeItems { get; set; }

        /// <summary>
        /// Gets or sets the commandbutton.
        /// </summary>
        /// <value>
        /// The commandbutton.
        /// </value>
        public string CommandButton { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? Area { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? City { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? State { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public short? Country { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> CountryItems { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> StateItems { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> CityItems { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> VendorItems { get; set; }

        /// <summary>
        /// Gets or sets the Business Name.
        /// </summary>
        /// <value>
        /// The Business Name.
        /// </value>
        [Display(Name = "Group")]
        public int? Group { get; set; }
    }
}
