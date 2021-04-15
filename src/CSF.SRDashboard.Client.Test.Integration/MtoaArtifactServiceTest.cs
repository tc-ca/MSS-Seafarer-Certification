using CSF.Web.Client.Utilities;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using CSF.SRDashboard.Client;
using CSF.Web.Client.Data.Services;
using CSF.SRDashboard.Client.Services;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CSF.Common.Library;

namespace CSF.SRDashboard.Client.Test.Integration
{
    public class MtoaArtifactServiceTest
    {
        private readonly IConfigurationRoot configuration;
        private readonly IRestClient restClient;
        private readonly ILogger<MtoaArtifactService> logger;

        public MtoaArtifactServiceTest()
        {
            this.configuration = this.BuildConfiguration();            
            this.restClient = BuildRestClient();
            this.logger = new Mock<ILogger<MtoaArtifactService>>().Object;
        }

        [Fact]
        public void GetDashboardRowsInParallel_ReturnsListOfDashboardRows()
        {
            //Arrange
            var mtoaArtifactService = new MtoaArtifactService(this.configuration, this.restClient, this.logger);

            //Act
            var dashboardRows = mtoaArtifactService.GetDashboardRowsInParallel();
            
            //Assert
            dashboardRows.Should().HaveCount(c => c > 1);           
        }


        private IRestClient BuildRestClient( )
        {
            var azureKeyVaultService = new AzureKeyVaultService(this.configuration);
            var uri = this.configuration.GetSection("ServiceLocatorEndpoints")["Mtoa"];

            var mockServiceLocator = Mock.Of<IServiceLocator>(x => x.GetServiceUri(ServiceLocatorDomain.Mtoa) == new System.Uri(uri));
            return new RestClient(new System.Net.Http.HttpClient(), configuration, mockServiceLocator);

        }

        private IConfigurationRoot BuildConfiguration()
        {
            string azureSettings = "\"AzureKeyVaultSettings\": {\"KeyVaultServiceEndpoint\": \"https://kv-seafarer-acc.vault.azure.net/\",\"SecretNames\": {\"MtoaApiKey\": \"MtoaApiKey\",\"MtoaJwtToken\": \"MtoaJwt\"}},\"ServiceLocatorEndpoints\": {\"Mtoa\": \"https://wwwappstestext.tc.gc.ca/Saf-Sec-Sur/13/MTAPI-INT/api/\"}";
            string mtoaServiceSettings = "\"MtoaServiceSettings\": {\"ServiceId\": 57,\"GetServiceRequestsPath\": \"v1/servicerequests?services={0}&page=1&excludeMetadata=true&excludeServiceRequests=false\",\"GetArtifactByServiceRequestIdPath\": \"v1/artifacts?serviceRequestId={0}&artifactType=JsonDocument\"}";
            string partialAppSettings = "{" + azureSettings +"," + mtoaServiceSettings + "}";

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(partialAppSettings)));
            var configuration = builder.Build();

            return configuration;
        }
    }

    
}
