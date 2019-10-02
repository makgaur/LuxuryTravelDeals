// <copyright file="TourPackageNightsValiditys.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public class TourPackageNightsValiditys : PckageBaseModel
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
        /// Gets or sets the hotel room type identifier.
        /// </summary>
        /// <value>
        /// The hotel room type identifier.
        /// </value>
        public short HotelRoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the room type.
        /// </summary>
        /// <value>
        /// The name of the room type.
        /// </value>
        public string RoomTypeName { get; set; }

        /// <summary>
        /// Gets or sets the rate valid from.
        /// </summary>
        /// <value>
        /// The rate valid from.
        /// </value>
        public DateTime RateValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the rate valid to.
        /// </summary>
        /// <value>
        /// The rate valid to.
        /// </value>
        public DateTime RateValidTo { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public ICollection<TourPackageNightsValidityViewModel> TourPackageNightsValidity { get; set; }

        /// <summary>
        /// Gets or sets the package price.
        /// </summary>
        /// <value>
        /// The package price.
        /// </value>
        public decimal PackagePrice { get; set; }

        /// <summary>
        /// Gets or sets the package valid from.
        /// </summary>
        /// <value>
        /// The package valid from.
        /// </value>
        public DateTime PackageValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the package valid to.
        /// </summary>
        /// <value>
        /// The package valid to.
        /// </value>
        public DateTime PackageValidTo { get; set; }

        /// <summary>
        /// Gets or sets the descriptions.
        /// </summary>
        /// <value>
        /// The descriptions.
        /// </value>
        public string Descriptions { get; set; }
    }
}
