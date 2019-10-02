// <copyright file="SpecialChars.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    /// <summary>
    /// SpecialChars
    /// </summary>
    public class SpecialChars
    {
        /// <summary>
        /// Removes the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>string</returns>
        public static string Remove(string str)
        {
            // Create  a string array and add the special characters you want to remove
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
            }

            string[] chars = new string[] { "   ", "  ", " ", ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };

            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "-");
                }
            }

            return str.ToLower().Replace("----", "-").Replace("---", "-").Replace("--", "-");
        }
    }
}