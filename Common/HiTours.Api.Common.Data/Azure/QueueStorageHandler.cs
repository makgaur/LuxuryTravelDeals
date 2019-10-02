using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    public class QueueStorageHandler : IQueueHandler
    {
        public const string QueueParameter = "queueName";

        public const string ConnectionStringParameterName = "connectionString";

        private readonly CloudQueue queue;

        public QueueStorageHandler(string connectionString, string queueName)
        {
            // Retrieve storage account from connection string.
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to the queue.
            this.queue = queueClient.GetQueueReference(queueName);
            //  this.queue.CreateIfNotExistsAsync();
        }

        public async Task WriteToQueue(QueueInputModel queueInputModel)
        {
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(queueInputModel));
            await this.queue.AddMessageAsync(queueMessage);
        }
    }
}
