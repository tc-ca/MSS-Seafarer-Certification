namespace CSF.Web.Client.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GoC.WebTemplate.Components.Core.Services;
    using GoC.WebTemplate.Components.Entities;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The middle ware for the page settings.
    /// </summary>
    public class PageSettingsMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSettingsMiddleware"/> class.
        /// </summary>
        /// <param name="next">The request.</param>
        public PageSettingsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Adds to the page settings.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="modelAccessor">The ModelAccessor.</param>
        /// <returns>Task with the modified page settings.</returns>
        public async Task InvokeAsync(HttpContext context, ModelAccessor modelAccessor)
        {
            // Perhaps we can fail more gracefully then just throwing an exception
            if (modelAccessor == null)
            {
                throw new ArgumentNullException(nameof(modelAccessor));
            }

            // add page settings like: Modified Date, Breadcrumbs, Culture, Title etc.
            modelAccessor.Model.DateModified = DateTime.Now.Date;
            modelAccessor.Model.HeaderTitle = "Seafarer Credentials Online Prototype";
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(new Breadcrumb { Href = "http://www.canada.ca/en/index.html", Title = "Canada.ca" });
            breadcrumbs.Add(new Breadcrumb { Href = "https://www.tc.gc.ca/en/services/marine.html", Title = "Marine Transportation" });
            breadcrumbs.Add(new Breadcrumb { Href = "/", Title = "Seafarer Credentials Online Prototype" });
            modelAccessor.Model.Breadcrumbs = breadcrumbs;
            modelAccessor.Model.LeftMenuItems = new List<MenuSection>();

            await this.next.Invoke(context).ConfigureAwait(false);
        }
    }
}