// <copyright file="SearchTermViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Search Term View Model
    /// </summary>
    public class SearchTermViewModel
    {
        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public int Adults { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public int Kids { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public int Infants { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public int Rooms { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public string EndDate { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public string CompleteDateString { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public DateTime? StartDateVar { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public DateTime? EndDateVar { get; set; }

        /// <summary>
        /// Gets or sets the SearchType. Reference in Enums.SearchType
        /// </summary>
        /// <value>
        /// The SearchType.
        /// </value>
        public int SearchType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the SearchType. Reference in Enums.SearchType
        /// </summary>
        /// <value>
        /// The SearchType.
        /// </value>
        public bool ShowSearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the SearchType. Reference in Enums.SearchType
        /// </summary>
        /// <value>
        /// The SearchType.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the KidsAge.
        /// </summary>
        /// <value>
        /// The SearchType.
        /// </value>
        public string KidsAge { get; set; }
    }
}