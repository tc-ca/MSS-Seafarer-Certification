namespace CSF.SRDashboard.Client.Services.Document
{
    using CSF.SRDashboard.Client.DTO.DocumentStorage;
    using CSF.SRDashboard.Client.Services.Document.Entities;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IDocumentService
    {
        public Task<DocumentDTO> GetDocumentsWithDocumentIds(List<Guid> documentIds);

        public Task<List<Guid>> InsertDocument(int correlationId, string userName, IFormFile file, string fileContentType, string shortDescription, string submissionMethod, string fileLanguage, List<DocumentTypeDTO> documentTypes, string customMetadata);
        //public Task<List<Guid>> InsertDocument(int correlationId, string userName, IFormFile file, string fileContentType, string shortDescription, string submissionMethod, string fileLanguage, List<string> documentTypes, string customMetadata);

        public Task<List<DocumentUpdatedResult>> UpdateMetadataForDocument(Guid documentId, string userName, string fileName, string fileContentType, string shortDescription, string submissionMethod, string fileLanguage, string documentTypes);
    }
}
