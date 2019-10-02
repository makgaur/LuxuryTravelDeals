using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models.Response.FareRuleResponse
{
    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FareRule
    {
        public string Airline { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public string FareBasisCode { get; set; }
        public string FareRestriction { get; set; }
        public string FareRuleDetail { get; set; }
        public int FlightId { get; set; }
        public string Origin { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    public class Response
    {
        public Error Error { get; set; }
        public List<FareRule> FareRules { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
    }

    public class FareRuleResponse
    {
        public Response Response { get; set; }
    }
}
