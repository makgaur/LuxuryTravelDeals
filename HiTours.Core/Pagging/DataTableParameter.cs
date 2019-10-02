// <copyright file="DataTableParameter.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// DataTableParameter
    /// </summary>
    public class DataTableParameter
    {
        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        public int Draw { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>
        /// The search.
        /// </value>
        public DataTableSearch Search { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public IEnumerable<DataTableOrder> Order { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public IEnumerable<DataTableColumn> Columns { get; set; }
    }
}
