using HiTours.Api.Common.Data.Azure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching.Repository
{
    public class CacheRepository : ICacheRepository
    {
        private readonly ITableHandler<CacheEntity> cacheEntityHandler;
        private readonly IBlobHandler blobHandler;

        public CacheRepository(ITableHandler<CacheEntity> cacheEntityHandler, IBlobHandler blobHandler)
        {
            this.cacheEntityHandler = cacheEntityHandler;
            this.blobHandler = blobHandler;
        }

        public async Task<Response> Create(string cacheType, string cacheKey, string cacheObject, int expiryInSeconds)
        {
            if (string.IsNullOrEmpty(cacheType))
            {
                return Response.Failed("Cache type is mission");
            }

            if (string.IsNullOrEmpty(cacheKey))
            {
                return Response.Failed("Cache key is missing");
            }

            var cacheEntity = EntityFactory.CreateCacheEntity(cacheType, cacheKey, expiryInSeconds);

            var result = await this.cacheEntityHandler.InsertOrUpdateAsync(cacheEntity);

            await this.blobHandler.WriteToBlob(cacheKey, cacheObject);

            return result;
        }

        public async Task<string> Get(string cacheType, string cacheKey)
        {
            var cacheEntity = await this.cacheEntityHandler.GetItemAsync(cacheType, cacheKey);

            if (cacheEntity == null || DateTime.UtcNow > cacheEntity.Timestamp.AddSeconds(cacheEntity.ExpiryInSeconds))
            {
                return string.Empty;
            }

            var result = await this.blobHandler.ReadFromBlob(cacheKey);

            return result;
        }
    }
}
