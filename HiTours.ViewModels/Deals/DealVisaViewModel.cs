// <copyright file="DealVisaViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// CategoryModel
    /// </summary>
    public class DealVisaViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of Currency
        /// </summary>
        /// <value>
        /// The name of the Currency.
        /// </value>
        public int PackageId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public short CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        [Required]
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public decimal ChildPrice { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public int BufferDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public ICollection<SelectListItem> VendorItems { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public ICollection<SelectListItem> CountriesItems { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Display(Name = "Markup (Flat)")]
        public decimal Markup { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Display(Name = "Documents Required")]
        public string DocumentsRequired { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Display(Name = "Photo Specification")]
        public string PhotoSpecification { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Display(Name = "Processing Time")]
        public string ProcessingTime { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        [Display(Name = "General Policy")]
        public string GeneralPolicy { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public int RowCount { get; set; }
    }
}