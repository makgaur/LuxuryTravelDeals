// <copyright file="TourPackageImageModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using HiTours.Core;

    /// <summary>
    /// TourPackageImage
    /// </summary>
    public class TourPackageImageModel
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
        public string ImageDescription { get; set; }

        /// <summary>
        /// Gets or sets the alt tag.
        /// </summary>
        /// <value>
        /// The alt tag.
        /// </value>
        public string AltTag { get; set; }

        /// <summary>
        /// Gets or sets the tour package.
        /// </summary>
        /// <value>
        /// The tour package.
        /// </value>
        public TourPackageModel TourPackage { get; set; }

        /// <summary>
        /// Gets or sets the sequence no.
        /// </summary>
        /// <value>
        /// The sequence no.
        /// </value>
        public short? SequenceNo { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

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
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>
        /// The state of the object.
        /// </value>
        [NotMapped]
        public Microsoft.EntityFrameworkCore.EntityState ObjectState { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public int UpdatedBy { get; set; }
    }
}
