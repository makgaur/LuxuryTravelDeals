// <copyright file="TourPackageViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// TourPackageViewModel
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class TourPackageViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the tour package.
        /// </summary>
        /// <value>
        /// The type of the tour package.
        /// </value>
        public byte TourPackageType { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        [Required(ErrorMessage = "Vendor is required")]
        [Display(Name = "Vendor")]
        public int? VendorId { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> VendorListItems { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public string Prefix { get; set; } = "PC";

        /// <summary>
        /// Gets or sets the deal code.
        /// </summary>
        /// <value>
        /// The deal code.
        /// </value>
        public int DealCode { get; set; } = 1001;

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>
        /// The suffix.
        /// </value>
        public string Suffix { get; set; } = "HT";

        /// <summary>
        /// Gets or sets the visitors.
        /// </summary>
        /// <value>
        /// The visitors.
        /// </value>
        [Display(Name = "Visitors")]
        public int? Visitors { get; set; }

        /// <summary>
        /// Gets or sets the URL title.
        /// </summary>
        /// <value>
        /// The URL title.
        /// </value>
        [Display(Name = "Url Title")]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_@.-]*$", ErrorMessage = "Special character not allow allowed")]
        [Remote("IsDuplicateUrl", "TourPackage", AdditionalFields = "Id", ErrorMessage = "Url Title is already exist..!")]
        public string UrlTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        [Required(ErrorMessage = "Package Name is required")]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the deal type identifier.
        /// </summary>
        /// <value>
        /// The deal type identifier.
        /// </value>
        public string DealTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        [Display(Name = "Deal Type")]
        [Required(ErrorMessage = "Deal Type is required")]
        public string[] DealType { get; set; }

        /// <summary>
        /// Gets or sets the package valid from.
        /// </summary>
        /// <value>
        /// The package valid from.
        /// </value>
        [Display(Name = "Travel Valid From")]
        [Required(ErrorMessage = "Package Valid From is required")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PackageValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the package valid to.
        /// </summary>
        /// <value>
        /// The package valid to.
        /// </value>
        [Display(Name = "Travel Valid To")]
        [Required(ErrorMessage = "Package Valid To is required")]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PackageValidTo { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        [Display(Name = "Hotel")]
        public int? HotelId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is flight included.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is flight included; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Add Flight")]
        public bool IsFlightIncluded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Hotel Only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel only; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Hotel Only")]
        public bool IsHotelOnly { get; set; }

        /// <summary>
        /// Gets or sets the package description.
        /// </summary>
        /// <value>
        /// The package description.
        /// </value>
        [Display(Name = "Inclusions")]
        public string PackageDescription { get; set; }

        /// <summary>
        /// Gets or sets the quote.
        /// </summary>
        /// <value>
        /// The quote.
        /// </value>
        [StringLength(100)]
        public string Quote { get; set; }

        /// <summary>
        /// Gets or sets the high lights.
        /// </summary>
        /// <value>
        /// The high lights.
        /// </value>
        public string HighLights { get; set; }

        /// <summary>
        /// Gets or sets the programs.
        /// </summary>
        /// <value>
        /// The programs.
        /// </value>
        public string Programs { get; set; }

        /// <summary>
        /// Gets or sets the hotel description.
        /// </summary>
        /// <value>
        /// The hotel description.
        /// </value>
        [Display(Name = "Hotel Description")]
        public string HotelDescription { get; set; }

        /// <summary>
        /// Gets or sets the hotel review.
        /// </summary>
        /// <value>
        /// The hotel review.
        /// </value>
        [Display(Name = "Hotel Review")]
        public string HotelReview { get; set; }

        /// <summary>
        /// Gets or sets the map script.
        /// </summary>
        /// <value>
        /// The map script.
        /// </value>
        public string MapScript { get; set; }

        /// <summary>
        /// Gets or sets the travel style.
        /// </summary>
        /// <value>
        /// The travel style.
        /// </value>
        [Display(Name = "Travel Style")]
        public string[] TravelStyle { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> Deals { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> HotelList { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> TravelStyles { get; set; }

        /// <summary>
        /// Gets or sets the tour package city.
        /// </summary>
        /// <value>
        /// The tour package city.
        /// </value>
        public ICollection<TourPackageCityViewModel> TourPackageCity { get; set; }

        /// <summary>
        /// Gets or sets the tour package book date.
        /// </summary>
        /// <value>
        /// The tour package book date.
        /// </value>
        public ICollection<TourPackageBookDateViewModel> TourPackageBookDate { get; set; }

        /// <summary>
        /// Gets or sets the tour package book date.
        /// </summary>
        /// <value>
        /// The tour package book date.
        /// </value>
        public List<TourPackageImageViewModel> TourPackageImage { get; set; } = new List<TourPackageImageViewModel>();

        /// <summary>
        /// Gets or sets the package travel style.
        /// </summary>
        /// <value>
        /// The package travel style.
        /// </value>
        public ICollection<TourPackageTravelStyleViewModel> TourPackageTravelStyle { get; set; } = new List<TourPackageTravelStyleViewModel>();

        /// <summary>
        /// Gets or sets the commandbutton.
        /// </summary>
        /// <value>
        /// The commandbutton.
        /// </value>
        public string CommandButton { get; set; }

        /////// <summary>
        /////// Gets or sets the package images.
        /////// </summary>
        /////// <value>
        /////// The package images.
        /////// </value>
        ////[Display(Name = "Package Images")]
        ////public IList<IFormFile> Files { get; set; }

        /// <summary>
        /// Gets or sets the default tab.
        /// </summary>
        /// <value>
        /// The default tab.
        /// </value>
        public string DefaultTabView { get; set; } = "tourpackage";
    }
}