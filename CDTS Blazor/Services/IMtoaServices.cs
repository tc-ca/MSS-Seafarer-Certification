namespace CDNApplication.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the collection of calls to MTOA services.
    /// </summary>
    public interface IMtoaServices
    {
        /// <summary>
        /// This procedure posts the submission email template to MTOA on application startup.
        /// </summary>
        /// <returns>Returns nothing (void).</returns>
        public Task PostSubmissionEmailNotificationTemplateAsync();
    }
}