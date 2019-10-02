// <copyright file="DataTableResult.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// DataTable Result
    /// </summary>
    public class DataTableResult
    {
        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        /// <summary>
        /// Gets or sets the records total.
        /// </summary>
        /// <value>
        /// The records total.
        /// </value>
        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }

        /// <summary>
        /// Gets or sets the records filtered.
        /// </summary>
        /// <value>
        /// The records filtered.
        /// </value>
        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [JsonProperty(PropertyName = "error")]
        public object Error { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty(PropertyName = "data")]
        public List<object> Data { get; set; }
    }
}
