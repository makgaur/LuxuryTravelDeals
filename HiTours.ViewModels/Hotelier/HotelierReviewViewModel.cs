// <copyright file="HotelierReviewViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class HotelierReviewViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>   [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Overall Rating")]
        public decimal Rating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Cleanliness Rating")]
        public decimal Rating_Cleanliness { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Comfort Rating")]
        public decimal Rating_Comfort { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Value Rating")]
        public decimal Rating_Value { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Location Rating")]
        public decimal Rating_Location { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Review Comments")]
        [StringLength(300, ErrorMessage ="Limit Exceeds")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        [Required]
        [Display(Name = "Recommended By User")]
        public bool UserRecommend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public bool IsActive { get; set; }
    }
}
