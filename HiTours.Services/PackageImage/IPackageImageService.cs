// <copyright file="IPackageImageService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Models;

    /// <summary>
    /// IPackageImageService
    /// </summary>
    public interface IPackageImageService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>Insert Record Async</returns>
        Task<int> InsertAsync(PackageImageModel packageImage);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>Insert Bulk Records</returns>
        Task<int> InsertAsync(IEnumerable<PackageImageModel> packageImage);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>Update Record Async</returns>
        Task<int> UpdateAsync(PackageImageModel packageImage);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>Delete Record Async</returns>
        Task<int> DeleteAsync(PackageImageModel packageImage);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="packageImageId">The package image identifier.</param>
        /// <returns>Get Record By Id Async</returns>
        Task<PackageImageModel> GetByIdAsync(Guid packageImageId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Get All Record Async</returns>
        Task<IList<PackageImageModel>> GetAllAsync();
    }
}