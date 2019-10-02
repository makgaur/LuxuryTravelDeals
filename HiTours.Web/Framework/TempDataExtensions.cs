// <copyright file="TempDataExtensions.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Newtonsoft.Json;

    /// <summary>
    /// TempDataExtensions
    /// </summary>
    public static class TempDataExtensions
    {
        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T">t</typeparam>
        /// <param name="tempData">The temporary data.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
            where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T">t</typeparam>
        /// <param name="tempData">The temporary data.</param>
        /// <param name="key">The key.</param>
        /// <returns>type</returns>
        public static T Get<T>(this ITempDataDictionary tempData, string key)
            where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}