// <copyright file="HotelTermModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// HotelTerm
    /// </summary>
    public class HotelTermModel
    {
        /// <summary>
        /// Gets or sets the hotel terms identifier.
        /// </summary>
        /// <value>
        /// The hotel terms identifier.
        /// </value>
        [Key]
        public Guid HotelTermsId { get; set; }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        public Guid SourceId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the entry.
        /// </summary>
        /// <value>
        /// The type of the entry.
        /// </value>
        public string EntryType { get; set; }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public short SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the special rate.
        /// </summary>
        /// <value>
        /// The special rate.
        /// </value>
        public float SpecialRate { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
    }
}