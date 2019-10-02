// <copyright file="SeoDetailModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// SeoDetailModel
    /// </summary>
    public class SeoDetailModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the page.
        /// </summary>
        /// <value>
        /// The type of the page.
        /// </value>
        public string PageType { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>
        /// The page identifier.
        /// </value>
        public string PageId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        /// <value>
        /// The meta description.
        /// </value>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta keyword.
        /// </summary>
        /// <value>
        /// The meta keyword.
        /// </value>
        public string MetaKeyword { get; set; }

        /// <summary>
        /// Gets or sets the header meta code.
        /// </summary>
        /// <value>
        /// The header meta code.
        /// </value>
        public string HeaderMetaCode { get; set; }

        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        public string Schema { get; set; }

        /// <summary>
        /// Gets or sets the alt tag.
        /// </summary>
        /// <value>
        /// The alt tag.
        /// </value>
        public string AltTag { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [JsonIgnore]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        [JsonIgnore]
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Updates the audit information.
        /// </summary>
        /// <param name="updatedBy">The updated by.</param>
        public void UpdateAuditInfo(int updatedBy)
        {
            this.UpdatedDate = DateTime.Now;
            this.UpdatedBy = updatedBy;
        }

        /// <summary>
        /// Sets the audit information.
        /// </summary>
        /// <param name="createdBy">The created by.</param>
        public void SetAuditInfo(int createdBy)
        {
            this.CreatedBy = createdBy;
            this.UpdatedBy = createdBy;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
        }
    }
}