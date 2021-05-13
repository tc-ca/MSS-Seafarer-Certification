namespace CSF.SRDashboard.Client.Services.Document
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class InsertDocumentParameter
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("fileBytes")]
        public byte[] FileBytes { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("fileSize")]
        public long FileSize => this.FileBytes.Length;

        [JsonPropertyName("fileContentType")]
        public string FileContentType { get; set; }

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonPropertyName("submissionMethod")]
        public string SubmissionMethod { get; set; }

        [JsonPropertyName("fileLanguage")]
        public string FileLanguage { get; set; }

        [JsonPropertyName("documentTypes")]
        public List<string> DocumentTypes { get; set; }

        [JsonPropertyName("customMetadata")]
        public string CustomMetadata { get; set; }
    }
}
