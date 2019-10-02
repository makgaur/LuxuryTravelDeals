using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class FlightBook
    {
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public string ResultIndex { get; set; }

        public List<AirSearchResult> AirSearchResults { get; set; }

        ////public AirSearchResult AirSearchResult { get; set; }
        public List<Passengers> Passengers { get; set; }

        public bool StatusFlightQuote { get; set; }
        public string Message { get; set; }

        public int TotalPassengers { get; set; }
        public long TotalBaseFareAmount { get; set; }
        public long TotalBaseFareTaxAmount { get; set; }
        public long TotalAmount { get; set; }

        public string RedirectUrl { get; set; }

        public PaymentStatus Payment { get; set; }

        public decimal MarkUpPrice { get; set; }
        public decimal HotelBookingPrice { get; set; }
        public Guid HotelBookingId { get; set; }
        public string HotelName { get; set; }

        public bool AutoBooking { get; set; }

        public AirSearch AirSearch { get; set; }
    }
}