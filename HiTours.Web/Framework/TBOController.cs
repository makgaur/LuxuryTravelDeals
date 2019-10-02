// <copyright file="TBOController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.TBO.Models;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// TBOController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = Constants.AdminRole)]
    [Authorize(AuthenticationSchemes = Constants.ApplicationAdminCookies)]
    [Area("tbo")]
    public class TBOController : Controller
    {
        /// <summary>
        /// The service urls
        /// </summary>
        protected readonly string serviceUrl;
        private readonly ITBOService tboService;
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TBOController"/> class.
        /// </summary>
        /// <param name="domainSetting">The service urls.</param>
        public TBOController(IOptions<DomainSetting> domainSetting)
        {
            this.serviceUrl = domainSetting.Value.WebApiServiceUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TBOController"/> class.
        /// </summary>
        /// <param name="tboService">The TBO service.</param>
        /// <param name="configuration">Web Configuration</param>
        /// <param name="hostingEnvironment">Hosting Enviorment</param>
        public TBOController(
            ITBOService tboService,
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.tboService = tboService;
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the json ignore nullable.
        /// </summary>
        /// <value>
        /// The json ignore nullable.
        /// </value>
        public JsonSerializerSettings JsonIgnoreNullable
        {
            get { return new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }; }
        }

        /// <summary>
        /// Gets or sets the clip board data.
        /// </summary>
        /// <value>
        /// The clip board data.
        /// </value>
        protected Dictionary<string, object> ClipBoardData { get; set; }

        /// <summary>
        /// Gets or sets the clip board data.
        /// </summary>
        /// <value>
        /// The clip board data.
        /// </value>
        protected Dictionary<string, object> ClearClipBoard { get; set; }

        /// <summary>
        /// Called after the action method is invoked.
        /// </summary>
        /// <param name="context">The action executed context.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (this.ClipBoardData != null && this.ClipBoardData.Count > 0)
            {
                this.ViewBag.ClipBoardData = JsonConvert.SerializeObject(this.ClipBoardData);
            }

            base.OnActionExecuted(context);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <param name="methodName">Method Name</param>
        /// <returns>
        /// Post Request
        /// </returns>
        public HiTours.TBO.Models.ApiResponse PostCustom(string url, string content, string methodName)
        {
            HiTours.TBO.Models.ApiResponse result;
            string responseString = string.Empty;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(content);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseString = streamReader.ReadToEnd();
                }

                result = new TBO.Models.ApiResponse
                {
                    IsSuccess = true,
                    Request = content,
                    Response = responseString,
                    Exception = string.Empty
                };
            }
            catch (Exception ex)
            {
                result = new TBO.Models.ApiResponse
                {
                    IsSuccess = false,
                    Request = content,
                    Response = ex.ToString(),
                    Exception = ex.ToString()
                };
            }

            this.CreateTBOLogs(result, url, methodName);
            return result;
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="result">The URL.</param>
        /// <param name="url">URL</param>
        /// <param name="methodName">The content.</param>
        public void CreateTBOLogs(TBO.Models.ApiResponse result, string url, string methodName)
        {
            try
            {
                if (this.configuration.GetValue<bool>("TBOCredentials:Logging"))
                {
                    //// Logs Code Below
                    string todayFolder = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    string folderPath = Path.Combine(this.hostingEnvironment.WebRootPath, "Logs", "TBO", todayFolder);
                    string fileName = string.Empty;
                    string filePath = string.Empty;
                    if (!Directory.Exists(folderPath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(folderPath);
                    }

                    if (methodName == TboMethods.Authenticate)
                    {
                        fileName = "Login.Txt";
                        filePath = Path.Combine(folderPath, fileName);
                    }
                    else if (methodName == TboMethods.AgencyBalance)
                    {
                        fileName = "Balance.Txt";
                        filePath = Path.Combine(folderPath, fileName);
                    }
                    else
                    {
                        if (result.IsSuccess)
                        {
                            dynamic responseObject = JObject.Parse(result.Response);
                            if (responseObject.Response != null)
                            {
                                fileName = responseObject.Response.TraceId.ToString() + ".Txt";
                            }
                            else
                            {
                                responseObject = JObject.Parse(result.Request);
                                if (responseObject.Response != null && responseObject.Response.TraceId != null)
                                {
                                    fileName = responseObject.Response.TraceId.ToString() + ".Txt";
                                }
                                else
                                {
                                    fileName = "Exceptions.Txt";
                                }
                            }
                        }
                        else
                        {
                            fileName = "Exceptions.Txt";
                        }

                        filePath = Path.Combine(folderPath, fileName);
                    }

                    using (StreamWriter writer = System.IO.File.AppendText(filePath))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine("Request URL :" + url);
                        writer.WriteLine("Method Name :" + methodName);
                        writer.WriteLine();
                        writer.WriteLine("Request");
                        writer.WriteLine(this.FormatJson(result.Request));
                        writer.WriteLine();
                        writer.WriteLine("Response");
                        writer.WriteLine(this.FormatJson(result.Response));
                    }
                }
            }
            catch (Exception ex)
            {
                string filePath = Path.Combine(this.hostingEnvironment.WebRootPath, "Logs\\TBOError.txt");

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);
                        ex = ex.InnerException;
                    }

                    writer.WriteLine();
                    writer.WriteLine("URL : " + url);
                    writer.WriteLine("Method : " + methodName);
                    writer.WriteLine("Result : " + JsonConvert.SerializeObject(result));
                }
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="json">The URL.</param>
        /// <returns>Formated</returns>
        public string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="key">The URL.</param>
        /// <returns>URL</returns>
        public string GetTboUrl(string key)
        {
            return this.configuration.GetValue<string>("TboServiceUrl:" + key);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <returns>LOGIN TOKEN A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<string> GetTBOLoginToken()
        {
            string token = await this.tboService.GetTodayTokenIdAsync();
            if (!string.IsNullOrEmpty(token))
            {
                return token;
            }
            else
            {
                UserCredential userCredential = new UserCredential
                {
                    ClientId = this.configuration.GetValue<string>("TBOCredentials:ClientId"),
                    UserName = this.configuration.GetValue<string>("TBOCredentials:UserName"),
                    Password = this.configuration.GetValue<string>("TBOCredentials:Password"),
                    EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp")
                };
                var apiResponse = this.PostCustom(this.GetTboUrl(TboMethods.Authenticate), JsonConvert.SerializeObject(userCredential), TboMethods.Authenticate);
                if (apiResponse.IsSuccess)
                {
                    var results = JsonConvert.DeserializeObject<AuthenticateResponse>(apiResponse.Response);
                    LoginModel loginModel = new LoginModel
                    {
                        CallDateTime = DateTime.Now,
                        ClientId = userCredential.ClientId,
                        GeneratedDate = DateTime.Now.Date,
                        EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                        Password = userCredential.Password,
                        Response = apiResponse.Response,
                        Token = results.TokenId,
                        Username = userCredential.UserName
                    };
                    await this.tboService.InsertLoginRecordAsync(loginModel);
                    return results.TokenId;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Download file
        /// </returns>
        protected FileResult DownloadFile(string filePath, string fileName)
        {
            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";

            return this.File(readStream, mimeType, fileName);
        }

        /// <summary>
        /// Adds the clip board.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected void AddClipBoard(string key, object value)
        {
            if (this.ClipBoardData == null)
            {
                this.ClipBoardData = new Dictionary<string, object>();
            }

            if (this.ClipBoardData.ContainsKey(key))
            {
                this.ClipBoardData[key] = value;
            }
            else
            {
                this.ClipBoardData.Add(key, value);
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// Post Request
        /// </returns>
        protected async Task<ApiResponse> PostAsync(string url, string content)
        {
            ApiResponse result;
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri uri;
            using (var client = new HttpClient())
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                {
                    result = new ApiResponse
                    {
                        IsSuccess = false,
                        Exception = "Invalid Requested Url.",
                        Response = JsonConvert.SerializeObject(new { Message = "Invalid Requested Url." })
                    };
                }
                else
                {
                    try
                    {
                        var apiResponse = await client.PostAsync(uri, new StringContent(content, Encoding.Default, "application/json"));
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ApiResponse>(response);
                    }
                    catch (Exception ex)
                    {
                        result = new ApiResponse
                        {
                            Exception = ex.Message,
                            Request = content,
                            Response = JsonConvert.SerializeObject(new { Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message })
                        };
                    }
                }
            }

            this.ViewBag.ApiRequest = result.Request;
            this.ViewBag.ApiResponse = result.Response;
            return result;
        }

        ///// <summary>
        ///// Adds the clip board.
        ///// </summary>
        ///// <param name="key">The key.</param>
        ///// <param name="value">The value.</param>
        ////protected void AddApiSearchClipBoard(string key, string value)
        ////{
        ////    if (this.ApiSearchClipBoard == null)
        ////    {
        ////        this.ApiSearchClipBoard = new Dictionary<string, string>();
        ////    }

        ////    if (this.ApiSearchClipBoard.ContainsKey(key))
        ////    {
        ////        this.ApiSearchClipBoard[key] = value;
        ////    }
        ////    else
        ////    {
        ////        this.ApiSearchClipBoard.Add(key, value);
        ////    }
        ////}
    }
}