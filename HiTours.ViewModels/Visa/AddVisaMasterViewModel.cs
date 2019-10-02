// <copyright file="AddVisaMasterViewModel.cs" company="Luxury Travel Deals">
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
    public class AddVisaMasterViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        [Required]
        [Display(Name = "Vendor")]
        public int VendorID { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        [Required]
        [Display(Name = "Country")]
        public short CountryId { get; set; }

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

        /// <summary>
        /// Gets or sets the BufferDays.
        /// </summary>
        /// <value>
        /// The BufferDays.
        /// </value>
        public string BufferDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> CountryItems { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> VendorItems { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Required]
        [Display(Name = "Markup (Flat)")]
        public decimal Markup { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Required]
        [Display(Name = "Documents Required")]
        public string DocumentsRequired { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Required]
        [Display(Name = "Photo Specification")]
        public string PhotoSpecification { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Required]
        [Display(Name = "Processing Time")]
        public string ProcessingTime { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Required]
        [Display(Name = "General Policy")]
        public string GeneralPolicy { get; set; }
    }
}