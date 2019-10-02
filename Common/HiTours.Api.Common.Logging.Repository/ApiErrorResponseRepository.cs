using HiTours.Api.Common.Data.Azure;
using HiTours.Api.Common.Logging.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Repository
{
    public class ApiErrorResponseRepository : IApiErrorResponseRepository
    {
        //private readonly ITableHandler<ApiErrorResponse> tableHandler;

        //public ApiErrorResponseRepository(ITableHandler<ApiErrorResponse> tableHandler)
        //{
        //    this.tableHandler = tableHandler;
        //}

        //public async Task<Response> Create(ApiErrorResponseModel apiErrorResponseModel)
        //{
        //    var apiErrorResponse = new ApiErrorResponse
        //    {
        //        ErrorCode = apiErrorResponseModel.ErrorCode,
        //        ReasonPhrase = apiErrorResponseModel.ReasonPhrase,
        //        RequestBody = apiErrorResponseModel.RequestBody,
        //        RequestUri = apiErrorResponseModel.RequestUri,
        //        ResponseBody = apiErrorResponseModel.ResponseBody
        //    };

        //    var result = await this.tableHandler.InsertAsync(apiErrorResponse);

        //    return result;
        //}



        //public ApiErrorResponseRepository(ITableHandler<ApiErrorResponse> tableHandler)
        //{
        //    this.tableHandler = tableHandler;
        //}

        public async Task<Response> Create(ApiErrorResponseModel apiErrorResponseModel)
        {
            //var apiErrorResponse = new ApiErrorResponse
            //{
            //    ErrorCode = apiErrorResponseModel.ErrorCode,
            //    ReasonPhrase = apiErrorResponseModel.ReasonPhrase,
            //    RequestBody = apiErrorResponseModel.RequestBody,
            //    RequestUri = apiErrorResponseModel.RequestUri,
            //    ResponseBody = apiErrorResponseModel.ResponseBody
            //};

            //var result = await this.tableHandler.InsertAsync(apiErrorResponse);

            //return result;

           return  null;
        }
    }
}
