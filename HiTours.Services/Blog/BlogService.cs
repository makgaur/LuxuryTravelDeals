// <copyright file="BlogService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using static HiTours.Core.Enums;

    /// <summary>
    /// PackageService
    /// </summary>
    /// <seealso cref="HiTours.Services.IBlogService" />
    public class BlogService : IBlogService
    {
        private readonly IRepository<BlogPostsModel> blogRepository;
        private readonly IRepository<BlogPostGridViewModel> blogGridModelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService" /> class.
        /// </summary>
        /// <param name="blogRepository">Curations Repo</param>
        /// <param name="blogGridModelRepository">Curation Grid Repo</param>
        public BlogService(IRepository<BlogPostsModel> blogRepository, IRepository<BlogPostGridViewModel> blogGridModelRepository)
        {
            this.blogGridModelRepository = blogGridModelRepository;
            this.blogRepository = blogRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService" /> class.
        /// </summary>
        /// <param name="model">Data Table</param>
        /// <param name="mode">Curation Mode</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<DataTableResult> GetAllBlogsAsync(DataTableParameter model, string mode)
        {
            var result = this.blogRepository.Table.Select(x => new BlogPostGridViewModel
            {
                Id = x.Id,
                Image = x.Image,
                Line1 = x.Line1,
                Line2 = x.Line2,
                Line3 = x.Line3,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                IsFeatured = x.IsFeatured,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                Url = x.Url
            });

            if (mode == "feature")
            {
                result = result.Where(x => x.IsFeatured);
            }

            if (mode == "sub")
            {
                result = result.Where(x => !x.IsFeatured);
            }

            return await this.blogGridModelRepository.ToPagedListAsync(result, model);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="id">Data Table</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<BlogPostsModel> GetBlogsByIdAsync(int id)
        {
            var result = await this.blogRepository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<int> InsertAsync(BlogPostsModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Curation");
            }

            return await this.blogRepository.InsertAsync(model);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<int> UpdateAsync(BlogPostsModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Curation");
            }

            return await this.blogRepository.UpdateAsync(model);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public BlogsLayoutViewModel GetBlogsMainPage()
        {
            var result = new BlogsLayoutViewModel
            {
                FeaturedArticles = this.blogRepository.Table.Where(x => x.IsFeatured && x.IsActive).Select(x => new BlogPostAddViewModel
                {
                    Id = x.Id,
                    Line1 = x.Line1,
                    Line2 = x.Line2,
                    Line3 = x.Line3,
                    Image = x.Image,
                    RedirectText = x.RedirectText,
                    Url = x.Url
                }).ToList(),
                SubArticles = this.blogRepository.Table.Where(x => !x.IsFeatured && x.IsActive).Select(x => new BlogPostAddViewModel
                {
                    Id = x.Id,
                    Line1 = x.Line1,
                    Line2 = x.Line2,
                    Line3 = x.Line3,
                    Image = x.Image,
                    RedirectText = x.RedirectText,
                    Url = x.Url
                }).ToList(),
            };
            return result;
        }
    }
}