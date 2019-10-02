// <copyright file="Address.cs" company="Luxury Travel Deals">
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
    public class Address
    {
        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string adminDistrict { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string adminDistrict2 { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string countryRegion { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string formattedAddress { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string locality { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string landmark { get; set; }
    }
}
