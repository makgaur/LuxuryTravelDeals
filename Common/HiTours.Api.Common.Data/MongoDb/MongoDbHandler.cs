using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.MongoDb
{
    /// <summary>
    /// Handles database operations for items of type T
    /// </summary>
    /// <typeparam name="T">The type to be handled. It must be an object that implements BaseEntity to work with cosmos db</typeparam>
    public class MongoDbHandler<T> : IMongoDbHandler<T>
        where T : BaseEntity, new()
    {
        private readonly IMongoDbContext<T> cosmosDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDbHandler{T}"/> class. 
        /// </summary>
        /// <param name="cosmosDbContext">The cosmos db context to use for the handler</param>
        public MongoDbHandler(IMongoDbContext<T> cosmosDbContext)
        {
            this.cosmosDbContext = cosmosDbContext;
        }

        public async Task<Response> InsertAsync(T item)
        {
            try
            {
                await this.cosmosDbContext.InsertAsync(item);
            }
            catch (MongoWriteException me)
            {
                return Response.Failed(me.Message);
            }

            return Response.Success();
        }

        public async Task<Response> InsertAsync(IEnumerable<T> items)
        {
            try
            {
                await this.cosmosDbContext.InsertAsync(items);
            }
            catch (MongoException me)
            {
                return Response.Failed(me.Message);
            }

            return Response.Success();
        }

        public async Task<Response> UpdateAsync(T item)
        {
            try
            {
                var result = await this.cosmosDbContext.UpdateAsync(item);
                if (result != null && ((result.IsAcknowledged && result.MatchedCount == 0)
                                       || (result.IsModifiedCountAvailable && !(result.ModifiedCount > 0))))
                {
                    Response.Failed("Item does not exists");
                }
            }
            catch (MongoWriteException me)
            {
                return Response.Failed(me.Message);
            }

            return Response.Success();
        }

        public async Task<Response> InsertOrUpdateAsync(T item)
        {
            try
            {
                var result = await this.cosmosDbContext.UpdateAsync(item);
                if (result != null && ((result.IsAcknowledged && result.MatchedCount == 0)
                                       || (result.IsModifiedCountAvailable && !(result.ModifiedCount > 0))))
                {
                    await this.InsertAsync(item);
                }
            }
            catch (MongoWriteException me)
            {
                return Response.Failed(me.Message);
            }

            return Response.Success();
        }

        public async Task<Response> DeleteAsync(T item)
        {
            try
            {
                await this.cosmosDbContext.DeleteAsync(item.Id);
            }
            catch (MongoException me)
            {
                return Response.Failed(me.Message);
            }

            return Response.Success();
        }

        public async Task<T> GetItemAsync(string id)
        {
            return await this.cosmosDbContext.GetAsync(id);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(IEnumerable<string> ids)
        {
            return await this.cosmosDbContext.GetAsync(ids);
        }

        public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await this.cosmosDbContext.FindAsync(expression);
        }
    }
}
