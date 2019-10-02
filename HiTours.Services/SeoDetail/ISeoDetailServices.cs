// <copyright file="ISeoDetailServices.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System.Threading.Tasks;
    using HiTours.Models;

    /// <summary>
    /// ISeoDetailServices
    /// </summary>
    public interface ISeoDetailServices
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="seoDetail">The seo detail.</param>
        /// <returns>integer value</returns>
        Task<int> InsertAsync(SeoDetailModel seoDetail);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="seoDetail">The seo detail.</param>
        /// <returns>integer value</returns>
        Task<int> UpdateAsync(SeoDetailModel seoDetail);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="pageType">Type of the page.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// model detail
        /// </returns>
        Task<SeoDetailModel> GetByIdAsync(string pageType, string pageId);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="url">The page identifier.</param>
        /// <returns>
        /// Seo
        /// </returns>
        Task<SeoDetailModel> GetSeoDetail(string url);
    }
}