// <copyright file="TourPackageCityModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using HiTours.Models;

    /// <summary>
    /// TourPackageCityModel
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class TourPackageCityModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tour package identifier.
        /// </summary>
        /// <value>
        /// The tour package identifier.
        /// </value>
        public Guid TourPackageId { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public short RegionId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public short CountryId { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int? StateId { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the city description.
        /// </summary>
        /// <value>
        /// The city description.
        /// </value>
        public string CityDescription { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

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
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the tour package.
        /// </summary>
        /// <value>
        /// The tour package.
        /// </value>
        public TourPackageModel TourPackage { get; set; }

        /// <summary>
        /// Gets or sets the package city.
        /// </summary>y
        /// <value>
        /// The package city.
        /// </value>
        public PackageCityModel TourPackageCityCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the tour package.
        /// </summary>
        /// <value>
        /// The state of the tour package.
        /// </value>
        public PackageStateModel TourPackageCityState { get; set; }

        /// <summary>
        /// Gets or sets the package city.
        /// </summary>
        /// <value>
        /// The package city.
        /// </value>
        public PackageCountryModel TourPackageCityCountry { get; set; }

        /// <summary>
        /// Gets or sets the tour package city region.
        /// </summary>
        /// <value>
        /// The tour package city region.
        /// </value>
        public PackageRegionModel TourPackageCityRegion { get; set; }

        /// <summary>
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>
        /// The state of the object.
        /// </value>
        [NotMapped]
        public Microsoft.EntityFrameworkCore.EntityState ObjectState { get; set; }
    }
}