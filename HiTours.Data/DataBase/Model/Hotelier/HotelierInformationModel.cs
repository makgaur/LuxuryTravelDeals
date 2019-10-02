// <copyright file="HotelierInformationModel.cs" company="Luxury Travel Deals">
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
    ///  Vendor Package Relation Model
    /// </summary>
    public class HotelierInformationModel : BaseModel
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
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Url.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public TimeSpan? CheckIn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Check Out
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public TimeSpan? CheckOut { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Star Rating
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int StarRating { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Property Type
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int PropertyType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Open Check In
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool IsOpenCheckIn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Active.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Is Deleted
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Status.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Sub Status.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? SubStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Lat.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public decimal? Lat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Long.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public decimal? Long { get; set; }

        /// <summary>
        /// Gets or sets a value to Hotelier Property Type Model
        /// </summary>
        public HotelierPropertyTypeModel HotelierPropertyTypeModel { get; set; }

        /// <summary>
        /// Gets or sets a value to Hotelier Property Type Model
        /// </summary>
        public VendorInformationModel VendorInformationModel { get; set; }

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
        public ICollection<HotelierRoomConfigurationModel> HotelierRoomConfigModels { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<HotelierContentModel> HotelierContentsModels { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<HotelierImageModel> HotelierImageModels { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<HotelierReviewModel> HotelierReviewModels { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<HotelierPromotionModel> HotelierPromotionModels { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? Area { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? City { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public int? State { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public short? Country { get; set; }

        /// <summary>
        /// Gets or sets the Package Id.
        /// </summary>
        /// <value>
        /// The Package Id.
        /// </value>
        public string Name { get; set; }
    }
}
