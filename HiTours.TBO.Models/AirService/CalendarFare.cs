using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class CalendarFare
    {
        [Required]
        [Display(Name = "IP Address of the end user")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name = "Journey Type")]
        public int JourneyType { get; set; }

        [Display(Name = "Preferred Airlines")]
        public string[] PreferredAirlines { get; set; }

        [Required]
        [Display(Name = "Segments")]
        public Segments[] Segments { get; set; }

        [Display(Name = "Sources")]
        public string[] Sources { get; set; }
    }
}