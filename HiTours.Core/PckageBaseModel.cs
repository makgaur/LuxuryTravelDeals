// <copyright file="PckageBaseModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// PckageBaseModel
    /// </summary>
    public class PckageBaseModel
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
        public int CreatedBy { get; set; }

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
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; } = true;

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