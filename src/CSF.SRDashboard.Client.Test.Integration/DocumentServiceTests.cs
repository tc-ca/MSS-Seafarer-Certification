namespace CSF.SRDashboard.Client.Test.Integration
{
    using CSF.Common.Library;
    using CSF.Common.Library.Azure;
    using CSF.Common.Library.Rest;
    using CSF.SRDashboard.Client.DTO.DocumentStorage;
    using CSF.SRDashboard.Client.Services.Document;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Xunit;

    public class DocumentServiceTests
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<DocumentService> logger;
        private readonly string documentServiceAPIUri = "https://document-storage-dev-api.azurewebsites.net/api/v1";

        public DocumentServiceTests()
        {
            this.configuration = this.BuildConfiguration();
            this.restClient = BuildRestClient();
            this.logger = new Mock<ILogger<DocumentService>>().Object;
        }

        [Fact]
        public void WhenInsertingDocument_Succeeds_ReturnDocumentId()
        {
            // Arrange
            var documentService = new DocumentService(this.configuration, this.restClient, this.logger);
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 10, "Data", "image.png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };
            
            // Act
            var result = documentService.InsertDocument(1, "John Wick", file, string.Empty, "My Test file", "FAX", "EN", new List<DocumentTypeDTO>(), string.Empty).ConfigureAwait(false).GetAwaiter().GetResult();

            // Assert
            Assert.NotNull(result);
        }
         
        [Fact]
        public void WhenRetrievingDocument_Succeeds_ReturnsDocuments()
        {
            // Arrange
            var documentService = new DocumentService(this.configuration, this.restClient, this.logger);
            var documentIds = new List<Guid>()
            {
                new Guid("14980cb7-6d3b-4477-85ad-181aa82e103d"),
                new Guid("caf99bf0-9593-4599-94c1-5a0e690a83cd")
            };

            // Act
            var result = documentService.GetDocumentsWithDocumentIds(documentIds).ConfigureAwait(false).GetAwaiter().GetResult();

            // Assert
            Assert.NotEmpty(result);
        } 

        private IRestClient BuildRestClient()
        {
            var azureKeyVaultService = new AzureKeyVaultService(this.configuration);
            var uri = this.configuration.GetSection("ServiceLocatorEndpoints")["Document"];

            var mockServiceLocator = Mock.Of<IServiceLocator>(x => x.GetServiceUri(ServiceLocatorDomain.Document) == new System.Uri(uri));

            return new UnauthenticatedRestClient(new System.Net.Http.HttpClient(), mockServiceLocator);


        }

        private IConfigurationRoot BuildConfiguration()
        {
            string azureSettings = "\"AzureKeyVaultSettings\": {\"KeyVaultServiceEndpoint\": \"https://kv-seafarer-acc.vault.azure.net/\",\"SecretNames\": {\"MtoaApiKey\": \"MtoaApiKey\",\"MtoaJwtToken\": \"MtoaJwt\"}},\"ServiceLocatorEndpoints\": {\"Document\": \""+this.documentServiceAPIUri+"\"}";
            string partialAppSettings = "{" + azureSettings + "}";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(partialAppSettings)));
            var configuration = builder.Build();

            return configuration;
        }
    }
}
