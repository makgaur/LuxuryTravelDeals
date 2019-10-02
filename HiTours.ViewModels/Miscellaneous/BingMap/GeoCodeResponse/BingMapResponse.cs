// <copyright file="BingMapResponse.cs" company="Luxury Travel Deals">
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
    public class BingMapResponse
    {
        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string authenticationResultCode { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string brandLogoUri { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string copyright { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public List<ResourceSet> resourceSets { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public int statusCode { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string statusDescription { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string traceId { get; set; }
    }
}
