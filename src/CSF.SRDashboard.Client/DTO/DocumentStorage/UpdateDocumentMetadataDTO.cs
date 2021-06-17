﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSF.SRDashboard.Client.DTO.DocumentStorage
{
    public class UpdateDocumentMetadataDTO
    {
        [JsonPropertyName("documentId")]
        public Guid DocumentId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("submissionMethod")]
        public string SubmissionMethod { get; set; }

        [JsonPropertyName("fileLanguage")]
        public string FileLanguage { get; set; }

        [JsonPropertyName("documentTypes")]
        public List<DocumentTypeDTO> DocumentTypes { get; set; }

        [JsonPropertyName("customMetadata")]
        public string CustomMetadata { get; set; }
    }
}
