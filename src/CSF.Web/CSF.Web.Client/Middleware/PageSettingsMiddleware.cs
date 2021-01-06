namespace CSF.Web.Client.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSF.Web.Client.Utilities;
    using GoC.WebTemplate.Components.Core.Services;
    using GoC.WebTemplate.Components.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The middle ware for the page settings.
    /// </summary>
    public class PageSettingsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        private readonly BreadcrumbOptions canadaBreadcrumbOptions = new BreadcrumbOptions();
        private readonly BreadcrumbOptions marineBreadcrumbOptions = new BreadcrumbOptions();
        private readonly BreadcrumbOptions csfBreadcrumbOptions = new BreadcrumbOptions();

        /// <summary>
        /// Initializes a new instance of the <see cref="PageSettingsMiddleware"/> class.
        /// </summary>
        /// <param name="next">The request.</param>
        /// <param name="configuration">Application configuration.</param>
        public PageSettingsMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;

            configuration?.GetSection(BreadcrumbOptions.CanadaBreadcrumb).Bind(this.canadaBreadcrumbOptions);
            configuration?.GetSection(BreadcrumbOptions.MarineBreadcrumb).Bind(this.marineBreadcrumbOptions);
            configuration?.GetSection(BreadcrumbOptions.CSFBreadcrumb).Bind(this.csfBreadcrumbOptions);
        }

        /// <summary>
        /// Adds to the page settings.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="modelAccessor">The ModelAccessor.</param>
        /// <returns>Task with the modified page settings.</returns>
        public async Task InvokeAsync(HttpContext context, ModelAccessor modelAccessor)
        {
            if (modelAccessor == null)
            {
                throw new ArgumentNullException(nameof(modelAccessor));
            }

            // add page settings like: Modified Date, Breadcrumbs, Culture, Title etc.
            modelAccessor.Model.HeaderTitle = this.configuration.GetSection("GoCWebTemplate")["HeaderTitle"];
            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb { Href = this.canadaBreadcrumbOptions.Href, Title = this.canadaBreadcrumbOptions.Title },
                new Breadcrumb { Href = this.marineBreadcrumbOptions.Href, Title = this.marineBreadcrumbOptions.Title },
                new Breadcrumb { Href = this.csfBreadcrumbOptions.Href, Title = this.csfBreadcrumbOptions.Title },
            };
            modelAccessor.Model.Breadcrumbs = breadcrumbs;
            modelAccessor.Model.LeftMenuItems = new List<MenuSection>();

            await this.next.Invoke(context).ConfigureAwait(false);
        }
    }
}