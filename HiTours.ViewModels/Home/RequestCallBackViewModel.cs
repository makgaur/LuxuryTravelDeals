// <copyright file="RequestCallBackViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using static HiTours.Core.Enums;

    /// <summary>
    /// Request Call Back View Model
    /// </summary>
    public class RequestCallBackViewModel
    {
        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        /// <value>
        /// The Username.
        /// </value>
        [Required]
        [StringLength(50)]
        [Display(Name ="Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Mobile.
        /// </summary>
        /// <value>
        /// The Mobile.
        /// </value>
        [Required]
        [StringLength(10, ErrorMessage = "Mobile number should be 10 digits")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Mobile number should be numeric.")]
        [MinLength(10, ErrorMessage = "Mobile number should be 10 digits")]
        [MaxLength(10, ErrorMessage = "Mobile number should be 10 digits")]
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidEmail")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the best time to call.
        /// </summary>
        /// <value>
        /// The best time to call.
        /// </value>
        public BestTimeToCall BestTimeToCall { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        public string PageUrl { get; set; }
    }
}