using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Contract
{
    public class ApiErrorResponseModel
    {
        public int ErrorCode { get; set; }

        public string ResponseBody { get; set; }

        public string RequestBody { get; set; }

        public string RequestUri { get; set; }

        public string ReasonPhrase { get; set; }
    }
}
