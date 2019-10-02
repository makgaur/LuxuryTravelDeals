// <copyright file="VendorInformationViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///  Vendor Model
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class VendorInformationViewModel : BaseModel
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
        /// Gets or sets the Business Name.
        /// </summary>
        /// <value>
        /// The Business Name.
        /// </value>
        [Display(Name = "Group")]
        public int? Group { get; set; }

        /// <summary>
        /// Gets or sets the Business Name.
        /// </summary>
        /// <value>
        /// The Business Name.
        /// </value>
        [Required]
        [Display(Name = "Name")]
        [Remote("IsVendorAvailable", "Vendor", AdditionalFields = "Id", ErrorMessage = "This Vendor already Exist.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Business Name.
        /// </summary>
        /// <value>
        /// The Business Name.
        /// </value>
        [Required]
        [Display(Name = "Service Type")]
        public int[] ServiceTypes { get; set; }

        /// <summary>
        /// Gets or sets Property Category.
        /// </summary>
        /// <value>
        /// The Property Category.
        /// </value>
        [Required]
        [Display(Name = "Vendor Category")]
        public int Category { get; set; }

        /// <summary>
        /// Gets or sets Property Category.
        /// </summary>
        /// <value>
        /// The Property Category.
        /// </value>
        [Required]
        [Display(Name = "Currency")]
        public int Currency { get; set; }

        /// <summary>
        /// Gets or sets the Website Address.
        /// </summary>
        /// <value>
        /// The Website Address
        /// </value>
        [Required]
        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        [Required]
        [Display(Name = "City")]
        public int City { get; set; }

        /// <summary>
        /// Gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        [Display(Name = "State")]
        public int? State { get; set; }

        /// <summary>
        /// Gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        [Required]
        [Display(Name = "Country")]
        public short Country { get; set; }

        /// <summary>
        /// Gets or sets the commandbutton.
        /// </summary>
        /// <value>
        /// The commandbutton.
        /// </value>
        public string CommandButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        public bool IsDeleted { get; set; }

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
        public IEnumerable<SelectListItem> CategoryItems { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> CurrencyItems { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> VendorGroupItems { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> ServiceTypeItems { get; set; }
    }
}