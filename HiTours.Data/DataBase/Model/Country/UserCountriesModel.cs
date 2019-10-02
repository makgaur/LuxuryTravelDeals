// <copyright file="UserCountriesModel.cs" company="Tetraskelion Softwares Pvt. Ltd.">
// Copyright (c) Tetraskelion Softwares Pvt. Ltd. All rights reserved.
// </copyright>

namespace HiTours.Models
{
    /// <summary>
    /// UserCountriesModel
    /// </summary>
    public class UserCountriesModel
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
        /// Gets or sets the name of the sort.
        /// </summary>
        /// <value>
        /// The name of the sort.
        /// </value>
        public string SortName { get; set; }

        /// <summary>
        /// Gets or sets the phone code.
        /// </summary>
        /// <value>
        /// The phone code.
        /// </value>
        public string PhoneCode { get; set; }
    }
}