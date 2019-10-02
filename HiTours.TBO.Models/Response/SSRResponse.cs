using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class SSRNonLCCResponse
    {
        public ApiError Error { get; set; }

        public Meal[] Meal { get; set; }

        public long ResponseStatus { get; set; }

        public Meal[] SeatPreference { get; set; }

        public string TraceId { get; set; }
    }
    public class SSRNonLCCResponseRoot
    {
        public SSRNonLCCResponse Response { get; set; }
    }


}