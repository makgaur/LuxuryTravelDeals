using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HiTours.Api.Common
{
    /// <summary>
    /// Creates a unique key representing an object
    /// </summary>
    public class KeyCreator
    {
        /// <summary>
        /// Creates a key based on MD5 hashing of the Json representation of the object
        /// </summary>
        /// <param name="value">The object to serialize and hash</param>
        /// <returns>The hashed string modified for use in url</returns>
        public static string Create(object value)
        {
            var serialized = JsonConvert.SerializeObject(value);

            var md5 = MD5.Create();
            var byteArray = Encoding.UTF8.GetBytes(serialized);

            var hashedValue = Convert.ToBase64String(md5.ComputeHash(byteArray));

            // Replaces values to make it url friendly
            hashedValue = hashedValue.Replace("/", "_");
            hashedValue = hashedValue.Replace("+", "-");
            var substring = hashedValue.Substring(0, 22);

            return substring;
        }
    }
}
