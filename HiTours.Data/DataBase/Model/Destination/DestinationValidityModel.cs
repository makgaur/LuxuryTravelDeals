// <copyright file="DestinationValidityModel.cs" company="Luxury Travel Deals">
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
    public class DestinationValidityModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the Destination Validity identifier.
        /// </summary>
        /// <value>
        /// The Destination Validity identifier.
        /// </value>
        [Key]
        public int DV_Id { get; set; }

        /// <summary>
        /// Gets or sets the DestinationId.
        /// </summary>
        /// <value>
        /// The DestinationId.
        /// </value>
        public int DV_DestinationId { get; set; }

        /// <summary>
        /// Gets or sets the Validity Start Date.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public DateTime DV_ValidityStartDate { get; set; }

        /// <summary>
        /// Gets or sets the Validity End Date.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public DateTime DV_ValidityEndDate { get; set; }

        /// <summary>
        /// Gets or sets the Adult Price Per Person on Based of Double Occupency.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public decimal DV_AdultPriceDBL { get; set; }

        /// <summary>
        /// Gets or sets the Child Price Per Person on Based of Double Occupency.
        /// </summary>
        /// <value>
        /// The Child Price Per Person on Based of Double Occupency.
        /// </value>
        public decimal DV_ChildPriceDBL { get; set; }

        /// <summary>
        /// Gets or sets the Infant Price Per Person on Based of Double Occupency.
        /// </summary>
        /// <value>
        /// The Infant Price Per Person on Based of Double Occupency.
        /// </value>
        public decimal DV_InfantPriceDBL { get; set; }

        /// <summary>
        /// Gets or sets the Extra Adult Price.
        /// </summary>
        /// <value>
        /// The Extra Adult Price.
        /// </value>
        public decimal DV_ExtraAdultPrice { get; set; }

        /// <summary>
        /// Gets or sets the Extra Child Price.
        /// </summary>
        /// <value>
        /// The Extra Child Price.
        /// </value>
        public decimal DV_ExtraChildPrice { get; set; }

        /// <summary>
        /// Gets or sets the Extra Infant Price
        /// </summary>
        /// <value>
        /// The Extra Infant Price.
        /// </value>
        public decimal DV_ExtraInfantPrice { get; set; }

        /// <summary>
        /// Gets or sets Supplement Adult Value.
        /// </summary>
        /// <value>
        /// The Supplement Adult Value.
        /// </value>
        public decimal DV_SupplementAdult { get; set; }

        /// <summary>
        /// Gets or sets the Adult Min Age.
        /// </summary>
        /// <value>
        /// The Adult Min Age Default 12.
        /// </value>
        public int DV_AdultMinAge { get; set; }

        /// <summary>
        /// Gets or sets infant Max Age.
        /// </summary>
        /// <value>
        /// The infant Max Age. Default 2
        /// </value>
        public int DV_InfantMaxAge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Is Active.
        /// </summary>
        /// <value>
        /// The Is Active.
        /// </value>
        public bool DV_IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Supplement Infant Value.
        /// </summary>
        /// <value>
        /// The Supplement Infant Value.
        /// </value>
        public DestinationModel DestinationModel { get; set; }
    }
}