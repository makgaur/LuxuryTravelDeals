// <copyright file="ReminderTask.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services.SchedulerTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Reminder Deals Task
    /// </summary>
    /// <seealso cref="HiTours.Services.SchedulerTask.HostedService" />
    public class ReminderTask : HostedService
    {
        /////// <summary>
        /////// The package service
        /////// </summary>
        ////private readonly IPackageService packageService;

        /////// <summary>
        /////// Initializes a new instance of the <see cref="ReminderTask"/> class.
        /////// </summary>
        /////// <param name="packageService">The package service.</param>
        ////public ReminderTask(IPackageService packageService)
        ////{
        ////    this.packageService = packageService;
        ////}

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// ExecuteAsync
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                ////var upcommingDeals = await this.packageService.GetUpcommingReminderDeals();
                ////var deafultCalling = upcommingDeals.Count == 0;
                ////if (upcommingDeals.Count > 0)
                ////{
                ////    var firstDeal = upcommingDeals.FirstOrDefault();
                ////    var secondDeal = upcommingDeals.Skip(1).Take(1).FirstOrDefault();
                ////    if (firstDeal.DealStartDate >= DateTime.Now) //// Deal Started
                ////    {
                ////        // send email to users
                ////        var emails = firstDeal.UserEmails;

                ////        // Set Next Reminder
                ////        if (secondDeal != null)
                ////        {
                ////            await Task.Delay(secondDeal.DealStartDate.Subtract(DateTime.Now.AddMinutes(-1)), cancellationToken);
                ////        }

                ////        deafultCalling = secondDeal == null;
                ////    }
                ////    else
                ////    {
                ////        await Task.Delay(firstDeal.DealStartDate.Subtract(DateTime.Now.AddMinutes(-1)), cancellationToken);
                ////    }
                ////}

                ////if (deafultCalling)
                ////{
                ////    await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
                ////}

                await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
            }
        }
    }
}