using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Caching.Repository
{
    public class EntityFactory
    {
        internal static CacheEntity CreateCacheEntity(string cacheType, string cacheKey, int expiryInSeconds)
        {
            return new CacheEntity
            {
                PartitionKey = cacheType,
                RowKey = cacheKey,
                ExpiryInSeconds = expiryInSeconds
            };
        }
    }
}
