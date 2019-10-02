using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching
{
    /// <summary>
    /// Provides functionality for storing and retrieving cached data.
    /// </summary>
    public interface ICacheHandler
    {
        /// <summary>
        /// Retrieves a task ro retrieve the item from cache if it exist there. Otherwise, it runs the supplied function and stores the result in cache. 
        /// This can be used for asynchronous retrieval of data
        /// </summary>
        /// <typeparam name="T">The object type to search for</typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="retrieveValues">The function to retrieve values from if nothing is found in cache</param>
        /// <returns>The task to perform the cache request operation</returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> retrieveValues) where T : class;

        /// <summary>
        /// Get object from cache store
        /// </summary>
        /// <typeparam name="T">The object type to search for</typeparam>
        /// <param name="key">The cache key</param>
        /// <returns>The data requested</returns>
        Task<T> GetAsync<T>(string key) where T : class;

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
    }
}
