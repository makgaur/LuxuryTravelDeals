// <copyright file="HttpClient.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HiTours.TBO
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using HiTours.Core.Api;

    /// <summary>
    /// Web Client Helper
    /// </summary>
    public class HttpClient : IHttpClient
    {
        /// <summary>
        /// The content type
        /// </summary>
        private const string ContentType = "application/json";

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>Post Request</returns>
        public async Task<Response> PostAsync(string url, string content)
        {
            var result = new Response();
            Uri uri;
            using (var client = new System.Net.Http.HttpClient())
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                {
                    result.IsSuccess = false;
                    result.Exception = "Invalid Url";
                    return result;
                }

                this.SetHeaders(client);
                var apiResponse = await client.PostAsync(uri, new StringContent(content, Encoding.UTF8, ContentType), new CancellationToken(false));
                var stream = await apiResponse.Content.ReadAsStreamAsync();
                if (apiResponse.IsSuccessStatusCode)
                {
                    using (StreamReader readStream = new StreamReader(new GZipStream(stream, CompressionMode.Decompress)))
                    {
                        try
                        {
                            result.Content = await readStream.ReadToEndAsync();
                            result.IsSuccess = true;
                        }
                        catch (Exception ex)
                        {
                            result.IsSuccess = false;
                            result.Exception = ex.Message;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Sets the headers.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        private void SetHeaders(System.Net.Http.HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
        }
    }
}