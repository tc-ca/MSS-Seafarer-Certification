namespace CSF.SRDashboard.Client.Services.Document.Entities
{
    using CSF.SRDashboard.Client.DTO.DocumentStorage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class DocumentInfo
    {
        /// <summary>
        /// gets or sets the description of the file
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// gets or sets the size of the file
        /// </summary>
        [JsonPropertyName("fileSize")]
        public long FileSize { get; set; }

        /// <summary>
        /// gets or sets the language the file is in
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// gets or sets the name of the file
        /// </summary>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// gets or sets the URL to the document
        /// </summary>
        [JsonPropertyName("documentUrl")]
        public string DocumentUrl { get; set; }

        /// <summary>
        /// gets or sets the information on how the file was submitted
        /// </summary>
        [JsonPropertyName("submissionMethod")]
        public string SubmissionMethod { get; set; }

        /// <summary>
        /// gets or sets the file type
        /// </summary>
        [JsonPropertyName("fileType")]
        public string FileType { get; set; }

        /// <summary>
        /// gets or sets the date the document was created
        /// </summary>
        [JsonPropertyName("dateCreated")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// gets or sets the user who created the document
        /// </summary>
        [JsonPropertyName("userCreatedById")]
        public string UserCreatedById { get; set; }

        /// <summary>
        /// gets or sets the date the file was modified
        /// </summary>
        [JsonPropertyName("dateLastUpdated")]
        public DateTime? DateLastUpdated { get; set; }

        /// <summary>
        /// gets or sets the user who last updated the document
        /// </summary>
        [JsonPropertyName("userLastUpdatedById")]
        public string UserLastUpdatedById { get; set; }

        /// <summary>
        /// gets or sets the flag to determine if file was removed or not (soft delete)
        /// </summary>
        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// gets or sets the date the file was removed
        /// </summary>
        [JsonPropertyName("dateDeleted")]
        public DateTime? DateDeleted { get; set; }

        /// <summary>
        /// gets or sets the user who removed the file
        /// </summary>
        [JsonPropertyName("deletedById")]
        public string DeletedById { get; set; }

        /// <summary>
        /// gets or sets this table's primary key
        /// </summary>
        [JsonPropertyName("documentId")]
        public Guid DocumentId { get; set; }

        [JsonPropertyName("documentTypes")]
        public List<DocumentTypeDTO> DocumentTypes { get; set; }
    }
}
