// <copyright file="HtmlHelperExtensions.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Newtonsoft.Json;

    /// <summary>
    /// Extension Methods Mvc Rendering
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Automatics the complete for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// AutoCompleteFor
        /// </returns>
        /// <exception cref="ArgumentNullException">htmlHelper
        /// or
        /// expression</exception>
        public static IHtmlContent AutoCompleteFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, AutoCompleteHtml(new AutoComplete(), htmlAttributes));
        }

        /// <summary>
        /// Automatics the complete for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="autoComplete">The automatic complete.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// AutoCompleteFor custom class
        /// </returns>
        /// <exception cref="ArgumentNullException">htmlHelper
        /// or
        /// expression</exception>
        public static IHtmlContent AutoCompleteFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, AutoComplete autoComplete, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, AutoCompleteHtml(autoComplete, htmlAttributes));
        }

        /// <summary>
        /// Buttons the top.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="model">The model.</param>
        /// <returns>Render UI Button For Top Section</returns>
        public static IHtmlContent ButtonTop<TEntity>(this IHtmlHelper htmlHelper, IEnumerable<TEntity> model)
            where TEntity : Button
        {
            return htmlHelper.Partial("_ButtonTop", model == null ? new List<TEntity>() : model);
        }

        /// <summary>
        /// Buttons the bottom.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="model">The model.</param>
        /// <returns>Render UI Button For Bottom Section</returns>
        public static IHtmlContent ButtonBottom<TEntity>(this IHtmlHelper htmlHelper, IEnumerable<TEntity> model)
            where TEntity : Button
        {
            return htmlHelper.Partial("_ButtonBottomBox", model == null ? new List<TEntity>() : model);
        }

        /// <summary>
        /// Buttons the bottom.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="model">The model.</param>
        /// <returns>Render UI Button For Bottom Section</returns>
        public static IHtmlContent ButtonBottomForm<TEntity>(this IHtmlHelper htmlHelper, IEnumerable<TEntity> model)
            where TEntity : Button
        {
            return htmlHelper.Partial("_ButtonBottomForm", model == null ? new List<TEntity>() : model);
        }

        /// <summary>
        /// Automatics the complete HTML.
        /// </summary>
        /// <param name="autoComplete">The automatic complete.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Dictionary Values</returns>
        private static IDictionary<string, object> AutoCompleteHtml(AutoComplete autoComplete, object htmlAttributes)
        {
            var htmlAttributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var dataKey = "data-pluggin-select22";
            object keyValue = string.Empty;

            if (autoComplete != null && autoComplete.Disabled)
            {
                htmlAttributesDictionary.Add("disabled", "disabled");
            }

            if (!htmlAttributesDictionary.TryGetValue(dataKey, out keyValue))
            {
                htmlAttributesDictionary.Add(dataKey, JsonConvert.SerializeObject(autoComplete));
            }

            return htmlAttributesDictionary;
        }
    }
}