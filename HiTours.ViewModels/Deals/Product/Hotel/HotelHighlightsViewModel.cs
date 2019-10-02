// <copyright file="HotelHighlightsViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.Deals.Product.Hotel
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
    public class HotelHighlightsViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public int StarRating { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public string Description { get; set; }
    }
}