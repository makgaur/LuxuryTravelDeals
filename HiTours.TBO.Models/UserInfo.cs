using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int? CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNo { get; set; }
        public int? CountryofIssueId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string DetailType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
