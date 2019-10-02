using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class ApiSearchResultResponse
    {
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Display(Name = "Error")]
        public ApiError Error { get; set; }

        [Display(Name = "Origin")]
        public string Origin { get; set; }

        [Display(Name = "ResponseStatus")]
        public long ResponseStatus { get; set; }

        [Display(Name = "Results")]
        public AirSearchResult[][] Results { get; set; }

        [Display(Name = "TraceId")]
        public string TraceId { get; set; }

        [JsonIgnore]
        public string TokenId { get; set; }

        [Display(Name = "Journey Type")]
        public int JourneyType { get; set; }

        public bool IncludeReturn { get; set; }

        public bool SessionExpired { get; set; }

        [JsonIgnore]
        public int TotalPassengers { get; set; }

        [JsonIgnore]
        public bool InterNationalReturnFlight { get; set; }
    }

    public class AirSearchResponse
    {
        public ApiSearchResultResponse Response { get; set; }
    }
}