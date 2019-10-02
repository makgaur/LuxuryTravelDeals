using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Contract
{
    /// <summary>
    /// The change repository
    /// </summary>
    public interface IChangeRepository
    {
        /// <summary>
        /// Logs a change by saving a changemodel to the database
        /// </summary>
        /// <param name="changeLogModel">The changelogmodel containing the change details</param>
        /// <returns>True if the logging was successful, otherwise false</returns>
        Task<Response> LogChange(ChangeLogModel changeLogModel);

        /// <summary>
        /// Gets a list of changes made by the user supplied
        /// </summary>
        /// <param name="userId">The user Id to request changes on</param>
        /// <returns>A list of changes made by the user</returns>
        Task<IList<ChangeLogModel>> GetChangesByUser(string userId);

        /// <summary>
        /// Gets a list of changes made to the specific table
        /// </summary>
        /// <param name="changeLogRequestModel">The request data</param>
        /// <returns>A list of changes made to the table</returns>
        Task<IList<ChangeLogModel>> GetChangesByTable(ChangeLogRequestModel changeLogRequestModel);

        /// <summary>
        /// Gets all tables with saved changes
        /// </summary>
        /// <returns>Changed table names</returns>
        Task<IList<string>> GetChangedTables();

        /// <summary>
        /// Gets information about all tables in the log
        /// </summary>
        /// <returns>Returns the table overview</returns>
        Task<IList<ChangeLogTableOverviewModel>> GetTableOverview();
    }
}
