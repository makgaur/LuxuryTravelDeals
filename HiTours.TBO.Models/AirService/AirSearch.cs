using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class AirSearch
    {
        [Required]
        [Display(Name = "End User Ip Address")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name = "Adult Count")]
        public int AdultCount { get; set; }

        [Required]
        [Display(Name = "Child Count")]
        public int ChildCount { get; set; }

        [Required]
        [Display(Name = "Infant Count")]
        public int InfantCount { get; set; }

        [Display(Name = "Direct Flight")]
        public bool DirectFlight { get; set; }

        [Display(Name = "One Stop Flight")]
        public bool OneStopFlight { get; set; }

        [Required]
        [Display(Name = "Journey Type")]
        public int JourneyType { get; set; }

        [Display(Name = "Preferred Airlines")]
        public string[] PreferredAirlines { get; set; }

        [Display(Name = "Segments")]
        public Segments[] Segments { get; set; }

        [Display(Name = "Airline Sources")]
        public string[] Sources { get; set; }

        [JsonIgnore]
        public DateTime? DepartureDateTime { get; set; }

        [JsonIgnore]
        public DateTime? ReturnDateTime { get; set; }

        [JsonIgnore]
        public int FlightCabinClass { get; set; }

        public bool ReturnFlight { get; set; }

        ////public Guid HotelBookingId { get; set; }
    }
}