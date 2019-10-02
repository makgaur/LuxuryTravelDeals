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
    public class TableStorageHandler<T> : ITableHandler<T>
        where T : ITableEntity, new()
    {
        private readonly ITableContext<T> tableContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableStorageHandler{T}"/> class. 
        /// </summary>
        /// <param name="tableContext">The table context to use for the handler</param>
        public TableStorageHandler(ITableContext<T> tableContext)
        {
            this.tableContext = tableContext;
        }

        /// <summary>
        /// Inserts the item to the table storage
        /// </summary>
        /// <param name="item">The item to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        public async Task<Response> InsertAsync(T item)
        {
            var tableOperation = TableOperation.Insert(item);
            var result = await this.tableContext.ExecuteAsync(tableOperation);

            return GetResponse(result);
        }

        /// <summary>
        /// Inserts a list of items to the table storage
        /// </summary>
        /// <param name="items">The list of items to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        public async Task<Response> InsertAsync(IEnumerable<T> items)
        {
            var tableBatchOperation = new TableBatchOperation();
            var resultList = new List<TableResult>();
            var previousPartition = string.Empty;

            foreach (var item in items)
            {
                if (tableBatchOperation.Count == 100
                    || (item.PartitionKey != previousPartition && previousPartition != string.Empty))
                {
                    resultList.AddRange(await this.RunTableOperation(tableBatchOperation));
                    tableBatchOperation = new TableBatchOperation();
                }

                previousPartition = item.PartitionKey;
                tableBatchOperation.Insert(item);
            }

            if (tableBatchOperation.Count > 0)
            {
                resultList.AddRange(await this.RunTableOperation(tableBatchOperation));
            }

            return GetResponse(resultList);
        }

        /// <summary>
        /// Inserts the item to the table storage. If the item exists, the item will be updated
        /// </summary>
        /// <param name="item">The item to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        public async Task<Response> InsertOrUpdateAsync(T item)
        {
            var tableOperation = TableOperation.InsertOrReplace(item);
            var result = await this.tableContext.ExecuteAsync(tableOperation);

            return GetResponse(result);
        }

        /// <summary>
        /// Inserts a list of items to the table storage. If any of the items exists, it will be updated instead of inserted
        /// </summary>
        /// <param name="items">The list of items to insert into the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        public async Task<Response> InsertOrUpdateAsync(IEnumerable<T> items)
        {
            var tableBatchOperation = new TableBatchOperation();
            var resultList = new List<TableResult>();

            foreach (var item in items)
            {
                tableBatchOperation.InsertOrReplace(item);

                if (tableBatchOperation.Count == 100)
                {
                    resultList.AddRange(await this.tableContext.ExecuteBatchAsync(tableBatchOperation));
                    tableBatchOperation = new TableBatchOperation();
                }
            }

            if (tableBatchOperation.Count > 0)
            {
                resultList.AddRange(await this.tableContext.ExecuteBatchAsync(tableBatchOperation));
            }

            return GetResponse(resultList);
        }

        /// <summary>
        /// Deletes an item from the table storage
        /// </summary>
        /// <param name="item">The item to delete from the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        public async Task<Response> DeleteAsync(T item)
        {
            var tableOperation = TableOperation.Delete(item);
            var result = await this.tableContext.ExecuteAsync(tableOperation);

            return GetResponse(result);
        }

        /// <summary>
        /// Deletes a list of items from the table storage
        /// </summary>
        /// <param name="items">The items to delete from the table storage</param>
        /// <returns>The Response containing information about the operation</returns>
        public async Task<Response> DeleteAsync(IEnumerable<T> items)
        {
            var tableBatchOperation = new TableBatchOperation();
            var resultList = new List<TableResult>();

            foreach (var item in items)
            {
                tableBatchOperation.Delete(item);

                if (tableBatchOperation.Count == 100)
                {
                    resultList.AddRange(await this.tableContext.ExecuteBatchAsync(tableBatchOperation));
                    tableBatchOperation = new TableBatchOperation();
                }
            }

            if (tableBatchOperation.Count > 0)
            {
                resultList.AddRange(await this.tableContext.ExecuteBatchAsync(tableBatchOperation));
            }

            return GetResponse(resultList);
        }

        /// <summary>
        /// Gets a single item from the table storage
        /// </summary>
        /// <param name="partitionKey">The partition key for the item to retrieve</param>
        /// <param name="rowKey">The unique rowkey to use</param>
        /// <returns>The item with given parameters</returns>
        public async Task<T> GetItemAsync(string partitionKey, string rowKey)
        {
            var tableOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var result = await this.tableContext.ExecuteAsync(tableOperation);
            return (T)result.Result;
        }

        /// <summary>
        /// Gets a list of items from the table storage based on partition key
        /// </summary>
        /// <param name="partitionKey">The partition key to retrieve items from</param>
        /// <returns>A list of items with the given partitionkey</returns>
        public async Task<IEnumerable<T>> GetItemsAsync(string partitionKey)
        {
            var tableQuery =
                new TableQuery<T>().Where(
                    TableQuery.GenerateFilterCondition(
                        nameof(ITableEntity.PartitionKey),
                        QueryComparisons.Equal,
                        partitionKey));

            var result = await this.tableContext.ExecuteQueryAsync(tableQuery);

            return result;
        }

        /// <summary>
        /// Executes a tablequery of the type on the storage
        /// </summary>
        /// <param name="query">the query to perform</param>
        /// <returns>returns the result items of the query</returns>
        public async Task<IEnumerable<T>> ExecuteQueryAsync(TableQuery<T> query)
        {
            return await this.tableContext.ExecuteQueryAsync(query);
        }

        private static Response GetResponse(TableResult result)
        {
            if (result.HttpStatusCode >= 200 && result.HttpStatusCode < 300)
            {
                return Response.Success();
            }

            return Response.Failed(result.ToString());
        }

        private static Response GetResponse(IEnumerable<TableResult> result)
        {
            var failCodes = new List<int>();
            var failed = false;

            foreach (var tableResult in result)
            {
                if (tableResult.HttpStatusCode < 200 || tableResult.HttpStatusCode >= 300)
                {
                    failed = true;
                    failCodes.Add(tableResult.HttpStatusCode);
                }
            }

            if (failed == false)
            {
                return Response.Success();
            }

            var errors = string.Empty;

            foreach (var failCode in failCodes)
            {
                errors += failCode.ToString();
            }

            return Response.Failed(errors);
        }

        private async Task<IList<TableResult>> RunTableOperation(TableBatchOperation tableBatchOperation)
        {
            try
            {
                return await this.tableContext.ExecuteBatchAsync(tableBatchOperation);
            }
            catch (Exception)
            {
                return new List<TableResult>();
            }
        }
    }
}
