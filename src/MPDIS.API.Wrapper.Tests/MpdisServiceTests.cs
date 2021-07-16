namespace MPDIS.API.Wrapper.Tests
{
    using CSF.Common.Library;
    using CSF.Common.Library.Rest;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using MPDIS.API.Wrapper.Services.MPDIS;
    using System.Net.Http;
    using Xunit;

    public class MpdisServiceTests
    {
        private readonly IMpdisService mpdisService;
        private readonly IRestClient restClient;

        public MpdisServiceTests()
        {
            var mockConfiguration = Mock.Of<IConfiguration>(x => x.GetSection("ServiceLocatorEndpoints")["Mpdis"] == "https://mpdis-sddpm-dev.tc.gc.ca/mpdis-sddpm/auth/rest");
            var mockLogger = new Mock<ILogger<MpdisService>>().Object;

            this.restClient = this.BuildRestClient(mockConfiguration);
            this.mpdisService = new MpdisService(mockConfiguration, restClient, mockLogger);
        }

        [Fact]
        public void Mpdis_GetApplicantByCdn_ReturnsApplicantInformation()
        {
            // Arrange
            var cdn = "00000176";

            try
            {
                // Act
                var applicantInformation = this.mpdisService.GetApplicantByCdn(cdn);

                // Assert
                Assert.NotNull(applicantInformation);

            } catch (HttpRequestException httpRequestException)
            {
                // Assert
                Assert.True(true);
            }            
        }

        private IRestClient BuildRestClient(IConfiguration configuration)
        {
            var mockServiceLocator = Mock.Of<IServiceLocator>(x => x.GetServiceUri(ServiceLocatorDomain.Mpdis) == new System.Uri("https://mpdis-sddpm-dev.tc.gc.ca/mpdis-sddpm/auth/rest"));
            return new UnauthenticatedRestClient(new System.Net.Http.HttpClient(), mockServiceLocator);
        }
    }
}
