// <copyright file="MealPlanMasterModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;

    /// <summary>
    /// MealPlanMasterModel
    /// </summary>
    public class MealPlanMasterModel
    {
        /// <summary>
        /// Gets or sets the meal plan identifier.
        /// </summary>
        /// <value>
        /// The meal plan identifier.
        /// </value>
        public Guid MealPlanId { get; set; }

        /// <summary>
        /// Gets or sets the meal plan.
        /// </summary>
        /// <value>
        /// The meal plan.
        /// </value>
        public string MealPlan { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MealPlanMasterModel" /> is breakfast.
        /// </summary>
        /// <value>
        ///   <c>true</c> if breakfast; otherwise, <c>false</c>.
        /// </value>
        public bool Breakfast { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MealPlanMasterModel" /> is lunch.
        /// </summary>
        /// <value>
        ///   <c>true</c> if lunch; otherwise, <c>false</c>.
        /// </value>
        public bool Lunch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MealPlanMasterModel" /> is dinner.
        /// </summary>
        /// <value>
        ///   <c>true</c> if dinner; otherwise, <c>false</c>.
        /// </value>
        public bool Dinner { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public short SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tax include.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is tax include; otherwise, <c>false</c>.
        /// </value>
        public bool IsTaxInclude { get; set; }
    }
}