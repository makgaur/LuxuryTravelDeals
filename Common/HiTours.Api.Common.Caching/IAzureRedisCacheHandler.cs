using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Caching
{
    /// <summary>
    /// Provides functionality for storing and retrieving cached data using azure redis cache store.
    /// </summary>
    public interface IAzureRedisCacheHandler
    {
        /// <summary>
        /// Get decimal value from cache store
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <returns>The data requested</returns>
        decimal GetDecimal(string key);

        /// <summary>
        /// Get object from cache store
        /// </summary>
        /// <typeparam name="T">The object type to search for</typeparam>
        /// <param name="key">The cache key</param>
        /// <returns>The data requested</returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// Get object from cache store
        /// </summary>
        /// <typeparam name="T">The object type to search for</typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="retrieveValues">The function to retrieve values if nothing is found in cache</param>
        /// <param name="cacheTimeInMinutes">The cache time in minutes</param>
        /// <returns>The data requested</returns>
        T Get<T>(string key, Func<T> retrieveValues, int cacheTimeInMinutes) where T : class;

        /// <summary>
        /// Set the object into cache store
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="value">The object to be put on cache</param>
        /// <param name="cacheTimeInMinutes">The cache time in minutes</param>
        void Set(string key, object value, int cacheTimeInMinutes);

        /// <summary>
        /// Delete the cache explicitly by a key
        /// </summary>
        /// <param name="key">The cache key</param>
        void Delete(string key);

        /// <summary>
        /// Sets and increments cache value.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="incrementvalue">The value to be incremented by</param>
        void Set(string key, int incrementvalue);

        /// <summary>
        /// Get remaining time of a cache key
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <returns>The remaining time in minutes to expire the cached item</returns>
        decimal GetCacheLeftOverTime(string key);
    }
}
