// <copyright file="PackagePromotions_PackageModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Models;

    /// <summary>
    /// Package Cancellation_Package Relation
    /// </summary>
    public class PackagePromotions_PackageModel
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
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets Promotion Id.
        /// </summary>
        /// <value>
        /// The Promotion Id.
        /// </value>
        public int PromotionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Cancellation Id.
        /// </summary>
        /// <value>
        /// The Cancellation Id.
        /// </value>
        public bool IsActive { get; set; }
    }
}