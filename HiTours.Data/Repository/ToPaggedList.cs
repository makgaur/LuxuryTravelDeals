// <copyright file="ToPaggedList.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.Repository
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using HiTours.Core;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// To PaggedList
    /// </summary>
    public abstract class ToPaggedList
    {
        /// <summary>
        /// To the pagged list asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="model">The model.</param>
        /// <returns>ToPaggedListAsync</returns>
        public async Task<DataTableResult> ToPaggedListAsync<TEntity>(IQueryable<TEntity> query, DataTableParameter model)
            where TEntity : class
        {
            DataTableResult dt = new DataTableResult();

            foreach (var item in model.Columns)
            {
                var col = item.Data;
                if (!string.IsNullOrEmpty(item.Search.Value))
                {
                    string spliter = "@#$";
                    var values = item.Search.Value.Split(new string[] { spliter }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        var names = item.Name.Split(new string[] { spliter }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        for (int i = 0; i < values.Count; i++)
                        {
                            var value = values[i];
                            var colType = this.GetColumnType<TEntity>(col);
                            var searchType = names[i];
                            switch (colType)
                            {
                                case "int":
                                case "int64":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToInt32(value), searchType);
                                    break;

                                case "short":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToInt32(value), searchType);
                                    break;

                                case "decimal":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToDecimal(value), searchType);
                                    break;

                                case "datetime":
                                    var searchDate = Convert.ToDateTime(value);

                                    if (Convert.ToDateTime(value).TimeOfDay == System.TimeSpan.FromSeconds(0) && searchType == "<=")
                                    {
                                        searchDate = searchDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                                    }

                                    query = this.WhereHelper<TEntity>(query, col, searchDate, searchType);
                                    break;

                                case "bool":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToBoolean(value), searchType);
                                    break;

                                case "string":
                                    query = this.WhereHelper<TEntity>(query, col, value, searchType, false);
                                    break;
                            }
                        }
                    }
                }
            }

            ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(TEntity), string.Empty) };

            if (model.Order.Count() > 0)
            {
                var orderByField = model.Columns.ToList()[model.Order.FirstOrDefault().Column].Data;
                System.Reflection.PropertyInfo pi = typeof(TEntity).GetProperty(orderByField);
                if (pi == null)
                {
                    pi = typeof(TEntity).GetProperty(model.Columns.FirstOrDefault(m => m.Data != string.Empty).Data);
                }

                query = query.Provider.CreateQuery<TEntity>(Expression.Call(typeof(Queryable), model.Order.FirstOrDefault().Dir == "asc" ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), pi.PropertyType }, query.Expression, Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
            }

            try
            {
                if (model.Length == -1)
                {
                    dt.Data = (await query.ToListAsync()).ToList<object>();
                }
                else
                {
                    dt.Data = (await query.Skip(model.Start).Take(model.Length).ToListAsync()).ToList<object>();
                }

                dt.RecordsTotal = await query.CountAsync();
                dt.RecordsFiltered = dt.RecordsTotal;
                dt.Draw = model.Draw;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        /// <summary>
        /// To the pagged list synchronize.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="model">The model.</param>
        /// <returns>Pagging List Records</returns>
        public DataTableResult ToPaggedListSync<TEntity>(IQueryable<TEntity> query, DataTableParameter model)
            where TEntity : class
        {
            DataTableResult dt = new DataTableResult();

            foreach (var item in model.Columns)
            {
                var col = item.Data;
                if (!string.IsNullOrEmpty(item.Search.Value))
                {
                    string spliter = "@#$";
                    var values = item.Search.Value.Split(new string[] { spliter }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        var names = item.Name.Split(new string[] { spliter }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        for (int i = 0; i < values.Count; i++)
                        {
                            var value = values[i];
                            var colType = this.GetColumnType<TEntity>(col);
                            var searchType = names[i];
                            switch (colType)
                            {
                                case "int":
                                case "int64":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToInt32(value), searchType);
                                    break;

                                case "short":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToInt32(value), searchType);
                                    break;

                                case "decimal":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToDecimal(value), searchType);
                                    break;

                                case "datetime":
                                    var searchDate = Convert.ToDateTime(value);
                                    if (Convert.ToDateTime(value).TimeOfDay == System.TimeSpan.FromSeconds(0) && searchType == "<=")
                                    {
                                        searchDate = searchDate.AddHours(24);
                                    }

                                    query = this.WhereHelper<TEntity>(query, col, searchDate, searchType);
                                    break;

                                case "bool":
                                    query = this.WhereHelper<TEntity>(query, col, Convert.ToBoolean(value), searchType);
                                    break;

                                case "string":
                                    query = this.WhereHelper<TEntity>(query, col, value, searchType, false);
                                    break;
                            }
                        }
                    }
                }
            }

            ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(TEntity), string.Empty) };

            if (model.Order.Any())
            {
                var orderByField = model.Columns.ToList()[model.Order.FirstOrDefault().Column].Data;
                System.Reflection.PropertyInfo pi = typeof(TEntity).GetProperty(orderByField);
                if (pi == null)
                {
                    pi = typeof(TEntity).GetProperty(model.Columns.FirstOrDefault(m => m.Data != string.Empty).Data);
                }

                query = query.Provider.CreateQuery<TEntity>(Expression.Call(typeof(Queryable), model.Order.FirstOrDefault().Dir == "asc" ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), pi.PropertyType }, query.Expression, Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
            }

            dt.Data = model.Length == -1 ? query.ToList().ToList<object>() : query.Skip(model.Start).Take(model.Length).ToList().ToList<object>();
            dt.RecordsTotal = query.Count();
            dt.RecordsFiltered = dt.RecordsTotal;
            dt.Draw = model.Draw;
            return dt;
        }

        /// <summary>
        /// Gets the type of the column.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Get Column Data Type</returns>
        private string GetColumnType<TEntity>(string columnName)
            where TEntity : class
        {
            string columnType = string.Empty;

            foreach (PropertyDescriptor propertyInfo in TypeDescriptor.GetProperties(typeof(TEntity)))
            {
                if (propertyInfo.Name == columnName)
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        columnType = "string";
                    }
                    else if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
                    {
                        columnType = "int";
                    }
                    else if (propertyInfo.PropertyType == typeof(short) || propertyInfo.PropertyType == typeof(short?))
                    {
                        columnType = "short";
                    }
                    else if (propertyInfo.PropertyType == typeof(long) || propertyInfo.PropertyType == typeof(long?))
                    {
                        columnType = "int64";
                    }
                    else if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                    {
                        columnType = "decimal";
                    }
                    else if (propertyInfo.PropertyType == typeof(short))
                    {
                        columnType = "int";
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                    {
                        columnType = "datetime";
                    }
                    else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
                    {
                        columnType = "bool";
                    }
                }
            }

            return columnType;
        }

        /// <summary>
        /// Wheres the helper.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="value">The value.</param>
        /// <param name="filterType">Type of the filter.</param>
        /// <param name="isFromDB">if set to <c>true</c> [is from database].</param>
        /// <returns>Create Where Helpers</returns>
        private IQueryable<TEntity> WhereHelper<TEntity>(IQueryable<TEntity> source, string columnName, object value, string filterType, bool isFromDB = true)
        {
            ParameterExpression table = Expression.Parameter(typeof(TEntity), string.Empty);
            Expression column = Expression.PropertyOrField(table, columnName);
            Expression valueExpression = Expression.Convert(Expression.Constant(value), column.Type);
            Expression where = null;
            try
            {
                switch (filterType)
                {
                    case "<":
                        where = Expression.LessThan(column, valueExpression);
                        break;

                    case "<=":
                        where = Expression.LessThanOrEqual(column, valueExpression);
                        break;

                    case "=":
                        where = Expression.Equal(column, valueExpression);
                        break;

                    case ">":
                        where = Expression.GreaterThan(column, valueExpression);
                        break;

                    case ">=":
                        where = Expression.GreaterThanOrEqual(column, valueExpression);
                        break;

                    case "!=":
                        where = Expression.NotEqual(column, valueExpression);
                        break;

                    case "contains":
                        where = isFromDB
                             ? Expression.Call(column, typeof(string).GetMethod("Contains"), valueExpression)
                             : where = Expression.Call(Expression.Call(column, "ToUpper", null), "Contains", null, Expression.Convert(Expression.Constant(value.ToString().ToUpper()), column.Type));
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
            }

            Expression lambda = Expression.Lambda(where, new ParameterExpression[] { table });

            Type[] exprArgTypes = { source.ElementType };

            MethodCallExpression methodCall = Expression.Call(typeof(Queryable), "Where", exprArgTypes, source.Expression, lambda);

            return (IQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(methodCall);
        }
    }
}