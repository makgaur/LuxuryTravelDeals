// <copyright file="PlanTypeModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// PlanTypeModel
    /// </summary>
    public class PlanTypeModel
    {
        /// <summary>
        /// Gets or sets the plan type identifier.
        /// </summary>
        /// <value>
        /// The plan type identifier.
        /// </value>
        [Key]
        public Guid PlanTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the plan.
        /// </summary>
        /// <value>
        /// The type of the plan.
        /// </value>
        public string PlanType { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public int Color { get; set; }
    }
}