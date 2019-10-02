using HiTours.Api.Common.Data.Azure;
using HiTours.Api.Common.Logging.Contract;
using HiTours.Api.Common.Logging.Repository.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly ITableHandler<ErrorLogEntity> errorTableHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLogRepository"/> class. 
        /// </summary>
        /// <param name="errorTableHandler">The tablehandler for logging errors</param>
        public ErrorLogRepository(ITableHandler<ErrorLogEntity> errorTableHandler)
        {
            this.errorTableHandler = errorTableHandler;
        }

        /// <summary>
        /// Logs an ErrorLogEntity to the database
        /// </summary>
        /// <param name="errorLogModel">The class holding the ErrorLogEntity information</param>
        /// <returns>A result object</returns>
        public async Task<Response> LogError(ErrorLogModel errorLogModel)
        {
            var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            var error = new ErrorLogEntity
            {
                PartitionKey = errorLogModel.Controller,
                Id = Guid.NewGuid().ToString("N"),
                Controller = errorLogModel.Controller,
                Action = errorLogModel.Action,
                UserEmail = errorLogModel.UserEmail,
                UserId = errorLogModel.UserId,
                Message = errorLogModel.Message,
                Exception = JsonConvert.SerializeObject(errorLogModel.Exception, Formatting.Indented),
                RequestUrl = errorLogModel.RequestUrl
            };

            error.RowKey = error.Id;

            if (claimsIdentity != null)
            {
                var emailClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

                if (emailClaim != null)
                {
                    error.UserEmail = emailClaim.Value;
                }

                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    error.UserId = userIdClaim.Value;
                }
            }

            try
            {
                await this.errorTableHandler.InsertAsync(error);
                return Response.Success();
            }
            catch (Exception exp)
            {
                return Response.Failed(exp.Message);
            }
        }

        ///// <summary>
        ///// Creates a paged list of results from the ErrorLogEntity log
        ///// </summary>
        ///// <param name="pagedRequest">The request data containing page number and items per page</param>
        ///// <returns>A paged list of ErrorLogEntity models</returns>
        //public async Task<PagedList<ErrorLogModel>> GetErrorLogItems(ErrorLogRequest pagedRequest)
        //{
        //    var result = from ErrorLogEntity in this.loggingContext.ErrorLog select ErrorLogEntity;

        //    var filtered = result.OrderBy(o => o.Id);

        //    var pagedListCreator = new PagedResultFactory<ErrorLogEntity, ErrorLogModel>(pagedRequest);

        //    var pageListData = await pagedListCreator.CreatePageListDataAsync(filtered, CreateErrorLogModel);

        //    return pageListData;
        //}

        private static ErrorLogModel CreateErrorLogModel(ErrorLogEntity errorLogEntity)
        {
            var errorLogModel = new ErrorLogModel
            {
                Controller = errorLogEntity.Controller,
                Action = errorLogEntity.Action,
                UserId = errorLogEntity.UserId,
                Message = errorLogEntity.Message,
                RequestUrl = errorLogEntity.RequestUrl,
                UserEmail = errorLogEntity.UserEmail,
                CreatedDateTime = errorLogEntity.Timestamp.UtcDateTime
            };

            return errorLogModel;
        }
    }
}
