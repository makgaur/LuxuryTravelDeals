using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching
{
    /// <summary>
    /// Caches data into azure table
    /// </summary>
    public class AzureTableCacheHandler : ITableCacheHandler
    {
        private readonly ICacheRepository cacheRepository;

        public AzureTableCacheHandler(ICacheRepository cacheRepository)
        {
            this.cacheRepository = cacheRepository;
        }

        /// <summary>
        /// Retrieves a task ro retrieve the item from cache if it exist there. Otherwise, it runs the supplied function and stores the result in cache. 
        /// This can be used for asynchronous retrieval of data
        /// </summary>
        /// <typeparam name="T">The object type to search for</typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="retrieveValues">The function to retrieve values from if nothing is found in cache</param>
        /// <param name="expiryInSeconds">The lifetime for the cached object before it needs to be refreshed</param>
        /// <returns>The task to perform the cache request operation</returns>
        public async Task<Response<T>> GetFromCacheAsync<T>(
            string key,
            Func<Task<Response<T>>> retrieveValues,
            int expiryInSeconds) where T : class
        {
            var check_reload = key.Split('#');
            var cacheType = typeof(T).ToString();
            var retrievedValue = await this.cacheRepository.Get(cacheType, key);
            var reload = false;
            if (check_reload.Length > 1 && check_reload[1] == "reload")
            {
                reload = true;
                key = check_reload[0];
            }
            if (string.IsNullOrEmpty(retrievedValue) || reload)
            {
                var result = await retrieveValues.Invoke();
                if (result.Successful)
                {
                    var serialized = JsonConvert.SerializeObject(result);
                    await this.cacheRepository.Create(cacheType, key, serialized, expiryInSeconds);
                }

                return result;
            }

            return new Response<T>() { Result = JsonConvert.DeserializeObject<Response<T>>(retrievedValue).Result };
        }
    }
}
