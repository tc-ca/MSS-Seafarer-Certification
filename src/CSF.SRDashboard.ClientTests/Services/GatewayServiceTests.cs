using Xunit;
using CSF.Common.Library;
using Moq;
using Microsoft.Extensions.Logging;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace CSF.SRDashboard.Client.Services.Tests
{
    public class GatewayServiceTests
    {
        private readonly IGatewayService gatewayServiceUnderTest;
        private readonly Mock<GatewayRestClient> mockRestClient;
        
        public GatewayServiceTests()
        {
            Mock<ILogger<GatewayService>> mockLogger = new Mock<ILogger<GatewayService>>(); ;
            Mock<IServiceLocator> mockServiceLocator = new Mock<IServiceLocator>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(moq => moq.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"]).Returns("stubOfTokenValue");
            this.mockRestClient = new Mock<GatewayRestClient>(new HttpClient(), mockServiceLocator.Object, mockConfiguration.Object);
            
            this.gatewayServiceUnderTest = new GatewayService(new List<IRestClient> { mockRestClient.Object }, mockLogger.Object);
        }

        [Fact()]
        public void GetApplicantInfoByCdn_UseRestClientWhenCdnNotNull()
        {
            // Arrange: setup the mock client to check if GetAsync<T> gets called
            mockRestClient.Setup(client => client.GetAsync<ApplicantPersonalInfo>(ServiceLocatorDomain.GatewayToMpdis, "mockPath")).Verifiable();

            // Act
            var returnedObject = gatewayServiceUnderTest.GetApplicantInfoByCdn("stubOfCdnString");

            // Assert
            this.mockRestClient.Verify(prop => prop.GetAsync<ApplicantPersonalInfo>(ServiceLocatorDomain.GatewayToMpdis, "mockPath"), Times.Once);
        }

        [Fact()]
        public void GetApplicantInfoByCdn_DoNotUseRestClientWhenCdnNullOrEmpty_ReturnsNonNullObject()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void Search_UseRestClientWhenCdnNotNullOrEmpty()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void Search_DoNotUseRestClientWhenCdnNullOrEmpty_ReturnsNonNullObject()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}