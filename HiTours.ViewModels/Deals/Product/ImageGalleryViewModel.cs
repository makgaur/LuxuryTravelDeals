// <copyright file="ImageGalleryViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels;
    using HiTours.ViewModels.Deals.Product;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class ImageGalleryViewModel
    {
        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<DealsImageViewModel> DealsImageViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<ProductReviewViewModel> ProductReviewViewModels { get; set; }
    }
}
