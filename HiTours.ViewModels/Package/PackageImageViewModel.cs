// <copyright file="PackageImageViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using HiTours.Core;

    /// <summary>
    /// PackageImageViewModel
    /// </summary>
    public class PackageImageViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        [Display(Name = "Package Name")]
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the sequence no.
        /// </summary>
        /// <value>
        /// The sequence no.
        /// </value>
        public short? SequenceNo { get; set; }
    }
}