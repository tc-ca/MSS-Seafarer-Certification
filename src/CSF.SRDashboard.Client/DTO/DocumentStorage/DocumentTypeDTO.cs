using System.Text.Json.Serialization;

namespace CSF.SRDashboard.Client.DTO.DocumentStorage
{
    public class DocumentTypeDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
