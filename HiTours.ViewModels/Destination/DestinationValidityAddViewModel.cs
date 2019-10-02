// <copyright file="DestinationValidityAddViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
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
    public class DestinationValidityAddViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the Destination Validity identifier.
        /// </summary>
        /// <value>
        /// The Destination Validity identifier.
        /// </value>
        public int DV_Id { get; set; }

        /// <summary>
        /// Gets or sets the DestinationId.
        /// </summary>
        /// <value>
        /// The DestinationId.
        /// </value>
        [Required(ErrorMessage = "Select Destination")]
        [Display(Name = "Destination")]
        public int DV_DestinationId { get; set; }

        /// <summary>
        /// Gets or sets the Validity Start Date.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Date Is Required")]
        [Display(Name = "Validity Start Date")]
        public DateTime DV_ValidityStartDate { get; set; }

        /// <summary>
        /// Gets or sets the Validity End Date.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Date Is Required")]
        [Display(Name = "Validity End Date")]
        public DateTime DV_ValidityEndDate { get; set; }

        /// <summary>
        /// Gets or sets the Adult Price Per Person on Based of Double Occupency.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Price Is Required (0 for Nothing)")]
        [Display(Name = "Single Adult Price (DBLO)")]
        public decimal DV_AdultPriceDBL { get; set; }

        /// <summary>
        /// Gets or sets the Child Price Per Person on Based of Double Occupency.
        /// </summary>
        /// <value>
        /// The Child Price Per Person on Based of Double Occupency.
        /// </value>
        [Required(ErrorMessage = "Price Is Required (0 for Nothing)")]
        [Display(Name = "Single Child Price (DBLO)")]
        public decimal DV_ChildPriceDBL { get; set; }

        /// <summary>
        /// Gets or sets the Infant Price Per Person on Based of Double Occupency.
        /// </summary>
        /// <value>
        /// The Infant Price Per Person on Based of Double Occupency.
        /// </value>
        [Required(ErrorMessage = "Price Is Required (0 for Nothing)")]
        [Display(Name = "Single Infant Price (DBLO)")]
        public decimal DV_InfantPriceDBL { get; set; }

        /// <summary>
        /// Gets or sets the Extra Adult Price.
        /// </summary>
        /// <value>
        /// The Extra Adult Price.
        /// </value>
        [Required(ErrorMessage = "Price Is Required (0 for Nothing)")]
        [Display(Name = "Extra Adult Price")]
        public decimal DV_ExtraAdultPrice { get; set; }

        /// <summary>
        /// Gets or sets the Extra Child Price.
        /// </summary>
        /// <value>
        /// The Extra Child Price.
        /// </value>
        [Required(ErrorMessage = "Price Is Required (0 for Nothing)")]
        [Display(Name = "Extra Child Price")]
        public decimal DV_ExtraChildPrice { get; set; }

        /// <summary>
        /// Gets or sets the Extra Infant Price
        /// </summary>
        /// <value>
        /// The Extra Infant Price.
        /// </value>
        [Required(ErrorMessage = "Price Is Required (0 for Nothing)")]
        [Display(Name = "Extra Infant Price")]
        public decimal DV_ExtraInfantPrice { get; set; }

        /// <summary>
        /// Gets or sets Supplement Adult Value.
        /// </summary>
        /// <value>
        /// The Supplement Adult Value.
        /// </value>
        [Required(ErrorMessage = "Price Is Required (0 for Nothing)")]
        [Display(Name = "Adult Supplement Price")]
        public decimal DV_SupplementAdult { get; set; }

        /// <summary>
        /// Gets or sets the Adult Min Age.
        /// </summary>
        /// <value>
        /// The Adult Min Age Default 12.
        /// </value>
        [Required(ErrorMessage = "Age Is Required")]
        [Display(Name = "Adult Minimum Age")]
        public int DV_AdultMinAge { get; set; }

        /// <summary>
        /// Gets or sets infant Max Age.
        /// </summary>
        /// <value>
        /// The infant Max Age. Default 2
        /// </value>
        [Required(ErrorMessage = "Age Is Required")]
        [Display(Name = "Infant Maximum Age")]
        public int DV_InfantMaxAge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Is Active.
        /// </summary>
        /// <value>
        /// The Is Active.
        /// </value>
        public bool DV_IsActive { get; set; }

        /// <summary>
        /// Gets or sets Select List Destination Item.
        /// </summary>
        /// <value>
        /// The Select List Destination Item.
        /// </value>
        public IEnumerable<SelectListItem> DestinationItems { get; set; }
    }
}
