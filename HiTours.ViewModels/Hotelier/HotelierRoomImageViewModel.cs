// <copyright file="HotelierRoomImageViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Hotelier Room Configuration model
    /// </summary>
    public class HotelierRoomImageViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int RoomConfigId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the tile image of room configuration.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public bool IsTileImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the tile image of room configuration.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public HotelierRoomConfigurationViewModel HotelierRoomConfigurationViewModel { get; set; }
    }
}
