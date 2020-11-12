namespace CDNApplication.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for the collection of calls to MTOA services.
    /// </summary>
    public interface IMtoaServices
    {
        /// <summary>
        /// This procedure posts the submission email template to MTOA.
        /// </summary>
        /// <returns>Returns nothing (void).</returns>
        public Task PostSubmissionEmailNotificationTemplateAsync();

        /// <summary>
        /// This procedure posts the submision email for sending to MTOA.
        /// </summary>
        /// <returns>Returns the task.</returns>
        public Task PostSendEmailNotificationAsync();
    }
}