using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    /// <summary>
    /// Handles table/database operations for items of type T
    /// </summary>
    /// <typeparam name="T">The type to be handled. It must be an object that implements ITableEntity to work with table storages</typeparam>
    public interface ITableHandler<T>
        where T : ITableEntity, new()
    {
        /// <summary>
        /// Inserts the item to the table storage
        /// </summary>
        /// <param name="item">The item to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> InsertAsync(T item);

        /// <summary>
        /// Inserts a list of items to the table storage
        /// </summary>
        /// <param name="items">The list of items to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> InsertAsync(IEnumerable<T> items);

        /// <summary>
        /// Inserts the item to the table storage. If the item exists, the item will be updated
        /// </summary>
        /// <param name="item">The item to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> InsertOrUpdateAsync(T item);

        /// <summary>
        /// Inserts a list of items to the table storage. If any of the items exists, it will be updated instead of inserted
        /// </summary>
        /// <param name="items">The list of items to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> InsertOrUpdateAsync(IEnumerable<T> items);

        /// <summary>
        /// Deletes an item from the table storage
        /// </summary>
        /// <param name="item">The item to delete from the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> DeleteAsync(T item);

        /// <summary>
        /// Deletes a list of items from the table storage
        /// </summary>
        /// <param name="items">The items to delete from the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        Task<Response> DeleteAsync(IEnumerable<T> items);

        /// <summary>
        /// Gets a single item from the table storage
        /// </summary>
        /// <param name="partitionKey">The partition key for the item to retrieve</param>
        /// <param name="rowKey">The unique rowkey to use</param>
        /// <returns>The item with given parameters</returns>
        Task<T> GetItemAsync(string partitionKey, string rowKey);

        /// <summary>
        /// Gets a list of items from the table storage based on partition key
        /// </summary>
        /// <param name="partitionKey">The partition key to retrieve items from</param>
        /// <returns>A list of items with the given partitionkey</returns>
        Task<IEnumerable<T>> GetItemsAsync(string partitionKey);

        /// <summary>
        /// Executes a tablequery of the type on the storage
        /// </summary>
        /// <param name="query">the query to perform</param>
        /// <returns>returns the result items of the query</returns>
        Task<IEnumerable<T>> ExecuteQueryAsync(TableQuery<T> query);
    }
}
