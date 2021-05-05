namespace CSF.SRDashboard.Client.Services.Document
{
    using CSF.Common.Library;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public void InsertDocument(int correlationId, string userName, IFormFile file, string fileContentType, string shortDescription, string submissionMethod, string fileLanguage, List<string> documentTypes, string customMetadata)
        {
            var insertDocumentParameter = new InsertDocumentParameter()
            {
                CorrelationId = correlationId,
                UserName = userName,
                File = file,
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
                ParameterContentType = "multipart/form-data",
                DataObject = insertDocumentParameter,
                ServiceName = ServiceLocatorDomain.Document
            };

            try
            {
                var result = restClient.PostAsync<Object>(restClientRequestOptions).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
