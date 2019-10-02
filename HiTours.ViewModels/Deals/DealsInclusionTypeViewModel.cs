// <copyright file="DealsInclusionTypeViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class DealsInclusionTypeViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public ICollection<DealsInclusionViewModel> DealsInclusionViewModels { get; set; }
    }
}
