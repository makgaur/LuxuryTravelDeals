// <copyright file="PackageCityViewModel.cs" company="Luxury Travel Deals">
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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// PackageCityViewModel
    /// </summary>
    public class PackageCityViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        [Display(Name = "State")]
        public int? StateId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public short CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Name is required")]
        [Display(Name ="City Name")]
        ////[Remote("IsDuplicate", "city", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
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
        /// Gets or sets the city description.
        /// </summary>
        /// <value>
        /// The city description.
        /// </value>
        [Display(Name = "City Description")]
        public string CityDescription { get; set; }

        /// <summary>
        /// Gets or sets the short detail.
        /// </summary>
        /// <value>
        /// The short detail.
        /// </value>
        public string ShortDetail { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [Display(Name ="City Code")]
        [Required(ErrorMessage = "City Code is required.")]
        [Remote("IsDuplicateCityCode", "city", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> Coutries { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> States { get; set; }
    }
}
