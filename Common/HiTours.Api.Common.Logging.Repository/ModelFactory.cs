using HiTours.Api.Common.Logging.Contract;
using HiTours.Api.Common.Logging.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiTours.Api.Common.Logging.Repository
{
    public class ModelFactory
    {
        internal static ChangeLogModel CreateChangeLogModel(ChangeLogEntity changeLogEntity)
        {
            var changeLogModel = new ChangeLogModel
            {
                UserId = changeLogEntity.UserId,
                TableName = changeLogEntity.TableName,
                Context = changeLogEntity.Context,
                Type = changeLogEntity.Type,
                ItemId = changeLogEntity.ItemId,
                UserEmail = changeLogEntity.UserEmail,
                ChangeData = changeLogEntity.ChangeData,
                ChangedDataType = changeLogEntity.ChangedDataType,
                LogDate = changeLogEntity.Timestamp.DateTime,
                ItemPartitionKey = changeLogEntity.ItemPartitionKey,
                ItemRowKey = changeLogEntity.ItemRowKey
            };

            return changeLogModel;
        }

        internal static ChangeLogTableOverviewModel CreateChangeLogTableOverviewModel(ChangeLogTableOverviewEntity changeLogTableOverviewEntity)
        {
            var changeLogTableOverviewModel = new ChangeLogTableOverviewModel
            {
                TableName = changeLogTableOverviewEntity.TableName,
                Context = changeLogTableOverviewEntity.Context,
                Users = changeLogTableOverviewEntity.Users.Split(',').ToList(),
                CreateLogCount = changeLogTableOverviewEntity.CreateLogCount,
                DeleteLogCount = changeLogTableOverviewEntity.DeleteLogCount,
                ModifyLogCount = changeLogTableOverviewEntity.ModifyLogCount,
            };

            return changeLogTableOverviewModel;
        }
    }
}
