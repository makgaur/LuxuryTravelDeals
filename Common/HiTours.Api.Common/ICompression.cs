using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common
{
    public interface ICompression
    {
        T Decompress<T>(string key) where T : class;

        string Compress<T>(T model);
    }
}
