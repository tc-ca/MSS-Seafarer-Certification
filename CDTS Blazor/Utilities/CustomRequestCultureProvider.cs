namespace CDNApplication.Utilities
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Testing purposes")]
    public class CustomRequestCultureProvider : RequestCultureProvider
    {
        private readonly string englishCulture = "en-CA";
        private readonly string frenchCulture = "fr-CA";
        private readonly string english = "/en";
        private readonly string french = "/fr";

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Testing purposes")]
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
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
