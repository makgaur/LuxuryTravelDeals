// <copyright file="TourPackageImageViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// TourPackageImageViewModel
    /// </summary>
    public class TourPackageImageViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tour package identifier.
        /// </summary>
        /// <value>
        /// The tour package identifier.
        /// </value>
        public Guid TourPackageId { get; set; }

        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the image description.
        /// </summary>
        /// <value>
        /// The image description.
        /// </value>
        [StringLength(100)]
        public string ImageDescription { get; set; }

        /// <summary>
        /// Gets or sets the alt tag.
        /// </summary>
        /// <value>
        /// The alt tag.
        /// </value>
        [StringLength(50)]
        public string AltTag { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public string UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the sequence no.
        /// </summary>
        /// <value>
        /// The sequence no.
        /// </value>
        public short? SequenceNo { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFormFile File { get; set; }
    }
}