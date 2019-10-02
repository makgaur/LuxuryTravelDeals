// <copyright file="PackageImageModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using HiTours.Core;

    /// <summary>
    /// PackageImageModel
    /// </summary>
    public class PackageImageModel : BaseModel
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
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the sequence no.
        /// </summary>
        /// <value>
        /// The sequence no.
        /// </value>
        public short? SequenceNo { get; set; }

        /// <summary>
        /// Gets or sets the package.
        /// </summary>
        /// <value>
        /// The package.
        /// </value>
        public PackageModel Package { get; set; }
    }
}