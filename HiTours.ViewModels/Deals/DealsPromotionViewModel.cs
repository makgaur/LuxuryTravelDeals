// <copyright file="DealsPromotionViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// PackageCountry
    /// </summary>
    public class DealsPromotionViewModel : BaseModel
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
        public int PackageId { get; set; }

        /// <summary>
        /// Gets or sets the name of Currency
        /// </summary>
        /// <value>
        /// The name of the Currency.
        /// </value>
        [Display(Name = "Promotion Type")]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name = "Discount Type")]
        public int DiscountType { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        [Display(Name = "Discount Value")]
        [Required]
        public decimal? DiscountValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "Booking Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? BookingStartDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "Booking End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? BookingEndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "Travel Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? TravelStartDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "Travel End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? TravelEndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool Mon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool Tue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool Wed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool Thu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool Fri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool Sat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "All Week")]
        public bool AllWeek { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool Sun { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "Start Day")]
        public int? StartDay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "End Day")]
        public int? EndDay { get; set; }

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
        [Display(Name ="Coupon Code")]
        public string CouponCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Display(Name = "Length Of Stay")]
        public int LengthOfStay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public ICollection<SelectListItem> MarginTypeItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public ICollection<SelectListItem> PromotionTypeItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public ICollection<SelectListItem> RoomTypeItems { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name = "Room Type")]
        public short? RoomType { get; set; }
    }
}