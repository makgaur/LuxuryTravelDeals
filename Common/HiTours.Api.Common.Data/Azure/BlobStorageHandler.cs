using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    public class BlobStorageHandler : IBlobHandler
    {
        public const string ContainerParameter = "containerName";
        public const string ConnectionStringParameterName = "connectionString";

        private readonly CloudBlobContainer container;

        public BlobStorageHandler(string connectionString, string containerName)
        {
            // Retrieve storage account from connection string.
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            this.container = blobClient.GetContainerReference(containerName);
            this.container.CreateIfNotExistsAsync();
        }

        public async Task WriteToBlob(string identifier, string item)
        {
            var blockBlob = this.container.GetBlockBlobReference(identifier);

            await blockBlob.UploadTextAsync(item);
        }

        public async Task<string> ReadFromBlob(string identifier)
        {
            var blockBlob = this.container.GetBlockBlobReference(identifier);

            return await blockBlob.DownloadTextAsync();
        }

        public CloudBlockBlob GetBlockBlobReference(string identifier)
        {
            return this.container.GetBlockBlobReference(identifier);
        }
    }
}
