// <copyright file="DealVisaModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// PackageCountry
    /// </summary>
    public class DealVisaModel : BaseModel
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
        /// Gets or sets the name of Currency
        /// </summary>
        /// <value>
        /// The name of the Currency.
        /// </value>
        public int? PackageId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public short? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public int? VendorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal? AdultPrice { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public decimal? ChildPrice { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public int? BufferDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public DealsPackageModel DealsPackageModel { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public decimal Markup { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string DocumentsRequired { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string PhotoSpecification { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string ProcessingTime { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string GeneralPolicy { get; set; }
    }
}