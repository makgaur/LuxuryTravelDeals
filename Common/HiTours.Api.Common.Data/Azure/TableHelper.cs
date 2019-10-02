using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.Azure
{
    public class TableHelper
    {
        private static DateTime minAzureUtcDate = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime? GetSafeDate(DateTime? value)
        {
            if (value < minAzureUtcDate)
            {
                return minAzureUtcDate;
            }

            return value;
        }

        public static DateTime? GetAzureDate(DateTime? value)
        {
            if (minAzureUtcDate == value)
            {
                return DateTime.MinValue;
            }

            return value;
        }

        public static DateTime GetSafeDate(DateTime value)
        {
            if (value < minAzureUtcDate)
            {
                return minAzureUtcDate;
            }

            return value;
        }

        public static DateTime GetAzureDate(DateTime value)
        {
            if (minAzureUtcDate == value)
            {
                return DateTime.MinValue;
            }

            return value;
        }
    }
}
