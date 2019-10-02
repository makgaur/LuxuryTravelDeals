// <copyright file="RatePlanAddViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// RoomTypeModel
    /// </summary>
    public class RatePlanAddViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        [Key]
        public int RP_Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [Required(ErrorMessage = "Ameneties are required")]
        [Display(Name = "Ameneties")]
        public int[] RatePlanAmeneties { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int RP_RoomConfigId { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        [Required(ErrorMessage = "Plan Name is required")]
        [Display(Name = "Plan Name")]
        public string RP_Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Booking Start Date")]
        public DateTime RP_BookingStartDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Booking End Date")]
        public DateTime RP_BookingEndDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Travel Start Date")]
        public DateTime RP_TravelStartDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Travel End Date")]
        public DateTime RP_TravelEndDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Selling Price is required")]
        [Display(Name = "Selling Price")]
        public decimal RP_SellingPrice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Fake Price is required")]
        [Display(Name = "Fake Price")]
        public decimal RP_FakePrice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Cost is required (0 if Nothing)")]
        [Display(Name = "Extra Adult Cost")]
        public decimal RP_ExtraAdultPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Cost is required (0 if Nothing)")]
        [Display(Name = "Extra Child Cost")]
        public decimal RP_ExtraChildPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Cost is required (0 if Nothing)")]
        [Display(Name = "Extra Infant Cost")]
        public decimal RP_ExtraInfantPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool RP_IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool RP_IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> RoomConfigItems { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> AmenetiesItems { get; set; }
    }
}