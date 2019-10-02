using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.MongoDb
{
    /// <summary>
    /// Serves as a wrapper around the azure cosmos db calls
    /// </summary>
    /// <typeparam name="T">The type associated with the entities in use</typeparam>
    public class MongoDbContext<T> : MongoDbContext, IMongoDbContext<T> where T : BaseEntity, new()
    {
        private readonly MongoDbAuthentication cosmosDbAuthentication;
        private readonly string collectionName;
        private IMongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<T> collection;

        private IMongoCollection<T> Collection
        {
            get
            {
                if (this.collection == null)
                {
                    var settings = new MongoClientSettings
                    {
                        Server = new MongoServerAddress(this.cosmosDbAuthentication.HostName, this.cosmosDbAuthentication.PortNumber),
                        UseSsl = true,
                        SslSettings =
                                           new SslSettings
                                           {
                                               EnabledSslProtocols =
                                                   SslProtocols.Tls12
                                           }
                    };

                    var identity = new MongoInternalIdentity(this.cosmosDbAuthentication.DatabaseName, this.cosmosDbAuthentication.UserName);
                    MongoIdentityEvidence evidence = new PasswordEvidence(this.cosmosDbAuthentication.Password);

                    settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

                    this.client = new MongoClient(settings);

                    this.database = this.client.GetDatabase(this.cosmosDbAuthentication.DatabaseName);

                    this.collection = this.database.GetCollection<T>(this.collectionName);
                }

                return this.collection;
            }
        }

        /// <summary>
        /// Initializes the TableContext
        /// </summary>
        /// <param name="authentication">The authentication details to connect with cosmos db</param>
        /// <param name="collectionName">The collection name</param>
        public MongoDbContext(MongoDbAuthentication authentication, string collectionName)
        {
            this.cosmosDbAuthentication = authentication;
            this.collectionName = collectionName;
        }

        public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Collection.Find(expression).ToListAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await this.Collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<T> GetAsync(string id)
        {
            return await this.Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(IEnumerable<string> ids)
        {
            return await Task.Run(() => this.Collection.Find(x => ids.Contains(x.Id)).ToEnumerable());
        }

        public async Task InsertAsync(T item, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.Collection.InsertOneAsync(item, new InsertOneOptions { BypassDocumentValidation = true }, cancellationToken);
        }

        public async Task InsertAsync(IEnumerable<T> items)
        {
            await this.Collection.InsertManyAsync(
                items,
                new InsertManyOptions { BypassDocumentValidation = true, IsOrdered = false });
        }

        public async Task<ReplaceOneResult> UpdateAsync(T item, CancellationToken cancellationToken = default(CancellationToken))
        {
            var idFilter = Builders<T>.Filter.Eq(e => e.Id, item.Id);
            var result = await this.Collection.ReplaceOneAsync(idFilter, item, null, cancellationToken);

            return result;
        }
    }
    public class MongoDbContext
    {
        public const string AuthenticationParameterName = "authentication";
        public const string CollectionParameterName = "collectionName";
    }
}
