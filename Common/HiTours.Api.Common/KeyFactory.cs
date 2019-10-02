using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common
{
    public class KeyFactory
    {
        /// <summary>
        /// Generate key based on flight rules tab selected
        /// </summary>
        /// <param name="key">The partition key</param>
        /// <param name="flightRulesTab">The flight rules tab selected</param>
        /// <returns></returns>
        public static string GeneratePartitionKey(string key)
        {
            return key;
        }

      
       
    }
}
