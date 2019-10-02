// <copyright file="SettingPromoDiscountModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// PlanTypeModel
    /// </summary>
    public class SettingPromoDiscountModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the plan type identifier.
        /// </summary>
        /// <value>
        /// The plan type identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the plan.
        /// </summary>
        /// <value>
        /// The type of the plan.
        /// </value>
        public int PromoType { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public decimal MinMarkUp { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public decimal MaxMarkUp { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public decimal DiscountMarkUp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public bool IsActive { get; set; }
    }
}