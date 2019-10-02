using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    public class IncrementalSequenceRepository : IIncrementalSequenceRepository
    {
        private const int InitialSequenceValue = 0;

        private readonly IBlobHandler blobHandler;
        private readonly ITableHandler<IncrementalSequenceTableEntity> sequenceEntityHandler;
        //private readonly IInsightsTracker insightsTracker;

        private string blobLockName = string.Empty;

        public IncrementalSequenceRepository(
            IBlobHandler blobHandler,
            ITableHandler<IncrementalSequenceTableEntity> sequenceEntityHandler
           )
        {
            this.blobHandler = blobHandler;
            this.sequenceEntityHandler = sequenceEntityHandler;
            //this.insightsTracker = insightsTracker;
        }

        public async Task<Response<string>> GetNextSequence(string partitionKey, string rowKey)
        {
            var validationResponse = this.ValidateParameters(partitionKey, rowKey);

            if (!validationResponse.Successful)
            {
                return validationResponse;
            }

            await this.InitializeBlobAndTableHandler(partitionKey, rowKey);

            return await this.GenerateNextSequence(partitionKey, rowKey);
        }

        private async Task<Response<string>> GenerateNextSequence(string partitionKey, string rowKey)
        {
            var nextNumber = InitialSequenceValue;
            var newSequenceGenerated = false;

            var acquireLeaseTimeInSeconds = 1000;
            //Convert.ToInt32(ConfigurationManager.AppSettings["BookingNumberBlobLeaseTimeInSeconds"]);
            var holdThreadTimeInSeconds = 1000;
            //Convert.ToInt32(ConfigurationManager.AppSettings["BookingNumberHoldThreadTimeInSeconds"]);

            var cloudBlockBlob = this.blobHandler.GetBlockBlobReference(this.blobLockName);

            while (!newSequenceGenerated)
            {
                try
                {
                    var leaseId = cloudBlockBlob.AcquireLeaseAsync(
                        TimeSpan.FromSeconds(acquireLeaseTimeInSeconds),
                        Convert.ToString(Guid.NewGuid())).Result;

                    try
                    {
                        nextNumber = await this.GetIncrementalSequenceNumber(partitionKey, rowKey);
                        newSequenceGenerated = true;
                    }
                    catch (Exception ex)
                    {
                        newSequenceGenerated = false;
                        //this.insightsTracker.TrackException(
                        //    ex,
                        //    nameof(IncrementalSequenceRepository),
                        //    nameof(this.GetNextSequence));
                    }
                    finally
                    {
                       await  cloudBlockBlob.ReleaseLeaseAsync(AccessCondition.GenerateLeaseCondition(leaseId));
                    }
                }
                catch (StorageException storageException)
                {
                    if (storageException.RequestInformation.HttpStatusCode == (int)HttpStatusCode.Conflict)
                    {
                        this.HoldSubsequentConflictedThread(holdThreadTimeInSeconds);
                    }

                    //this.insightsTracker.TrackException(
                    //    storageException,
                    //    nameof(IncrementalSequenceRepository),
                    //    nameof(this.GetNextSequence));
                }
            }

            if (nextNumber >= 1)
            {
                return Response<string>.Success(Convert.ToString(nextNumber));
            }

            return Response<string>.Failed(Convert.ToString(nextNumber));
        }

        private void HoldSubsequentConflictedThread(int holdThreadTimeInSeconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(holdThreadTimeInSeconds));
        }

        private async Task<int> GetIncrementalSequenceNumber(string partitionKey, string rowKey)
        {
            var tableResult = await this.sequenceEntityHandler.GetItemAsync(partitionKey, rowKey);

            var incrementalSequenceTableEntity = tableResult;
            incrementalSequenceTableEntity.CurrentSequence++;

            await this.sequenceEntityHandler.InsertOrUpdateAsync(incrementalSequenceTableEntity);

            return incrementalSequenceTableEntity.CurrentSequence;
        }

        private async Task InitializeBlobAndTableHandler(string partitionKey, string rowKey)
        {
            this.SetBlobLockName(partitionKey);

            try
            {
                var incrementalSequenceEntity = new IncrementalSequenceTableEntity(partitionKey, rowKey)
                {
                    CurrentSequence = InitialSequenceValue
                };

                await this.blobHandler.WriteToBlob(this.blobLockName, Convert.ToString(InitialSequenceValue));
                await this.sequenceEntityHandler.InsertAsync(incrementalSequenceEntity);
            }
            catch (Exception ex)
            {
                //this.insightsTracker.TrackException(
                //    ex,
                //    nameof(IncrementalSequenceRepository),
                //    nameof(this.InitializeBlobAndTableHandler));
            }
        }

        private Response<string> ValidateParameters(string partitionKey, string rowKey)
        {
            if (string.IsNullOrWhiteSpace(partitionKey))
            {
                //this.insightsTracker.TrackException(
                //    new ArgumentNullException(nameof(partitionKey)),
                //    nameof(IncrementalSequenceRepository),
                //    nameof(this.GetNextSequence));
                return Response<string>.Failed("Patition key is required");
            }

            if (string.IsNullOrWhiteSpace(rowKey))
            {
                //this.insightsTracker.TrackException(
                //    new ArgumentNullException(nameof(rowKey)),
                //    nameof(IncrementalSequenceRepository),
                //    nameof(this.GetNextSequence));
                return Response<string>.Failed("Row key is required");
            }

            return Response<string>.Success(bool.TrueString);
        }

        private void SetBlobLockName(string partitionKey)
        {
            this.blobLockName = $"{partitionKey}.lck";
        }
    }
}
