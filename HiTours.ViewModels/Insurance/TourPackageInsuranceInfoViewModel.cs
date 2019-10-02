// <copyright file="TourPackageInsuranceInfoViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// StateModel
    /// </summary>
    public class TourPackageInsuranceInfoViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public bool SelectThis { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public decimal AdultRate { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public decimal ChildRate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> VendorItems { get; set; }
    }
}