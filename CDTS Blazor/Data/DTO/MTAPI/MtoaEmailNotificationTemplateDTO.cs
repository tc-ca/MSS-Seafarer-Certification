namespace CDNApplication.Data.DTO.MTAPI
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Payload for the MTOA EmailNotification template.
    /// </summary>
    public class MtoaEmailNotificationTemplateDTO
    {
        /// <summary>
        /// Gets or sets the name for the email template as stored in MTOA.
        /// </summary>
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the service name as defined in MTOA.
        /// </summary>
        [JsonPropertyName("ServiceName")]
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are parameters in the email subject text.
        /// </summary>
        [JsonPropertyName("HasSubjectParameters")]
        public bool HasSubjectParameters { get; set; }

        /// <summary>
        /// Gets or sets the content for the English subject text.
        /// </summary>
        [JsonPropertyName("SubjectTextEnglish")]
        public string SubjectTextEnglish { get; set; }

        /// <summary>
        /// Gets or sets the content for the French subject text.
        /// </summary>
        [JsonPropertyName("SubjectTextFrench")]
        public string SubjectTextFrench { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are parameters in the email message body text.
        /// </summary>
        [JsonPropertyName("HasBodyParameters")]
        public bool HasBodyParameters { get; set; }

        /// <summary>
        /// Gets the content for the English message body.
        /// </summary>
        [JsonPropertyName("BodyTextEnglish")]
        public List<string> BodyTextEnglish { get; } = new List<string>();

        /// <summary>
        /// Gets the content for the French message body.
        /// </summary>
        [JsonPropertyName("BodyTextFrench")]
        public List<string> BodyTextFrench { get; } = new List<string>();
    }
}