using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class UserCredential
    {
        [Required]
        [Display(Name ="Client ID")]
        public string ClientId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "End User Ip Address")]
        public string EndUserIp { get; set; }
    }
}