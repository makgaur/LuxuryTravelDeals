// <copyright file="FilterViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// BookingOnDate
    /// </summary>
    public class FilterViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterViewModel"/> class.
        /// Initialize
        /// </summary>
        public FilterViewModel()
        {
            this.FilterOptions = new List<FilterOptionViewModel>();
        }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public Guid FilterId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public int SelectType { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public bool Check { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public List<FilterOptionViewModel> FilterOptions { get; set; }
    }
}