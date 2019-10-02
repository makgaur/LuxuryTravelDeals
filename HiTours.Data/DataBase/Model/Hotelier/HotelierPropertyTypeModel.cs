// <copyright file="HotelierPropertyTypeModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>
namespace HiTours.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// Hotelier Property Type model
    /// </summary>
    public class HotelierPropertyTypeModel : BaseModel
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
        /// Gets or sets Property Type Name
        /// </summary>
        /// <value>
        /// Property Type Name
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets  a value indicating if is active or not.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public ICollection<HotelierInformationModel> HotelierInformationModels { get; set; }
    }
}

