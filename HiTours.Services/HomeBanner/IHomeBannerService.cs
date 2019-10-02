// <copyright file="IHomeBannerService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels;

    /// <summary>
    /// IHomeBannerService
    /// </summary>
    public interface IHomeBannerService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="homeBanner">The package image.</param>
        /// <returns>Insert Record Async</returns>
        Task<int> InsertAsync(HomeBannerModel homeBanner);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="homeBanner">The package image.</param>
        /// <returns>Insert Bulk Records</returns>
        Task<int> InsertAsync(IEnumerable<HomeBannerModel> homeBanner);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="popularDestinationModel">The Popular Destination.</param>
        /// <returns>Insert Multiple Records Async</returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        Task<int> InsertPopularDestinationAsync(PopularDestinationModel popularDestinationModel);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="homeBanner">The package image.</param>
        /// <returns>Update Record Async</returns>
        Task<int> UpdateAsync(HomeBannerModel homeBanner);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="popularDestinationModel">Popular Destination Model</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        Task<int> UpdatePopularDestinationAsync(PopularDestinationModel popularDestinationModel);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="packageImage">The package image.</param>
        /// <returns>
        /// Delete Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">packageImage</exception>
        Task<int> DeletePopularDestinationAsync(PopularDestinationModel packageImage);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="homeBanner">The package image.</param>
        /// <returns>Delete Record Async</returns>
        Task<int> DeleteAsync(HomeBannerModel homeBanner);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="homeBannerId">The package image identifier.</param>
        /// <returns>Get Record By Id Async</returns>
        Task<HomeBannerModel> GetByIdAsync(int homeBannerId);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="popularDestinationId">The package image identifier.</param>
        /// <returns>
        /// Get Record By Id Async
        /// </returns>
        Task<PopularDestinationModel> GetPopularDestinationByIdAsync(int popularDestinationId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Get All Record Async
        /// </returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<DataTableResult> GetAllPopularDestinationAsync(DataTableParameter model);

        /// <summary>
        /// Gets the banners list asynchronous.
        /// </summary>
        /// <returns>List</returns>
        Task<IList<HomeBannerViewModel>> GetBannersListAsync();

        /// <summary>
        /// Gets the banners list asynchronous.
        /// </summary>
        /// <returns>GetBannersListAsync</returns>
        Task<List<PopularDestinationViewModel>> GetPopularDestinationListAsync();
    }
}
