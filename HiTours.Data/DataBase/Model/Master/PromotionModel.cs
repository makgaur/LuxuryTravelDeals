// <copyright file="PromotionModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// PackageCountry
    /// </summary>
    public class PromotionModel : BaseModel
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public int DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public decimal? DiscountValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public string CouponCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public PackageMarginTypeModel MarginTypeModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public decimal? MaxDiscountFlat { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public DateTime ValidityStart { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public DateTime ValidityEnd { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public int MaxCount { get; set; }
    }
}