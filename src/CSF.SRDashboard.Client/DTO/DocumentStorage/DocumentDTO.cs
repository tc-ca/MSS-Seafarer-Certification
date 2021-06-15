namespace CSF.SRDashboard.Client.DTO.DocumentStorage
{
    using CSF.SRDashboard.Client.Services.Document.Entities;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class DocumentDTO
    {
        [JsonProperty("documents")]
        public List<DocumentInfo> Documents { get; set; } = new List<DocumentInfo>();
    }
}
