using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Caching
{
    public class CacheEntity : TableEntity
    {
        public int ExpiryInSeconds { get; set; }
    }
}
