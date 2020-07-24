namespace CDNApplication.Views
{
    using System;
    using CDNApplication.Utilities;
    using GoC.WebTemplate.Components.Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Testing purposes")]
    public class LayoutViewModel
    {
        private readonly ModelAccessor modelAccessor;

        // private readonly ResourceManager localizer;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<LayoutViewModel> logger;
        private readonly IConfiguration config;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Testing purposes")]
        public LayoutViewModel(ModelAccessor modelAccessor, IHttpContextAccessor httpContextAccessor, ILogger<LayoutViewModel> logger, IConfiguration config)
        {
            this.modelAccessor = modelAccessor;

            // this.localizer = new ResourceManager(typeof(Common));
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.config = config;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Testing purposes")]
        public void InitializePage()
        {
            GoC.WebTemplate.Components.Model webTemplateModel;
            webTemplateModel = this.modelAccessor.Model;
            var currentLanguage = SessionHelper.GetLanguageFromContext(this.httpContextAccessor.HttpContext);
            var toggleLanguage = currentLanguage.Equals("en", StringComparison.OrdinalIgnoreCase) ? "/fr" : "/en"; // flip
            this.logger.LogTrace($"current Language: {currentLanguage} | toggle Language: {toggleLanguage}");
            string baseUrl = $"{this.httpContextAccessor.HttpContext.Request.Scheme}://{this.httpContextAccessor.HttpContext.Request.Host.Value}{this.config["BaseUrl"]}";
            webTemplateModel.LanguageLink.Href = $"{baseUrl}{toggleLanguage}/langtoggle";

            // WebTemplateModel.HeaderTitle = this.localizer.GetString("AppLayoutHeaderTitle");
            // WebTemplateModel.ApplicationTitle.Text = this.localizer.GetString("AppLayoutTitlethis.Text");
            webTemplateModel.ApplicationTitle.Href = "/";

            // WebTemplateModel.ApplicationTitle.Acronym = this.localizer.GetString("ApplicationTitlethis.Acronym");
            webTemplateModel.ApplicationTitle.NewWindow = true;
            /*
            WebTemplateModel.HTMLHeaderElements = new List<string> {
            "<meta name=\"keywords\" content=\"Certification,Marine,Insurance,Unit,TC,Cost,Recovery\">",
            "<meta name=\"description\" content=\"MIU external application\">",
            "<meta name=\"author\" content=\"Team Narwhals\">"
            };
            */
            webTemplateModel.DateModified = new DateTime(2020, 04, 29);
            /*
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "https://www.canada.ca", Title = this.localizer.GetString("Breadcrumbthis.Home") });
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "https://www.canada.ca/en/services/transport.html", Title = this.localizer.GetString("Breadcrumbthis.TCServices") });
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "https://www.tc.gc.ca/en/services/marine.html", Title = this.localizer.GetString("Breadcrumb_MarineInsurance") });
            */
        }
    }
}