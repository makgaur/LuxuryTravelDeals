// <copyright file="RoomAvailabilityViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;

    /// <summary>
    /// Room Availability View Model
    /// </summary>
    public class RoomAvailabilityViewModel
    {
        /// <summary>
        /// Gets or sets the booked date.
        /// </summary>
        /// <value>
        /// The booked date.
        /// </value>
        public DateTime BookedDate { get; set; }

        /// <summary>
        /// Gets or sets the total booked.
        /// </summary>
        /// <value>
        /// The total booked.
        /// </value>
        public int TotalBooked { get; set; }

        /// <summary>
        /// Gets or sets the available room.
        /// </summary>
        /// <value>
        /// The available room.
        /// </value>
        public int AvailableRoom { get; set; }

        /// <summary>
        /// Gets or sets the room per day.
        /// </summary>
        /// <value>
        /// The room per day.
        /// </value>
        public int RoomPerDay { get; set; }
    }
}