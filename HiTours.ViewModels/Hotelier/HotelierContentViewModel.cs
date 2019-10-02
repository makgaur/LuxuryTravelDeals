// <copyright file="HotelierContentViewModel.cs" company="Luxury Travel Deals">
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
    /// Hotelier Content model
    /// </summary>
    public class HotelierContentViewModel : BaseModel
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
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the about hotelier.
        /// </summary>
        /// <value>
        /// The hotel about html content.
        /// </value>
        [Required]
        [Display(Name = "About Hotel")]
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the Trip Advisor URL for hotelier.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [Required]
        [Display(Name = "Trip Advisor Script")]
        public string TAUrl { get; set; }

        /// <summary>
        /// Gets or sets the logo image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Hotel Logo (Ratio 19:10)")]
        public string LogoImg { get; set; }

        /// <summary>
        /// Gets or sets the logo image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public IFormFile LogoImgFile { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Card Image (Ratio 73:41)")]
        public string CardImg { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFormFile CardImgFile { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Display(Name = "About Image (Ratio 19:10)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Upload File")]
        public string AboutImg { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFormFile AboutImgFile { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Banner Master Tile (Ratio: 134 : 101)")]
        public string BannerImg4x4 { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFormFile BannerImg4x4File { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Banner Long Tile (Ratio: 338 : 505)")]
        public string BannerImg2x4 { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFormFile BannerImg2x4File { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Banner Tile 01 (Ratio: 169 : 125)")]
        public string BannerImg2x2_1 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public IFormFile BannerImg2x2_1File { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Banner Tile 02 (Ratio: 169 : 125)")]
        public string BannerImg2x2_2 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public IFormFile BannerImg2x2_2File { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Banner Tile 03 (Ratio: 169 : 125)")]
        public string BannerImg2x2_3 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public IFormFile BannerImg2x2_3File { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Display(Name = "Banner Tile 04 (Ratio: 169 : 125)")]
        public string BannerImg2x2_4 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public IFormFile BannerImg2x2_4File { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Overall Rating")]
        public decimal OverallRating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Cleanliness Rating")]
        public decimal OverallCleaninessRating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Comfort Rating")]
        public decimal OverallComfortRating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Value Rating")]
        public decimal OverallValueRating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Hotel Ameneties")]
        public int[] HotelAmeneties { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public ICollection<SelectListItem> HotelAmenetiesItem { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public List<HotelierRoomConfigurationViewModel> HotelierRoomConfigurations { get; set; }
    }
}
