using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class FlightBookResult
    {
        public ApiError Error { get; set; }

        public bool Status { get; set; }

        public BookingResult Booking { get; set; }

        public List<BookingResult> Bookings { get; set; }

        public string Json { get; set; }
    }
}