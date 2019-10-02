// <copyright file="TourPackageBookDateViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// TourPackageBookDateViewModel
    /// </summary>
    /// <seealso cref="PckageBaseModel" />
    public class TourPackageBookDateViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tour package identifier.
        /// </summary>
        /// <value>
        /// The tour package identifier.
        /// </value>
        public Guid TourPackageId { get; set; }

        /// <summary>
        /// Gets or sets the booking valid from.
        /// </summary>
        /// <value>
        /// The booking valid from.
        /// </value>
        [Display(Name = "Booking Valid From")]
        [Required(ErrorMessage = "this field is required")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BookingValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the booking valid to.
        /// </summary>
        /// <value>
        /// The booking valid to.
        /// </value>
        [Display(Name = "Booking Valid To")]
        [Required(ErrorMessage = "this field is required")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BookingValidTo { get; set; }
    }
}
