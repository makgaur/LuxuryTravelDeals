using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Contract
{
    public class ChangeLogTableOverviewModel
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
        /// List of users making changes to the table
        /// </summary>
        public List<string> Users { get; set; }

        /// <summary>
        /// The number of users making changes
        /// </summary>
        public int UserCount { get; set; }

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
