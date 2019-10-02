using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class MealDynamic
    {
        [Required]
        [Display(Name = "Way type")]
        public WayType WayType { get; set; }
        [Required]
        [Display(Name = "Baggage code")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Description")]
        public Description Description { get; set; }
        [Required]
        [Display(Name = "Meal description")]
        public string AirlineDescription { get; set; }
        [Required]
        [Display(Name = "Meal quantity")]
        public int Quantity { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        [Display(Name = "Meal Price")]
        public double Price { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        public string BaseCurrency { get; set; }
        public double BaseCurrencyPrice { get; set; }
    }
}
