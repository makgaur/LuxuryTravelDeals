// <copyright file="LoginViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;

    /// <summary>
    /// LoginViewModel
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the OTP expiry datetime.
        /// </summary>
        /// <value>
        /// The OTP expiry datetime.
        /// </value>
        public DateTime? OtpExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the OTP.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Display(Name = "OTP")]
        public string OTP { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidEmail")]
        [Display(Name = "Email Id")]
        public string Email { get; set; }

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

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the mobile no.
        /// </summary>
        /// <value>
        /// The mobile no.
        /// </value>
        [Required(ErrorMessage = "Number Reqiured")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [StringLength(10)]
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public string Mode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public bool Redirection { get; set; }
    }
}