// <copyright file="IHttpClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HiTours.TBO
{
    using System.Threading.Tasks;
    using HiTours.Core.Api;

    /// <summary>
    /// IHttpClientHelper
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>Post Request</returns>
        Task<Response> PostAsync(string url, string content);
    }
}