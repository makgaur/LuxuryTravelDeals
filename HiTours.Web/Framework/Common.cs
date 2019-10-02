// <copyright file="Common.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Common
    /// </summary>
    public class Common
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="items">The selectitems.</param>
        /// <param name="value">The identifier.</param>
        /// <returns>Get Text From Dropdown</returns>
        public static string GetText(IEnumerable<SelectListItem> items, object value)
        {
            var text = string.Empty;
            var selectListItems = items == null ? new List<SelectListItem>().ToArray() : items as SelectListItem[] ?? items.ToArray();
            if (selectListItems.Any())
            {
                var record = selectListItems.FirstOrDefault(m => m.Value.ToLower() == Convert.ToString(value));
                if (record != null)
                {
                    text = record.Text;
                }
            }

            return text;
        }
    }
}