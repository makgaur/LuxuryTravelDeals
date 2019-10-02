// <copyright file="ZohoUpdate.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services.Zoho
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using HiTours.ViewModels.FlightApi;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Zoho record Update
    /// </summary>
    public static class ZohoUpdate
    {
        /// <summary>
        /// Authenticates this instance.
        /// </summary>
        /// <returns>Json Result Authenticate</returns>
        public static string Authenticate()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://accounts.zoho.com/oauth/v2/token?refresh_token=1000.4f28ecf7e79dcfff4dab6cc5546f38d1.9b93a8ebc57a8daddd067f7d835d2616&client_id=1000.63K8BHFD2B6353167J78VCCRBYZ5UH&client_secret=89597d69ecaa83c52965f4b8ffcedd7d58a9624e51&grant_type=refresh_token");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string access_token = string.Empty;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic data = JObject.Parse(result);
                access_token = data.access_token;
            }

            return access_token;
        }

        /// <summary>
        /// Gets the API response.
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <param name="url">The URL.</param>
        /// <returns>responseXML</returns>
        public static string GetApiResponse(string requestData, string url)
        {
            var authorization = ZohoUpdate.Authenticate();
            string responseXML = string.Empty;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add("Authorization", "Zoho-oauthtoken " + authorization);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    responseXML = result;
                }
            }
            catch (WebException webEx)
            {
                // get the response stream
                WebResponse response = webEx.Response;
                Stream stream = response.GetResponseStream();
                string responseMessage = new StreamReader(stream).ReadToEnd();
            }

            return responseXML;
        }
    }
}
