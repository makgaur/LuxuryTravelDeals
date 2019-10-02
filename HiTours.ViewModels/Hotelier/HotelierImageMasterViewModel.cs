// <copyright file="HotelierImageMasterViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>
namespace HiTours.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// Hotelier Image model
    /// </summary>
    public class HotelierImageMasterViewModel
    {
        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public List<HotelierImageViewModel> HotelierImageViewModels { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public List<HotelierRoomImageViewModel> HotelierRoomImageViewModels { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public List<HotelierRoomConfigurationViewModel> HotelierRoomConfigurationViewModels { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public int HotelId { get; set; }
    }
}
