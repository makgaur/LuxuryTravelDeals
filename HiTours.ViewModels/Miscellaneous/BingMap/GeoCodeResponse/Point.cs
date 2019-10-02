// <copyright file="Point.cs" company="Luxury Travel Deals">
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
    public class Point
    {
        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Gets or sets gets all asynchronous.
        /// </summary>
        public List<double> coordinates { get; set; }
    }
}
