using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    public interface IIncrementalSequenceRepository
    {
        Task<Response<string>> GetNextSequence(string partitionKey, string rowKey);
    }
}
