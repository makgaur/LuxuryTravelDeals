using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Contract
{
    /// <summary>
    /// The model holding necessary data to log an error object
    /// </summary>
    public class ErrorLogModel
    {
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
        /// The exception that occured
        /// </summary>
        public Exception Exception { get; set; }

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

        /// <summary>
        /// The time when change is stored
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
    }
}
