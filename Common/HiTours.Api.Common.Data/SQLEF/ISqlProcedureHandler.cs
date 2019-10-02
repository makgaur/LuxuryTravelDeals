using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.SQLEF
{
    public interface ISqlProcedureHandler<T> where T : class
    {
        Task<IList<T>> GetItemsAsync(string procedureName, List<KeyValuePair<string, object>> commandParameters);
        Task<DataSet> GetDataSetAsync(string procedureName, List<KeyValuePair<string, object>> commandParameters);
        Task<T> InsertAsyncOutPut(string procedureName, List<KeyValuePair<string, object>> allParams, string outPut);
        Task<Response> InsertAsync(string procedureName, List<KeyValuePair<string, object>> allParams);
    }
}
