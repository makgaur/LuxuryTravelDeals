// <copyright file="Resource.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>
namespace HiTours.ViewModels.Miscellaneous
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// BingMapResponse
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public class Resource
    {
        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string __type { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public List<double> bbox { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public Point point { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public Address address { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string confidence { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string entityType { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public List<GeocodePoint> geocodePoints { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public List<string> matchCodes { get; set; }
    }
}
