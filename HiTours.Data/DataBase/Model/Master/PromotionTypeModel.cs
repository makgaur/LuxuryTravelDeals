// <copyright file="PromotionTypeModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// PackageCountry
    /// </summary>
    public class PromotionTypeModel
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
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public ICollection<HotelierPromotionModel> HotelierPromotionModels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public ICollection<DealsPromotionModel> DealsPromotionModels { get; set; }
    }
}