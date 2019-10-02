// <copyright file="OptionalToursItemsMasterModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// StateModel
    /// </summary>
    public class OptionalToursItemsMasterModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public int OptionalTourId { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public string TourName { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public decimal ChildPrice { get; set; }
    }
}