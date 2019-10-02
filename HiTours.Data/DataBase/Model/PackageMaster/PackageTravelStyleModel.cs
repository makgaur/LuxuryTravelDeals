// <copyright file="PackageTravelStyleModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// PackageTravelStyleModel
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class PackageTravelStyleModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IconClass
        /// </summary>
        /// <value>
        /// The Icon Class Name.
        /// </value>
        public string IconClass { get; set; }

        /// <summary>
        /// Gets or sets the IconClass
        /// </summary>
        /// <value>
        /// The Icon Class Name.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the tour package travel style.
        /// </summary>
        /// <value>
        /// The tour package travel style.
        /// </value>
        public ICollection<TourPackageTravelStyleModel> TourPackageTravelStyle { get; set; }
    }
}