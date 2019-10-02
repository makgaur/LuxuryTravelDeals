using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Book
    {
        [Required]
        [Display(Name = "End User Ip")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }

        [Required]
        [Display(Name = "Trace Id")]
        public string TraceId { get; set; }

        [Required]
        [Display(Name = "Fare Quote Result Index")]
        public string ResultIndex { get; set; }

        public List<Passengers> Passenger { get; set; }
        public IList<Fare> Fares { get; set; }
        public IList<Meal> Meals { get; set; }
        public IList<Seat> Seats { get; set; }
    }
}