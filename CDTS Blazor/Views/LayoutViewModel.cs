namespace CDNApplication.Views
{
    using System;
    using System.Linq;
    using CDNApplication.Utilities;
    using GoC.WebTemplate.Components.Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The layout view model.
    /// </summary>
    public class LayoutViewModel
    {
        private readonly ModelAccessor modelAccessor;

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<LayoutViewModel> logger;
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutViewModel"/> class.
        /// </summary>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="logger">The application's logger.</param>
        /// <param name="config">The application's configuration.</param>
        public LayoutViewModel(ModelAccessor modelAccessor, IHttpContextAccessor httpContextAccessor, ILogger<LayoutViewModel> logger, IConfiguration config)
        {
            this.modelAccessor = modelAccessor;

            // this.localizer = new ResourceManager(typeof(Common));
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.config = config;
        }

        /// <summary>
        /// Initialize the page for the CDTS template.
        /// </summary>
        public void InitializePage()
        {
            var encodedUrl = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedPathAndQuery(this.httpContextAccessor.HttpContext.Request).Split('/');
            var urlPath = string.Join("/", encodedUrl.SkipLast(2).Skip(1));
            GoC.WebTemplate.Components.Model webTemplateModel;
            webTemplateModel = this.modelAccessor.Model;
            var currentLanguage = SessionHelper.GetLanguageFromContext(this.httpContextAccessor.HttpContext);
            var toggleLanguage = currentLanguage.Equals("en", StringComparison.OrdinalIgnoreCase) ? "/fr" : "/en"; // flip
            this.logger.LogTrace($"current Language: {currentLanguage} | toggle Language: {toggleLanguage}");
            string baseUrl = $"{this.httpContextAccessor.HttpContext.Request.Scheme}://{this.httpContextAccessor.HttpContext.Request.Host.Value}/{urlPath}";
            webTemplateModel.LanguageLink.Href = $"{baseUrl}/{toggleLanguage}/langtoggle";
            webTemplateModel.ApplicationTitle.Href = "/";
            webTemplateModel.ApplicationTitle.NewWindow = true;
            webTemplateModel.DateModified = new DateTime(2020, 04, 29);
        }
    }
}