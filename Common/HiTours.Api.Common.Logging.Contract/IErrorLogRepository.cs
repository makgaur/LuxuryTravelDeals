using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Contract
{
    /// <summary>
    /// Repository responsible for logging error messages
    /// </summary>
    public interface IErrorLogRepository
    {
        /// <summary>
        /// Logs an error to the database
        /// </summary>
        /// <param name="errorLogModel">The class holding the error information</param>
        /// <returns>A result object</returns>
        Task<Response> LogError(ErrorLogModel errorLogModel);

        ///// <summary>
        ///// Creates a paged list of results from the error log
        ///// </summary>
        ///// <param name="pagedRequest">The request data containing page number and items per page</param>
        ///// <returns>A paged list of error models</returns>
        //Task<PagedList<ErrorLogModel>> GetErrorLogItems(ErrorLogRequest pagedRequest);
    }
}
