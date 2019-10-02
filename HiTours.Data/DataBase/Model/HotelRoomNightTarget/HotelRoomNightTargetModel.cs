// <copyright file="HotelRoomNightTargetModel.cs" company="Tetraskelion Softwares Pvt. Ltd.">
// Copyright (c) Tetraskelion Softwares Pvt. Ltd. All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Hotel Room Night Target Model
    /// </summary>
    public class HotelRoomNightTargetModel
    {
        /// <summary>
        /// Gets or sets the hotel room night identifier.
        /// </summary>
        /// <value>
        /// The hotel room night identifier.
        /// </value>
        [Key]
        public Guid HotelRoomNightId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Gets or sets the financial year.
        /// </summary>
        /// <value>
        /// The financial year.
        /// </value>
        public string FinancialYear { get; set; }

        /// <summary>
        /// Gets or sets the room night.
        /// </summary>
        /// <value>
        /// The room night.
        /// </value>
        public double? RoomNight { get; set; }

        /// <summary>
        /// Gets or sets the available room count.
        /// </summary>
        /// <value>
        /// The available room count.
        /// </value>
        public int? AvailableRoomCount { get; set; }

        /// <summary>
        /// Gets or sets the hotel price identifier.
        /// </summary>
        /// <value>
        /// The hotel price identifier.
        /// </value>
        public Guid HotelPriceId { get; set; }
    }
}