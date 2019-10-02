using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
   public class Sectors
    {
        [Required]
        [Display(Name = "Origin")]
        public string Origin { get; set; }
        [Required]
        [Display(Name = "Destination")]
        public string Destination { get; set; }
        [Required]
        [Display(Name = "Ticket Id")]
        public int TicketId { get; set; }
        [Required]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
}
