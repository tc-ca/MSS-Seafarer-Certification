namespace CSF.SRDashboard.Client.DTO.WorkLoadManagement
{
    using System;
    using System.Text.Json.Serialization;

    public class WorkItemAttachmentDTO
    {
        [JsonPropertyName("workItemId")]
        public int WorkItemId { get; set; }
        [JsonPropertyName("documentId")]
        public Guid DocumentId { get; set; }
    }
}
