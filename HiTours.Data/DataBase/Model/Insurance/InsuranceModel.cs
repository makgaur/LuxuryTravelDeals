// <copyright file="InsuranceModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class InsuranceModel : BaseModel
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
        /// Gets or sets the Vendor Id.
        /// </summary>
        /// <value>
        /// The Vendor Id.
        /// </value>
        public int VendorID { get; set; }

        /// <summary>
        /// Gets or sets the VendorModel.
        /// </summary>
        /// <value>
        /// The VendorModel.
        /// </value>
        public VendorInformationModel VendorModel { get; set; }

        /// <summary>
        /// Gets or sets the AdultPrice.
        /// </summary>
        /// <value>
        /// The AdultPrice.
        /// </value>
        public decimal AdultRate { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public decimal ChildRate { get; set; }

        /// <summary>
        /// Gets or sets the Days.
        /// </summary>
        /// <value>
        /// The BufferDays.
        /// </value>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether status is Active or not
        /// </summary>
        /// <value>
        /// ISActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}

