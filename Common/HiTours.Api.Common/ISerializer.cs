using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common
{
    public interface ISerializer
    {
        T DeserializeObject<T>(string serializedObject);

        string SerializeObject<T>(T obj);
    }
}
