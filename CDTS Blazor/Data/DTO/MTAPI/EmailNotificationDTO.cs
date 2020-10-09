using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDNApplication.Data.DTO.MTAPI
{
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
