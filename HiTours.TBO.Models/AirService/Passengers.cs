using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HiTours.TBO.Models
{
    public class Passengers
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Pax Type")]
        public string PaxType { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "GST Company Address")]
        public string GSTCompanyAddress { get; set; }

        [Display(Name = "GST Company Contact Number")]
        public string GSTCompanyContactNumber { get; set; }

        [Display(Name = "GST Company Name")]
        public string GSTCompanyName { get; set; }

        [Display(Name = "GST Number")]
        public string GSTNumber { get; set; }

        [Required]
        public FareBreakdown Fare { get; set; }

        [Display(Name = "FareBreakdown")]
        public FareBreakdown[] FareBreakdown { get; set; }

        [Required]
        public List<Baggage> Baggage { get; set; }

        [Required]
        [Display(Name = "Meal Dynamic")]
        public List<MealDynamic> MealDynamic { get; set; }

        [Display(Name = "Seat Dynamic")]
        public List<object> SeatDynamic { get; set; }

        [Display(Name = "GST Company Email")]
        [EmailAddress]
        public string GSTCompanyEmail { get; set; }

        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }

        [Display(Name = "Passport Expiry")]
        public DateTime? PassportExpiry { get; set; }

        [Required]
        [Display(Name = "AddressLine1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        [Required]
        public string City { get; set; }

        [Display(Name = "Country Code")]
        [Required]
        public string CountryCode { get; set; }

        [Display(Name = "Country Name")]
        [Required]
        public string CountryName { get; set; }

        [Display(Name = "ContactNo")]
        [Required]
        public string ContactNo { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Is Lead Pax")]
        [Required]
        public bool IsLeadPax { get; set; }

        [Display(Name = "FF Airline")]
        public string FFAirline { get; set; }

        [Display(Name = "FF Number")]
        public string FFNumber { get; set; }

        [Display(Name = "FF Airline Code")]
        public string FFAirlineCode { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        [JsonIgnore]
        public IEnumerable<SelectListItem> Countries { get; set; }

        [JsonIgnore]
        public IEnumerable<SelectListItem> Cities { get; set; }

        public bool IsLcc { get; set; }

        public bool InterNationalReturnFlight { get; set; }
    }
}