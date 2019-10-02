// <copyright file="UserDetailViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// UserDetailsViewModel
    /// </summary>
    public class UserDetailViewModel : BaseModel
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
       //// [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        //// [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InvalidEmail")]
        [Display(Name = "Email Id")]
        [Remote("IsDuplicate", "user", AdditionalFields = "ID", ErrorMessage = "Email Address Already Exist!")]
        public string EmailId { get; set; }

        ///// <summary>
        ///// Gets or sets the password.
        ///// </summary>
        ///// <value>
        ///// The password.
        ///// </value>
        ////[Required]
        ////[StringLength(50)]
        ////[Display(Name = "Password")]
        ////[DataType(DataType.Password)]
        ////public string Password { get; set; }

        /// <summary>
        /// Gets or sets the mobile no.
        /// </summary>
        /// <value>
        /// The mobile no.
        /// </value>
        [Required]
        [StringLength(10)]
        [Display(Name = "Mobile No")]
        [RegularExpression(@"^((\+)?(\d{2}[-])?(\d{10}){1})?(\d{11}){0,1}?$", ErrorMessage = "Enter a valid Mobile number")]
        [Remote("IsDuplicateMobile", "user", ErrorMessage = "Mobile Number Already Exist!")]
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delete; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Gets or sets the type of the detail.
        /// </summary>
        /// <value>
        /// The type of the detail.
        /// </value>
        public string DetailType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public bool Redirection { get; set; }

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
    }
}