namespace CSF.Web.Client.Data.DTO.MTAPI
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the MTOA ntofication data transfer object.
    /// </summary>
    public class MtoaEmailNotificationDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaEmailNotificationDto"/> class.
        /// </summary>
        public MtoaEmailNotificationDto()
        {
            this.Attachements = new List<EmailAttachmentDTO>();
            this.Parameters = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Gets or sets the notification template name.
        /// </summary>
        [JsonPropertyName("NotificationTemplateName")]
        public string NotificationTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the service request id.
        /// </summary>
        [JsonPropertyName("ServiceRequestId")]
        public int ServiceRequestId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        [JsonPropertyName("Language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the email's from.
        /// </summary>
        [JsonPropertyName("From")]
        public string From { get; set; }

        /// <summary>
        /// GEts or sets the email's to.
        /// </summary>
        [JsonPropertyName("To")]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the submitted template is html.
        /// </summary>
        [JsonPropertyName("IsHtml")]
        public bool IsHtml { get; set; }

        /// <summary>
        /// Gets the email's attachements.
        /// </summary>
        [JsonPropertyName("Attachments")]
        public List<EmailAttachmentDTO> Attachements { get; }

        /// <summary>
        /// Gets the email template's parameters.
        /// </summary>
        [JsonPropertyName("Parameters")]
        public List<KeyValuePair<string, string>> Parameters { get; }
    }
}
