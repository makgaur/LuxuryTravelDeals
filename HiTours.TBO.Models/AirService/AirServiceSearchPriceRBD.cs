using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class AirServiceSearchPriceRBD
    {
        [Required]
        [Display(Name = "End User Ip Address")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name = "Trace Id")]
        public string TraceId { get; set; }

        [Required]
        [Display(Name = "Adult Count")]
        public int AdultCount { get; set; }

        [Required]
        [Display(Name = "Child Count")]
        public int ChildCount { get; set; }

        [Required]
        [Display(Name = "Infant Count")]
        public int InfantCount { get; set; }

        public AirSearchResult[] AirSearchResult { get; set; }
    }
}