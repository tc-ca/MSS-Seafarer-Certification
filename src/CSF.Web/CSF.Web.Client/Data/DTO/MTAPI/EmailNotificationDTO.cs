namespace CSF.Web.Client.Data.DTO.MTAPI
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class EmailNotificationDTO
    {
        public EmailNotificationDTO() { }

        public string NotificationTemplateName { get; set; }
        public int ServiceRequestId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Language { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsHtml { get; set; }
        public List<EmailAttachmentDTO> Attachements { get; set; }
        public List<KeyValuePair<string, string>> Parameters { get; set; }

    }
}
