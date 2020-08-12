namespace CDNApplication.Components.Banners
{
    using System;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;

    /// <summary>
    /// Banner base.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "In this case we need different class name and file name")]
    public class BannerBase : ComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BannerBase"/> class.
        /// </summary>
        public BannerBase()
        {
            this.IsVisible = true;
        }

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
        /// Gets or sets a value indicating whether the banner should be visible.
        /// </summary>
        [Parameter]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Closes the banner.
        /// </summary>
        /// <param name="e">The mouse event arguements.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "We need this.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "it's fine here.")]
        public void CloseBanner(MouseEventArgs e)
        {
            this.IsVisible = false;
        }
    }
}