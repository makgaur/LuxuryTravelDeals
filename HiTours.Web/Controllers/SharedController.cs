// <copyright file="SharedController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using HiTours.Core;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;

    /// <summary>
    /// Shared Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class SharedController : Controller
    {
        /// <summary>
        /// The file provider
        /// </summary>
        private readonly IFileProvider fileProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedController"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public SharedController(IHostingEnvironment env)
        {
            this.fileProvider = env.WebRootFileProvider;
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="m">The mode.</param>
        /// <returns>
        /// ResizeImage
        /// </returns>
        [Route("/resized/{width}/{height}/{*url}")]
        public IActionResult ResizeImage(string url, int width, int height, int m)
        {
            var originalPath = PathString.FromUriComponent(("/" + url).ToUrl());
            var originalFileInfo = this.fileProvider.GetFileInfo(originalPath);
            if (originalFileInfo.Exists)
            {
                return this.Resize(width, height, originalPath, originalFileInfo);
            }
            else
            {
                 originalPath = PathString.FromUriComponent("/images/not-found.jpg");
                 originalFileInfo = this.fileProvider.GetFileInfo(originalPath);
                 return this.Resize(width, height, originalPath, originalFileInfo);
            }
        }

        /// <summary>
        /// Replaces the extension.
        /// </summary>
        /// <param name="wwwRelativePath">The WWW relative path.</param>
        /// <returns>ReplaceExtension</returns>
        private static string ReplaceExtension(string wwwRelativePath)
        {
            return Path.Combine(
                Path.GetDirectoryName(wwwRelativePath),
                Path.GetFileNameWithoutExtension(wwwRelativePath)) + ".jpg";
        }

        private IActionResult Resize(int width, int height, PathString originalPath, IFileInfo originalFileInfo)
        {
            try
            {
                var resizedPath = ReplaceExtension($"/images/{width}/{height}/{Path.GetFileName(originalFileInfo.PhysicalPath)}".ToUrl());
                var resizedInfo = this.fileProvider.GetFileInfo(resizedPath);
                if (!resizedInfo.Exists)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(resizedInfo.PhysicalPath));
                    using (var image = new Bitmap(Image.FromFile(originalFileInfo.PhysicalPath)))
                    {
                        var resized = new Bitmap(width, height);
                        using (var graphics = Graphics.FromImage(resized))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.DrawImage(image, 0, 0, width, height);
                            resized.Save(resizedInfo.PhysicalPath, ImageFormat.Jpeg);
                        }
                    }

                    return this.PhysicalFile(resizedInfo.PhysicalPath, "image/jpg");
                }

                return this.PhysicalFile(resizedInfo.PhysicalPath, "image/jpg");
            }
            catch (Exception ex)
            {
                var error = ex;
                return this.PhysicalFile(originalPath, "image/jpg");
            }
        }
    }
}