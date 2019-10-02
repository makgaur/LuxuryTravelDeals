// <copyright file="BaseApiController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HiTours.TBO
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// BaseApiController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseApiController : Controller
    {
        /// <summary>
        /// The client helper
        /// </summary>
        protected readonly IHttpClient httpClient;

        /// <summary>
        /// The service urls
        /// </summary>
        protected readonly ServiceUrls serviceUrls;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController" /> class.
        /// </summary>
        /// <param name="serviceUrls">The service urls.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public BaseApiController(IOptions<ServiceUrls> serviceUrls, IHttpClient httpClient)
        {
            this.serviceUrls = serviceUrls.Value;
            this.httpClient = httpClient;
        }
    }
}