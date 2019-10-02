// <copyright file="CountryCity.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Country City
    /// </summary>
    public class CountryCity
    {
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>dictionary</returns>
        public static Dictionary<string, List<KeyValuePair<string, string>>> List()
        {
            var list = new Dictionary<string, List<KeyValuePair<string, string>>>
            {
            {
                "India",  new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("jaipur", "Jaipur"),
                new KeyValuePair<string, string>("udaipur", "Udaipur"),
                new KeyValuePair<string, string>("agra", "Agra"),
                new KeyValuePair<string, string>("bhopal", "Bhopal"),
                new KeyValuePair<string, string>("ranthambore", "Ranthambore")
            }
                },
            {
                "Asia",  new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("bali", "Bali"),
                new KeyValuePair<string, string>("bhutan", "Bhutan"),
                new KeyValuePair<string, string>("dubai", "Dubai"),
                new KeyValuePair<string, string>("nepal", "Nepal"),
            }
                }
        };

            return list;
        }
    }
}