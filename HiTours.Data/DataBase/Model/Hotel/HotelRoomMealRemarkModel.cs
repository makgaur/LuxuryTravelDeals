// <copyright file="HotelRoomMealRemarkModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Hotel Room Meal Remark Model
    /// </summary>
    public class HotelRoomMealRemarkModel
    {
        /// <summary>
        /// Gets or sets the hotel price identifier.
        /// </summary>
        /// <value>
        /// The hotel price identifier.
        /// </value>
        [Key]
        public Guid HotelPriceId { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }
    }
}