using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Data.SQLEF
{

    public static class EFExtensions
    {
        public static void AddOrUpdate(this DbContext ctx, object entity)
        {

            var entry = ctx.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    ctx.Add(entity);
                    break;
                case EntityState.Modified:
                    ctx.Update(entity);
                    break;
                case EntityState.Added:
                    ctx.Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static DbCommand LoadStoredProc(this DbContext context, string storedProcName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }

        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
            return cmd;
        }

        public static DbCommand WithSqlParamOut(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Direction = System.Data.ParameterDirection.Output;
            param.DbType = System.Data.DbType.String;
            param.Size = 25;
            cmd.Parameters.Add(param);
            return cmd;
        }

        public static DbCommand GetOutParameter(this DbCommand cmd, string paramName)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            cmd.Parameters["@" + paramName].Value.ToString();
            return cmd;
        }



        public static IList<T> ExecuteStoredProc<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public async static Task<IList<T>> ExecuteStoredProcAsync<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    //command.Connection.Open();
                    await command.Connection.OpenAsync().ConfigureAwait(false);
                try
                {

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return reader.MapToList<T>();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public static async Task ExecuteNonQueryStoredProc(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync().ConfigureAwait(false);
                try
                {

                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        private static IList<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }
        public static async Task<DataSet> FillDataset(this DbCommand command)
        {
            DataSet ds = new DataSet();

            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync().ConfigureAwait(false);

                try
                {
                    DbDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;

                    adapter.Fill(ds);
                    return ds;

                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }
}
