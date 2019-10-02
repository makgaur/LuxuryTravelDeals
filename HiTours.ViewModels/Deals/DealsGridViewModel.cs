// <copyright file="DealsGridViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///  Vendor Model
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class DealsGridViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string PreviewUrl { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        public bool IsLive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out Time.
        /// </summary>
        /// <value>
        /// The Check Out Time.
        /// </value>
        public DateTime? ValidTo { get; set; }
    }
}