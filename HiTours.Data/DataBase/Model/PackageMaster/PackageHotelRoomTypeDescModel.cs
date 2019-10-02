// <copyright file="PackageHotelRoomTypeDescModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// HotelRoomTypeDescModel
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class PackageHotelRoomTypeDescModel
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
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the hotel room type identifier.
        /// </summary>
        /// <value>
        /// The hotel room type identifier.
        /// </value>
        public short HotelRoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>
        /// The state of the object.
        /// </value>
        [NotMapped]
        public EntityState ObjectState { get; set; }

        /// <summary>
        /// Gets or sets the package hotel.
        /// </summary>
        /// <value>
        /// The package hotel.
        /// </value>
        public PackageHotelModel PackageHotel { get; set; }
    }
}
