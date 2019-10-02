// <copyright file="PackageReminderViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using HiTours.Core;

    /// <summary>
    /// Package Reminder View model
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class PackageReminderViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        public Guid PackageId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }
    }
}