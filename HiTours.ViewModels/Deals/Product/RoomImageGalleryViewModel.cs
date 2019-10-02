// <copyright file="RoomImageGalleryViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels.Deals.Product;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class RoomImageGalleryViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public List<HotelierRoomImageViewModel> HotelierRoomImageViewModels { get; set; }

        /// <summary>
        /// Gets or sets banner View Model
        /// </summary>
        public List<ProductReviewViewModel> ProductReviewViewModels { get; set; }
    }
}
