// <copyright file="DestinationGridViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// RoomTypeModel
    /// </summary>
    public class DestinationGridViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        public string VendorName { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int DestinationId { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string IATACode { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public int NightCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool IsActive { get; set; }
    }
}