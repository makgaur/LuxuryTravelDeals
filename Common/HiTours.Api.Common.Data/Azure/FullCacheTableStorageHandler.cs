using HiTours.Api.Common.Caching;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    public class FullCacheTableStorageHandler<T> : ITableHandler<T> where T : ITableEntity, new()
    {
        private readonly ITableHandler<T> tableHandler;
        private readonly ICacheHandler cacheHandler;

        public FullCacheTableStorageHandler(ITableHandler<T> tableHandler, ICacheHandler cacheHandler)
        {
            this.tableHandler = tableHandler;
            this.cacheHandler = cacheHandler;
        }

        public async Task<Response> DeleteAsync(IEnumerable<T> items)
        {
            await this.tableHandler.DeleteAsync(items);

            await this.UpdateCache(items);

            return Response.Success();
        }

        public async Task<Response> DeleteAsync(T item)
        {
            await this.tableHandler.DeleteAsync(item);

            await this.UpdateCache(item);

            return Response.Success();
        }

        public Task<IEnumerable<T>> ExecuteQueryAsync(TableQuery<T> query)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetItemAsync(string partitionKey, string rowKey)
        {
            var cacheKey = GetFullKey(typeof(T), partitionKey);
            var cachedItems = this.cacheHandler.Get<IEnumerable<T>>(cacheKey);

            if (cachedItems != null && cachedItems.Any())
            {
                return cachedItems.FirstOrDefault(c => c.PartitionKey == partitionKey && c.RowKey == rowKey);
            }

            var result = await this.tableHandler.GetItemAsync(partitionKey, rowKey);

            if (result != null)
            {
                await this.UpdateCache(result);
            }

            return result;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string partitionKey)
        {
            var cacheKey = GetFullKey(typeof(T), partitionKey);
            var cachedItems = this.cacheHandler.Get<IEnumerable<T>>(cacheKey);

            if (cachedItems != null && cachedItems.Any())
            {
                return cachedItems.Where(c => c.PartitionKey == partitionKey);
            }

            var result = await this.tableHandler.GetItemsAsync(partitionKey);

            if (result != null && result.Any())
            {
                await this.UpdateCache(result);
            }

            return result;
        }

        public async Task<Response> InsertAsync(IEnumerable<T> items)
        {
            await this.tableHandler.InsertAsync(items);

            await this.UpdateCache(items);

            return Response.Success();
        }

        public async Task<Response> InsertAsync(T item)
        {
            await this.tableHandler.InsertAsync(item);

            await this.UpdateCache(item);

            return Response.Success();
        }

        public async Task<Response> InsertOrUpdateAsync(IEnumerable<T> items)
        {
            await this.tableHandler.InsertOrUpdateAsync(items);

            await this.UpdateCache(items);

            return Response.Success();
        }

        public async Task<Response> InsertOrUpdateAsync(T item)
        {
            await this.tableHandler.InsertOrUpdateAsync(item);

            await this.UpdateCache(item);

            return Response.Success();
        }

        private static string GetFullKey(T item)
        {
            var type = item.GetType();
            return type.Name + "_Full_" + item.PartitionKey;
        }

        private static string GetFullKey(Type type, string partitionKey)
        {
            return type.Name + "_Full_" + partitionKey;
        }

        private async Task UpdateCache(IEnumerable<T> items)
        {
            this.cacheHandler.Set(GetFullKey(items.First()), await tableHandler.GetItemsAsync(items.First().PartitionKey), 0);
        }

        private async Task UpdateCache(T item)
        {
            this.cacheHandler.Set(GetFullKey(item), await this.tableHandler.GetItemsAsync(item.PartitionKey), 0);
        }
    }
}
