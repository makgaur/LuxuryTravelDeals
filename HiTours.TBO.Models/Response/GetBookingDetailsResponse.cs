using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models.Response
{
    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class TaxBreakup
    {
        public string key { get; set; }
        public int value { get; set; }
    }

    public class ChargeBU
    {
        public string key { get; set; }
        public double value { get; set; }
    }

    public class Fare
    {
        public string Currency { get; set; }
        public int BaseFare { get; set; }
        public int Tax { get; set; }
        public List<TaxBreakup> TaxBreakup { get; set; }
        public int YQTax { get; set; }
        public int AdditionalTxnFeeOfrd { get; set; }
        public int AdditionalTxnFeePub { get; set; }
        public int PGCharge { get; set; }
        public double OtherCharges { get; set; }
        public List<ChargeBU> ChargeBU { get; set; }
        public int Discount { get; set; }
        public double PublishedFare { get; set; }
        public int CommissionEarned { get; set; }
        public int PLBEarned { get; set; }
        public int IncentiveEarned { get; set; }
        public double OfferedFare { get; set; }
        public int TdsOnCommission { get; set; }
        public int TdsOnPLB { get; set; }
        public int TdsOnIncentive { get; set; }
        public int ServiceFee { get; set; }
        public int TotalBaggageCharges { get; set; }
        public int TotalMealCharges { get; set; }
        public int TotalSeatCharges { get; set; }
        public int TotalSpecialServiceCharges { get; set; }
    }
    public class Ssr
    {
        public string Detail { get; set; }
        public string SsrCode { get; set; }
        public object SsrStatus { get; set; }
        public int Status { get; set; }
    }

    public class Ticket
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string ValidatingAirline { get; set; }
        public string Remarks { get; set; }
        public string ServiceFeeDisplayType { get; set; }
        public string Status { get; set; }
        public string ConjunctionNumber { get; set; }
        public string TicketType { get; set; }
    }

    public class SegmentAdditionalInfo
    {
        public string FareBasis { get; set; }
        public string NVA { get; set; }
        public string NVB { get; set; }
        public string Baggage { get; set; }
        public string Meal { get; set; }
        public string Seat { get; set; }
        public string SpecialService { get; set; }
    }

    public class Passenger
    {
        public int PaxId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaxType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string PassportNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public Fare Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public object FFAirlineCode { get; set; }
        public object FFNumber { get; set; }
        public List<Ssr> Ssr { get; set; }
        public Ticket Ticket { get; set; }
        public List<SegmentAdditionalInfo> SegmentAdditionalInfo { get; set; }
    }

    public class Airline
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string OperatingCarrier { get; set; }
    }

    public class Airport
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string Terminal { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class Origin
    {
        public Airport Airport { get; set; }
        public DateTime DepTime { get; set; }
    }

    public class Destination
    {
        public Airport Airport { get; set; }
        public DateTime ArrTime { get; set; }
    }

    public class Segment
    {
        public object Baggage { get; set; }
        public object CabinBaggage { get; set; }
        public int TripIndicator { get; set; }
        public int SegmentIndicator { get; set; }
        public Airline Airline { get; set; }
        public string AirlinePNR { get; set; }
        public Origin Origin { get; set; }
        public Destination Destination { get; set; }
        public int Duration { get; set; }
        public int GroundTime { get; set; }
        public int Mile { get; set; }
        public bool StopOver { get; set; }
        public string FlightInfoIndex { get; set; }
        public string StopPoint { get; set; }
        public object StopPointArrivalTime { get; set; }
        public object StopPointDepartureTime { get; set; }
        public string Craft { get; set; }
        public object Remark { get; set; }
        public bool IsETicketEligible { get; set; }
        public string FlightStatus { get; set; }
        public string Status { get; set; }
        public int? AccumulatedDuration { get; set; }
    }

    public class FareRule
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Airline { get; set; }
        public string FareBasisCode { get; set; }
        public string FareRuleDetail { get; set; }
        public string FareRestriction { get; set; }
        public string FareFamilyCode { get; set; }
        public string FareRuleIndex { get; set; }
    }

    public class FlightItinerary
    {
        public string IssuancePcc { get; set; }
        public int TripIndicator { get; set; }
        public bool BookingAllowedForRoamer { get; set; }
        public int BookingId { get; set; }
        public bool IsCouponAppilcable { get; set; }
        public bool IsManual { get; set; }
        public string PNR { get; set; }
        public bool IsDomestic { get; set; }
        public int Source { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string AirlineCode { get; set; }
        public string ValidatingAirlineCode { get; set; }
        public string AirlineRemark { get; set; }
        public bool IsLCC { get; set; }
        public bool NonRefundable { get; set; }
        public string FareType { get; set; }
        public object CreditNoteNo { get; set; }
        public Fare Fare { get; set; }
        public object CreditNoteCreatedOn { get; set; }
        public List<Passenger> Passenger { get; set; }
        public object CancellationCharges { get; set; }
        public List<Segment> Segments { get; set; }
        public List<FareRule> FareRules { get; set; }
        public int Status { get; set; }
        public int InvoiceAmount { get; set; }
        public string InvoiceNo { get; set; }
        public int InvoiceStatus { get; set; }
        public DateTime InvoiceCreatedOn { get; set; }
        public string Remarks { get; set; }
    }

    public class Response
    {
        public Error Error { get; set; }
        public int ResponseStatus { get; set; }
        public string TraceId { get; set; }
        public FlightItinerary FlightItinerary { get; set; }
    }

    public class GetBookingDetailsResponse
    {
        public Response Response { get; set; }
    }
}
