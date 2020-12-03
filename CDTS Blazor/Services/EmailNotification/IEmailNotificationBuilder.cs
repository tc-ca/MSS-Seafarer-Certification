namespace CDNApplication.Services.EmailNotification
{
    using CDNApplication.Data.DTO.MTAPI;

    /// <summary>
    /// Defines an email builder to build an mtoa compatible object.
    /// </summary>
    /// <typeparam name="T">The model to transfer.</typeparam>
    public interface IEmailNotificationBuilder<in T>
    {
        /// <summary>
        /// Gets the template name for the email.
        /// </summary>
        string TemplateName { get; }

        /// <summary>
        /// Builds the email given the model.
        /// </summary>
        /// <param name="model">The model to transfer.</param>
        /// <returns>The MtoaEmailNotificationDto with the needed fields.</returns>
        MtoaEmailNotificationDto Build(T model);
    }
}
