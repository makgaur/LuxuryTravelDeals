using HiTours.Api.Common.Contract;
using HiTours.Api.Common.Logging.Contract;
using HiTours.Api.Common.Logging.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace HiTours.Api.Common.Logging.Repository
{
    public class EntityFactory
    {
        private const string DefaultUnknownUser = "Unknown";

        internal static ChangeLogEntity GetChangeLogEntity(ChangeLogModel changeModel)
        {
            var userId = GetUserId();
            var userEmail = GetUserEmail();

            if (userId == DefaultUnknownUser)
            {
                //insightsTracker.TrackEvent(
                //    "UserIdNotFound",
                //    new Dictionary<string, string>
                //        {
                //            { nameof(ChangeLogEntity.UserId), "Unknown" },
                //            { nameof(ChangeLogEntity.UserEmail), userEmail },
                //            { nameof(ChangeLogEntity.Context), changeModel.Context },
                //            { nameof(ChangeLogEntity.TableName), changeModel.TableName },
                //            { nameof(ChangeLogEntity.Type), changeModel.Type }
                //        });
            }

            var changeLogEntity = new ChangeLogEntity
            {
                ChangeData = changeModel.ChangeData,
                Context = changeModel.Context,
                ItemId = changeModel.ItemId,
                TableName = changeModel.TableName,
                Type = changeModel.Type,
                UserEmail = userEmail,
                UserId = userId,
                ChangedDataType = changeModel.ChangedDataType,
                ItemPartitionKey = changeModel.ItemPartitionKey,
                ItemRowKey = changeModel.ItemRowKey
            };
            return changeLogEntity;
        }

        internal static ChangeLogTableOverviewEntity CreateChangeLogTableOverviewEntity(ChangeLogEntity changeLogEntity)
        {
            var changeLogOverviewEntity = new ChangeLogTableOverviewEntity
            {
                PartitionKey = StorageValues.TableOverviewPartitionKey,
                RowKey = GetRowKey(changeLogEntity),
                TableName = changeLogEntity.TableName,
                Context = changeLogEntity.Context,
                Users = changeLogEntity.UserId
            };

            return changeLogOverviewEntity;
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

        private static string GetUserId()
        {
            var userId = DefaultUnknownUser;
            var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            var userIdClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                userId = userIdClaim.Value;
            }

            return userId;
        }

        internal static void AppendOverviewValues(ChangeLogEntity changeLogEntity, ChangeLogTableOverviewEntity changeLogOverviewEntity)
        {
            if (changeLogOverviewEntity.Users.Contains(changeLogEntity.UserId) == false)
            {
                changeLogOverviewEntity.Users += "," + changeLogEntity.UserId;
            }

            if (changeLogEntity.Type == ChangeTypeValues.Modify)
            {
                changeLogOverviewEntity.ModifyLogCount += 1;
            }
            if (changeLogEntity.Type == ChangeTypeValues.Create)
            {
                changeLogOverviewEntity.CreateLogCount += 1;
            }
            if (changeLogEntity.Type == ChangeTypeValues.Delete)
            {
                changeLogOverviewEntity.DeleteLogCount += 1;
            }
        }

        internal static string GetRowKey(ChangeLogModel changeLogModel)
        {
            return changeLogModel.Context + "_" + changeLogModel.TableName;
        }

        internal static string GetRowKey(ChangeLogEntity changeLogEntity)
        {
            return changeLogEntity.Context + "_" + changeLogEntity.TableName;
        }

        internal static string GetRowKey(ChangeLogRequestModel changeLogRequestModel)
        {
            return changeLogRequestModel.Context + "_" + changeLogRequestModel.TableName;
        }
    }
}
