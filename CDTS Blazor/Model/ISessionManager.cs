namespace CDNApplication.Model
{
    /// <summary>
    /// Defines the session manager.
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// Updates the session state with the current language and the last viewed page.
        /// </summary>
        /// <param name="currentLanguage">The current language.</param>
        /// <param name="lastViewedPage">The last viewed page.</param>
        void UpdateSessionState(string currentLanguage, string lastViewedPage);
    }
}
