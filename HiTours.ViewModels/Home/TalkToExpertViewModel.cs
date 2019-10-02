// <copyright file="TalkToExpertViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// Request Call Back View Model
    /// </summary>
    public class TalkToExpertViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile should be 10 digits")]
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
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the more.
        /// </summary>
        /// <value>
        /// The more.
        /// </value>
        public string More { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        public string PageUrl { get; set; }
    }
}