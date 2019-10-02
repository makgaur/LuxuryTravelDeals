// <copyright file="IBlogService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;

    /// <summary>
    /// ICurationsService
    /// </summary>
    public interface IBlogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService" /> class.
        /// </summary>
        /// <param name="model">Data Table</param>
        /// <param name="mode">Curation Mode</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<DataTableResult> GetAllBlogsAsync(DataTableParameter model, string mode);

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="id">Data Table</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<BlogPostsModel> GetBlogsByIdAsync(int id);

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<int> UpdateAsync(BlogPostsModel model);

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<int> InsertAsync(BlogPostsModel model);

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        BlogsLayoutViewModel GetBlogsMainPage();
    }
}