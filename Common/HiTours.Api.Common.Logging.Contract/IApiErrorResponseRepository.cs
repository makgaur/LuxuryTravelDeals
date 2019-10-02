using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Contract
{
    public interface IApiErrorResponseRepository
    {
        Task<Response> Create(ApiErrorResponseModel apiErrorResponse);
    }


}
