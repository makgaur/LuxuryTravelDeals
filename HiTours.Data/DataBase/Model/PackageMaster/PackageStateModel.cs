// <copyright file="PackageStateModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using HiTours.Models;

    /// <summary>
    /// PackageStateModel
    /// </summary>
    public class PackageStateModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public short CountryId { get; set; }

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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public PackageCountryModel PackageCountryModel { get; set; }

        /// <summary>
        /// Gets or sets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<PackageCityModel> PackageCityModels { get; set; }

        /// <summary>
        /// Gets or sets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<DestinationModel> DestinationModels { get; set; }

        /// <summary>
        /// Gets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<TourPackageCityModel> TourPackageCity { get; internal set; }
    }
}