// <copyright file="IViewRenderService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// IViewRenderService
    /// </summary>
    public interface IViewRenderService
    {
        /// <summary>
        /// Renders to string asynchronous.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>string</returns>
        Task<string> RenderToStringAsync(string viewName, object model);
    }

    /// <summary>
    /// ViewRenderService
    /// </summary>
    /// <seealso cref="HiTours.Web.IViewRenderService" />
    public class ViewRenderService : IViewRenderService
    {
        /// <summary>
        /// The razor view engine
        /// </summary>
        private readonly IRazorViewEngine razorViewEngine;

        /// <summary>
        /// The temporary data provider
        /// </summary>
        private readonly ITempDataProvider tempDataProvider;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRenderService"/> class.
        /// </summary>
        /// <param name="razorViewEngine">The razor view engine.</param>
        /// <param name="tempDataProvider">The temporary data provider.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public ViewRenderService(
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            this.razorViewEngine = razorViewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Renders to string asynchronous.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>string</returns>
        /// <exception cref="ArgumentNullException">exception</exception>
        public async Task<string> RenderToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = this.serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = this.razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, this.tempDataProvider),
                    sw,
                    new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}