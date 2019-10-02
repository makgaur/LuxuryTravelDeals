using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class AgencyBalance
    {
        [Required]
        [Display(Name ="Client ID")]
        public string ClientId { get; set; }

        [Required]
        [Display(Name ="Agency ID")]
        public string TokenAgencyId { get; set; }

        [Required]
        [Display(Name ="Member ID")]
        public string TokenMemberId { get; set; }

        [Required]
        [Display(Name ="End User ID")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name ="Token ID")]
        public string TokenId { get; set; }
    }
}