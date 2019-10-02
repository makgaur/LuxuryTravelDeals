// <copyright file="RatePlanAmenitiesModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// RoomTypeModel
    /// </summary>
    public class RatePlanAmenitiesModel
    {
        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        [Key]
        public int RA_Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int RA_RatePlanId { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public int RA_AmenitiesId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool RA_IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public RatePlanModel RatePlanModel { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public AmenitiesMasterModel AmenitiesMasterModel { get; set; }
    }
}