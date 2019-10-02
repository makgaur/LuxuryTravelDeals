// <copyright file="IPCheckFilter.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class
    /// </summary>
    public class IPCheckFilter : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private readonly string safelist;
        private readonly string mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="IPCheckFilter"/> class.
        /// Get Or Set
        /// </summary>
        /// <param name="loggerFactory">Logger</param>
        /// <param name="configuration">Configuration</param>
        public IPCheckFilter(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.logger = loggerFactory.CreateLogger("IPCheckFilter");
            this.safelist = configuration["AdminSafeList"];
            this.mode = configuration["RunningMode"];
        }

        /// <summary>
        /// Get Or set
        /// </summary>
        /// <param name="context">Context</param>
        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.logger.LogInformation(
                $"Remote IpAddress: {context.HttpContext.Connection.RemoteIpAddress}");
            if (this.mode == "local")
            {
                base.OnActionExecuting(context);
            }
            else
            {
                var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
                this.logger.LogDebug($"Request from Remote IP address: {remoteIp}");

                string[] ip = this.safelist.Split(';');

                var bytes = remoteIp.GetAddressBytes();
                var badIp = true;
                foreach (var address in ip)
                {
                    var testIp = IPAddress.Parse(address);
                    if (testIp.GetAddressBytes().SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    this.logger.LogInformation(
                        $"Forbidden Request from Remote IP address: {remoteIp}");
                    context.Result = new StatusCodeResult(401);
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
