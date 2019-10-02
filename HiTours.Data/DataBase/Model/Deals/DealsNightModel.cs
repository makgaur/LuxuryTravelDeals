// <copyright file="DealsNightModel.cs" company="Luxury Travel Deals">
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
    public class DealsNightModel : BaseModel
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
        public int PackageId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public bool VisaRequired { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public DealsPackageModel DealsPackageModel { get; set; }

        /// <summary>
        /// Gets or sets collection of Itinerary Model
        /// </summary>
        public ICollection<DealsItineraryModel> DealsItineraryModels { get; set; }

        /// <summary>
        /// Gets or sets collection of Itinerary Model
        /// </summary>
        public List<DealsRatePlanModel> DealsRatePlanModel { get; set; }

        /// <summary>
        /// Gets or sets collection of Itinerary Model
        /// </summary>
        public ICollection<DealsDepartureDatesModel> DealsDepartureDatesModels { get; set; }
    }
}
