// <copyright file="HolidayPriceViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    /// <summary>
    /// Holiday Price View Model
    /// </summary>
    public class HolidayPriceViewModel
    {
        /// <summary>
        /// Gets or sets the booking price.
        /// </summary>
        /// <value>
        /// The booking price.
        /// </value>
        public decimal BookingPrice { get; set; }

        /// <summary>
        /// Gets or sets the weekend price.
        /// </summary>
        /// <value>
        /// The weekend price.
        /// </value>
        public decimal WeekendPrice { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public decimal GstAmount { get; set; }

        /// <summary>
        /// Gets or sets the adults count.
        /// </summary>
        /// <value>
        /// The adults count.
        /// </value>
        public int AdultsCount { get; set; }

        /// <summary>
        /// Gets or sets the deposit amount.
        /// </summary>
        /// <value>
        /// The deposit amount.
        /// </value>
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Hotel Deal.
        /// </summary>
        /// <value>
        /// Is Hotel Deal.
        /// </value>
        public bool IsHotel { get; set; }
    }
}