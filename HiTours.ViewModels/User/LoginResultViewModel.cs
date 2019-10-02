// <copyright file="LoginResultViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Login Response View Model
    /// </summary>
    public class LoginResultViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LoginResultViewModel"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the user detail.
        /// </summary>
        /// <value>
        /// The user detail.
        /// </value>
        public UserDetailViewModel UserDetail { get; set; }
    }
}