// <copyright file="PackageMarginTypeModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System.Collections.Generic;
    using HiTours.Models;

    /// <summary>
    /// PackageCityModel
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class PackageMarginTypeModel
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
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<VendorContractModel> VendorContractModels { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<PromotionModel> PromotionModels { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<HotelierPromotionModel> HotelierPromotionModels { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<DealsPromotionModel> DealsPromotionModels { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<CancellationPolicyModel> CancellationPolicyModels { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<HotelierCancellationPolicyModel> HotelierCancellationPolicyModels { get; set; }

        /// <summary>
        /// Gets or sets the Vendor Models.
        /// </summary>
        /// <value>
        /// The Vendor Models.
        /// </value>
        public ICollection<DealsCancellationPolicyModel> DealsCancellationPolicyModels { get; set; }
    }
}