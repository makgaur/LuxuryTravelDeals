// <copyright file="DestinationAddViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// RoomTypeModel
    /// </summary>
    public class DestinationAddViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the Destination identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        public int D_Id { get; set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public Guid D_PackageId { get; set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [Required(ErrorMessage = "Vendor is Required")]
        [Display(Name = "Vendor")]
        public int D_VendorId { get; set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [Required(ErrorMessage = "Region is Required")]
        [Display(Name = "Region")]
        public short D_Region { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        [Required(ErrorMessage = "Counrty is Required")]
        [Display(Name = "Country")]
        public short D_Country { get; set; }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "State")]
        public int? D_State { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City")]
        public int D_City { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "IATA Code is Required")]
        [Display(Name = "IATA Code")]
        public string D_IATACode { get; set; }

        /// <summary>
        /// Gets or sets the Number of nights.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Number of Nights are Required")]
        [Display(Name = "Number of Nights")]
        public int D_Nights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the Active Status.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public bool D_IsActive { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> CityItems { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> RegionItems { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> CountryItems { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> StateItems { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public IEnumerable<SelectListItem> VendorItems { get; set; }
    }
}