using HiTours.Api.Common.Data.Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Repository.Entities
{
    public class ChangeLogTableOverviewEntity : CustomTableEntity
    {
        /// <summary>
        /// The database context
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// The table name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// The user Ids of users that changed this table
        /// </summary>
        public string Users { get; set; }

        /// <summary>
        /// The number of create operations logged
        /// </summary>
        public int CreateLogCount { get; set; }

        /// <summary>
        /// The number of modify operations logged
        /// </summary>
        public int ModifyLogCount { get; set; }

        /// <summary>
        /// The number of delete operations logged
        /// </summary>
        public int DeleteLogCount { get; set; }

    }
}
