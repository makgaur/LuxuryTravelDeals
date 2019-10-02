// <copyright file="VisaModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class VisaModel : BaseModel
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
        /// Gets or sets the CountryModel.
        /// </summary>
        /// <value>
        /// The CountryModel.
        /// </value>
        public PackageCountryModel CountryModel { get; set; }

        /// <summary>
        /// Gets or sets the CountryId.
        /// </summary>
        /// <value>
        /// The CountryId.
        /// </value>
        public short CountryId { get; set; }

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
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public decimal ChildPrice { get; set; }

        /// <summary>
        /// Gets or sets the BufferDays.
        /// </summary>
        /// <value>
        /// The BufferDays.
        /// </value>
        public int BufferDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether status is Active or not
        /// </summary>
        /// <value>
        /// ISActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public decimal Markup { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string DocumentsRequired { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string PhotoSpecification { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string ProcessingTime { get; set; }

        /// <summary>
        /// Gets or sets the ChildPrice.
        /// </summary>
        /// <value>
        /// The ChildPrice.
        /// </value>
        public string GeneralPolicy { get; set; }
    }
}
