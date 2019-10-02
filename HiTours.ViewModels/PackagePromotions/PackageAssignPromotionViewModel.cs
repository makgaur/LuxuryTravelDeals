// <copyright file="PackageAssignPromotionViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// CategoryModel
    /// </summary>
    public class PackageAssignPromotionViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public List<int> PromotionsIds { get; set; }

        /// <summary>
        /// Gets or sets the Service Fees Deduction Value.
        /// </summary>
        /// <value>
        /// The Service Fees Deduction Value.
        /// </value>
        public IEnumerable<SelectListItem> PromotionItems { get; set; }

        /// <summary>
        /// Gets or sets the Service Fees Deduction Value.
        /// </summary>
        /// <value>
        /// The Service Fees Deduction Value.
        /// </value>
        public Guid PackageId { get; set; }
    }
}