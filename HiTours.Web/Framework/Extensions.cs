// <copyright file="Extensions.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// To the select list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>ToPagged List for Combo box - for use of Select 2 Instance</returns>
        public static dynamic ToPaggedList<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : class
        {
            if (entities == null)
            {
                entities = new List<TEntity>();
            }

            var enumerable = entities as IList<TEntity> ?? entities.ToList();
            return new
            {
                results = enumerable.ToList(),
                pagination = new { more = enumerable.Count >= Constants.ComboPaginationSize }
            };
        }

        /// <summary>
        /// To the select list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>ToPagged List for Combo box - for use of Select 2 Instance</returns>
        public static List<SelectListItem> ToSelectList<TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : Dropdown
        {
            return entities?.Select(x => new SelectListItem() { Value = x.Id, Text = x.Name }).ToList() ?? new List<SelectListItem>();
        }

        /////// <summary>
        /////// To the amount.
        /////// </summary>
        /////// <param name="amount">The amount.</param>
        /////// <returns>ToAmount</returns>
        ////public static string ToAmount(this decimal amount)
        ////{
        ////    return amount.ToString("0.00").Replace(".00", string.Empty);
        ////}

        /// <summary>
        /// To the amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="upperRoundUp">if set to <c>true</c> [upper round up].</param>
        /// <returns>
        /// Convert Value to Formatted Value
        /// </returns>
        public static string ToAmount(this object amount, bool upperRoundUp = false)
        {
            decimal.TryParse(Convert.ToString(amount), out decimal value);

            if (upperRoundUp)
            {
                value = Math.Ceiling(value);
            }

            return value.ToString("N").Replace(".00", string.Empty);
        }

        /// <summary>
        /// To the currency amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>ToCurrencyAmount</returns>
        public static string ToCurrencyAmount(this object amount)
        {
            decimal value = 0;

            decimal.TryParse(Convert.ToString(amount), out value);
            return string.Format("{0:n}", value).Replace(".00", string.Empty);
        }

        /// <summary>
        /// Validates the URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>ValidateUrl</returns>
        public static string ToUrl(this string url)
        {
            return (url ?? string.Empty).Replace("//", "/").Replace("~", string.Empty);
        }

        /// <summary>
        /// Gets the admin URL.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <returns>GetAdminUrl</returns>
        public static string GetAdminUrl(this IUrlHelper urlHelper, string controller, string action)
        {
            return urlHelper.RouteUrl(Constants.RouteArea, new { controller = controller, action = action, area = Constants.AreaAdmin });
        }

        /// <summary>
        /// Enums the select list.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="htmlUrlHelper">The HTML URL helper.</param>
        /// <param name="isTextDisplayName">Show Display Attribute instance of Text</param>
        /// <param name="isValueInteger">if set to <c>true</c> [is value integer].</param>
        /// <returns>
        /// EnumSelectList
        /// </returns>
        public static List<SelectListItem> EnumSelectList<TEnum>(this IHtmlHelper htmlUrlHelper, bool isTextDisplayName = false, bool isValueInteger = true)
        {
            var type = typeof(TEnum);
            List<SelectListItem> enumList = new List<SelectListItem>();
            foreach (TEnum data in Enum.GetValues(typeof(TEnum)))
            {
                var items = Enum.GetValues(typeof(TEnum)).Cast<Enum>().Where(x => x.ToString() == data.ToString());
                foreach (var item in items)
                {
                    enumList.Add(new SelectListItem()
                    {
                        Text = isTextDisplayName ? item.GetDisplayName() : item.ToString(),
                        Value = (isValueInteger ? ((int)Enum.Parse(typeof(TEnum), item.ToString())).ToString() : item.ToString()).Replace("_", string.Empty)
                    });
                }
            }

            return enumList;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Get Display Name Attribute Value</returns>
        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();
            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
            {
                return string.Empty;
            }

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
            return attributes == null ? string.Empty : ((DisplayNameAttribute)attributes).DisplayName;
        }
    }
}