// <copyright file="PackageViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Package view Model
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class PackageViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(100)]
        [Display(Name = "Deal Name")]
        [Remote("IsDuplicate", "Package", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the deal type identifier.
        /// </summary>
        /// <value>
        /// The deal type identifier.
        /// </value>
        [Required]
        [Display(Name = "Deal Type")]
        public Guid DealTypeId { get; set; }

        /// <summary>
        /// Gets or sets the valid from.
        /// </summary>
        /// <value>
        /// The valid from.
        /// </value>
        [Required]
        [Display(Name = "Valid From")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the valid to.
        /// </summary>
        /// <value>
        /// The valid to.
        /// </value>
        [Required]
        [Display(Name = "Valid To")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the nights.
        /// </summary>
        /// <value>
        /// The nights.
        /// </value>
        [Required]
        [Display(Name = "Nights")]
        public string Nights { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        [Required]
        [Display(Name = "City")]
        public Guid CityId { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        [Required]
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        [Required]
        [Display(Name = "Hotel")]
        public Guid HotelId { get; set; }

        /// <summary>
        /// Gets or sets the hotel validity identifier.
        /// </summary>
        /// <value>
        /// The hotel validity identifier.
        /// </value>
        [Required]
        [Display(Name = "Hotel Validity")]
        public Guid HotelValidityId { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [Display(Name = "Hotel Price")]
        [Range(0.0, Constants.Max14DecimalRange)]
        public decimal HotelPrice { get; set; }

        /// <summary>
        /// Gets or sets the hight lights.
        /// </summary>
        /// <value>
        /// The hight lights.
        /// </value>
        [Display(Name = "Highlights")]
        public string HighLights { get; set; }

        /// <summary>
        /// Gets or sets the deal quotes.
        /// </summary>
        /// <value>
        /// The deal quotes.
        /// </value>
        [Required]
        [Display(Name = "Deal Quotes")]
        [StringLength(200)]
        public string DealQuotes { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [Display(Name = "Description")]
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        [Display(Name = "Details")]
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        [Display(Name = "Prefix")]
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the postfix.
        /// </summary>
        /// <value>
        /// The postfix.
        /// </value>
        [Display(Name = "Postfix")]
        public string Postfix { get; set; }

        /// <summary>
        /// Gets or sets the deal code.
        /// </summary>
        /// <value>
        /// The deal code.
        /// </value>
        [Display(Name = "Code")]
        public int DealCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Extra Night")]
        public bool IsExtraNight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delete; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(Name = "Is Delete")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// Gets or sets the hotel review.
        /// </summary>
        /// <value>
        /// The hotel review.
        /// </value>
        [Display(Name = "Hotel Review")]
        public string HotelReview { get; set; }

        /// <summary>
        /// Gets or sets the package images.
        /// </summary>
        /// <value>
        /// The package images.
        /// </value>
        [Display(Name = "Package Images")]
        public IList<IFormFile> Files { get; set; }

        /// <summary>
        /// Gets or sets the package images.
        /// </summary>
        /// <value>
        /// The package images.
        /// </value>
        public List<PackageImageViewModel> PackageImages { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> DealType { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public IEnumerable<SelectListItem> Categories { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> Cities { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        public IEnumerable<SelectListItem> Countries { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> Hotels { get; set; }

        /// <summary>
        /// Gets or sets the hotel validities.
        /// </summary>
        /// <value>
        /// The hotel validities.
        /// </value>
        public IEnumerable<SelectListItem> HotelValidities { get; set; }

        /// <summary>
        /// Gets or sets the room types.
        /// </summary>
        /// <value>
        /// The room types.
        /// </value>
        public IEnumerable<HotelRoomTypeViewModel> HotelRoomTypes { get; set; }

        /// <summary>
        /// Gets or sets the specific price list.
        /// </summary>
        /// <value>
        /// The specific price list.
        /// </value>
        public IEnumerable<SpecificPriceViewModel> SpecificPriceList { get; set; }
    }
}