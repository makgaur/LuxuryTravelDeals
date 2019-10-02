using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
  public class SendChange
    {
        [Required]
        [Display(Name = "EndUser Ip")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name = "Booking Id")]
        public string BookingId { get; set; }

        [Required]
        [Display(Name = "Request Type")]
        public int RequestType { get; set; }

        [Required]
        [Display(Name = "Cancellation Type")]
        public int CancellationType { get; set; }

        public Sectors Sector { get; set;}
    }
}
