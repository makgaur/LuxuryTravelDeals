using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class AirResultSegments
    {
        [Display(Name = "Accumulated Duration")]
        public long? AccumulatedDuration { get; set; }

        [Display(Name = "Airline")]
        public Airline Airline { get; set; }

        [Display(Name = "Baggage")]
        public string Baggage { get; set; }

        [Display(Name = "CabinBaggage")]
        public string CabinBaggage { get; set; }

        [Display(Name = "Craft")]
        public string Craft { get; set; }

        [Display(Name = "Destination")]
        public Destination Destination { get; set; }

        [Display(Name = "Duration")]
        public long Duration { get; set; }

        [Display(Name = "FlightStatus")]
        public string FlightStatus { get; set; }

        [Display(Name = "GroundTime")]
        public long GroundTime { get; set; }

        [Display(Name = "IsETicketEligible")]
        public bool IsETicketEligible { get; set; }

        [Display(Name = "Mile")]
        public long Mile { get; set; }

        [Display(Name = "NoOfSeatAvailable")]
        public long NoOfSeatAvailable { get; set; }

        [Display(Name = "Origin")]
        public Origin Origin { get; set; }

        [Display(Name = "Remark")]
        public object Remark { get; set; }

        [Display(Name = "SegmentIndicator")]
        public long SegmentIndicator { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "StopOver")]
        public bool StopOver { get; set; }

        [Display(Name = "StopPoint")]
        public string StopPoint { get; set; }

        [Display(Name = "StopPointArrivalTime")]
        public DateTime StopPointArrivalTime { get; set; }

        [Display(Name = "StopPointDepartureTime")]
        public DateTime StopPointDepartureTime { get; set; }

        [Display(Name = "TripIndicator")]
        public long TripIndicator { get; set; }

        public long ConnectingDuration { get; set; }
    }
}
