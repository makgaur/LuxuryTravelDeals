using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.Azure
{
    public class IncrementalSequenceTableEntity : TableEntity
    {
        public IncrementalSequenceTableEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public IncrementalSequenceTableEntity()
        {
        }

        public int CurrentSequence { get; set; }
    }
}
