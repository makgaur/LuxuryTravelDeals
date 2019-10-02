// <copyright file="BaseModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System;

    /// <summary>
    /// BaseModel
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Updates the audit information.
        /// </summary>
        /// <param name="updatedBy">The updated by.</param>
        public void UpdateAuditInfo(Guid? updatedBy)
        {
            this.UpdatedDate = DateTime.Now;
            this.UpdatedBy = updatedBy;
        }

        /// <summary>
        /// Sets the audit information.
        /// </summary>
        /// <param name="createdBy">The created by.</param>
        public void SetAuditInfo(Guid? createdBy)
        {
            this.CreatedBy = createdBy;
            this.UpdatedBy = createdBy;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
        }
    }
}