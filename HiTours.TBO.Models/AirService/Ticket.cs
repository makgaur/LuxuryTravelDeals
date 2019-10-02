using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Ticket
    {
        [Required]
        [Display(Name = "IP Address of the end user")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name = "Trace Id")]
        public string TraceId { get; set; }

        [Required]
        [Display(Name = "PNR")]
        public string PNR { get; set; }

        [Required]
        [Display(Name = "Booking Id")]
        public long BookingId { get; set; }

        public object PreferredCurrency { get; set; }

        [Required]
        [Display(Name = "Result Index")]
        public string ResultIndex { get; set; }

        public string AgentReferenceNo { get; set; }

        public List<Passengers> Passenger { get; set; }

        public bool LCCRequest { get; set; }
    }
}