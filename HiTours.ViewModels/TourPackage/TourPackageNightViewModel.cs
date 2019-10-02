// <copyright file="TourPackageNightViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels.TourPackage;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// TourPackageNightViewModel
    /// </summary>
    /// <seealso cref="PckageBaseModel" />
    public class TourPackageNightViewModel : PckageBaseModel
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
        /// Gets or sets the no of nights.
        /// </summary>
        /// <value>
        /// The no of nights.
        /// </value>
        [Display(Name = "No Of Nights")]
        [Required(ErrorMessage = "No Of Nights is required")]
        [Range(1, 255, ErrorMessage = "No Of Nights between 1 to 255")]
        ////[Remote("IsDuplicate", "tourpackage", AdditionalFields = "Id", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public byte NoOfNights { get; set; }

        /// <summary>
        /// Gets or sets the deposit amount.
        /// </summary>
        /// <value>
        /// The deposit amount.
        /// </value>
        [Display(Name = "Deposit Amount")]
        [Range(1, long.MaxValue, ErrorMessage = "Deposit Amount should not be zero or negative.")]
        ////[Required(ErrorMessage = "Deposit Amount is required")]
        public decimal? DepositAmount { get; set; }

        /// <summary>
        /// Gets or sets the package price.
        /// </summary>
        /// <value>
        /// The package price.
        /// </value>
        [Display(Name = "Package Price")]
        [Range(0, long.MaxValue, ErrorMessage = "Package Price should not be negative.")]
        [Required(ErrorMessage = "Package Price is required")]
        public decimal PackagePrice { get; set; }

        /// <summary>
        /// Gets or sets the package discount price.
        /// </summary>
        /// <value>
        /// The package discount price.
        /// </value>
        [Display(Name = "Package Discount Price")]
        [Range(0, long.MaxValue, ErrorMessage = "Package Discount should not be negative.")]
        [Required(ErrorMessage = "Package Discount Price is required")]
        public decimal PackageDiscountPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is extra night.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extra night; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Extra Night")]
        public bool IsExtraNight { get; set; }

        /// <summary>
        /// Gets or sets the cities.
        /// </summary>
        /// <value>
        /// The cities.
        /// </value>
        [Display(Name = "Depart City")]
        public string[] Cities { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> CityList { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights validity.
        /// </summary>
        /// <value>
        /// The tour package nights validity.
        /// </value>
        public List<TourPackageNightsValidityViewModel> TourPackageNightsValidity { get; set; }
        = new List<TourPackageNightsValidityViewModel>();

        /// <summary>
        /// Gets or sets the tour package nights validity.
        /// </summary>
        /// <value>
        /// The tour package nights validity.
        /// </value>
        public List<TourPackageNightsDepartCityViewModel> TourPackageNightsDepartCity { get; set; }
        = new List<TourPackageNightsDepartCityViewModel>();

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights validity parent.
        /// </summary>
        /// <value>
        /// The tour package nights validity parent.
        /// </value>
        public List<TourPackageNightsValidityParentViewModel> TourPackageNightsValidityParent { get; set; }
       = new List<TourPackageNightsValidityParentViewModel>();

        /// <summary>
        /// Gets or sets the type of the tour package.
        /// </summary>
        /// <value>
        /// The type of the tour package.
        /// </value>
        public byte TourPackageType { get; set; }
    }
}