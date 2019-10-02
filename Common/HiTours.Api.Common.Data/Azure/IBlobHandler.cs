using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.Azure
{
    public interface IBlobHandler
    {
        Task WriteToBlob(string identifier, string item);

        Task<string> ReadFromBlob(string identifier);

        CloudBlockBlob GetBlockBlobReference(string identifier);
    }
}
