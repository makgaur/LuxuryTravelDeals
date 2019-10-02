using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    /// <summary>
    /// Serves as a wrapper around the azure storage calls
    /// </summary>
    /// <typeparam name="T">The type associated with the Table in use</typeparam>
    public class TableContext<T> : TableContext, ITableContext<T> where T : ITableEntity, new()
    {
        private readonly string connectionString;
        private readonly string tableName;
        private CloudTable table;

        private CloudTable Table
        {
            get
            {
                if (this.table == null)
                {
                    // Retrieve the storage account from the connection string.
                    var storageAccount = CloudStorageAccount.Parse(this.connectionString);

                    // CreateAsync the Table client.
                    var tableClient = storageAccount.CreateCloudTableClient();

                    // CreateAsync the Table if it doesn't exist.
                    this.table = tableClient.GetTableReference(this.tableName);
                }

                return this.table;
            }
        }

        /// <summary>
        /// Initializes the TableContext
        /// </summary>
        /// <param name="connectionString">The connection string to the azure storage</param>
        /// <param name="tableName">The name of the cloudtable to use</param>
        public TableContext(string connectionString, string tableName)
        {
            this.connectionString = connectionString;
            this.tableName = tableName;
        }

        //public IEnumerable<T> ExecuteQuery(TableQuery<T> tableQuery)
        //{
        //    return this.Table.ExecuteQuery(tableQuery);
        //}

        //public async Task<TableResult> Execute(TableOperation tableOperation)
        //{
        //    return await this.Table.ExecuteAsync(tableOperation);
        //}

        public Task<TableResult> ExecuteAsync(TableOperation tableOperation)
        {
            return this.Table.ExecuteAsync(tableOperation);
        }

        public async Task<IList<T>> ExecuteQueryAsync(
            TableQuery<T> tableQuery,
            CancellationToken cancellationToken = default(CancellationToken),
            Action<IList<T>> onProgress = null)
        {
            var result = new List<T>();
            TableContinuationToken token = null;

            do
            {
                var seg = await this.Table.ExecuteQuerySegmentedAsync(tableQuery, token);
                token = seg.ContinuationToken;
                result.AddRange(seg);
                onProgress?.Invoke(result);
            }
            while (token != null && !cancellationToken.IsCancellationRequested);

            return result;
        }

        //public IList<TableResult> ExecuteBatch(TableBatchOperation tableBatchOperation)
        //{
        //    return this.Table.ExecuteBatch(tableBatchOperation);
        //}

        public Task<IList<TableResult>> ExecuteBatchAsync(TableBatchOperation tableBatchOperation)
        {
            return this.Table.ExecuteBatchAsync(tableBatchOperation);
        }
    }

    public class TableContext
    {
        public const string ConnectionStringParameterName = "connectionString";
        public const string TableNameParameterName = "tableName";
    }
}
