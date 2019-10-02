// <copyright file="AdminController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Api.Common;
    using HiTours.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    /// AdminController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = Constants.AdminRole)]
    [Authorize(AuthenticationSchemes = Constants.ApplicationAdminCookies)]
    [Area("admin")]
    public class AdminController : Controller
    {
        private readonly CloudBlobContainer blobContainer;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        public AdminController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="configuration">Web Config Initialize</param>
        public AdminController(IMapper mapper, IConfiguration configuration)
        {
            this.configuration = configuration;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.configuration.GetValue<string>("ConnectionStrings:AzureConnectionString"));
            //// We are going to use Blob Storage, so we need a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //// Data in blobs are organized in containers.
            //// Here, we create a new, empty container.
            this.blobContainer = blobClient.GetContainerReference(this.configuration.GetValue<string>("AzureBlobAppSetting:BlobContainer"));
            this.blobContainer.CreateIfNotExistsAsync();

            //// We also set the permissions to "Public", so anyone will be able to access the file.
            //// By default, containers are created with private permissions only.
            this.blobContainer.SetPermissionsAsync(
                new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        protected IMapper Mapper => this.mapper;

        /// <summary>
        /// Determines whether [is ajax request].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is ajax request]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAjaxRequest()
        {
            return this.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">The type.</param>
        public void ShowMessage(string message, Enums.MessageType messageType = Enums.MessageType.Success)
        {
            this.TempData["Message"] = message;
            this.TempData["MessageType"] = messageType.ToString().ToLower();
        }

        /// <summary>
        /// Upload Image Blob Storage.
        /// </summary>
        /// <returns>Flag</returns>
        /// <param name="folder">the folder name.</param>
        /// <param name="file">the file.</param>
        [HttpPost]
        public async Task<string> UploadImageBlobStorage(string folder, IFormFile file)
        {
            ////var fileName = file.GetFilename();'
            string fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

            byte[] imageBytes = await file.GetBytes();

            //// The parameter to the GetBlockBlobReference method will be the name
            //// of the image (the blob) as it appears on the storage server.
            //// You can name it anything you like; in this example, I am just using
            //// the actual filename of the uploaded image.
            CloudBlockBlob blockBlob = this.blobContainer.GetBlockBlobReference(this.GetImagePrefix(folder) + fileName);
            ////blockBlob.Properties.ContentType = "image/" + image.ImageFormat;
            ////blockBlob.Properties.ContentType = "image/" + file.ContentType;
            blockBlob.Properties.ContentType = file.ContentType;
            await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);

            return fileName;
        }

        private string GetImagePrefix(string folderName)
        {
            var prefix = this.configuration.GetValue<string>("AzureBlobAppSetting:BlobContentBasePath");
            prefix = prefix + folderName + "/";
            return prefix;
        }
    }
}