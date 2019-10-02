// <copyright file="CategoryViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CategoryModel
    /// </summary>
    public class CategoryViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(50)]
        [Required]
        [Display(Name = "Name")]
        [Remote("IsDuplicate", "Category", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AlreadyExists")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delete; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelete { get; set; }
    }
}