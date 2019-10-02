using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.MongoDb
{
    /// <summary>
    /// Provides an interface for handling the database operations towards the Mongo DB
    /// </summary>
    /// <typeparam name="T">The entity type of the table</typeparam>
    public interface IMongoDbContext<T>
        where T : BaseEntity, new()
    {
        /// <summary>
        /// Get all items based on the expression
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns>True/False</returns>
        Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Gets a single item by id from the database
        /// </summary>
        /// <param name="id">The id of the item to fetch</param>
        /// <returns>An item, specialized for type T, of the results of executing the query</returns>
        Task<T> GetAsync(string id);

        /// <summary>
        /// Gets a list of items from the database based on the give ids
        /// </summary>
        /// <param name="ids">The ids of the item to fetch from</param>
        /// <returns>A list of items with the given ids</returns>
        Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids);

        /// <summary>
        /// Inserts the item to the database
        /// </summary>
        /// <param name="item">The item to insert into the database</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        Task InsertAsync(T item, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts a list of items to the database
        /// </summary>
        /// <param name="items">The items to insert into the database</param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<T> items);

        /// <summary>
        /// Updates an item into the database
        /// </summary>
        /// <param name="item">The item to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        Task<ReplaceOneResult> UpdateAsync(T item, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes an item by id from the database
        /// </summary>
        /// <param name="id">The id of the item to delete for</param>
        /// <returns></returns>
        Task DeleteAsync(string id);
    }
}
