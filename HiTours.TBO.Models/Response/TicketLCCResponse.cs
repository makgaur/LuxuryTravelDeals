using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public partial class TicketLCCResponseRoot
    {
        [JsonProperty("Response")]
        public TicketLCCResponse Response { get; set; }
    }

    public partial class TicketLCCResponse
    {
        [JsonProperty("B2B2BStatus")]
        public bool B2B2BStatus { get; set; }

        [JsonProperty("Error")]
        public ApiError Error { get; set; }

        [JsonProperty("ResponseStatus")]
        public long ResponseStatus { get; set; }

        [JsonProperty("TraceId")]
        public string TraceId { get; set; }

        [JsonProperty("Response")]
        public TicketLCCResult Response { get; set; }
    }

    public partial class TicketLCCResult
    {
        [JsonProperty("PNR")]
        public string Pnr { get; set; }

        [JsonProperty("BookingId")]
        public long BookingId { get; set; }

        [JsonProperty("SSRDenied")]
        public bool SsrDenied { get; set; }

        [JsonProperty("SSRMessage")]
        public object SsrMessage { get; set; }

        [JsonProperty("Status")]
        public long Status { get; set; }

        [JsonProperty("IsPriceChanged")]
        public bool IsPriceChanged { get; set; }

        [JsonProperty("IsTimeChanged")]
        public bool IsTimeChanged { get; set; }

        [JsonProperty("FlightItinerary")]
        public FlightItinerary FlightItinerary { get; set; }

        [JsonProperty("TicketStatus")]
        public long TicketStatus { get; set; }
    }

    public partial class FlightItinerary
    {
        [JsonProperty("IssuancePcc")]
        public string IssuancePcc { get; set; }

        [JsonProperty("BookingId")]
        public long BookingId { get; set; }

        [JsonProperty("IsManual")]
        public bool IsManual { get; set; }

        [JsonProperty("PNR")]
        public string Pnr { get; set; }

        [JsonProperty("IsDomestic")]
        public bool IsDomestic { get; set; }

        [JsonProperty("Source")]
        public long Source { get; set; }

        [JsonProperty("Origin")]
        public string Origin { get; set; }

        [JsonProperty("Destination")]
        public string Destination { get; set; }

        [JsonProperty("AirlineCode")]
        public string AirlineCode { get; set; }

        [JsonProperty("LastTicketDate")]
        public string LastTicketDate { get; set; }

        [JsonProperty("ValidatingAirlineCode")]
        public string ValidatingAirlineCode { get; set; }

        [JsonProperty("AirlineRemark")]
        public string AirlineRemark { get; set; }

        [JsonProperty("IsLCC")]
        public bool IsLcc { get; set; }

        [JsonProperty("NonRefundable")]
        public bool NonRefundable { get; set; }

        [JsonProperty("FareType")]
        public string FareType { get; set; }

        [JsonProperty("CreditNoteNo")]
        public object CreditNoteNo { get; set; }

        [JsonProperty("Fare")]
        public FareResult Fare { get; set; }

        [JsonProperty("CreditNoteCreatedOn")]
        public object CreditNoteCreatedOn { get; set; }

        [JsonProperty("Passenger")]
        public Passenger[] Passenger { get; set; }

        [JsonProperty("CancellationCharges")]
        public object CancellationCharges { get; set; }

        [JsonProperty("Segments")]
        public SegmentResult[] Segments { get; set; }

        [JsonProperty("FareRules")]
        public FareRule[] FareRules { get; set; }

        [JsonProperty("Status")]
        public long Status { get; set; }

        [JsonProperty("InvoiceAmount")]
        public long InvoiceAmount { get; set; }

        [JsonProperty("InvoiceNo")]
        public string InvoiceNo { get; set; }

        [JsonProperty("InvoiceStatus")]
        public long InvoiceStatus { get; set; }

        [JsonProperty("InvoiceCreatedOn")]
        public string InvoiceCreatedOn { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }
    }

    public partial class SegmentResult
    {
        [JsonProperty("Baggage")]
        public object Baggage { get; set; }

        [JsonProperty("CabinBaggage")]
        public object CabinBaggage { get; set; }

        [JsonProperty("TripIndicator")]
        public long TripIndicator { get; set; }

        [JsonProperty("SegmentIndicator")]
        public long SegmentIndicator { get; set; }

        [JsonProperty("Airline")]
        public Airline Airline { get; set; }

        [JsonProperty("AirlinePNR")]
        public string AirlinePnr { get; set; }

        [JsonProperty("Origin")]
        public Origin Origin { get; set; }

        [JsonProperty("Destination")]
        public Destination Destination { get; set; }

        [JsonProperty("Duration")]
        public long Duration { get; set; }

        [JsonProperty("GroundTime")]
        public long GroundTime { get; set; }

        [JsonProperty("Mile")]
        public long Mile { get; set; }

        [JsonProperty("StopOver")]
        public bool StopOver { get; set; }

        [JsonProperty("StopPoint")]
        public string StopPoint { get; set; }

        [JsonProperty("StopPointArrivalTime")]
        public string StopPointArrivalTime { get; set; }

        [JsonProperty("StopPointDepartureTime")]
        public string StopPointDepartureTime { get; set; }

        [JsonProperty("Craft")]
        public string Craft { get; set; }

        [JsonProperty("Remark")]
        public object Remark { get; set; }

        [JsonProperty("IsETicketEligible")]
        public bool IsETicketEligible { get; set; }

        [JsonProperty("FlightStatus")]
        public string FlightStatus { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }

    public partial class Passenger
    {
        [JsonProperty("PaxId")]
        public long PaxId { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("PaxType")]
        public long PaxType { get; set; }

        [JsonProperty("DateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("Gender")]
        public long Gender { get; set; }

        [JsonProperty("PassportNo")]
        public string PassportNo { get; set; }

        [JsonProperty("AddressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("AddressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("Fare")]
        public Fare Fare { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("CountryName")]
        public string CountryName { get; set; }

        [JsonProperty("Nationality")]
        public string Nationality { get; set; }

        [JsonProperty("ContactNo")]
        public string ContactNo { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("IsLeadPax")]
        public bool IsLeadPax { get; set; }

        [JsonProperty("FFAirlineCode")]
        public string FfAirlineCode { get; set; }

        [JsonProperty("FFNumber")]
        public string FfNumber { get; set; }

        [JsonProperty("Ticket")]
        public TicketResult Ticket { get; set; }

        [JsonProperty("GSTCompanyAddress")]
        public string GstCompanyAddress { get; set; }

        [JsonProperty("GSTCompanyContactNumber")]
        public string GstCompanyContactNumber { get; set; }

        [JsonProperty("GSTCompanyEmail")]
        public string GstCompanyEmail { get; set; }

        [JsonProperty("GSTCompanyName")]
        public string GstCompanyName { get; set; }

        [JsonProperty("GSTNumber")]
        public string GstNumber { get; set; }

        [JsonProperty("SegmentAdditionalInfo")]
        public SegmentAdditionalInfo[] SegmentAdditionalInfo { get; set; }
    }

    public partial class TicketResult
    {
        [JsonProperty("TicketId")]
        public long TicketId { get; set; }

        [JsonProperty("TicketNumber")]
        public string TicketNumber { get; set; }

        [JsonProperty("IssueDate")]
        public string IssueDate { get; set; }

        [JsonProperty("ValidatingAirline")]
        public string ValidatingAirline { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("ServiceFeeDisplayType")]
        public string ServiceFeeDisplayType { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }

    public partial class SegmentAdditionalInfo
    {
        [JsonProperty("FareBasis")]
        public string FareBasis { get; set; }

        [JsonProperty("NVA")]
        public object Nva { get; set; }

        [JsonProperty("NVB")]
        public object Nvb { get; set; }

        [JsonProperty("Baggage")]
        public string Baggage { get; set; }

        [JsonProperty("Meal")]
        public string Meal { get; set; }

        [JsonProperty("Seat")]
        public string Seat { get; set; }

        [JsonProperty("SpecialService")]
        public string SpecialService { get; set; }
    }

    public partial class FareRule
    {
        [JsonProperty("Origin")]
        public string Origin { get; set; }

        [JsonProperty("Destination")]
        public string Destination { get; set; }

        [JsonProperty("Airline")]
        public string Airline { get; set; }

        [JsonProperty("FareBasisCode")]
        public string FareBasisCode { get; set; }

        [JsonProperty("FareRuleDetail")]
        public string FareRuleDetail { get; set; }

        [JsonProperty("FareRestriction")]
        public object FareRestriction { get; set; }
    }

    public partial class FareResult
    {
        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("BaseFare")]
        public long BaseFare { get; set; }

        [JsonProperty("Tax")]
        public long Tax { get; set; }

        [JsonProperty("TaxBreakup")]
        public TaxBreakup[] TaxBreakup { get; set; }

        [JsonProperty("YQTax")]
        public long YqTax { get; set; }

        [JsonProperty("AdditionalTxnFeeOfrd")]
        public long AdditionalTxnFeeOfrd { get; set; }

        [JsonProperty("AdditionalTxnFeePub")]
        public long AdditionalTxnFeePub { get; set; }

        [JsonProperty("PGCharge")]
        public long PgCharge { get; set; }

        [JsonProperty("OtherCharges")]
        public long OtherCharges { get; set; }

        [JsonProperty("ChargeBU")]
        public TaxBreakup[] ChargeBu { get; set; }

        [JsonProperty("Discount")]
        public long Discount { get; set; }

        [JsonProperty("PublishedFare")]
        public long PublishedFare { get; set; }

        [JsonProperty("CommissionEarned")]
        public double CommissionEarned { get; set; }

        [JsonProperty("PLBEarned")]
        public double PlbEarned { get; set; }

        [JsonProperty("IncentiveEarned")]
        public double IncentiveEarned { get; set; }

        [JsonProperty("OfferedFare")]
        public double OfferedFare { get; set; }

        [JsonProperty("TdsOnCommission")]
        public double TdsOnCommission { get; set; }

        [JsonProperty("TdsOnPLB")]
        public double TdsOnPlb { get; set; }

        [JsonProperty("TdsOnIncentive")]
        public double TdsOnIncentive { get; set; }

        [JsonProperty("ServiceFee")]
        public long ServiceFee { get; set; }

        [JsonProperty("TotalBaggageCharges")]
        public long TotalBaggageCharges { get; set; }

        [JsonProperty("TotalMealCharges")]
        public long TotalMealCharges { get; set; }

        [JsonProperty("TotalSeatCharges")]
        public long TotalSeatCharges { get; set; }

        [JsonProperty("TotalSpecialServiceCharges")]
        public long TotalSpecialServiceCharges { get; set; }
    }

    public partial class TaxBreakup
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }
}