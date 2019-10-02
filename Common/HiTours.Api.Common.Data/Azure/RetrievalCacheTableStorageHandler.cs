using HiTours.Api.Common.Caching;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    public class RetrievalCacheTableStorageHandler<T> : ICachedAuditTableHandler<T>
         where T : class, ITableEntity, new()
    {
        private const int DefaultCacheTime = 86400;
        private readonly ITableHandler<T> tableHandler;
        private readonly ICacheHandler cacheHandler;

        public RetrievalCacheTableStorageHandler(ITableHandler<T> tableHandler, ICacheHandler cacheHandler)
        {
            this.tableHandler = tableHandler;
            this.cacheHandler = cacheHandler;

        }

        public async Task<Response> DeleteAsync(IEnumerable<T> items)
        {
            await this.tableHandler.DeleteAsync(items);

            this.DeleteCache(items);

            return Response.Success();
        }

        public async Task<Response> DeleteAsync(T item)
        {
            await this.tableHandler.DeleteAsync(item);

            this.DeleteCache(item);

            return Response.Success();
        }

        public Task<IEnumerable<T>> ExecuteQueryAsync(TableQuery<T> query)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetItemAsync(string partitionKey, string rowKey)
        {
            var result = this.cacheHandler.Get<T>(GetItemKey(typeof(T), partitionKey, rowKey));

            if (result == null)
            {
                result = await this.tableHandler.GetItemAsync(partitionKey, rowKey);

                if (result != null)
                {
                    this.cacheHandler.Set(GetItemKey(result), result, DefaultCacheTime);
                }
            }

            return result;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string partitionKey)
        {
            var result = this.cacheHandler.Get<IEnumerable<T>>(GetItemsKey(typeof(T), partitionKey));

            if (result == null || result.Any() == false)
            {
                result = await this.tableHandler.GetItemsAsync(partitionKey);

                if (result != null && result.Any())
                {
                    this.cacheHandler.Set(GetItemsKey(result.First()), result, DefaultCacheTime);
                }
            }

            return result;
        }

        public async Task<Response> InsertAsync(IEnumerable<T> items)
        {
            await this.tableHandler.InsertAsync(items);

            this.DeleteCache(items);

            return Response.Success();
        }

        public async Task<Response> InsertAsync(T item)
        {
            await this.tableHandler.InsertAsync(item);

            this.DeleteCache(item);

            return Response.Success();
        }

        public async Task<Response> InsertOrUpdateAsync(IEnumerable<T> items)
        {
            await this.tableHandler.InsertOrUpdateAsync(items);

            this.DeleteCache(items);

            return Response.Success();
        }

        public async Task<Response> InsertOrUpdateAsync(T item)
        {
            await this.tableHandler.InsertOrUpdateAsync(item);

            this.DeleteCache(item);

            return Response.Success();
        }

        private static string GetItemKey(T item)
        {
            var type = item.GetType();

            return GetItemKey(type, item.PartitionKey, item.RowKey);
        }

        private static string GetItemKey(Type type, string partitionKey, string rowKey)
        {
            return type.Name + "_" + partitionKey + "_" + rowKey;
        }

        private static string GetItemsKey(T item)
        {
            var type = item.GetType();
            return GetItemsKey(type, item.PartitionKey);
        }

        private static string GetItemsKey(Type type, string partitionKey)
        {
            return type.Name + "_" + partitionKey;
        }

        private void DeleteCache(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            var first = true;
            foreach (var item in items)
            {
                if (first)
                {
                    first = false;
                    this.cacheHandler.Delete(GetItemsKey(item));
                }

                this.cacheHandler.Delete(GetItemKey(item));
            }
        }

        private void DeleteCache(T item)
        {
            if (item == null)
            {
                return;
            }

            this.cacheHandler.Delete(GetItemsKey(item));
            this.cacheHandler.Delete(GetItemKey(item));
        }
    }
}
