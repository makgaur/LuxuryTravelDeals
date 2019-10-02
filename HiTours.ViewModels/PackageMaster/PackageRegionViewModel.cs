// <copyright file="PackageRegionViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// PackageRegionViewModel
    /// </summary>
    public class PackageRegionViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Name is required")]
        [Display(Name ="Region Name")]
        [Remote("IsDuplicate", "region", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        ////[Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
