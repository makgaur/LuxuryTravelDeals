using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common
{
    public class FlightAppSettingsJson
    {
        public static IConfigurationRoot GetAppSettings()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
