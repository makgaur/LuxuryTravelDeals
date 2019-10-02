using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.MongoDb
{
    /// <summary>
    /// Handles database operations for items of type T
    /// </summary>
    /// <typeparam name="T">The type to be handled. It must be an object that implements BaseEntity to work with cosmos db</typeparam>
    public interface IMongoDbHandler<T>
        where T : BaseEntity, new()
    {
        /// <summary>
        /// Inserts the item to the cosmos db
        /// </summary>
        /// <param name="item">The item to insert into the cosmos db</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> InsertAsync(T item);

        /// <summary>
        /// Inserts a list of items to the database
        /// </summary>
        /// <param name="items">The list of items to insert into the database</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> InsertAsync(IEnumerable<T> items);

        /// <summary>
        /// Updates the item to the database.
        /// </summary>
        /// <param name="item">The item to update into the collection</param>
        Task<Response> UpdateAsync(T item);

        /// <summary>
        /// Inserts/updates the item to the cosmos db
        /// </summary>
        /// <param name="item">The item to insert/update into the cosmos db</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> InsertOrUpdateAsync(T item);

        /// <summary>
        /// Deletes an item from the database
        /// </summary>
        /// <param name="item">The item to delete from the collections</param>
        Task<Response> DeleteAsync(T item);

        /// <summary>
        /// Gets a single item from the collection of respective API
        /// </summary>
        /// <param name="id">The id for the item to retrieve</param>
        /// <returns>The item with given parameters</returns>
        Task<T> GetItemAsync(string id);

        /// <summary>
        /// Gets a list of items from the the collection of respective API
        /// </summary>
        /// <param name="ids">The list of ids to retrieve items from</param>
        /// <returns>A list of items with the given ids</returns>
        Task<IEnumerable<T>> GetItemsAsync(IEnumerable<string> ids);

        /// <summary>
        /// Get all items from a collection based on the expression
        /// </summary>
        /// <returns>returns the result items of the collection</returns>
        Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> expression);
    }
}
