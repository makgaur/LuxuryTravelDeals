// <copyright file="TourPackageNightsValidityParentViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.TourPackage
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TourPackageNightsValidityViewModel
    /// </summary>
    public class TourPackageNightsValidityParentViewModel
    {
        /// <summary>
        /// Gets or sets the date from.
        /// </summary>
        /// <value>
        /// The date from.
        /// </value>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the date to.
        /// </summary>
        /// <value>
        /// The date to.
        /// </value>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights validity.
        /// </summary>
        /// <value>
        /// The tour package nights validity.
        /// </value>
        public List<TourPackageNightsValidityViewModel> TourPackageNightsValidity { get; set; }
    }
}