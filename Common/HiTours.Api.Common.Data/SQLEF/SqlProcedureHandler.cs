using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.SQLEF
{
    public class SqlProcedureHandler<T> : TableContext, ISqlProcedureHandler<T> where T : class
    {

        private readonly string connectionString;
        /// <summary>
        /// Initializes the TableContext
        /// </summary>
        /// <param name="connectionString">The connection string to the azure storage</param>
        public SqlProcedureHandler(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<IList<T>> GetItemsAsync(string procedureName, List<KeyValuePair<string, object>> commandParameters)
        {

            IList<T> T = new List<T>();
            using (var context = new ApplicationContext(this.connectionString))
            {
                var cmd = context.LoadStoredProc(procedureName);
                if (commandParameters != null)
                {
                    foreach (var param in commandParameters)
                    {
                        cmd.WithSqlParam(param.Key, param.Value);
                    }
                }
                T = await cmd.ExecuteStoredProcAsync<T>();

            }

            return T;
        }

        public async Task<DataSet> GetDataSetAsync(string procedureName, List<KeyValuePair<string, object>> commandParameters)
        {

            var T = new DataSet();
            using (var context = new ApplicationContext(this.connectionString))
            {
                var cmd = context.LoadStoredProc(procedureName);
                if (commandParameters != null)
                {
                    foreach (var param in commandParameters)
                    {
                        cmd.WithSqlParam(param.Key, param.Value);
                    }
                }
                T = await cmd.FillDataset();


            }

            return T;
        }
        public async Task<T> InsertAsyncOutPut(string procedureName, List<KeyValuePair<string, object>> commandParameters, string outPutParameters)
        {
            using (var context = new ApplicationContext(this.connectionString))
            {
                var cmd = context.LoadStoredProc(procedureName);
                foreach (var param in commandParameters)
                {
                    if (outPutParameters != null)
                    {
                        if (outPutParameters == param.Key)
                        {
                            cmd.WithSqlParamOut(param.Key, param.Value);
                        }
                        else
                        {
                            cmd.WithSqlParam(param.Key, param.Value);
                        }

                    }
                    else
                    {
                        cmd.WithSqlParam(param.Key, param.Value);
                    }

                }
                await cmd.ExecuteNonQueryStoredProc();
                return (T)Convert.ChangeType(cmd.Parameters[outPutParameters].Value, typeof(T));
                //return cmd.Parameters[outPut].Value.ToString();

            }


        }

        public async Task<Response> InsertAsync(string procedureName, List<KeyValuePair<string, object>> commandParameters)
        {
            try
            {
                using (var context = new ApplicationContext(this.connectionString))
                {
                    var cmd = context.LoadStoredProc(procedureName);
                    foreach (var param in commandParameters)
                    {
                        cmd.WithSqlParam(param.Key, param.Value);
                    }
                    await cmd.ExecuteNonQueryStoredProc();

                }
            }
            catch (Exception ex)
            {
                return Response.Failed(ex.Message);
            }
            return Response.Success();
        }
    }


    public class TableContext
    {
        public const string ConnectionStringParameterName = "connectionString";
        public const string TableNameParameterName = "tableName";
    }
}
