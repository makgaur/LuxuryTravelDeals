using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.MongoDb
{
    public class MongoDbAuthentication
    {
        public string HostName { get; set; }

        public int PortNumber { get; set; }


        public string UserName { get; set; }


        public string Password { get; set; }

        public string DatabaseName { get; set; }

    }
}
