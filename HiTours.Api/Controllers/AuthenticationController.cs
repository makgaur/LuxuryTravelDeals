// <copyright file="AuthenticationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HiTours.TBO.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using HiTours.TBO.Contracts;

    /// <summary>
    /// AuthenticationController
    /// </summary>
    /// <seealso cref="HiTours.TBO.BaseApiController" />
    [Route("api/[controller]")]
    public class AuthenticationController : BaseApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="serviceUrls">The service urls.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public AuthenticationController(IOptions<ServiceUrls> serviceUrls, IHttpClient httpClient)
            : base(serviceUrls, httpClient)
        {
        }

        /// <summary>
        /// Logins the specified user credential.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>userCredential</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<JsonResult> Login([FromBody] IUserCredential userCredential)
        {
            var apiResponse = await this.httpClient.PostAsync(this.serviceUrls.Authenticate, JsonConvert.SerializeObject(userCredential));
            var apiResult = JsonConvert.DeserializeObject<dynamic>(apiResponse.Content);
            return this.Json(apiResult);
        }

        /// <summary>
        /// Logouts the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Logout Loggedin  User By Token</returns>
        [HttpPost]
        [Route("[action]")]
        public JsonResult Logout([FromBody] string token)
        {
            return this.Json(new { Status = "ok" });
        }
    }
}