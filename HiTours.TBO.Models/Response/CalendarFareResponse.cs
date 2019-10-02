using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models.Response
{
    public class CalendarFareResponse
    {
        public CalendarFareResultResponse Response { get; set; }
    }
    public class CalendarFareResultResponse
    {
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Display(Name = "Error")]
        public ApiError Error { get; set; }

        [Display(Name = "Origin")]
        public string Origin { get; set; }
       
        [Display(Name = "TraceId")]
        public string TraceId { get; set; }

        [JsonIgnore]
        public string TokenId { get; set; }

        [Display(Name = "Journey Type")]
        public int JourneyType { get; set; }

        [Display(Name = "Results")]
        public List<CalendarFareResult> SearchResults { get; set; }
    }
    public class CalendarFareResult
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool IsLowestFareOfMonth { get; set; }
        public double Fare { get; set; }
        public double BaseFare { get; set; }
        public double Tax { get; set; }
        public double OtherCharges { get; set; }
        public double FuelSurcharge { get; set; }
    }
}
