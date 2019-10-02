// <copyright file="PackageCountryModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// PackageCountry
    /// </summary>
    public class PackageCountryModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public short? RegionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the sort.
        /// </summary>
        /// <value>
        /// The name of the sort.
        /// </value>
        public string SortName { get; set; }

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
        /// Gets or sets the phone code.
        /// </summary>
        /// <value>
        /// The phone code.
        /// </value>
        public string PhoneCode { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public ICollection<TourPackageCityModel> TourPackageCity { get; set; }

        /// <summary>
        /// Gets or sets the tour package Currency.
        /// </summary>
        /// <value>
        /// The tour package Currency.
        /// </value>
        public ICollection<PackageCurrencyModel> PackageCurrency { get; set; }

        /// <summary>
        /// Gets or sets the package region.
        /// </summary>
        /// <value>
        /// The package region.
        /// </value>
        public PackageRegionModel PackageRegion { get; set; }

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
        public ICollection<PackageStateModel> PackageStateModels { get; set; }

        /// <summary>
        /// Gets or sets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<DestinationModel> DestinationModels { get; set; }

        /// <summary>
        /// Gets or sets the tour package Currency.
        /// </summary>
        /// <value>
        /// The tour package Currency.
        /// </value>
        public ICollection<CurrencyModel> CurrencyModel { get; set; }

        /// <summary>
        /// Gets or sets the Destination Models.
        /// </summary>
        /// <value>
        /// The Destination Models.
        /// </value>
        public ICollection<VendorInformationModel> VendorModels { get; set; }

        /// <summary>
        /// Gets or sets the Visa Models.
        /// </summary>
        /// <value>
        /// The Visa Models.
        /// </value>
        public ICollection<VisaModel> VisaModels { get; set; }
    }
}