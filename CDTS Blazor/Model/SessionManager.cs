namespace CDNApplication.Model
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Testing purposes")]
    public class SessionManager : ISessionManager
    {
        private readonly SessionStateModel sessionState;

        public SessionManager(SessionStateModel sessionState)
        {
            this.sessionState = sessionState;
        }

        public void UpdateSessionState(string currentLanguage, string lastViewedPage)
        {
            this.sessionState.CurrentLanguage = !string.IsNullOrEmpty(currentLanguage) ? currentLanguage : this.sessionState.CurrentLanguage;
            this.sessionState.LastViewedPage = !string.IsNullOrEmpty(lastViewedPage) ? lastViewedPage : this.sessionState.LastViewedPage;
        }
    }
}
