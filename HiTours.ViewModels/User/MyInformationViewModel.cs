// <copyright file="MyInformationViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// MyInformationViewModel
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class MyInformationViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the mobile no.
        /// </summary>
        /// <value>
        /// The mobile no.
        /// </value>
        [Required]
        [StringLength(20, ErrorMessage = "Mobile should be 20 digits")]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the nationality identifier.
        /// </summary>
        /// <value>
        /// The nationality identifier.
        /// </value>
        [Display(Name = "Nationality")]
        public int? NationalityId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Display(Name = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [StringLength(50)]
        [Display(Name = "City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [StringLength(10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the passport no.
        /// </summary>
        /// <value>
        /// The passport no.
        /// </value>
        [StringLength(50)]
        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }

        /// <summary>
        /// Gets or sets the countryof issue identifier.
        /// </summary>
        /// <value>
        /// The countryof issue identifier.
        /// </value>
        [Display(Name = "Country of Issue")]
        public int? CountryofIssueId { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> Country { get; set; }

        /// <summary>
        /// Gets or sets the cities.
        /// </summary>
        /// <value>
        /// The cities.
        /// </value>
        public IEnumerable<SelectListItem> Cities { get; set; }

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        /// <value>
        /// The nationality.
        /// </value>
        public IEnumerable<SelectListItem> Nationality { get; set; }

        /// <summary>
        /// Gets or sets the country issue.
        /// </summary>
        /// <value>
        /// The country issue.
        /// </value>
        public IEnumerable<SelectListItem> CountryIssue { get; set; }
    }
}