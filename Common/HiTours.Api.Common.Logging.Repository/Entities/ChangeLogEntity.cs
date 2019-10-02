using HiTours.Api.Common.Data.Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Repository.Entities
{
    public class ChangeLogEntity : CustomTableEntity
    {
        /// <summary>
        /// The type of change made, "Create", "Modify" or "Delete"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The context of the change
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// The name of the table that changed
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// The Id of the item created, updated or deleted
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// The email of the user that executed the change (at the time of change)
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// The Id of the user that executed the change
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The changed data (preferably a json object for readability)
        /// </summary>
        public string ChangeData { get; set; }

        /// <summary>
        /// The changed data type
        /// </summary>
        public string ChangedDataType { get; set; }

        /// <summary>
        /// The ItemRowKey for the object stored
        /// </summary>
        public string ItemRowKey { get; set; }

        /// <summary>
        /// The ItemPartitionKey for the object stored
        /// </summary>
        public string ItemPartitionKey { get; set; }
    }
}
