using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models.SSRResponse
{
    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Seat
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string CraftType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int AvailablityType { get; set; }
        public int Description { get; set; }
        public string Code { get; set; }
        public string RowNo { get; set; }
        public string SeatNo { get; set; }
        public int SeatType { get; set; }
        public int SeatWayType { get; set; }
        public int Compartment { get; set; }
        public int Deck { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
    }

    public class RowSeat
    {
        public List<Seat> Seats { get; set; }
    }

    public class SegmentSeat
    {
        public List<RowSeat> RowSeats { get; set; }
    }

    public class SeatDynamic
    {
        public List<SegmentSeat> SegmentSeat { get; set; }
    }

    public class SSRService
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string Code { get; set; }
        public int ServiceType { get; set; }
        public string Text { get; set; }
        public int WayType { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
    }

    public class SegmentSpecialService
    {
        public List<SSRService> SSRService { get; set; }
    }

    public class SpecialService
    {
        public List<SegmentSpecialService> SegmentSpecialService { get; set; }
    }

    public class Response
    {
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public string TraceId { get; set; }
        public List<List<Baggage>> Baggage { get; set; }
        public List<List<MealDynamic>> MealDynamic { get; set; }
        public List<SeatDynamic> SeatDynamic { get; set; }
        public List<SpecialService> SpecialServices { get; set; }
    }

    public class SSRLCCResponse
    {
        public Response Response { get; set; }
    }

    public class Baggage
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public int Weight { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
    public class MealDynamic
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public string AirlineDescription { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
