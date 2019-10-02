// <copyright file="BulkPromotionViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// BookingOnDate
    /// </summary>
    public class BulkPromotionViewModel : PromotionViewModel
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Required]
        [Display(Name = "Number Of Coupons")]
        public int NoOfCoupons { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Required(ErrorMessage = "Preffix Required")]
        [StringLength(4, ErrorMessage = "Preffix must not be more than 4 char")]
        [Display(Name = "Preffix Characters (Max 4)")]
        public string Preffix { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [Required]
        [Display(Name = "Postfix Amount")]
        public int Postfix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [Display(Name = "Characters")]
        public bool Characters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public bool Numbers { get; set; }
    }
}