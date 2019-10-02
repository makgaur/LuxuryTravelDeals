using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching
{
    public interface ITableCacheHandler
    {
        /// <summary>
        /// Retrieves a task ro retrieve the item from cache if it exist there. Otherwise, it runs the supplied function and stores the result in cache. 
        /// This can be used for asynchronous retrieval of data
        /// </summary>
        /// <typeparam name="T">The object type to search for</typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="retrieveValues">The function to retrieve values from if nothing is found in cache</param>
        /// <param name="expiryInSeconds">The lifetime for the cached object before it needs to be refreshed</param>
        /// <returns>The task to perform the cache request operation</returns>
        Task<Response<T>> GetFromCacheAsync<T>(string key, Func<Task<Response<T>>> retrieveValues, int expiryInSeconds)
            where T : class;
    }
}
