using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public partial class BookingResponseRoot
    {
        [JsonProperty("Response")]
        public BookingResponse Response { get; set; }
    }

    public partial class BookingResponse
    {
        [JsonProperty("Error")]
        public ApiError Error { get; set; }

        [JsonProperty("TraceId")]
        public string TraceId { get; set; }

        [JsonProperty("ResponseStatus")]
        public long ResponseStatus { get; set; }

        [JsonProperty("Response")]
        public BookingResult Response { get; set; }
    }

    public partial class BookingResult
    {
        [JsonProperty("PNR")]
        public string Pnr { get; set; }

        [JsonProperty("BookingId")]
        public long BookingId { get; set; }

        [JsonProperty("SSRDenied")]
        public bool SsrDenied { get; set; }

        [JsonProperty("SSRMessage")]
        public object SsrMessage { get; set; }

        [JsonProperty("Status")]
        public long Status { get; set; }

        [JsonProperty("IsPriceChanged")]
        public bool IsPriceChanged { get; set; }

        [JsonProperty("IsTimeChanged")]
        public bool IsTimeChanged { get; set; }

        [JsonProperty("FlightItinerary")]
        public FlightItinerary FlightItinerary { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}