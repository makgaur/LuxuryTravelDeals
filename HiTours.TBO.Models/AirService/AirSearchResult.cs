using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public partial class AirSearchResult
    {
        [Display(Name = "Adult Count")]
        public int AdultCount { get; set; }

        [Display(Name = "Child Count")]
        public int ChildCount { get; set; }

        [Display(Name = "Infant Count")]
        public int InfantCount { get; set; }

        public Segments[] Segment { get; set; }

        [Display(Name = "Airline Code")]
        public string AirlineCode { get; set; }

        [Display(Name = "Airline Remark")]
        public string AirlineRemark { get; set; }

        [Display(Name = "Baggage Allowance")]
        public string BaggageAllowance { get; set; }

        [Display(Name = "Fare")]
        public Fare Fare { get; set; }

        [Display(Name = "FareBreakdown")]
        public FareBreakdown[] FareBreakdown { get; set; }

        [Display(Name = "FareRules")]
        public FareRule[] FareRules { get; set; }

        [Display(Name = "IsGSTMandatory")]
        public bool IsGSTMandatory { get; set; }

        [Display(Name = "GSTAllowed")]
        public bool GSTAllowed { get; set; }

        [Display(Name = "IsLCC")]
        public bool IsLCC { get; set; }

        [Display(Name = "IsRefundable")]
        public bool IsRefundable { get; set; }

        [Display(Name = "LastTicketDate")]
        public string LastTicketDate { get; set; }

        [Display(Name = "ResultIndex")]
        public string ResultIndex { get; set; }

        [Display(Name = "Segments")]
        public AirResultSegments[][] Segments { get; set; }

        [Display(Name = "Segments")]
        [JsonIgnore]
        public AirResultSegments[] CustomSegments { get; set; }

        [Display(Name = "Source")]
        public long Source { get; set; }

        [Display(Name = "TicketAdvisory")]
        public string TicketAdvisory { get; set; }

        [Display(Name = "ValidatingAirline")]
        public string ValidatingAirline { get; set; }

        public long TotalPassengers { get; set; }
        public long TotalBaseFareAmount { get; set; }
        public long TotalBaseFareTaxAmount { get; set; }
        public long TotalAmount { get; set; }
        public int JourneyType { get; set; }

        public int FlightCabinClass { get; set; }
    }
}