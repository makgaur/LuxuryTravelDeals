using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Baggage
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
        [Display(Name = "Baggage weight")]
        public int Weight { get; set; }
        [Required]
        [Display(Name = "Base Currency Price")]
        public double BaseCurrencyPrice { get; set; }
        [Required]
        [Display(Name = "Base Currency")]
        public string BaseCurrency { get; set; }
        [Required]
        [Display(Name = "Currency")]
        public string Currency { get; set; }
        [Required]
        [Display(Name = "Currency")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Origin")]
        public string Origin { get; set; }
        [Required]
        [Display(Name = "Destination")]
        public string Destination { get; set; }
    }
}
