// <copyright file="Vendor_PackageModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class Vendor_PackageModel
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
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Record Is Active.
        /// </summary>
        /// <value>
        /// The Is Active.
        /// </value>
        public bool IsActive { get; set; }
    }
}