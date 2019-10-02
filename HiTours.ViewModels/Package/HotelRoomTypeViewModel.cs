// <copyright file="HotelRoomTypeViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// HotelRoomTypeViewModel
    /// </summary>
    public class HotelRoomTypeViewModel
    {
        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public string RoomType { get; set; }

        /// <summary>
        /// Gets or sets the double cost.
        /// </summary>
        /// <value>
        /// The double cost.
        /// </value>
        public double? DoubleCost { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        public Guid RoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the room type description.
        /// </summary>
        /// <value>
        /// The room type description.
        /// </value>
        [DisplayFormat(DataFormatString = "{0:F}")]
        public string RoomTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the hotel price identifier.
        /// </summary>
        /// <value>
        /// The hotel price identifier.
        /// </value>
        public Guid HotelPriceId { get; set; }

        /// <summary>
        /// Gets or sets the total rooms.
        /// </summary>
        /// <value>
        /// The total rooms.
        /// </value>
        public int? RoomPerDay { get; set; }

        /// <summary>
        /// Gets or sets the validity from.
        /// </summary>
        /// <value>
        /// The validity from.
        /// </value>
        public DateTime ValidityFrom { get; set; }

        /// <summary>
        /// Gets or sets the validity to.
        /// </summary>
        /// <value>
        /// The validity to.
        /// </value>
        public DateTime ValidityTo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rate increase by per.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rate increase by per; otherwise, <c>false</c>.
        /// </value>
        public bool IsRateIncreaseByPer { get; set; }

        /// <summary>
        /// Gets or sets the rate increase.
        /// </summary>
        /// <value>
        /// The rate increase.
        /// </value>
        public double RateIncrease { get; set; }

        /// <summary>
        /// Gets or sets the mark up amount.
        /// </summary>
        /// <value>
        /// The mark up amount.
        /// </value>
        public double? MarkUpAmount { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        /// <value>
        /// The discount amount.
        /// </value>
        public double? DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the discount percent.
        /// </summary>
        /// <value>
        /// The discount percent.
        /// </value>
        public double? DiscountPercent { get; set; } = 0;

        /// <summary>
        /// Gets or sets the black out date range.
        /// </summary>
        /// <value>
        /// The black out date range.
        /// </value>
        public List<DateRangeViewModel> BlackOutDateRange { get; set; }

        /// <summary>
        /// Gets or sets the booked on dates.
        /// </summary>
        /// <value>
        /// The booked on dates.
        /// </value>
        public List<RoomAvailabilityViewModel> BookedOnDates { get; set; }

        /// <summary>
        /// Gets or sets the nights.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        public string Nights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        public bool IsExtraNight { get; set; }

        /// <summary>
        /// Gets or sets the requested rooms.
        /// </summary>
        /// <value>
        /// The requested rooms.
        /// </value>
        public int RequestedRooms { get; set; }

        /// <summary>
        /// Gets or sets the specific date price list.
        /// </summary>
        /// <value>
        /// The specific date price list.
        /// </value>
        public List<SpecificPriceViewModel> SpecificDatePriceList { get; set; }
    }
}