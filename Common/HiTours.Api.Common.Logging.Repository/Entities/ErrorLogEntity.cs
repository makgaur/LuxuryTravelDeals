using HiTours.Api.Common.Data.Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Repository.Entities
{
    /// <summary>
    /// The model holding necessary data to log an error object
    /// </summary>
    public class ErrorLogEntity : CustomTableEntity
    {
        /// <summary>
        /// the storage key
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The controller where the error occured
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// The action attempted when error occured
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The requested url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// The exception that occured in json string format
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The email of the user that executed the change (at the time of change)
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// The Id of the user that executed the change
        /// </summary>
        public string UserId { get; set; }
    }
}
