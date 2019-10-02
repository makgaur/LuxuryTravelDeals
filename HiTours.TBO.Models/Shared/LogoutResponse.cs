using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class LogoutResponse
    {
        public ApiError Error { get; set; }
        public int Status { get; set; }
    }
}
