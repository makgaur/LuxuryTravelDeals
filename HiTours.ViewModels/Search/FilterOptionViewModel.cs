// <copyright file="FilterOptionViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Package view Model
    /// </summary>
    public class FilterOptionViewModel
    {
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public Guid FilterOptionId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Display { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public bool IsRange { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the deal type identifier.
        /// </summary>
        /// <value>
        /// The deal type identifier.
        /// </value>
        public decimal MinValue { get; set; }

        /// <summary>
        /// Gets or sets the valid from.
        /// </summary>
        /// <value>
        /// The valid from.
        /// </value>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the valid to.
        /// </summary>
        /// <value>
        /// The valid to.
        /// </value>
        public string DealIds { get; set; }

        /// <summary>
        /// Gets or sets the valid to.
        /// </summary>
        /// <value>
        /// The valid to.
        /// </value>
        public int ResultCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the IsSelected.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        public bool IsSelected { get; set; }
    }
}