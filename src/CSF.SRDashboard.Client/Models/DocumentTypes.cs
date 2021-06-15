using System.Text.Json.Serialization;

namespace CSF.SRDashboard.Client.Models
{
    public class DocumentTypes
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
