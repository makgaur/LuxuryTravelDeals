using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Logout
    {
        [Required]
        [Display(Name = "Client ID")]
        public string ClientId { get; set; }

        [Required]
        [Display(Name = "End User Ip Address")]
        public string EndUserIp { get; set; }

        [Required]
        [Display(Name = "Agency ID")]
        public int TokenAgencyId { get; set; }

        [Required]
        [Display(Name = "Member ID")]
        public int TokenMemberId { get; set; }

        [Required]
        [Display(Name = "Token ID")]
        public string TokenId { get; set; }
    }
}