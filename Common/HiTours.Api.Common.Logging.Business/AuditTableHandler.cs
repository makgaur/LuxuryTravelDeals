using HiTours.Api.Common.Data.Azure;
using HiTours.Api.Common.Logging.Contract;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Business
{
    public class AuditTableHandler<T> : IAuditTableHandler<T>
        where T : ITableEntity, new()
    {
        private const string DefaultUnknownUser = "Unknown";
        private readonly ITableHandler<T> tableHandler;
        private readonly IChangeRepository changeRepository;
        // private readonly IInsightsTracker insightsTracker;

        public AuditTableHandler(
            ITableHandler<T> tableHandler,
            IChangeRepository changeRepository,

            string context,
            string tableName)
        {
            this.tableHandler = tableHandler;
            this.changeRepository = changeRepository;
            // this.insightsTracker = insightsTracker;
            this.TableName = tableName;
            this.Context = context;
        }

        public string TableName { get; }

        public string Context { get; }

        public async Task<Response> InsertAsync(T item)
        {
            try
            {
                var response = await this.tableHandler.InsertAsync(item);
                if (response.Successful)
                {
                    await this.LogChange(item, new CompareEntitiesFactory(null, item, ChangeTypeValues.Create));
                }

                return response;
            }
            catch (Exception exp)
            {
                //this.insightsTracker.TrackException(exp, nameof(AuditTableHandler<T>), nameof(this.InsertAsync), this.TableName);
                return Response.Failed(exp.Message);
            }
        }

        public async Task<Response> InsertAsync(IEnumerable<T> items)
        {
            return await this.tableHandler.InsertAsync(items);
        }

        public async Task<Response> InsertOrUpdateAsync(T item)
        {
            try
            {
                var existing = await this.tableHandler.GetItemAsync(item.PartitionKey, item.RowKey);
                var compareFactory = new CompareEntitiesFactory(existing, item, ChangeTypeValues.Modify);

                if (compareFactory.AreEqual())
                {
                    return Response.Success();
                }

                var response = await this.tableHandler.InsertOrUpdateAsync(item);
                if (response.Successful)
                {
                    await this.LogChange(item, compareFactory);
                }

                return response;
            }
            catch (Exception exp)
            {
                //   this.insightsTracker.TrackException(exp, nameof(AuditTableHandler<T>), nameof(this.InsertOrUpdateAsync), this.TableName);
                return Response.Failed(exp.Message);
            }
        }

        public async Task<Response> InsertOrUpdateAsync(IEnumerable<T> items)
        {
            var tasks = items.Select(this.InsertOrUpdateAsync);

            var responses = await Task.WhenAll(tasks);

            return responses.FirstOrDefault();
        }

        public async Task<Response> DeleteAsync(T item)
        {
            try
            {
                var response = await this.tableHandler.DeleteAsync(item);
                if (response.Successful)
                {
                    await this.LogChange(item, new CompareEntitiesFactory(item, null, ChangeTypeValues.Delete));
                }

                return response;
            }
            catch (Exception exp)
            {
                //this.insightsTracker.TrackException(exp, nameof(AuditTableHandler<T>), nameof(this.DeleteAsync), this.TableName);
                return Response.Failed(exp.Message);
            }
        }

        public async Task<Response> DeleteAsync(IEnumerable<T> items)
        {
            var tasks = items.Select(this.DeleteAsync);

            var responses = await Task.WhenAll(tasks);

            return responses.FirstOrDefault();
        }

        public async Task<T> GetItemAsync(string partitionKey, string rowKey)
        {
            return await this.tableHandler.GetItemAsync(partitionKey, rowKey);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string partitionKey)
        {
            return await this.tableHandler.GetItemsAsync(partitionKey);
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync(TableQuery<T> query)
        {
            return await this.tableHandler.ExecuteQueryAsync(query);
        }

        private async Task LogChange(T item, CompareEntitiesFactory compareEntitiesFactory)
        {
            var changeLogModel = new ChangeLogModel
            {
                Type = compareEntitiesFactory.GetChangeType(),
                TableName = this.TableName,
                Context = this.Context,
                ChangedDataType = item.GetType().Name,
                ChangeData = compareEntitiesFactory.GetChanges(),
                LogDate = DateTimeOffset.Now.Date,
                ItemPartitionKey = item.PartitionKey,
                ItemRowKey = item.RowKey,
                UserId = GetUserId(),
                UserEmail = GetUserEmail()
            };

            if (changeLogModel.UserId == DefaultUnknownUser || string.IsNullOrWhiteSpace(changeLogModel.UserEmail))
            {
                //this.insightsTracker.TrackEvent(
                //    "InvalidUserClaims",
                //    new Dictionary<string, string>
                //        {
                //                { nameof(ChangeLogEntity.UserId), changeLogModel.UserId },
                //                { nameof(ChangeLogEntity.UserEmail), changeLogModel.UserEmail },
                //                { nameof(ChangeLogEntity.Context), changeLogModel.Context },
                //                { nameof(ChangeLogEntity.TableName), changeLogModel.TableName },
                //                { nameof(ChangeLogEntity.Type), changeLogModel.Type }
                //        });
            }

            await this.changeRepository.LogChange(changeLogModel);
        }

        private static string GetUserId()
        {
            var userId = DefaultUnknownUser;
            var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            var userIdClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null && string.IsNullOrWhiteSpace(userIdClaim.Value) == false)
            {
                userId = userIdClaim.Value;
            }

            return userId;
        }

        private static string GetUserEmail()
        {
            var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            string userEmail = null;
            var userEmailClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (userEmailClaim != null)
            {
                userEmail = userEmailClaim.Value;
            }

            return userEmail;
        }

        public class CompareEntitiesFactory
        {
            private readonly ITableEntity existingEntity;

            private readonly ITableEntity changedEntity;


            private readonly string changeType;

            public CompareEntitiesFactory(ITableEntity existingEntity, ITableEntity changedEntity, string changeType)
            {
                this.existingEntity = existingEntity;
                this.changedEntity = changedEntity;
                // this.insightsTracker = insightsTracker;
                this.changeType = changeType;
            }

            public bool IsCreateNew()
            {
                return this.existingEntity == null;
            }

            public bool IsDelete()
            {
                return this.changeType == ChangeTypeValues.Delete;
            }

            public string GetChangeType()
            {
                if (this.changeType == ChangeTypeValues.Delete)
                {
                    return ChangeTypeValues.Delete;
                }
                else if (this.IsCreateNew())
                {
                    return ChangeTypeValues.Create;
                }

                return ChangeTypeValues.Modify;
            }

            public bool AreEqual()
            {
                if (this.existingEntity == null)
                {
                    return false;
                }

                var existingJson = JsonConvert.SerializeObject(this.existingEntity, Formatting.Indented, new TableEntitySerializer());
                var changedJson = JsonConvert.SerializeObject(this.changedEntity, Formatting.Indented, new TableEntitySerializer());

                return existingJson == changedJson;
            }

            public string GetChanges()
            {
                try
                {
                    if (this.IsCreateNew())
                    {
                        return JsonConvert.SerializeObject(this.changedEntity, Formatting.Indented);
                    }

                    if (this.IsDelete())
                    {
                        return JsonConvert.SerializeObject(this.existingEntity, Formatting.Indented);
                    }

                    return this.GetChangedProperties();
                }
                catch (Exception exp)
                {
                    var exception = new Exception("Failed during change retrieval", exp);
                    //this.insightsTracker.TrackException(exception, nameof(AuditTableHandler), nameof(this.GetChanges), this.GetChangeType());
                    return "Failed during change retrieval";
                }
            }

            private string GetChangedProperties()
            {
                var changedProperties = this.existingEntity.GetType().GetProperties();
                var changed = new List<ChangeObject>();

                foreach (var changedProperty in changedProperties)
                {
                    if (changedProperty.Name == nameof(ITableEntity.ETag) || changedProperty.Name == nameof(ITableEntity.Timestamp))
                    {
                        continue;
                    }

                    var existingValue = changedProperty.GetValue(this.existingEntity);
                    var changedValue = changedProperty.GetValue(this.changedEntity);

                    if ((existingValue == null && changedValue != null)
                        || (existingValue != null && existingValue.Equals(changedValue) == false))
                    {
                        changed.Add(new ChangeObject { Changed = changedProperty.Name, From = existingValue, To = changedValue });
                    }
                }

                return changed.Any() ? JsonConvert.SerializeObject(changed, Formatting.Indented) : string.Empty;
            }
        }
    }

    public class ChangeObject
    {
        public string Changed { get; set; }

        public object From { get; set; }

        public object To { get; set; }
    }

    public class TableEntitySerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var token = JToken.FromObject(value);
            var obj = new JObject();

            foreach (JProperty prop in token)
            {
                if (prop.Name != nameof(ITableEntity.ETag) && prop.Name != nameof(ITableEntity.Timestamp)) obj.Add(prop);
            }

            obj.WriteTo(writer);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType != typeof(IList);
        }
    }

    public class AuditTableHandler
    {
        public const string TableNameParameterName = "tableName";

        public const string ContextParameterName = "context";
    }
}
