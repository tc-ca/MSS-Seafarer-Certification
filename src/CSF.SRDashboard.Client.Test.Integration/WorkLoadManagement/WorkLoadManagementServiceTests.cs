using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CSF.SRDashboard.Client.Services;
using CSF.Common.Library;
using Microsoft.Extensions.Configuration;
using System.IO;
using CSF.Common.Library.Azure;
using Microsoft.Extensions.Logging;

namespace CSF.SRDashboard.Client.Test.Integration.WorkLoadManagement
{
    public class WorkLoadManagementServiceTests
    {
        private readonly IConfiguration configuration;
        private readonly IEnumerable<IRestClient> restClient;
        private readonly ILogger<WorkLoadManagementService> logger;
        private IWorkLoadManagementService workLoadManagementService;
        private string workLoadManagementAPI = "http://work-management-service-dev.azurewebsites.net";

        public WorkLoadManagementServiceTests()
        {
            this.configuration = this.BuildConfiguration();
            this.restClient = BuildRestClient();
            this.logger = new Mock<ILogger<WorkLoadManagementService>>().Object;
        }

        [Fact]
        public void GetByWorkItemById_Succeeds_WhenWorkItemNotNull()
        {
            // Arrange
            this.workLoadManagementService = new WorkLoadManagementService(this.restClient, this.logger);

            // Act
            var workItem = this.workLoadManagementService.GetByWorkItemById(1000098);

            // Assert
            Assert.NotNull(workItem);
        }

        private IEnumerable<IRestClient> BuildRestClient()
        {
            var restClients = new List<IRestClient>();
            var azureKeyVaultService = new AzureKeyVaultService(this.configuration);
            var uri = this.configuration.GetSection("ServiceLocatorEndpoints")["WorkLoadManagement"];

            var mockServiceLocator = Mock.Of<IServiceLocator>(x => x.GetServiceUri(ServiceLocatorDomain.WorkLoadManagement) == new System.Uri(uri));
            restClients.Add(new UnauthenticatedRestClient(new System.Net.Http.HttpClient(), mockServiceLocator));
            return restClients;
        }

        private IConfigurationRoot BuildConfiguration()
        {
            string azureSettings = "\"AzureKeyVaultSettings\": {\"KeyVaultServiceEndpoint\": \"https://kv-seafarer-acc.vault.azure.net/\",\"SecretNames\": {\"MtoaApiKey\": \"MtoaApiKey\",\"MtoaJwtToken\": \"MtoaJwt\"}},\"ServiceLocatorEndpoints\": {\"WorkLoadManagement\": \"" + this.workLoadManagementAPI + "\"}";
            string partialAppSettings = "{" + azureSettings + "}";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(partialAppSettings)));
            var configuration = builder.Build();

            return configuration;
        }
    }
}
