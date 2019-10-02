// <copyright file="EnquiryFormViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// EnquiryFormViewModel
    /// </summary>
    public class EnquiryFormViewModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the mobile no.
        /// </summary>
        /// <value>
        /// The mobile no.
        /// </value>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the travel date.
        /// </summary>
        /// <value>
        /// The travel date.
        /// </value>
        public string TravelDate { get; set; }

        /// <summary>
        /// Gets or sets the text message.
        /// </summary>
        /// <value>
        /// The text message.
        /// </value>
        public string TextMessage { get; set; }
    }
}
