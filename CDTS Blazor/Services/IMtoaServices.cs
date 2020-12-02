namespace CDNApplication.Services
{
    using CDNApplication.Data.DTO.MTAPI;
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
        public Task PostSendEmailNotificationAsync(MtoaEmailNotificationDto mtoaEmailNotification);

        /// <summary>
        /// This procedure posts the service requests to MTOA.
        /// </summary>
        /// <returns>Returns the task.</returns>
        public ServiceRequestCreationResult PostServiceRequests();

        /// This procedure posts the file attachment to MTOA.
        /// </summary>
        /// <returns>Return the task.</returns>
        public Task PostFileAttachmentsAsync();

    }
}