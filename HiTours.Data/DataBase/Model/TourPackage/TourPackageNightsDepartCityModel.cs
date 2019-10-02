// <copyright file="TourPackageNightsDepartCityModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TourPackageNightsDepartCityModel
    /// </summary>
    public class TourPackageNightsDepartCityModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights identifier.
        /// </summary>
        /// <value>
        /// The tour package nights identifier.
        /// </value>
        public Guid TourPackageNightsId { get; set; }

        /// <summary>
        /// Gets or sets the depart city identifier.
        /// </summary>
        /// <value>
        /// The depart city identifier.
        /// </value>
        public int DepartCityId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public TourPackageNightModel TourPackageNight { get; set; }

        /// <summary>
        /// Gets or sets the name of the depart city.
        /// </summary>
        /// <value>
        /// The name of the depart city.
        /// </value>
        public string DepartCityName { get; set; }
    }
}