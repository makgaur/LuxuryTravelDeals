// <copyright file="PackageCountryViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// PackageCountryViewModel
    /// </summary>
    public class PackageCountryViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        [Display(Name="Region")]
        [Required(ErrorMessage = "Region is required.")]
        public short? RegionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the sort.
        /// </summary>
        /// <value>
        /// The name of the sort.
        /// </value>
        public string SortName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name ="Country Name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        ////[Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the package images.
        /// </summary>
        /// <value>
        /// The package images.
        /// </value>
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// Gets or sets the phone code.
        /// </summary>
        /// <value>
        /// The phone code.
        /// </value>
        public string PhoneCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>
        /// The name of the region.
        /// </value>
        [Display(Name = "Country Phone Code")]
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> RegionList { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
