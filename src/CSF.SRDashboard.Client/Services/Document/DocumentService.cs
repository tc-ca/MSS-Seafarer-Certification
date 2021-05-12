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
            string queryString = string.Empty;
            foreach(var documentId in documentIds)
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

        public async Task<List<Guid>> InsertDocument(int correlationId, string userName, IFormFile file, string fileContentType, string shortDescription, string submissionMethod, string fileLanguage, List<string> documentTypes, string customMetadata)
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
                return await restClient.PostAsync<List<Guid>>(restClientRequestOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Guid>();
        }
    }
}
