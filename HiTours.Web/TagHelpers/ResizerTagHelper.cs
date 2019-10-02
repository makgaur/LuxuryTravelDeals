// <copyright file="ResizerTagHelper.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using SixLabors.ImageSharp.Processing;
    using SixLabors.ImageSharp.Processing.Transforms;

    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project

    /// <summary>
    /// ResizerTagHelper
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
    [HtmlTargetElement("img", TagStructure = TagStructure.WithoutEndTag)]
    public class ResizerTagHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets a value indicating whether [automatic resize].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic resize]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoResize { get; set; }

        ///// <summary>
        ///// Gets or sets the height.
        ///// </summary>
        ///// <value>
        ///// The height.
        ///// </value>
        ////public int Height { get; set; }

        ///// <summary>
        ///// Gets or sets the width.
        ///// </summary>
        ///// <value>
        ///// The width.
        ///// </value>
        ////public int Width { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is data source.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is data source; otherwise, <c>false</c>.
        /// </value>
        public bool IsSrc { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public ResizeMode Mode { get; set; } = ResizeMode.Max;

        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.AutoResize)
            {
                var originalPath = context.AllAttributes["src"].Value.ToString();
                ////if (!string.IsNullOrEmpty(originalPath))
                ////{
                ////    var path = $"/resized/{this.Width}/{this.Height}{originalPath.Replace("~", string.Empty)}?m={(int)this.Mode}";

                ////    if (this.IsSrc)
                ////    {
                ////        output.Attributes.SetAttribute("src", path);
                ////    }
                ////    else
                ////    {
                ////        output.Attributes.Add("data-src", path);
                ////        output.Attributes.SetAttribute("src", path);
                ////    }
                ////}
            }
        }
    }
}