// <copyright file="AdminLayout.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using HiTours.Core;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// LayoutComponent
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class AdminLayout : ViewComponent
    {
        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>Render View Component</returns>
        public async Task<IViewComponentResult> InvokeAsync(Enums.ViewComponent component)
        {
            var response = await Task.Run(() => this.ViewComponent(component));

            return this.View(response);
        }

        /// <summary>
        /// Views the component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>ViewComponent</returns>
        private string ViewComponent(Enums.ViewComponent component)
        {
            string viewName = string.Empty;
            switch (component)
            {
                case Enums.ViewComponent.Header:
                    viewName = "_Header"; break;

                case Enums.ViewComponent.PageHeader:
                    viewName = "_PageHeader"; break;

                case Enums.ViewComponent.LeftMenu:
                    viewName = "_LeftMenu"; break;
            }

            return viewName;
        }
    }
}
