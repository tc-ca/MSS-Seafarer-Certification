namespace CDNApplication.Utilities
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

        /// <inheritdoc/>
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (this.IsCanadianEnglishContext(httpContext))
            {
                return Task.FromResult(new ProviderCultureResult(this.englishCulture));
            }
            else if (this.IsCanadianFrenchContext(httpContext))
            {
                return Task.FromResult(new ProviderCultureResult(this.frenchCulture));
            }
            else
            {
                return Task.FromResult(new ProviderCultureResult(this.englishCulture));
            }
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
