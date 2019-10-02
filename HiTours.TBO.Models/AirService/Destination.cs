using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Destination
    {
        [Display(Name = "Airport")]
        public Airport Airport { get; set; }

        [Display(Name = "ArrTime")]
        public string ArrTime { get; set; }
    }
}
