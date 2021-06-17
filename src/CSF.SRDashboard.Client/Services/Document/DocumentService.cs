namespace CSF.SRDashboard.Client.Services.Document
{
    using CSF.Common.Library;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using CSF.Common.Library.Extensions.IFormFile;
    using System.Threading.Tasks;
    using CSF.SRDashboard.Client.Services.Document.Entities;
    using CSF.SRDashboard.Client.DTO.DocumentStorage;
    using CSF.SRDashboard.Client.Models;
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<DocumentService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MtoaServices"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration service.</param>
        /// <param name="restClient">REST client service.</param>
        /// <param name="logger">Application logger service.</param>
        public DocumentService(IConfiguration configuration, IRestClient restClient, ILogger<DocumentService> logger)
        {
            this.configuration = configuration;
            this.restClient = restClient;
            this.logger = logger;
        }

        public async Task<List<DocumentInfo>> GetDocumentsWithDocumentIds(List<Guid> documentIds)
        {
            if (documentIds == null || documentIds.Count == 0)
            {
                return new List<DocumentInfo>();
            }

            string queryString = string.Empty;
            foreach (var documentId in documentIds)
            {
                queryString += $"documentGuid={documentId}&";
            }
            string path = string.Format("documents?{0}", queryString.TrimEnd('&'));

            try
            {
                return await restClient.GetAsync<List<DocumentInfo>>(ServiceLocatorDomain.Document, path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<DocumentInfo>();
        }

        public async Task<DocumentInfo> InsertDocument(int correlationId, string userName, IFormFile file, string fileContentType, string shortDescription, string submissionMethod, string fileLanguage, List<DocumentTypeDTO> documentTypes, string customMetadata)
        {
            var insertDocumentParameter = new InsertDocumentParameter()
            {
                UserName = userName,
                FileBytes = await file.GetBytes(),
                FileName = file.FileName,
                FileContentType = fileContentType,
                ShortDescription = shortDescription,
                SubmissionMethod = submissionMethod,
                FileLanguage = fileLanguage,
                DocumentTypes = documentTypes,
                CustomMetadata = customMetadata
            };
            string path = string.Format("documents");

            var restClientRequestOptions = new RestClientRequestOptions()
            {
                Path = path,
                ParameterContentType = "application/json",
                DataObject = insertDocumentParameter,
                ServiceName = ServiceLocatorDomain.Document
            };

            try
            {
                return await restClient.PostAsync<DocumentInfo>(restClientRequestOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new DocumentInfo();
        }

        public async Task<DocumentInfo> UpdateMetadataForDocument(Guid documentId, string userName, string fileName, string fileContentType, string shortDescription, string submissionMethod, string fileLanguage, string documentTypes)
        {
            
            string queryString = string.Empty;

            queryString += $"documentId={documentId}&";

            if (!string.IsNullOrWhiteSpace(userName))
            {
                queryString += $"userName={userName}&";
            }

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                queryString += $"fileName={fileName}&";
            }

            if (!string.IsNullOrWhiteSpace(fileContentType))
            {
                queryString += $"fileContentType={fileContentType}&";
            }

            if (!string.IsNullOrWhiteSpace(shortDescription))
            {
                queryString += $"shortDescription={shortDescription}&";
            }

            if (!string.IsNullOrWhiteSpace(submissionMethod))
            {
                queryString += $"submissionMethod={submissionMethod}&";
            }

            if (!string.IsNullOrWhiteSpace(fileLanguage))
            {
                queryString += $"fileLanguage={fileLanguage}&";
            }

            if (!string.IsNullOrWhiteSpace(documentTypes))
            {
                queryString += $"documentTypes={documentTypes}&";
            }

            string path = string.Format("documents?{0}", queryString.TrimEnd('&'));

            try
            {

                return await restClient.PutAsync<DocumentInfo>(ServiceLocatorDomain.Document, path);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new DocumentInfo();
        }
    }
}
