using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
   public class ChangeRequestStatus
    {
        [Required]
        [Display(Name = "EndUser Ip")]
        public string EndUserIp { get; set; }
        [Required]
        [Display(Name = "Token Id")]
        public string TokenId { get; set; }
        [Required]
        [Display(Name = "Change Request Id")]
        public int ChangeRequestId { get; set; }
    }
}
