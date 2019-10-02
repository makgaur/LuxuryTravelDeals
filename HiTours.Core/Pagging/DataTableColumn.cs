// <copyright file="DataTableColumn.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// DataTable Column
    /// </summary>
    public class DataTableColumn
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataTableColumn"/> is orderable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if orderable; otherwise, <c>false</c>.
        /// </value>
        public bool Orderable { get; set; }

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>
        /// The search.
        /// </value>
        public DataTableSearch Search { get; set; }
    }
}
