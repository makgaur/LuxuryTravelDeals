// <copyright file="AmenitiesMasterModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Models;

    /// <summary>
    /// PackageCityModel
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class AmenitiesMasterModel
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Model.
        /// </summary>
        /// <value>
        /// The Vendor Model.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public bool IsHotelOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public bool IsRoomOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public bool IsFilter { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<HotelierAmenitiesModel> HotelierAmenitiesModels { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<HotelierRoomAmenetiesModel> HotelierRoomAmenetiesModels { get; set; }
    }
}