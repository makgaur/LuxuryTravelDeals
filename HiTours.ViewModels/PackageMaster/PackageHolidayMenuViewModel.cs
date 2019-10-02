// <copyright file="PackageHolidayMenuViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Package Holiday Menu View Model
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class PackageHolidayMenuViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is region.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is region; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(Name = "Is Region")]
        public bool IsRegion { get; set; }

        /// <summary>
        /// Gets or sets the child menu.
        /// </summary>
        /// <value>
        /// The child menu.
        /// </value>
        public string ChildMenu { get; set; }

        /// <summary>
        /// Gets or sets the menu list.
        /// </summary>
        /// <value>
        /// The menu list.
        /// </value>
        [Required(ErrorMessage = "The Field Is Required.")]
        public string[] MenuList { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public IEnumerable<SelectListItem> ChildMenuList { get; set; }

        /// <summary>
        /// Gets or sets the name list.
        /// </summary>
        /// <value>
        /// The name list.
        /// </value>
        public IEnumerable<SelectListItem> NameList { get; set; }
    }
}