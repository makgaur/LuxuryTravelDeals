// <copyright file="TourPackageTravelStyleViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// TourPackageTravelStyleViewModel
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class TourPackageTravelStyleViewModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tour package identifier.
        /// </summary>
        /// <value>
        /// The tour package identifier.
        /// </value>
        public Guid TourPackageId { get; set; }

        /// <summary>
        /// Gets or sets the travel style identifier.
        /// </summary>
        /// <value>
        /// The travel style identifier.
        /// </value>
        public int TravelStyleId { get; set; }

        /// <summary>
        /// Gets or sets the travel style.
        /// </summary>
        /// <value>
        /// The travel style.
        /// </value>
        public PackageTravelStyleViewModel TravelStyle { get; set; }
    }
}
