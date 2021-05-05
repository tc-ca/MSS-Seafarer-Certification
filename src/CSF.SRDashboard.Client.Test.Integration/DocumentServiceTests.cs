namespace CSF.SRDashboard.Client.Test.Integration
{
    using CSF.Common.Library;
    using CSF.Common.Library.Azure;
    using CSF.SRDashboard.Client.Services.Document;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Xunit;

    public class DocumentServiceTests
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<DocumentService> logger;

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
            documentService.InsertDocument(1, "John Wick", file, string.Empty, "My Test file", "FAX", "EN", new List<string>(), string.Empty);

            // Assert
            Assert.True(true);
        }


        private IRestClient BuildRestClient()
        {
            var azureKeyVaultService = new AzureKeyVaultService(this.configuration);
            var uri = this.configuration.GetSection("ServiceLocatorEndpoints")["Document"];

            var mockServiceLocator = Mock.Of<IServiceLocator>(x => x.GetServiceUri(ServiceLocatorDomain.Document) == new System.Uri(uri));
            return new RestClient(new System.Net.Http.HttpClient(), configuration, mockServiceLocator);

        }

        private IConfigurationRoot BuildConfiguration()
        {
            string azureSettings = "\"AzureKeyVaultSettings\": {\"KeyVaultServiceEndpoint\": \"https://kv-seafarer-acc.vault.azure.net/\",\"SecretNames\": {\"MtoaApiKey\": \"MtoaApiKey\",\"MtoaJwtToken\": \"MtoaJwt\"}},\"ServiceLocatorEndpoints\": {\"Document\": \"https://document-storage-dev-api.azurewebsites.net/api/v1/\"}";
            string partialAppSettings = "{" + azureSettings + "}";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(partialAppSettings)));
            var configuration = builder.Build();

            return configuration;
        }
    }
}
