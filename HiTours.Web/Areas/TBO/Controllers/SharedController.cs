// <copyright file="SharedController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.TBO.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.TBO.Models;
    using HiTours.ViewModels;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// AuthenticationController
    /// </summary>
    /// <seealso cref="HiTours.Web.TBOController" />
    public class SharedController : TBOController
    {
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedController" /> class.
        /// </summary>
        /// <param name="domainSetting">The service urls.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public SharedController(IOptions<DomainSetting> domainSetting, IHostingEnvironment hostingEnvironment)
            : base(domainSetting)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index</returns>
        public IActionResult Authenticate()
        {
            return this.View(new UserCredential());
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="jsonData">The json data.</param>
        /// <returns> GetAdvanceSearchSegments </returns>
        [HttpPost]
        public IActionResult GetAdvanceSearchSegments(string jsonData)
        {
            AirSearchResult airSearchResult = null;
            if (!string.IsNullOrEmpty(jsonData))
            {
                var apiAirSearchResult = JsonConvert.DeserializeObject<List<AirSearchResult>>(jsonData);
                if (apiAirSearchResult != null && apiAirSearchResult.Count > 0)
                {
                    var firstIndex = apiAirSearchResult.FirstOrDefault();

                    airSearchResult = firstIndex;
                    if (firstIndex.Segments != null && firstIndex.Segments.Length > 0)
                    {
                        airSearchResult.CustomSegments = firstIndex.Segments[0];
                    }
                }
            }

            return this.PartialView("_AirSearchResult", airSearchResult ?? new AirSearchResult());
        }

        /// <summary>
        /// Authenticates the specified user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>Authenticate</returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate(UserCredential userCredential)
        {
            var url = $"{this.serviceUrl}/Authentication/Login";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(userCredential));
            if (apiResponse.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<AuthenticateResponse>(apiResponse.Response);
                if (result != null && result.Status == (int)AuthenticateStatus.Successful)
                {
                    this.AddClipBoard(nameof(userCredential.ClientId), userCredential.ClientId);
                    this.AddClipBoard(nameof(userCredential.EndUserIp), userCredential.EndUserIp);
                    this.AddClipBoard(nameof(result.TokenId), result.TokenId);
                    if (result.Member != null)
                    {
                        this.AddClipBoard(nameof(result.Member.AgencyId), result.Member.AgencyId.ToString());
                        this.AddClipBoard(nameof(result.Member.MemberId), result.Member.MemberId.ToString());
                    }
                }
            }

            return this.View(userCredential);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>Logout Logged in Api User</returns>
        public IActionResult Logout()
        {
            return this.View(new Logout());
        }

        /// <summary>
        /// Authenticates the specified user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>Authenticate</returns>
        [HttpPost]
        public async Task<IActionResult> Logout(Logout userCredential)
        {
            var url = $"{this.serviceUrl}/Authentication/Logout";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(userCredential));
            if (apiResponse.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<LogoutResponse>(apiResponse.Response);
                if (result != null && result.Status == (int)AuthenticateStatus.Successful)
                {
                    this.ViewBag.ClearClipBoard = true;
                }
            }

            return this.View(userCredential);
        }

        /// <summary>
        /// Gets the agency balance.
        /// </summary>
        /// <returns>GetAgencyBalance</returns>
        public IActionResult GetAgencyBalance()
        {
            return this.View(new AgencyBalance());
        }

        /// <summary>
        /// Gets the agency balance.
        /// </summary>
        /// <param name="agencybalance">The agencybalance.</param>
        /// <returns>
        /// GetAgencyBalance
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> GetAgencyBalance(AgencyBalance agencybalance)
        {
            if (agencybalance != null)
            {
                var url = $"{this.serviceUrl}/Account/GetAgencyBalance";
                var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(agencybalance));
                return this.View(agencybalance);
            }

            return this.View();
        }

        /// <summary>
        /// Adds the new segment.
        /// </summary>
        /// <returns>Add New Segment</returns>
        public IActionResult AddNewSegment()
        {
            return this.PartialView("_Segment", new Segments());
        }

        /// <summary>
        /// Downloads the json.
        /// </summary>
        /// <param name="jsonSchema">The json schema.</param>
        /// <returns>
        /// Download Json
        /// </returns>
        [HttpPost]
        public IActionResult DownloadJsonFile(JsonSchema jsonSchema)
        {
            string wwwrootPath = this.hostingEnvironment.WebRootPath;
            var webapiPath = $"{wwwrootPath}/TBO-Services/{jsonSchema.Action}";
            string fileName = $"{jsonSchema.Action}_{DateTime.UtcNow.Ticks}.json";
            if (!Directory.Exists(webapiPath))
            {
                Directory.CreateDirectory(webapiPath);
            }

            using (StreamWriter jsonFile = new StreamWriter(System.IO.File.Create(Path.Combine(webapiPath, fileName))))
            {
                var sb = new StringBuilder();
                sb.Append($"//-- {jsonSchema.Action} Api Request --{Environment.NewLine}");
                sb.Append(JToken.Parse(jsonSchema.Request).ToString(Formatting.Indented));
                sb.Append($"{Environment.NewLine}//-- {jsonSchema.Action} Api Response --{Environment.NewLine}");
                sb.Append(JValue.Parse(jsonSchema.Response).ToString(Formatting.Indented));
                jsonFile.WriteLine(sb.ToString());
            }

            return this.DownloadFile(webapiPath, fileName);
        }
    }
}