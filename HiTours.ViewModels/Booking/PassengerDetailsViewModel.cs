// <copyright file="PassengerDetailsViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// PassengerDetailsViewModel
    /// </summary>
    public class PassengerDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        /// <value>
        /// The room number.
        /// </value>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Gets or sets the person number.
        /// </summary>
        /// <value>
        /// The person number.
        /// </value>
        public int Adults { get; set; }

        /// <summary>
        /// Gets or sets the type of the person.
        /// </summary>
        /// <value>
        /// The type of the person.
        /// </value>
        public int PersonType { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking person detail.
        /// </summary>
        /// <value>
        /// The hotel booking person detail.
        /// </value>
        public List<HotelBookingPersonDetailViewModel> PersonDetails { get; set; }
    }
}