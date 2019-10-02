// <copyright file="ImageTagHelper.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.TagHelpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using SixLabors.ImageSharp.Processing;
    using SixLabors.ImageSharp.Processing.Transforms;

    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project

    /// <summary>
    /// ImageTagHelper
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
    [HtmlTargetElement("img", TagStructure = TagStructure.WithoutEndTag)]
    public class ImageTagHelper : TagHelper
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;
        private readonly string imgNotFound = "/images/image-not-available.jpg";
        private string blobImageInitializer = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageTagHelper"/> class.
        /// Initialize
        /// </summary>
        /// <param name="hostingEnvironment">Hosting Enviornment</param>
        /// <param name="configuration">Web Configuration</param>
        public ImageTagHelper(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
            this.blobImageInitializer = this.configuration.GetValue<string>("AzureBlobAppSetting:ImageInitializer");
        }

        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                string originalPath = context.AllAttributes["src"].Value.ToString();
                if (string.IsNullOrEmpty(originalPath))
                {
                    output.Attributes.SetAttribute("src", this.imgNotFound);
                    return;
                }

                originalPath = originalPath.Replace("~", string.Empty);
                originalPath = this.blobImageInitializer + originalPath;
                output.Attributes.SetAttribute("src", originalPath);
                output.Attributes.SetAttribute("onerror", "this.src='" + this.blobImageInitializer + this.imgNotFound + "'");
                return;
            }
            catch (Exception ex)
            {
                output.Attributes.SetAttribute("src", this.imgNotFound);
                string msg = ex.ToString();
            }
        }

        /////// <summary>
        /////// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /////// <paramref name="output" />.
        /////// </summary>
        /////// <param name="context">Contains information associated with the current HTML tag.</param>
        /////// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        ////public override void Process(TagHelperContext context, TagHelperOutput output)
        ////{
        ////    try
        ////    {
        ////        string originalPath = context.AllAttributes["src"].Value.ToString();
        ////        if (string.IsNullOrEmpty(originalPath))
        ////        {
        ////            output.Attributes.SetAttribute("src", this.imgNotFound);
        ////            return;
        ////        }

        ////        originalPath = originalPath.Replace("~", string.Empty);
        ////        originalPath = "https://luxuryimages.azureedge.net" + originalPath;
        ////        try
        ////        {
        ////            CloudBlockBlob blob = this.blobContainer.GetBlockBlobReference(originalPath);
        ////            if (await blob.ExistsAsync())
        ////            {
        ////                output.Attributes.SetAttribute("src", originalPath);
        ////            }
        ////            else
        ////            {
        ////                output.Attributes.SetAttribute("src", "https://luxuryimages.azureedge.net" + this.imgNotFound);
        ////                return;
        ////            }
        ////        }
        ////        catch
        ////        {
        ////            output.Attributes.SetAttribute("src", "https://luxuryimages.azureedge.net" + this.imgNotFound);
        ////            return;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        output.Attributes.SetAttribute("src", this.imgNotFound);
        ////        string msg = ex.ToString();
        ////        return;
        ////    }
        ////}
    }
}