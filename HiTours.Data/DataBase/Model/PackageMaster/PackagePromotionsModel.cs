// <copyright file="PackagePromotionsModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// Package Cancellation Policy Model
    /// </summary>
    public class PackagePromotionsModel : BaseModel
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
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the Deduction Value.
        /// </summary>
        /// <value>
        /// The Deduction Value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the Deduction Days Period.
        /// </summary>
        /// <value>
        /// The Deduction Days Period.
        /// </value>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this cancellation policy Is Active.
        /// </summary>
        /// <value>
        /// True if Active.
        /// </value>
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
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the Service Fees Deduction Value.
        /// </summary>
        /// <value>
        /// The Service Fees Deduction Value.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this cancellation policy Is Active.
        /// </summary>
        /// <value>
        /// True if Active.
        /// </value>
        public PackageMarginTypeModel MarginTypeModel { get; set; }
    }
}