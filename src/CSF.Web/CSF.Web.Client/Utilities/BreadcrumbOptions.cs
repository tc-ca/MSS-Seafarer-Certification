namespace CSF.Web.Client.Utilities
{
    /// <summary>
    /// Used for binding to the breadcrumb options in appsettings.json.
    /// </summary>
    public class BreadcrumbOptions
    {
        /// <summary>
        /// The root Canada.ca breadcrumb option selector.
        /// </summary>
        public const string CanadaBreadcrumb = "GoCWebTemplate:Breadcrumbs:Canada";

        /// <summary>
        /// The second-level Marine Transportation breadcrumb option selector.
        /// </summary>
        public const string MarineBreadcrumb = "GoCWebTemplate:Breadcrumbs:Marine";

        /// <summary>
        /// The third-level CSF application breadcrumb option selector.
        /// </summary>
        public const string CSFBreadcrumb = "GoCWebTemplate:Breadcrumbs:CSF";

        /// <summary>
        /// Gets or sets a value indicating whether the hypertext reference for the breadcrumb.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets title text for the breadcrumb's hypertext reference.
        /// </summary>
        public string Title { get; set; }
    }
}