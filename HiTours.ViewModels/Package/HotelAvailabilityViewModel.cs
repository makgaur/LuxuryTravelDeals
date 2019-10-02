// <copyright file="HotelAvailabilityViewModel.cs" company="Tetraskelion Softwares Pvt. Ltd.">
// Copyright (c) Tetraskelion Softwares Pvt. Ltd. All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Hotel Availability View Model
    /// </summary>
    public class HotelAvailabilityViewModel
    {
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
        /// Gets or sets the total room.
        /// </summary>
        /// <value>
        /// The total room.
        /// </value>
        public int? RoomPerDay { get; set; }

        /// <summary>
        /// Gets or sets the total avail room.
        /// </summary>
        /// <value>
        /// The total avail room.
        /// </value>
        public int? TotalAvailableRoom { get; set; }

        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        /// <value>
        /// My property.
        /// </value>
        public List<DateRangeViewModel> BlackOutDateRange { get; set; }

        /// <summary>
        /// Gets or sets the nights.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        public string Nights { get; set; }

        /// <summary>
        /// Gets or sets the single cost.
        /// </summary>
        /// <value>
        /// The single cost.
        /// </value>
        public decimal SingleCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        public bool IsExtraNight { get; set; }

        /// <summary>
        /// Gets or sets the discount cost.
        /// </summary>
        /// <value>
        /// The discount cost.
        /// </value>
        public decimal DiscountCost { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the fit single.
        /// </summary>
        /// <value>
        /// The fit single.
        /// </value>
        public double? DoubleCost { get; set; }

        /// <summary>
        /// Gets or sets the hotel room night identifier.
        /// </summary>
        /// <value>
        /// The hotel room night identifier.
        /// </value>
        public Guid HotelPriceId { get; set; }
    }
}