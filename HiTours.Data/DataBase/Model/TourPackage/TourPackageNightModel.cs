// <copyright file="TourPackageNightModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// TourPackageNightModel
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class TourPackageNightModel
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
        /// Gets or sets the no of nights.
        /// </summary>
        /// <value>
        /// The no of nights.
        /// </value>
        public byte NoOfNights { get; set; }

        /// <summary>
        /// Gets or sets the deposit amount.
        /// </summary>
        /// <value>
        /// The deposit amount.
        /// </value>
        public decimal? DepositAmount { get; set; }

        /// <summary>
        /// Gets or sets the package price.
        /// </summary>
        /// <value>
        /// The package price.
        /// </value>
        public decimal PackagePrice { get; set; }

        /// <summary>
        /// Gets or sets the package discount price.
        /// </summary>
        /// <value>
        /// The package discount price.
        /// </value>
        public decimal PackageDiscountPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        public bool IsExtraNight { get; set; }

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
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>
        /// The state of the object.
        /// </value>
        [NotMapped]
        public Microsoft.EntityFrameworkCore.EntityState ObjectState { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights validity.
        /// </summary>
        /// <value>
        /// The tour package nights validity.
        /// </value>
        public List<TourPackageNightsValidityModel> TourPackageNightsValidity { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights validity.
        /// </summary>
        /// <value>
        /// The tour package nights validity.
        /// </value>
        public List<TourPackageNightsDepartCityModel> TourPackageNightsDepartCity { get; set; }

        /// <summary>
        /// Gets or sets the tour package.
        /// </summary>
        /// <value>
        /// The tour package.
        /// </value>
        public TourPackageModel TourPackage { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [NotMapped]
        public decimal Discount { get; set; }
    }
}