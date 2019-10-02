// <copyright file="DealsContentModel.cs" company="Luxury Travel Deals">
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
    public class DealsContentModel : BaseModel
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
        public int PackageId { get; set; }

        /// <summary>
        ///  Gets or sets gets or set Hotelier Info view model
        /// </summary>
        public DealsPackageModel DealsPackageModel { get; set; }

        /// <summary>
        /// Gets or sets the about hotelier.
        /// </summary>
        /// <value>
        /// The hotel about html content.
        /// </value>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the Trip Advisor URL for hotelier.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string TAUrl { get; set; }

        /// <summary>
        /// Gets or sets the logo image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string LogoImg { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string CardImg { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string AboutImg { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string BannerImg4x4 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string BannerImg2x4 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string BannerImg2x2_1 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string BannerImg2x2_2 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string BannerImg2x2_3 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public string BannerImg2x2_4 { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public decimal OverallRating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public decimal OverallCleaninessRating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public decimal OverallComfortRating { get; set; }

        /// <summary>
        /// Gets or sets the card image of hotelier.
        /// </summary>
        /// <value>
        /// The Image Identifier
        /// </value>
        public decimal OverallValueRating { get; set; }
    }
}
