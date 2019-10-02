using HiTours.Api.Common.Contract;
using HiTours.Api.Common.Data.Azure;
using HiTours.Api.Common.Logging.Contract;
using HiTours.Api.Common.Logging.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Logging.Repository
{
    public class ChangeLogRepository : IChangeRepository
    {
        private readonly ITableHandler<ChangeLogEntity> tableHandler;
        private readonly ITableHandler<ChangeLogTableOverviewEntity> overviewTableHandler;


        public ChangeLogRepository(ITableHandler<ChangeLogEntity> tableHandler, ITableHandler<ChangeLogTableOverviewEntity> overviewTableHandler)
        {
            this.tableHandler = tableHandler;
            this.overviewTableHandler = overviewTableHandler;

        }

        public async Task<Response> LogChange(ChangeLogModel changeModel)
        {
            try
            {
                await this.StoreChangeLogEntity(changeModel);
            }
            catch (Exception exp)
            {
                //  this.insightsTracker.TrackException(exp, nameof(ChangeLogRepository), nameof(this.LogChange), changeModel.TableName);
                return Response.Failed(exp.Message);
            }

            return Response.Success();
        }

        public async Task<IList<ChangeLogModel>> GetChangesByUser(string userId)
        {
            var changeLogEntities = await this.tableHandler.GetItemsAsync(userId);

            return changeLogEntities.Select(ModelFactory.CreateChangeLogModel).ToList();
        }

        public async Task<IList<ChangeLogModel>> GetChangesByTable(ChangeLogRequestModel changeLogRequestModel)
        {
            var changeLogEntities = await this.tableHandler.GetItemsAsync(EntityFactory.GetRowKey(changeLogRequestModel));

            return changeLogEntities.Select(ModelFactory.CreateChangeLogModel).ToList();
        }

        public async Task<IList<string>> GetChangedTables()
        {
            var tableChanges = await this.overviewTableHandler.GetItemsAsync(StorageValues.TableOverviewPartitionKey);

            return tableChanges.Select(tc => tc.TableName).ToList();
        }

        public async Task<IList<ChangeLogTableOverviewModel>> GetTableOverview()
        {
            var tableChanges = await this.overviewTableHandler.GetItemsAsync(StorageValues.TableOverviewPartitionKey);

            return tableChanges.Select(ModelFactory.CreateChangeLogTableOverviewModel).ToList();
        }

        private async Task StoreChangeLogEntity(ChangeLogModel changeLogModel)
        {
            var rowKey = Guid.NewGuid().ToString();
            var changeLogEntityTable = EntityFactory.GetChangeLogEntity(changeLogModel);
            var changeLogEntityUser = EntityFactory.GetChangeLogEntity(changeLogModel);

            changeLogEntityTable.PartitionKey = EntityFactory.GetRowKey(changeLogModel);
            changeLogEntityTable.RowKey = rowKey;
            changeLogEntityUser.PartitionKey = changeLogEntityUser.UserId;
            changeLogEntityUser.RowKey = rowKey;

            await this.tableHandler.InsertAsync(new List<ChangeLogEntity> { changeLogEntityUser, changeLogEntityTable });

            try
            {
                await this.UpdateOverview(changeLogEntityUser);
            }
            catch (Exception exp)
            {
                // this.insightsTracker.TrackException(exp, nameof(ChangeLogRepository), nameof(this.LogChange), "Overview");
            }
        }

        private async Task UpdateOverview(ChangeLogEntity changeLogEntity)
        {
            var changeLogOverviewEntity = await this.overviewTableHandler.GetItemAsync(StorageValues.TableOverviewPartitionKey, changeLogEntity.Context + "_" + changeLogEntity.TableName);

            if (changeLogOverviewEntity == null)
            {
                changeLogOverviewEntity = EntityFactory.CreateChangeLogTableOverviewEntity(changeLogEntity);
            }

            EntityFactory.AppendOverviewValues(changeLogEntity, changeLogOverviewEntity);

            await this.overviewTableHandler.InsertOrUpdateAsync(changeLogOverviewEntity);
        }
    }
}
