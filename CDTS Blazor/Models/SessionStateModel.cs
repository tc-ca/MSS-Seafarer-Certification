namespace CDNApplication.Models
{
    /// <summary>
    /// The session state model.
    /// </summary>
    public class SessionStateModel
    {
        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        public string CurrentLanguage { get; set; }

        /// <summary>
        /// Gets or sets the last viewed page.
        /// </summary>
        public string LastViewedPage { get; set; }
    }
}
