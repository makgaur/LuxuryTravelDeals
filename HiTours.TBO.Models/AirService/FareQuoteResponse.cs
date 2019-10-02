using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class FareQuoteResponse
    {
        public FareQuoteResult Response { get; set; }
    }

    public class FareQuoteResult
    {
        public ApiError Error { get; set; }

        public bool IsPriceChanged { get; set; }

        public long ResponseStatus { get; set; }

        public AirSearchResult Results { get; set; }

        public string TraceId { get; set; }
    }
}