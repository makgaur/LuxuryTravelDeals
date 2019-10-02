// <copyright file="PackageCityModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using HiTours.Models;

    /// <summary>
    /// PackageCityModel
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class PackageCityModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int? StateId { get; set; }

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
        /// Gets or sets the city description.
        /// </summary>
        /// <value>
        /// The city description.
        /// </value>
        public string CityDescription { get; set; }

        /// <summary>
        /// Gets or sets the short detail.
        /// </summary>
        /// <value>
        /// The short detail.
        /// </value>
        public string ShortDetail { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

            /// <summary>
            /// Gets or sets the tour package city.
            /// </summary>
            /// <value>
            /// The tour package city.
            /// </value>
        public PackageStateModel PackageStateModel { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public PackageCountryModel PackageCountryModel { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public ICollection<PackageAreaModel> PackageAreaModels { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<DestinationModel> DestinationModels { get; set; }

        /// <summary>
        /// Gets or sets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<VendorInformationModel> VendorModels { get; set; }

        /// <summary>
        /// Gets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<TourPackageCityModel> TourPackageCity { get; internal set; }
    }
}