// <copyright file="VendorServiceModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Models;

    /// <summary>
    /// PackageStateModel
    /// </summary>
    public class VendorServiceModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public VendorInformationModel VendorModel { get; set; }

        /// <summary>
        /// Gets or sets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ServiceTypeMasterModel ServiceTypeModel { get; set; }
    }
}