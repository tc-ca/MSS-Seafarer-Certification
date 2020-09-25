namespace CDNApplication.Components.Banners
{
    using System;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;

    /// <summary>
    /// Banner base.
    /// </summary>
    public class BannerBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets the banner Message.
        /// </summary>
        [Parameter]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        [Parameter]
        public string Phase { get; set; }

        /// <summary>
        /// Gets the classname.
        /// </summary>
        public string ClassName
        {
            get
            {
                switch (this.State)
                {
                    case BannerState.BETA:
                    case BannerState.ALPHA:
                        return "prototype";
                    case BannerState.WARNING:
                        return "warning";
                    case BannerState.SUCCESS:
                        return "success";
                    case BannerState.ERROR:
                        return "error";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the state of the banner.
        /// </summary>
        [Parameter]
        public BannerState State { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the banner should be shown or not.
        /// </summary>
        [Parameter]
        public bool ShowBanner { get; set; } = false;
    }
}