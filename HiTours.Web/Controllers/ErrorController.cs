// <copyright file="ErrorController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>
namespace HiTours.Web.Controllers
{
    using System;
    using AutoMapper;
    using HiTours.Services;
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Custom Error Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// Initialize Controller.
        /// </summary>
        /// <param name="configuration">The Web Config Service.</param>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="countryService">Country Service</param>
        /// <param name="cityService">City Service</param>
        /// <param name="mapper">Auto Mapper</param>
        /// <param name="stateService">State Service</param>
        /// <returns>
        /// error statuss
        /// </returns>
        public ErrorController(IConfiguration configuration, IHomePageService homePageService, ICountryService countryService, ICityService cityService, IMapper mapper, IStateService stateService)
           : base(mapper, homePageService, cityService, countryService, configuration, stateService)
        {
        }

        /// <summary>
        /// Indexes the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>
        /// error statuss
        /// </returns>
        [Route("/Error/StatusCode/{errorCode}")]
        public IActionResult Index(int errorCode)
        {
            ErrorViewModel model = new ErrorViewModel
            {
                ErrorCode = errorCode
            };
            ////var exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            ////var statusCode = this.HttpContext.Response.StatusCode;
            ////var message = exception != null ? exception.Error.Message : string.Empty;
            ////var stackTrace = exception != null ? exception.Error.StackTrace : string.Empty;
            ////return this.View(new ErrorHandler
            ////{
            ////    Message = message,
            ////    StatusCode = statusCode,
            ////    ErrorCode = errorCode,
            ////    StackTrace = stackTrace
            ////});
            return this.View("ErrorPage", model);
        }

        /// <summary>
        /// Errors the partial.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>partial</returns>
        public IActionResult PartialError(int errorCode)
        {
            var exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = this.HttpContext.Response.StatusCode;
            var message = exception != null ? exception.Error.Message : string.Empty;
            var stackTrace = exception != null ? exception.Error.StackTrace : string.Empty;
            return this.View(new ErrorHandler
            {
                Message = message,
                StatusCode = statusCode,
                ErrorCode = errorCode,
                StackTrace = stackTrace,
                Layout = null
            });
        }
    }
}