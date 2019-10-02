// <copyright file="DealInventoryViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class DealInventoryViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int RatePlanId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Url.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int Booking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Star Rating
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage ="Inventory Required")]
        public int Inventory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Property Type
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Open Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? SingleSupplement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Active.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? ExtraAdult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? ExtraChild_WB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? ExtraChild_NB { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? ExtraInfant { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? DaySurgCharge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        [Required(ErrorMessage = "Price Required")]
        public decimal? Surgcharge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Status.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool BlackOut { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Sub Status.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool IsActive { get; set; }
    }
}
