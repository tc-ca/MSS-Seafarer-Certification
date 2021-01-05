namespace CSF.Web.Client.Utilities
{
    /// <summary>
    /// Used for binding to the banner options in appsettings.json.
    /// </summary>
    public class BannerOptions
    {
        /// <summary>
        /// Settings for the Alpha banner.
        /// </summary>
        public const string AlphaBanner = "Banners:AlphaBanner";

        /// <summary>
        /// Settings for the Beta banner.
        /// </summary>
        public const string BetaBanner = "Banners:BetaBanner";

        /// <summary>
        /// Settings for the Warning banner.
        /// </summary>
        public const string WarningBanner = "Banners:WarningBanner";

        /// <summary>
        /// Gets or sets a value indicating whether to show or hide the banner.
        /// </summary>
        public bool ShowBanner { get; set; }

        /// <summary>
        /// Gets or sets message text to display in the banner.
        /// </summary>
        public string BannerMessage { get; set; }
    }
}