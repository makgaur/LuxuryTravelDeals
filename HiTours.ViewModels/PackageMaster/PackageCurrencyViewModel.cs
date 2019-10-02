// <copyright file="PackageCurrencyViewModel.cs" company="Luxury Travel Deals">
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
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// PackageCountryViewModel
    /// </summary>
    public class PackageCurrencyViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The Name.
        /// </value>
        [Display(Name = "Currency Name")]
        [Required(ErrorMessage = "Currency Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        /// <value>
        /// The Code.
        /// </value>
        [Display(Name = "Currency Code")]
        [Required(ErrorMessage = "Currency Code is required")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Country Value.
        /// </summary>
        /// <value>
        /// The Country.
        /// </value>
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public short Country { get; set; }

        /// <summary>
        /// Gets or sets the Country Name.
        /// </summary>
        /// <value>
        /// The Country Name.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
