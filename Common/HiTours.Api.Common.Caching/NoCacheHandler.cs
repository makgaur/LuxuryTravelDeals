using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching
{
    /// <summary>
    /// Runs the retrievefunction without looking up anything in cache
    /// </summary>
    public class NoCacheHandler : ICacheHandler
    {
        /// <summary>
        /// Used when no caching is required or supplied. It will just run the supplied function
        /// This can be used for asynchronous retrieval of data
        /// </summary>
        /// <typeparam name="T">The object type to search for</typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="retrieveValues">The function to retrieve values from</param>
        /// <returns>The task to run the supplied function</returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> retrieveValues) where T : class
        {
            return await Task.Run(retrieveValues);
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            return await Task.FromResult<T>(null);
        }

        public T Get<T>(string key) where T : class
        {
            return null;
        }

        public T Get<T>(string key, Func<T> retrieveValues, int cacheTimeInMinutes) where T : class
        {
            return retrieveValues();
        }

        public void Set(string key, object value, int cacheTimeInMinutes)
        {
        }

        public void Delete(string key)
        {
        }
    }
}
