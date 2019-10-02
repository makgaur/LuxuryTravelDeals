using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.MongoDb
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// The unique id representing as a key in table
        /// </summary>
        public string Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
    }
}
