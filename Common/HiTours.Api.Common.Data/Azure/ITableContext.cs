using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    /// <summary>
    /// Provides an interface for handling the database operations towards the data table storage
    /// </summary>
    /// <typeparam name="T">The entity type of the table</typeparam>
    public interface ITableContext<T>
        where T : ITableEntity, new()
    {
        ///// <summary>
        ///// Executes a query on the table
        ///// </summary>
        ///// <param name="tableQuery">The query to execute</param>
        ///// <returns>An enumerable collection, specialized for type T, of the results of executing the query</returns>
        //IEnumerable<T> ExecuteQuery(TableQuery<T> tableQuery);

        /// <summary>
        /// Executes an operation on the table
        /// </summary>
        ///// <param name="tableOperation">The table operation to execute</param>
        ///// <returns>TableResult explaining what happened</returns>
        //TableResult Execute(TableOperation tableOperation);

        /// <summary>
        /// Executes an operation on the table async
        /// </summary>
        /// <param name="tableOperation">The table operation to execute</param>
        /// <returns>TableResult explaining what happened</returns>
        Task<TableResult> ExecuteAsync(TableOperation tableOperation);

        /// <summary>
        /// Executes a query on the table
        /// </summary>
        /// <param name="tableQuery">The query to execute</param>
        /// <param name="cancellationToken">Cancellation token to request cancellation of the operation</param>
        /// <param name="onProgress">May be used to notify the caller of progress</param>
        /// <returns>An enumerable collection, specialized for type T, of the results of executing the query</returns>
        Task<IList<T>> ExecuteQueryAsync(
            TableQuery<T> tableQuery,
            CancellationToken cancellationToken = default(CancellationToken),
            Action<IList<T>> onProgress = null);

        ///// <summary>
        ///// Executes operations as a batch on the table
        ///// </summary>
        ///// <param name="tableBatchOperation">The batch of table operation to execute</param>
        ///// <returns>An enumerable collection of TableResult</returns>
        //IList<TableResult> ExecuteBatch(TableBatchOperation tableBatchOperation);

        /// <summary>
        /// Executes operations as a batch on the table async
        /// </summary>
        /// <param name="tableBatchOperation">The batch of table operation to execute</param>
        /// <returns>An enumerable collection of TableResult</returns>
        Task<IList<TableResult>> ExecuteBatchAsync(TableBatchOperation tableBatchOperation);

    }
}
