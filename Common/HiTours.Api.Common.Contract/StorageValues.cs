using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Contract
{
    public class StorageValues
    {
        public const string CacheContainerName = "cache-container";


        public const string CacheEntityTableName = "CacheData";

        public const string ChangeLogTableName = "ChangeLog";

        public const string ChangeLogOverviewTableName = "ChangeLogOverview";

        public const string StorageConnectionStringName = "StorageConnectionString";

        public const string HotelPluginConnectionTableName = "HotelPluginConnections";



        public const string ErrorLogTableName = "ErrorLog";

        public const string ApiErrorResponseTableName = "ApiErrorResponses";



        public const string TableOverviewPartitionKey = "TableOverview";
        public static string StorageConnectionString
        {
            get
            {
                return "Test";
            }
        }

      
        public const string FlightLogsBlobPath = "flightlogs";
        public const string FlightResultBlobPath = "flightresult";

    }



    public class CommonStorageValues
    {
        public const string CacheContainerName = "cache-container";


        public const string CacheEntityTableName = "CacheData";

        public const string ChangeLogTableName = "ChangeLog";

        public const string ChangeLogOverviewTableName = "ChangeLogOverview";

        public const string StorageConnectionStringName = "StorageConnectionString";

     

        public const string ErrorLogTableName = "ErrorLog";

        public const string ApiErrorResponseTableName = "ApiErrorResponses";



        public const string TableOverviewPartitionKey = "TableOverview";
        public static string StorageConnectionString
        {
            get
            {
                return "Test";
            }
        }

     
        public const string FlightLogsBlobPath = "flightlogs";
        public const string FlightResultBlobPath = "flightresult";

    }
}
