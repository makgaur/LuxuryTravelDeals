using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.SQLServer
{
    public class ConfigHandlers
    {
        public static Dictionary<string, string> GetDBConfigDynamic()
        {
            var appSettingsJson = FlightAppSettingsJson.GetAppSettings();
            var connectionString = appSettingsJson.GetSection("ConnectionStrings").GetSection("Default").Value;
            Dictionary<string, string> DBCredential = new Dictionary<string, string>();
          
       
            DBCredential.Add("ConnectionString", connectionString);
            return DBCredential;
        }
    }
}
