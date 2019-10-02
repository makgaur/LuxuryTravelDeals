using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Seat
    {
        public string AirlineCode { get; set; }

        public long AvailablityType { get; set; }

        [Display(Name = "Seat code")]
        public string Code { get; set; }

        public long Compartment { get; set; }

        public string CraftType { get; set; }

        public string Currency { get; set; }

        public long Deck { get; set; }

        [Display(Name = "Seat description")]
        public string Description { get; set; }

        public string Destination { get; set; }

        public string FlightNumber { get; set; }

        public string Origin { get; set; }

        public long Price { get; set; }

        public string RowNo { get; set; }

        public string SeatNo { get; set; }

        public long SeatType { get; set; }

        public long SeatWayType { get; set; }

        public string Text { get; set; }
    }
}