// <copyright file="DataTableSearch.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// DataTable Search
    /// </summary>
    public class DataTableSearch
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataTableSearch"/> is regex.
        /// </summary>
        /// <value>
        ///   <c>true</c> if regex; otherwise, <c>false</c>.
        /// </value>
        public bool Regex { get; set; }
    }
}
