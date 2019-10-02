using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HiTours.TBO.Models
{
    public class CancelPnr
    {
        [Required]
        [Display(Name ="EndUser Ip")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name ="Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name ="Booking Id")]
        public string BookingId { get; set; }


        [Required]
        [Display(Name ="Source")]
        public string Source { get; set; }

    }
}
