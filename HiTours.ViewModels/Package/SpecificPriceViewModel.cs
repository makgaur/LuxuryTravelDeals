// <copyright file="SpecificPriceViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// SpecificPriceViewModel
    /// </summary>
    public class SpecificPriceViewModel
    {
        /// <summary>
        /// Gets or sets the specific price identifier.
        /// </summary>
        /// <value>
        /// The specific price identifier.
        /// </value>
        public int SpecificPriceId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        public Guid PackageId { get; set; }
    }
}