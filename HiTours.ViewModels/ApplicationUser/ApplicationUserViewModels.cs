// <copyright file="ApplicationUserViewModels.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ApplicationUserViewModels
    /// </summary>
    public class ApplicationUserViewModels
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        [StringLength(50)]
        [Display(Name = "UserId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}