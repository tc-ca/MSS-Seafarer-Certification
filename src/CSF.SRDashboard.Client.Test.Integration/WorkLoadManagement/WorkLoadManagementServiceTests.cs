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
        private readonly IRestClient restClient;
        private readonly ILogger<IWorkLoadManagementService> logger;
        private IWorkLoadManagementService workLoadManagementService;
        //IEnumerable<IRestClient> restClientCollection, ILogger<WorkLoadManagementService> logger
        [Fact]
        public void Test()
        {
            Assert.True(true);
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
            string azureSettings = "\"AzureKeyVaultSettings\": {\"KeyVaultServiceEndpoint\": \"https://kv-seafarer-acc.vault.azure.net/\",\"SecretNames\": {\"MtoaApiKey\": \"MtoaApiKey\",\"MtoaJwtToken\": \"MtoaJwt\"}},\"ServiceLocatorEndpoints\": {\"Document\": \"" + this.documentServiceAPIUri + "\"}";
            string partialAppSettings = "{" + azureSettings + "}";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(partialAppSettings)));
            var configuration = builder.Build();

            return configuration;
        }
    }
}
