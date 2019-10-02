using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models.ViewModel
{
    public class TicketPassengerViewModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PaxType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportExpiry { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Nationality { get; set; }
        public string CountryName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public string FFAirline { get; set; }
        public string FFNumber { get; set; }
        public TicketFareViewModel Fare { get; set; }
        public HiTours.TBO.Models.SSRResponse.Baggage Baggage { get; set; }
        public MealDynamic MealDynamic { get; set; }
        public Meal Meal { get; set; }
        public Seat Seat { get; set; }
    }
}
