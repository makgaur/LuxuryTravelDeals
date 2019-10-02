// <copyright file="IRepository.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic IRepository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public partial interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets the table no tracking.
        /// </summary>
        /// <value>
        /// The table no tracking.
        /// </value>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        /// Adds to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void AddToContext(TEntity entity);

        /// <summary>
        /// Adds to context.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void AddToContext(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes to context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RemoveToContext(TEntity entity);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Insert Entity </returns>
        int Insert(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Delete Entity</returns>
        int Delete(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Update Entity</returns>
        int Update(TEntity entity);

        /// <summary>
        /// Updates the complete graph.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="key">The key.</param>
        /// <returns>type of entity</returns>
        TEntity UpdateCompleteGraph(TEntity t, object key);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Async Insert Entity</returns>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Async Delete Entity</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Async Update Entity</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// To the paged list asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="model">The model.</param>
        /// <returns>Get Pagging Entites List By Querable Data with search parameters</returns>
        Task<DataTableResult> ToPagedListAsync(IQueryable<TEntity> query, DataTableParameter model);

        /// <summary>
        /// To the option list asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// Get pagging Data for Dropdown Select Item List
        /// </returns>
        Task<IList<Dropdown>> ToOptionListAsync(IQueryable<Dropdown> query, int page);

        /// <summary>
        /// To the option list asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        /// Get pagging Data for Dropdown Select Item List
        /// </returns>
        Task<IList<Dropdown>> ToListAsync(IQueryable<Dropdown> query);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Save Changes </returns>
        Task<int> SaveChangesAsync();
    }
}