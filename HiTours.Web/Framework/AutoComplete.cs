// <copyright file="AutoComplete.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    /// <summary>
    /// AutoComplete
    /// </summary>
    public class AutoComplete
    {
        /// <summary>
        /// Gets or sets the ajax URL.
        /// </summary>
        /// <value>
        /// The ajax URL.
        /// </value>
        public string AjaxUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AutoComplete"/> is disabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disabled; otherwise, <c>false</c>.
        /// </value>
        public bool Disabled { get; set; }
    }
}