using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Airline
    {
        [Display(Name = "Airline Code")]
        public string AirlineCode { get; set; }

        [Display(Name = "Airline Name")]
        public string AirlineName { get; set; }

        [Display(Name = "Fare Class")]
        public string FareClass { get; set; }

        [Display(Name = "Flight Number")]
        public string FlightNumber { get; set; }

        [Display(Name = "Operating Carrier")]
        public string OperatingCarrier { get; set; }
    }
}
