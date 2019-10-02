// <copyright file="DomainSetting.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    /// <summary>
    /// Application Domain Setting
    /// </summary>
    public class DomainSetting
    {
        /// <summary>
        /// The application name
        /// </summary>
        public const string ApplicationName = "HiTours";

        /// <summary>
        /// Gets or sets the web API service URL.
        /// </summary>
        /// <value>
        /// The web API service URL.
        /// </value>
        public string GoogleMapKey { get; set; }

        /// <summary>
        /// Gets or sets the web API service URL.
        /// </summary>
        /// <value>
        /// The web API service URL.
        /// </value>
        public string WebApiServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the web site URL.
        /// </summary>
        /// <value>
        /// The web site URL.
        /// </value>
        public string WebSiteUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [razorpay live].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [razorpay live]; otherwise, <c>false</c>.
        /// </value>
        public bool RazorpayLive { get; set; }
    }
}