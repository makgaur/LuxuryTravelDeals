// <copyright file="TourPackageNightsValidityGroupedViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TourPackageNightsValidityGroupedViewModel
    /// </summary>
    public class TourPackageNightsValidityGroupedViewModel
    {
        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public List<TourPackageNightsValidityViewModel> Records { get; set; }
    }
}