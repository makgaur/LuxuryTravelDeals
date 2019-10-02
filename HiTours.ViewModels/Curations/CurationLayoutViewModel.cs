// <copyright file="CurationLayoutViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// RoomTypeModel
    /// </summary>
    public class CurationLayoutViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public List<CurationsAddViewModel> OneXOne { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public CurationsAddViewModel OneXTwo { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public CurationsAddViewModel TwoXTwo { get; set; }
    }
}
