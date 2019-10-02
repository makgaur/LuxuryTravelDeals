using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.Azure
{
    public class TableInitializer
    {
        public static void Create(string tableName, string connectionString)
        {
            // Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            // CreateAsync the Table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // CreateAsync the Table if it doesn't exist.
            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExistsAsync();
        }
    }
}
