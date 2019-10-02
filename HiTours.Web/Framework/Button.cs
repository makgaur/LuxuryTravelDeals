// <copyright file="Button.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;

    /// <summary>
    /// Button
    /// </summary>
    public class Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button" /> class.
        /// </summary>
        /// <param name="buttonType">Type of the button.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <param name="grouped">if set to <c>true</c> [grouped].</param>
        public Button(Enums.ButtonType buttonType, bool visible = true, bool grouped = false)
        {
            this.Type = buttonType;
            this.Visible = visible;
            this.Group = grouped;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button" /> class.
        /// </summary>
        /// <param name="buttonType">Type of the button.</param>
        /// <param name="url">The URL.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <param name="grouped">if set to <c>true</c> [grouped].</param>
        public Button(Enums.ButtonType buttonType, string url, bool visible = true, bool grouped = false)
        {
            this.Type = buttonType;
            this.RedirectUrl = url;
            this.Visible = visible;
            this.Group = grouped;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Enums.ButtonType Type { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>
        /// The class.
        /// </value>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Button"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the icon class.
        /// </summary>
        /// <value>
        /// The icon class.
        /// </value>
        public string IconClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Button"/> is group.
        /// </summary>
        /// <value>
        ///   <c>true</c> if group; otherwise, <c>false</c>.
        /// </value>
        public bool Group { get; set; }
    }
}
