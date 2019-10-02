// <copyright file="DestinationModel.cs" company="Luxury Travel Deals">
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
    /// Destination Model
    /// </summary>
    public class DestinationModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the Destination identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        [Key]
        public int D_Id { get; set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public Guid D_PackageId { get; set; }

        /// <summary>
        /// Gets or sets the Vendor.
        /// </summary>
        /// <value>
        /// The Vendor.
        /// </value>
        public int D_VendorId { get; set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public short D_Region { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public short D_Country { get; set; }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public int? D_State { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public int D_City { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string D_IATACode { get; set; }

        /// <summary>
        /// Gets or sets the Number of nights.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public int D_Nights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Active Status.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool D_IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Active Status.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public TourPackageModel PackageModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Active Status.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public PackageCityModel CityModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Active Status.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public PackageRegionModel RegionModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Active Status.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public PackageStateModel StateModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Active Status.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public PackageCountryModel CountryModel { get; set; }

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public ICollection<DestinationValidityModel> DestinationValidityModels { get; set; }
    }
}