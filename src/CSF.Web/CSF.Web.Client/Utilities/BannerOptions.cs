namespace CSF.Web.Client.Utilities
{
    public class BannerOptions
    {
            public const string AlphaBanner = "Banners:AlphaBanner";
            public const string BetaBanner = "Banners:BetaBanner";
            public const string WarningBanner = "Banners:WarningBanner";

            public bool ShowBanner { get; set; }

            public string BannerMessage { get; set; }
    }
}