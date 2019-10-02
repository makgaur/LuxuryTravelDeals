// <copyright file="PackageRoomDetailViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    /// <summary>
    /// PackageRoomDetailViewModel
    /// </summary>
    public class PackageRoomDetailViewModel
    {
        /// <summary>
        /// Gets or sets the room no.
        /// </summary>
        /// <value>
        /// The room no.
        /// </value>
        public short RoomNo { get; set; }

        /// <summary>
        /// Gets or sets the adult.
        /// </summary>
        /// <value>
        /// The adult.
        /// </value>
        public int Adult { get; set; }

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        public int Child { get; set; }
    }
}