// <copyright file="HostedService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services.SchedulerTask
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// HostedService
    /// </summary>
    /// <seealso cref="IHostedService" />
    public abstract class HostedService : IHostedService
    {
        // Example untested base class code kindly provided by David Fowler: https://gist.github.com/davidfowl/a7dd5064d9dcf35b6eae1a7953d615e3
        private Task executingTask;
        private CancellationTokenSource cts;

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>StartAsync</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a linked token so we can trigger cancellation outside of this token's cancellation
            this.cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Store the task we're executing
            this.executingTask = this.ExecuteAsync(this.cts.Token);

            // If the task is completed then return it, otherwise it's running
            return this.executingTask.IsCompleted ? this.executingTask : Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>StopAsync</returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (this.executingTask == null)
            {
                return;
            }

            // Signal cancellation to the executing method
            this.cts.Cancel();

            // Wait until the task completes or the stop token triggers
            await Task.WhenAny(this.executingTask, Task.Delay(-1, cancellationToken));

            // Throw if cancellation triggered
            cancellationToken.ThrowIfCancellationRequested();
        }

        // Derived classes should override this and execute a long running method until
        // cancellation is requested

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>ExecuteAsync</returns>
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}