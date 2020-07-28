namespace CDNApplication.Model
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Testing purposes")]
    public class SessionStateModel
    {
        public string CurrentLanguage { get; set; }

        public string LastViewedPage { get; set; }
    }
}
