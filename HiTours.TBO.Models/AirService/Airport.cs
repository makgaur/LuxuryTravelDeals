using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public partial class Airport
    {
        [Display(Name = "AirportCode")]
        public string AirportCode { get; set; }

        [Display(Name = "AirportName")]
        public string AirportName { get; set; }

        [Display(Name = "CityCode")]
        public string CityCode { get; set; }

        [Display(Name = "CityName")]
        public string CityName { get; set; }

        [Display(Name = "CountryCode")]
        public string CountryCode { get; set; }

        [Display(Name = "CountryName")]
        public string CountryName { get; set; }

        [Display(Name = "Terminal")]
        public string Terminal { get; set; }
    }
}
