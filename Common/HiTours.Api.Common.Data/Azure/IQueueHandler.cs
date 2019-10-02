using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    /// <summary>
    /// Queue Handler
    /// </summary>
    public interface IQueueHandler
    {
        /// <summary>
        /// Writes to queue.
        /// </summary>
        /// <param name="queueInputModel">The queue input model.</param>
        /// <returns></returns>
        Task WriteToQueue(QueueInputModel queueInputModel);
    }
}
