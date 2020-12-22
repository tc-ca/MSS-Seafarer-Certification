namespace CSF.Web.Client.Models
{
    /// <summary>
    /// The sessions manager.
    /// </summary>
    public class SessionManager : ISessionManager
    {
        private readonly SessionStateModel sessionStateModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManager"/> class.
        /// </summary>
        /// <param name="sessionStateModel">The session state model.</param>
        public SessionManager(SessionStateModel sessionStateModel)
        {
            this.sessionStateModel = sessionStateModel;
        }

        /// <inheritdoc/>
        public void UpdateSessionState(string currentLanguage, string lastViewedPage)
        {
            this.sessionStateModel.CurrentLanguage = !string.IsNullOrEmpty(currentLanguage) ? currentLanguage : this.sessionStateModel.CurrentLanguage;
            this.sessionStateModel.LastViewedPage = !string.IsNullOrEmpty(lastViewedPage) ? lastViewedPage : this.sessionStateModel.LastViewedPage;
        }
    }
}
