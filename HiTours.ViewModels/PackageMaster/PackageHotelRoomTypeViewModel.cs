// <copyright file="PackageHotelRoomTypeViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// PackageHotelRoomTypeViewModel
    /// </summary>
    /// <seealso cref="PckageBaseModel" />
    public class PackageHotelRoomTypeViewModel : PckageBaseModel
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
        [Required(ErrorMessage ="Name is required")]
        [Display(Name ="Room Type")]
        [Remote("IsDuplicate", "hotelroomtype", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public string Name { get; set; }
        ////public string Description { get; set; }
    }
}
