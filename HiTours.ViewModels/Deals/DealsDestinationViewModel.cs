// <copyright file="DealsDestinationViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class DealsDestinationViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int PackageId { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public DealsPackageViewModel DealsPackageViewModel { get; set; }

        /// <summary>
        /// Gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        [Display(Name = "Area")]
        public int? Area { get; set; }

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
        /// Gets or sets a value indicating whether gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        public bool VisaRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public ICollection<SelectListItem> CountryItems { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public ICollection<SelectListItem> StateItems { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public ICollection<SelectListItem> CityItems { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public ICollection<SelectListItem> AreaItems { get; set; }
    }
}
