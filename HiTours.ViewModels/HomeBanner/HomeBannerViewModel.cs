// <copyright file="HomeBannerViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// HomeBannerViewModel
    /// </summary>
    public class HomeBannerViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        [Required(ErrorMessage = "Image is Required")]
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the package images.
        /// </summary>
        /// <value>
        /// The package images.
        /// </value>
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// Gets or sets the text1.
        /// </summary>
        /// <value>
        /// The text1.
        /// </value>
        [Required(ErrorMessage ="Header Text is Required")]
        [Display(Name = "Header Text")]
        public string Text1 { get; set; }

        /// <summary>
        /// Gets or sets the text2.
        /// </summary>
        /// <value>
        /// The text2.
        /// </value>
        [Required(ErrorMessage = "Location Text is Required")]
        [Display(Name = "Location Text")]
        public string Text2 { get; set; }

        /// <summary>
        /// Gets or sets the text3.
        /// </summary>
        /// <value>
        /// The text3.
        /// </value>
        [Required(ErrorMessage = "Search Placeholder is Required")]
        [Display(Name = "Search Placeholder")]
        public string Text3 { get; set; }

        /// <summary>
        /// Gets or sets the text4.
        /// </summary>
        /// <value>
        /// The text4.
        /// </value>
        public string Text4 { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        [Required(ErrorMessage = "Small Image is Required")]
        public string ImageNameMobileS { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        [Required(ErrorMessage = "Medium Image is Required")]
        public string ImageNameMobileM { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        [Required(ErrorMessage = "Tablet Image is Required")]
        public string ImageNameMobileT { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        [Required(ErrorMessage = "Large Image is Required")]
        public string ImageNameMobileL { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        [Required(ErrorMessage = "Laptop Image is Required")]
        public string ImageNameMobileLaptop { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        public IFormFile ImageNameMobileSFile { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        public IFormFile ImageNameMobileMFile { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        public IFormFile ImageNameMobileTFile { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        public IFormFile ImageNameMobileLFile { get; set; }

        /// <summary>
        /// Gets or sets the RedirectUrl.
        /// </summary>
        /// <value>
        /// The RedirectUrl.
        /// </value>
        public IFormFile ImageNameMobileLaptopFile { get; set; }
    }
}