// <copyright file="PackageHolidayMenuModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    /// <summary>
    /// Package Holiday Menu Model
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class PackageHolidayMenuModel : PckageBaseModel
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
        /// Gets or sets a value indicating whether this instance is region.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is region; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegion { get; set; }

        /// <summary>
        /// Gets or sets the child menu.
        /// </summary>
        /// <value>
        /// The child menu.
        /// </value>
        public string ChildMenu { get; set; }
    }
}