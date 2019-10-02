// <copyright file="PackageImageService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// PackageImageService
    /// </summary>
    /// <seealso cref="HiTours.Services.IPackageImageService" />
    public class PackageImageService : IPackageImageService
    {
        private readonly IRepository<PackageImageModel> packageImageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageImageService"/> class.
        /// </summary>
        /// <param name="packageImageRepository">The package image repository.</param>
        public PackageImageService(IRepository<PackageImageModel> packageImageRepository)
        {
            this.packageImageRepository = packageImageRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Insert Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> InsertAsync(PackageImageModel packageImage)
        {
            if (packageImage == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            return await this.packageImageRepository.InsertAsync(packageImage);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageImages">The package images.</param>
        /// <returns>Insert Multiple Records Async</returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> InsertAsync(IEnumerable<PackageImageModel> packageImages)
        {
            if (packageImages == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            this.packageImageRepository.AddToContext(packageImages);
            return await this.packageImageRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> UpdateAsync(PackageImageModel packageImage)
        {
            if (packageImage == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            return await this.packageImageRepository.UpdateAsync(packageImage);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Delete Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        public async Task<int> DeleteAsync(PackageImageModel packageImage)
        {
            if (packageImage == null)
            {
                throw new ArgumentNullException("packageImage");
            }

            return await this.packageImageRepository.DeleteAsync(packageImage);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="packageImageId">The package image identifier.</param>
        /// <returns>
        /// Get Record By Id Async
        /// </returns>
        public async Task<PackageImageModel> GetByIdAsync(Guid packageImageId)
        {
            return await this.packageImageRepository.Table.FirstOrDefaultAsync(m => m.Id == packageImageId);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Get All Record Async
        /// </returns>
        public async Task<IList<PackageImageModel>> GetAllAsync()
        {
            return await this.packageImageRepository.Table.ToListAsync();
        }
    }
}