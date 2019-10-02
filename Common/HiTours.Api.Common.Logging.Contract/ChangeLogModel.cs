using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Contract
{
    public class ChangeLogModel
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
        /// The time when change is stored
        /// </summary>
        public DateTime LogDate { get; set; }

        /// <summary>
        /// The partitionkey of the updated item
        /// </summary>
        public string ItemPartitionKey { get; set; }

        /// <summary>
        /// The rowkey of the item 
        /// </summary>
        public string ItemRowKey { get; set; }

        /// <summary>
        /// Created a changemodel from the input parameters
        /// </summary>
        /// <typeparam name="T">the type of the object changed</typeparam>
        /// <param name="changeType">The type of change that is made</param>
        /// <param name="context">The context (database) where the change happened</param>
        /// <param name="tableId">The database table that was subject to change</param>
        /// <param name="itemId">The Id of the item changed</param>
        /// <param name="model">The model that was changed</param>
        /// <returns>The Changemodel</returns>
        public static ChangeLogModel CreateChangeModel<T>(
            string changeType,
            string context,
            string tableId,
            string itemId,
            T model)
        {
            var declaringType = model.GetType().DeclaringType;
            var result = new ChangeLogModel
            {
                ItemId = itemId,
                Type = changeType,
                Context = context,
                TableName = tableId,
                LogDate = DateTime.UtcNow,
                ChangeData = SerializeToJson(model)
            };
            if (declaringType != null)
            {
                result.ChangedDataType = declaringType.ToString();
            }
            else
            {
                result.ChangedDataType = string.Empty;
            }

            return result;
        }

        private static string SerializeToJson<T>(T model)
        {
            return JsonConvert.SerializeObject(model, Formatting.Indented);
        }
    }
}
