using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching
{
    /// <summary>
    /// The repository used for caching purposes
    /// </summary>
    public interface ICacheRepository
    {
        /// <summary>
        /// Creates the cache item
        /// </summary>
        /// <param name="cacheType">The type of cache</param>
        /// <param name="cacheKey">The key to identify the cached item</param>
        /// <param name="cacheObject">The serialized object to cache</param>
        /// <param name="expiryInSeconds">The expiry time of the cache</param>
        /// <returns>The result whether the cache was successful</returns>
        Task<Response> Create(string cacheType, string cacheKey, string cacheObject, int expiryInSeconds);

        /// <summary>
        /// Gets a cached item
        /// </summary>
        /// <param name="cacheType">The type of cache item to retrieve</param>
        /// <param name="cacheKey">The cache key to retrieve</param>
        /// <returns>the cached object</returns>
        Task<string> Get(string cacheType, string cacheKey);
    }




}
