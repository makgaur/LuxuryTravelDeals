// <copyright file="DealsFlightModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class DealsFlightModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int InclusionId { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public DealsInclusionModel DealsInclusionModel { get; set; }

        /// <summary>
        /// Gets or sets the CountryId.
        /// </summary>
        /// <value>
        /// The CountryId.
        /// </value>
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the AdultPrice.
        /// </summary>
        /// <value>
        /// The AdultPrice.
        /// </value>
        public string CabinClass { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public string StartTime { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public string EndTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public bool AllDay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Days.
        /// </value>
        public bool IsActive { get; set; }
    }
}
