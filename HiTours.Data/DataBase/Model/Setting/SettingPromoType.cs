// <copyright file="SettingPromoType.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// PlanTypeModel
    /// </summary>
    public class SettingPromoType
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public ICollection<DealsPromoScheduleModel> DealPromoScheduleModel { get; set; }
    }
}