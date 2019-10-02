using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models.ViewModel
{
    public class TicketViewModel
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public string ResultIndex { get; set; }
        public List<TicketPassengerViewModel> Passengers { get; set; }
    }
}
