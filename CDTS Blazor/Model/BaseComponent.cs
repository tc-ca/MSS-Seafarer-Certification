namespace CDNApplication.Model
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// The base component for any page or component in our application.
    /// </summary>
    public class BaseComponent : LayoutComponentBase
    {
        /// <summary>
        /// Gets or sets application's navigation manager.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Gets or sets the application's session manager.
        /// </summary>
        [Inject]
        public ISessionManager SessionManager { get; set; }

        /// <summary>
        /// Gets or sets the culture language code.
        /// </summary>
        [Parameter]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets the base uri for the language.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "In this case we want this tu be a string")]
        public string LangBaseUri => this.NavigationManager.BaseUri + this.LanguageCode;

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var lastViewedPage = this.NavigationManager.Uri;

            this.SessionManager.UpdateSessionState(this.LanguageCode, lastViewedPage);
        }
    }
}
