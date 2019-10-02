// <copyright file="VideoResizerTagHelper.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// VideoResizerTagHelper
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("video")]
    public class VideoResizerTagHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets a value indicating whether [automatic resize].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic resize]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoResize { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is data source.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is data source; otherwise, <c>false</c>.
        /// </value>
        public bool IsSrc { get; set; }

        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
        }
    }
}
