// <copyright file="PackageHotelRoomTypeModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HiTours.Models;

    /// <summary>
    /// PackageHotelRoomType
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class PackageHotelRoomTypeModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<HotelierRoomConfigurationModel> HotelierRoomConfigurationModels { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<DealRoomConfigurationModel> DealsRoomConfigurationModels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public List<DealsPromotion_RoomType> DealPromotionRoomTypeModel { get; set; }
    }
}
