// <copyright file="RatePlanModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// RoomTypeModel
    /// </summary>
    public class RatePlanModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        [Key]
        public int RP_Id { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int RP_RoomConfigId { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string RP_Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public DateTime RP_BookingStartDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public DateTime RP_BookingEndDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public DateTime RP_TravelStartDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public DateTime RP_TravelEndDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal RP_SellingPrice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal RP_FakePrice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal RP_ExtraAdultPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal RP_ExtraChildPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal RP_ExtraInfantPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool RP_IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool RP_IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public RoomConfigurationModel RoomConfigModel { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<RatePlanAmenitiesModel> RatePlanAmenitiesModels { get; set; }
    }
}