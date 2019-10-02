using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models.ViewModel
{
    public class TicketFareViewModel
    {
        public int PassengerType { get; set; }
        public long AdditionalTxnFee { get; set; }
        public long BaseFare { get; set; }
        public long Tax { get; set; }
        public long YQTax { get; set; }
        public string Currency { get; set; }
    }
}
