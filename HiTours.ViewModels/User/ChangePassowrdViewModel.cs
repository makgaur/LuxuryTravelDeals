// <copyright file="ChangePassowrdViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ChangePassowrdViewModel
    /// </summary>
    public class ChangePassowrdViewModel
    {
        /// <summary>
        /// Gets or sets the old passowrd.
        /// </summary>
        /// <value>
        /// The old passowrd.
        /// </value>
        public string OldPassowrd { get; set; }

        /// <summary>
        /// Gets or sets the new passowrd.
        /// </summary>
        /// <value>
        /// The new passowrd.
        /// </value>
        public string NewPassowrd { get; set; }

        /// <summary>
        /// Gets or sets the confirm passowrd.
        /// </summary>
        /// <value>
        /// The confirm passowrd.
        /// </value>
        public string ConfirmPassowrd { get; set; }
    }
}
