// <copyright file="ErrorViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Listing View Model
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorViewModel"/> class.
        /// Default Constructor
        /// </summary>
        public ErrorViewModel()
        {
            this.SearchTermViewModel = new SearchTermViewModel();
            this.ResultViewModels = new List<PackageCurationViewModel>();
            this.TrendingDeals = new List<PackageCurationViewModel>();
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public SearchTermViewModel SearchTermViewModel { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public List<PackageCurationViewModel> ResultViewModels { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public List<PackageCurationViewModel> TrendingDeals { get; set; }
    }
}