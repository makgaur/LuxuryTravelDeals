using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Contract
{
    public class ChangeLogRequestModel
    {
        /// <summary>
        /// The table name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// The database context
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// The user email if requested per user
        /// </summary>
        public string UserEmail { get; set; }
    }
}
