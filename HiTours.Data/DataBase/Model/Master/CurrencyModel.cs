// <copyright file="CurrencyModel.cs" company="Luxury Travel Deals">
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
    public class CurrencyModel : BaseModel
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public string Sign { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal? ExchangeRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal? SecurityMargin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool ApplySecurityMargin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal? SecurityMarginForOneMonth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal? SecurityMarginAfterOneMonth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal? EffectiveExchangeRateForOneMonth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal? EffectiveExchangeRateForAfterOneMonth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public int? CurrencyDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the Country Identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public short Country { get; set; }

        /// <summary>
        /// Gets or sets the tour package Country.
        /// </summary>
        /// <value>
        /// The tour package Country.
        /// </value>
        public PackageCountryModel PackageCountry { get; set; }

        /// <summary>
        /// Gets or sets the tour package Country.
        /// </summary>
        /// <value>
        /// The tour package Country.
        /// </value>
        public ICollection<VendorInformationModel> VendorModels { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public bool IsActive { get; set; }
    }
}