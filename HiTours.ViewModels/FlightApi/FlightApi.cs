// <copyright file="FlightApi.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Text;
    using HiTours.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// FlightApi
    /// </summary>
    public static class FlightApi
    {
        /// <summary>
        /// Gets the API response.
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <param name="url">The URL.</param>
        /// <returns>responseXML</returns>
        public static string GetApiResponse(string requestData, string url)
        {
            string responseXML = string.Empty;
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(requestData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Accept-Encoding", "gzip");
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
                WebResponse webResponse = request.GetResponse();
                var rsp = webResponse.GetResponseStream();
                if (rsp == null)
                {
                    // throw exception
                }

                using (StreamReader readStream = new StreamReader(new GZipStream(rsp, CompressionMode.Decompress)))
                {
                    var response = readStream.ReadToEnd();
                    responseXML = response;
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

        /// <summary>
        /// Authenticates this instance.
        /// </summary>
        /// <returns>Json Result Authenticate</returns>
        public static ApiToken Authenticate()
        {
            var url = Constants.BaseUrl + "SharedServices/SharedData.svc/rest/Authenticate";
            var requestData = JsonConvert.SerializeObject(new
            {
                ClientId = Constants.ClientId,
                UserName = Constants.ApiUserName,
                Password = Constants.ApiPassword,
                EndUserIp = Constants.PublicIP
            });
            var result = GetApiResponse(requestData, url);
            return JsonConvert.DeserializeObject<ApiToken>(result);
        }

        /// <summary>
        /// Gets the agency balance.
        /// </summary>
        /// <returns>Json Result Balance</returns>
        public static ApiBalance GetAgencyBalance()
        {
            var api = Authenticate();
            var url = Constants.BaseUrl + "SharedServices/SharedData.svc/rest/GetAgencyBalance";
            var requestData = JsonConvert.SerializeObject(new
            {
                ClientId = Constants.ClientId,
                TokenAgencyId = api.Member.AgencyId,
                TokenMemberId = api.Member.MemberId,
                EndUserIp = Constants.PublicIP,
                TokenId = api.TokenId
            });
            var result = GetApiResponse(requestData, url);
            return JsonConvert.DeserializeObject<ApiBalance>(result);
        }
    }
}