// <copyright file="PackageRegionModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using HiTours.Models;

    /// <summary>
    /// PackageRegion
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class PackageRegionModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the package country model.
        /// </summary>
        /// <value>
        /// The package country model.
        /// </value>
        public ICollection<PackageCountryModel> PackageCountry { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public ICollection<TourPackageCityModel> TourPackageCity { get; set; }

        /// <summary>
        /// Gets or sets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<DestinationModel> DestinationModels { get; set; }
    }
}