// <copyright file="PackagePromotionsManageViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Package Cancellation Policy Model
    /// </summary>
    public class PackagePromotionsManageViewModel : BaseModel
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
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        [Required(ErrorMessage = "Promotion Name is required")]
        [Display(Name = "Promotion Name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Super.
        /// </summary>
        /// <value>
        /// True if Super.
        /// </value>
        public bool IsSuper { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Deal Type From Category Master.
        /// </summary>
        /// <value>
        /// The Deal Type From Category Master.
        /// </value>
        public bool IsVendor { get; set; }

        /// <summary>
        /// Gets or sets the Deduction Type From Margin Type.
        /// </summary>
        /// <value>
        /// The Deduction Type From Margin Type Table.
        /// </value>
        [Required(ErrorMessage = "Promotion Type is required")]
        [Display(Name = "Promotion Type")]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the Deduction Value.
        /// </summary>
        /// <value>
        /// The Deduction Value.
        /// </value>
        [Required(ErrorMessage = "Value is required")]
        [Display(Name = "Value")]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the Deduction Days Period.
        /// </summary>
        /// <value>
        /// The Deduction Days Period.
        /// </value>
        [Required(ErrorMessage = "Days are required")]
        [Display(Name = "Minimum Days for Promotion")]
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this cancellation policy Is Active.
        /// </summary>
        /// <value>
        /// True if Active.
        /// </value>
        [Display(Name = "Is Promotion for period")]
        public bool IsPeriod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this cancellation policy Is Active.
        /// </summary>
        /// <value>
        /// True if Active.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this cancellation policy Is Active.
        /// </summary>
        /// <value>
        /// True if Active.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Service Fees DeductionType.
        /// </summary>
        /// <value>
        /// The Service Fees DeductionType.
        /// </value>
        [Required(ErrorMessage = "Start Day is Required")]
        [Display(Name = "Promotion Start Day")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the Service Fees Deduction Value.
        /// </summary>
        /// <value>
        /// The Service Fees Deduction Value.
        /// </value>
        [Required(ErrorMessage = "End Day is Required")]
        [Display(Name = "Promotion End Day")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the Service Fees Deduction Value.
        /// </summary>
        /// <value>
        /// The Service Fees Deduction Value.
        /// </value>
        public IEnumerable<SelectListItem> MarginItems { get; set; }
    }
}