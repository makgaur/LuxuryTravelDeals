using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class AuthenticateResponse
    {
        public int Status { get; set; }
        public string TokenId { get; set; }
        public ApiError Error { get; set; }
        public Member Member { get; set; }
    }
}
