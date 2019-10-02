using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Exception { get; set; }
    }
}