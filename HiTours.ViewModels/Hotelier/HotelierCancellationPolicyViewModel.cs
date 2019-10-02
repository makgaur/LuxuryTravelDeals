// <copyright file="HotelierCancellationPolicyViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// CategoryModel
    /// </summary>
    public class HotelierCancellationPolicyViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Required")]
        public int? MarginType { get; set; }

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        /// <value>
        /// The Symbol.
        /// </value>
        [Required(ErrorMessage = "Required")]
        public int? Charge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Required(ErrorMessage = "Required")]
        public int? MinDay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        [Required(ErrorMessage = "Required")]
        public int? MaxDay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Is Active.
        /// </summary>
        /// <value>
        /// Is Active
        /// </value>
        public IList<SelectListItem> MarginTypeItems { get; set; }
    }
}