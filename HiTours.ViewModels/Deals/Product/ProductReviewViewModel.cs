// <copyright file="ProductReviewViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.Deals.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///  Vendor Model
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class ProductReviewViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Review { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets banner View Model
        /// </summary>
        public bool UserRecommend { get; set; }
    }
}