using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Meal
    {
        [Display(Name = "Meal code")]
        public string Code { get; set; }

        [Display(Name = "Meal description")]
        public string Description { get; set; }
    }
}
