// <copyright file="LocationDealsCurationViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// RoomTypeModel
    /// </summary>
    public class LocationDealsCurationViewModel
    {
        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public int City { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public List<PackageCurationViewModel> Results { get; set; }
    }
}