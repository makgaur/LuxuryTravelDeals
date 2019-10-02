// <copyright file="DataTableModelBinder.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// DataTableModelBinder
    /// </summary>
    public class DataTableModelBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
        /// <returns>
        /// <para>
        /// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
        /// </para>
        /// <para>
        /// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
        /// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
        /// </para>
        /// <para>
        /// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
        /// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
        /// </para>
        /// </returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (bindingContext.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                try
                {
                    var request = bindingContext.HttpContext.Request.Form;

                    int draw = Convert.ToInt32(request["draw"]);
                    int start = Convert.ToInt32(request["start"]);
                    int length = Convert.ToInt32(request["length"]);

                    var search = new DataTableSearch
                    {
                        Value = request["search[value]"],
                        Regex = Convert.ToBoolean(request["search[regex]"])
                    };

                    var o = 0;
                    var order = new List<DataTableOrder>();

                    while (request["order[" + o + "][column]"].Count > 0)
                    {
                        order.Add(new DataTableOrder()
                        {
                            Column = Convert.ToInt32(request["order[0][column]"].ToList()[0]),
                            Dir = request["order[" + o + "][dir]"]
                        });
                        o++;
                    }

                    // Columns
                    var c = 0;
                    var columns = new List<DataTableColumn>();
                    while (request["columns[" + c + "][name]"].Count > 0)
                    {
                        columns.Add(new DataTableColumn
                        {
                            Data = request["columns[" + c + "][data]"][0],
                            Name = request["columns[" + c + "][name]"][0],
                            Orderable = Convert.ToBoolean(request["columns[" + c + "][orderable]"][0]),
                            Search = new DataTableSearch
                            {
                                Value = request["columns[" + c + "][search][value]"][0],
                                Regex = Convert.ToBoolean(request["columns[" + c + "][search][regex]"][0])
                            }
                        });
                        c++;
                    }

                    var result = new DataTableParameter
                    {
                        Draw = draw,
                        Start = start,
                        Length = length,
                        Search = search,
                        Order = order,
                        Columns = columns
                    };
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return Task.CompletedTask;
        }
    }
}