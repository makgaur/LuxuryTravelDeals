// <copyright file="CategoryService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CategoryService
    /// </summary>
    /// <seealso cref="HiTours.Services.ICategoryService" />
    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<CategoryModel> categoryRepository;

        private readonly IRepository<Dropdown> dropdownRespository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService" /> class.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="dropdownRepository">The dropdown repository.</param>
        public CategoryService(IRepository<CategoryModel> categoryRepository, IRepository<Dropdown> dropdownRepository)
        {
            this.categoryRepository = categoryRepository;
            this.dropdownRespository = dropdownRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(CategoryModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            category.IsActive = true;
            return await this.categoryRepository.InsertAsync(category);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(CategoryModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            return await this.categoryRepository.UpdateAsync(category);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(CategoryModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            category.IsDelete = true;
            return await this.categoryRepository.UpdateAsync(category);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<CategoryModel> GetByIdAsync(int categoryId)
        {
            if (categoryId == 0)
            {
                return null;
            }

            return await this.categoryRepository.Table.Where(x => !x.IsDelete).FirstOrDefaultAsync(m => m.ID == categoryId);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<IList<CategoryModel>> GetAllAsync()
        {
            return await this.categoryRepository.Table.Where(x => !x.IsDelete).ToListAsync();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllAsync(DataTableParameter model)
        {
            var query = this.categoryRepository.Table.Where(x => !x.IsDelete);
            var records = await this.categoryRepository.ToPagedListAsync(query, model);
            return records;
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryid">The categoryid.</param>
        /// <returns>GetDuplicateAsync</returns>
        public async Task<bool> IsDuplicateAsync(string name, int categoryid)
        {
            var category =
              await this.categoryRepository.Table.FirstOrDefaultAsync(x => x.ID != categoryid && x.Name.ToLower().Trim() == name.ToLower().Trim());
            return category == null;
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetOptions</returns>
        public async Task<IList<Dropdown>> GetOptions(string search, short page)
        {
            var query = this.categoryRepository.Table
                            .Where(x => x.Name.StartsWith(search))
                            .Select(x => new Dropdown { Id = x.ID.ToString(), Name = x.Name });

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }
    }
}