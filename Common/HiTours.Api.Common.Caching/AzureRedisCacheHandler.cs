using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching
{
    public class AzureRedisCacheHandler : IAzureRedisCacheHandler, ICacheHandler
    {
        //private readonly AppSettings settings;
        private static string redisCacheConnectionStringName;
        //public AzureRedisCacheHandler(IOptions<AppSettings> mysettings)
        //{
        //    this.settings = mysettings.Value;
        //    redisCacheConnectionStringName = settings.RedisCacheConnectionStringName;
        //}

        public AzureRedisCacheHandler(string mysettings)
        {
            //this.settings = mysettings.Value;
            redisCacheConnectionStringName = mysettings;
        }


        private static readonly Lazy<ConnectionMultiplexer> LazyConnection =
            new Lazy<ConnectionMultiplexer>(
                () =>
                {
                    ThreadPool.SetMinThreads(300, 300);
                    return ConnectionMultiplexer.Connect(redisCacheConnectionStringName);
                },
                LazyThreadSafetyMode.PublicationOnly);



        private static ConnectionMultiplexer Connection => LazyConnection.Value;

        private static IDatabase Cache
        {
            get
            {
                return Connection.GetDatabase();
            }
        }

        public Task<T> GetAsync<T>(string key, Func<Task<T>> retrieveValues) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key) where T : class
        {
            var startTime = DateTimeOffset.UtcNow;
            var stopwatch = Stopwatch.StartNew();
            var retrievedValue = string.Empty;
            try
            {
                retrievedValue = Cache.StringGet(key);
            }
            catch (TimeoutException timeoutException)
            {

            }

            var result = !string.IsNullOrEmpty(retrievedValue) ? JsonConvert.DeserializeObject<T>(retrievedValue) : null;

            stopwatch.Stop();


            return result;
        }

        public decimal GetDecimal(string key)
        {
            var startTime = DateTimeOffset.UtcNow;
            var stopwatch = Stopwatch.StartNew();
            var retrievedValue = new RedisValue();
            try
            {
                retrievedValue = Cache.StringGet(key);
            }
            catch (TimeoutException timeoutException)
            {

            }

            if (retrievedValue.HasValue == false)
            {
                stopwatch.Stop();


                return 0;
            }

            var format = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var value = retrievedValue.ToString().Replace(".", ",");

            var result = decimal.Parse(value, NumberStyles.AllowDecimalPoint, format);

            stopwatch.Stop();

            return result;
        }

        public T Get<T>(string key, Func<T> retrieveValues, int cacheTimeInMinutes) where T : class
        {
            var startTime = DateTimeOffset.UtcNow;
            var stopwatch = Stopwatch.StartNew();
            var success = false;

            var retrievedValue = new RedisValue();
            try
            {
                retrievedValue = Cache.StringGet(key);
            }
            catch (TimeoutException timeoutException)
            {

            }

            if (!string.IsNullOrEmpty(retrievedValue))
            {
                return JsonConvert.DeserializeObject<T>(retrievedValue);
            }

            var result = retrieveValues.Invoke();
            if (result != null)
            {
                Cache.StringSet(key, JsonConvert.SerializeObject(result), new TimeSpan(0, cacheTimeInMinutes, 0));
                success = true;
            }

            stopwatch.Stop();


            return result;
        }

        public void Set(string key, object value, int cacheTimeInMinutes)
        {
            try
            {
                Cache.StringSet(key, JsonConvert.SerializeObject(value), new TimeSpan(0, cacheTimeInMinutes, 0));
            }
            catch (TimeoutException timeoutException)
            {

            }

            this.TrackSession(key);
        }

        public void Set(string key, int incrementvalue)
        {
            try
            {
                Cache.StringIncrement(key, incrementvalue);
            }
            catch (TimeoutException timeoutException)
            {

            }
        }

        public decimal GetCacheLeftOverTime(string key)
        {
            var remainingTime = Cache.KeyTimeToLive(key);

            var format = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var value = remainingTime?.TotalSeconds.ToString(CultureInfo.InvariantCulture).Replace(".", ",");

            return value == null ? 0 : decimal.Parse(value, NumberStyles.AllowDecimalPoint, format);
        }

        public void Delete(string key)
        {
            Cache.KeyDelete(key);
        }

        private void TrackSession(string key)
        {
            var keyArray = key.Contains(":") ? key.Split(":".ToCharArray()) : null;

            if (keyArray == null || keyArray.Length <= 1)
            {
                return;
            }

            var source = keyArray.FirstOrDefault();

            var count = GetCountOfSessions(source);

        }

        private static double GetCountOfSessions(string source)
        {
            var endpoints = LazyConnection.Value.GetEndPoints();
            var server = LazyConnection.Value.GetServer(endpoints.FirstOrDefault());
            var keys = server.Keys(pattern: source + ":*");
            return keys.Count();
        }
    }
}
