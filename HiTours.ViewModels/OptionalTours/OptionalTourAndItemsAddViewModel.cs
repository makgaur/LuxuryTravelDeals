// <copyright file="OptionalTourAndItemsAddViewModel.cs" company="Luxury Travel Deals">
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
    public class OptionalTourAndItemsAddViewModel : BaseModel
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
        [Display(Name = "Vendor")]
        [Required(ErrorMessage = "Vendor is required")]
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public short CountryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public IEnumerable<SelectListItem> CountryItems { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public IEnumerable<SelectListItem> VendorItems { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public List<OptionalTourItemsViewModel> TourItems { get; set; }
    }
}