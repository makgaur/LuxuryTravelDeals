// <copyright file="SeoPageTypeDetail.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    /// <summary>
    /// SeoPageTypeDetail
    /// </summary>
    public class SeoPageTypeDetail
    {
        /// <summary>
        /// Gets or sets the name of the seo page type.
        /// </summary>
        /// <value>
        /// The name of the seo page type.
        /// </value>
        public string SeoPageTypeName { get; set; }

        /// <summary>
        /// Gets or sets the page identifier title.
        /// </summary>
        /// <value>
        /// The page identifier title.
        /// </value>
        public string PageIdTitle { get; set; }

        /// <summary>
        /// Gets or sets the page identifier option URL.
        /// </summary>
        /// <value>
        /// The page identifier option URL.
        /// </value>
        public string PageIdOptionUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the page.
        /// </summary>
        /// <value>
        /// The type of the page.
        /// </value>
        public string PageType { get; set; }
    }
}