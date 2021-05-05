namespace CSF.SRDashboard.Client.Services.Document
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    public class InsertDocumentParameter
    {
        [JsonProperty("correlationId")]
        public int CorrelationId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("file")]
        public IFormFile File { get; set; }

        [JsonProperty("fileContentType")]
        public string FileContentType { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("submissionMethod")]
        public string SubmissionMethod { get; set; }

        [JsonProperty("fileLanguage")]
        public string FileLanguage { get; set; }

        [JsonProperty("documentTypes")]
        public List<string> DocumentTypes { get; set; }

        [JsonProperty("customMetadata")]
        public string CustomMetadata { get; set; }
    }
}
