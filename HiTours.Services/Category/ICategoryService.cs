// <copyright file="ICategoryService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// ICategoryService
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>InsertAsync</returns>
        Task<int> InsertAsync(CategoryModel category);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>UpdateAsync</returns>
        Task<int> UpdateAsync(CategoryModel category);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(CategoryModel category);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>GetByIdAsync</returns>
        Task<CategoryModel> GetByIdAsync(int categoryId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>GetAllAsync</returns>
        Task<IList<CategoryModel>> GetAllAsync();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAllAsync</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryid">The categoryid.</param>
        /// <returns>GetDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int categoryid);

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetOptions</returns>
        Task<IList<Dropdown>> GetOptions(string search, short page);
    }
}