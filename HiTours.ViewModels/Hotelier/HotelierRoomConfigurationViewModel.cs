// <copyright file="HotelierRoomConfigurationViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Hotelier Room Configuration model
    /// </summary>
    public class HotelierRoomConfigurationViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public string RoomName { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        [Required]
        [Display(Name = "Room Name")]
        public int RoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the no of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Maximum Adults")]
        public int Adult { get; set; }

        /// <summary>
        /// Gets or sets the no of Child.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Maximum Childrens")]
        public int Child { get; set; }

        /// <summary>
        /// Gets or sets the no of Infant.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Maximum Infants")]
        public int Infant { get; set; }

        /// <summary>
        /// Gets or sets the no of Child.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Free Child")]
        public int FreeChild { get; set; }

        /// <summary>
        /// Gets or sets the no of Infant.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Free Infants")]
        public int FreeInfant { get; set; }

        /// <summary>
        /// Gets or sets the no of max occupants.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Maximum Persons")]
        public int Max { get; set; }

        /// <summary>
        /// Gets or sets the age of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Minimum Adult Age")]
        public int AdultAge { get; set; }

        /// <summary>
        /// Gets or sets the age of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Minimum Child Age")]
        public int ChildAge { get; set; }

        /// <summary>
        /// Gets or sets the age of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Display(Name = "Maximum Infant Age")]
        public int InfantAge { get; set; }

        /// <summary>
        /// Gets or sets the html description of Room
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Room Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the html description of Room
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Required]
        [Display(Name = "Thumb Image (Ratio 57:37)")]
        public string CardImg { get; set; }

        /// <summary>
        /// Gets or sets the html description of Room
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public IFormFile CardImgFile { get; set; }

        /// <summary>
        /// Gets or sets the html description of Room
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        [Display(Name = "Room Ameneties")]
        public int[] Ameneties { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<SelectListItem> AmenetiesItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public ICollection<SelectListItem> RoomTypeItems { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public PackageHotelRoomTypeViewModel PackageHotelRoomTypeViewModel { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public HotelierInfoViewModel HotelierInfoViewModel { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public List<HotelierRoomImageViewModel> HotelierRoomImageViewModels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool IsSelected { get; set; }
    }
}
