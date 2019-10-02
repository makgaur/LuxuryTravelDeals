// <copyright file="Encryption.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Encryption
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// The allowable characters
        /// </summary>
        private const string AllowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Generates the string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>string</returns>
        public static string GenerateString(int length)
        {
            var bytes = new byte[length];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            return new string(bytes.Select(x => AllowableCharacters[x % AllowableCharacters.Length]).ToArray());
        }

        /// <summary>
        /// Calculates the hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>string</returns>
        public string CalculateHash(string input)
        {
            using (var algorithm = SHA512.Create()) // or MD5 SHA256 etc.
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes).Replace("-", string.Empty).ToLower();
            }
        }
    }
}