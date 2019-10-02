using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class BookingDetails
    {
        [Required]
        [Display(Name = "End User Ip")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name = "Trace Id")]
        public string TraceId { get; set; }

        [Required]
        [Display(Name = "Booking Id")]
        public int BookingId { get; set; }

        [Display(Name = "PNR")]
        public string PNR { get; set; }
    }
}
