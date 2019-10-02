// <copyright file="PackageStateViewModel.cs" company="Luxury Travel Deals">
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
    /// StateViewModel
    /// </summary>
    public class PackageStateViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        [Display(Name = "Country")]
        public short CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name="State Name")]
        [Required(ErrorMessage = "State name is required")]
        [Remote("IsDuplicate", "state", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public string Name { get; set; }

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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        ////[Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> Coutries { get; set; }
    }
}
