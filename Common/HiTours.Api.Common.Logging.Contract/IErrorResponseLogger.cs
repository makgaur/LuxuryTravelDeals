using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Contract
{
    public interface IErrorResponseLogger
    {
        Task Log(HttpRequestMessage request, HttpResponseMessage responseBody);
    }
}
