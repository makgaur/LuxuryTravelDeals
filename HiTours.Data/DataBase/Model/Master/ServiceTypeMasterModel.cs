// <copyright file="ServiceTypeMasterModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// StateModel
    /// </summary>
    public class ServiceTypeMasterModel
    {
        /// <summary>
        /// Gets or sets the Service identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Service Name.
        /// </summary>
        /// <value>
        /// The Service Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Active.
        /// </summary>
        /// <value>
        /// The Service Name.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public ICollection<VendorServiceModel> VendorServiceModels { get; set; }
    }
}