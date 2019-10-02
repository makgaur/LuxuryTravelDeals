using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.Azure
{
    public class QueueInputModel
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public bool IsCompleted { get; set; }

        public string Reason { get; set; }

        public string Type { get; set; }
    }
}
