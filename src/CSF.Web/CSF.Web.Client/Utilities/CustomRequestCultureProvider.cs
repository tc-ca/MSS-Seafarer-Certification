namespace CSF.Web.Client.Utilities
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    /// <summary>
    /// The application's request culture provider for managing multiple languages (en and fr in this case).
    /// </summary>
    public class CustomRequestCultureProvider : RequestCultureProvider
    {
        private readonly string englishCulture = "en-CA";
        private readonly string frenchCulture = "fr-CA";
        private readonly string english = "/en";
        private readonly string french = "/fr";
        private readonly string blazorFrameworkURL = "/_blazor";
        private string lastSetCulture = "en-CA";

        /// <inheritdoc/>
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (!this.IsBlazorFramework(httpContext) && this.IsCanadianEnglishContext(httpContext))
            {
                this.lastSetCulture = this.englishCulture;
                return Task.FromResult(new ProviderCultureResult(this.englishCulture));
            }
            else if (!this.IsBlazorFramework(httpContext) && this.IsCanadianFrenchContext(httpContext))
            {
                this.lastSetCulture = this.frenchCulture;
                return Task.FromResult(new ProviderCultureResult(this.frenchCulture));
            }
            else if (!this.IsBlazorFramework(httpContext))
            {
                return Task.FromResult(new ProviderCultureResult(this.englishCulture));
            }

            return Task.FromResult(new ProviderCultureResult(this.lastSetCulture));
        }

        private bool IsBlazorFramework(HttpContext httpContext)
        {
            return httpContext.Request.Path.Value.Contains(this.blazorFrameworkURL, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsCanadianEnglishContext(HttpContext httpContext)
        {
            return httpContext.Request.Path.Value.StartsWith(this.english, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsCanadianFrenchContext(HttpContext httpContext)
        {
            return httpContext.Request.Path.Value.StartsWith(this.french, StringComparison.OrdinalIgnoreCase);
        }
    }
}
