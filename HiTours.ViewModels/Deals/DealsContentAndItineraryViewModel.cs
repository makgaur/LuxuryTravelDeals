// <copyright file="DealsContentAndItineraryViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class DealsContentAndItineraryViewModel
    {
        /// <summary>
        /// Gets or sets deal Content model
        /// </summary>
        public DealsContentViewModel DealContent { get; set; }

        /// <summary>
        /// Gets or sets deals Nights
        /// </summary>
        public ICollection<DealsNightViewModel> DealsNights { get; set; }

        /// <summary>
        /// Gets or sets deals Nights
        /// </summary>
        public ICollection<DealsItineraryViewModel> DealsItineraries { get; set; }

        /// <summary>
        /// Gets or sets deals Nights
        /// </summary>
        public ICollection<DealsHighlightViewModel> DealsHighlights { get; set; }

        /// <summary>
        /// Gets or sets deals Nights
        /// </summary>
        public ICollection<DealsAddOnViewModel> DealsAddOnModels { get; set; }
    }
}
