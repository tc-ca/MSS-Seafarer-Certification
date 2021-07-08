using Xunit;
using CSF.Common.Library;
using Moq;
using Microsoft.Extensions.Logging;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CSF.Common.Library.Rest;

namespace CSF.SRDashboard.Client.Services.Tests
{
    public class GatewayServiceTests
    {
        private readonly IGatewayService gatewayServiceUnderTest;
        private readonly Mock<GatewayRestClient> mockRestClient;
        
        public GatewayServiceTests()
        {
            Mock<ILogger<GatewayService>> mockLogger = new Mock<ILogger<GatewayService>>();
            Mock<IServiceLocator> mockServiceLocator = new Mock<IServiceLocator>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();

            // configure the mock of rest client, it must be a concrete type because GatewayService expects an implementation of type GatewayRestClient.
            mockConfiguration.Setup(moq => moq.GetSection("AzureKeyVaultSettings")["SecretNames:GatewayToken"]).Returns("stubOfTokenValue");
            this.mockRestClient = new Mock<GatewayRestClient>(new HttpClient(), mockServiceLocator.Object, mockConfiguration.Object);
            
            this.gatewayServiceUnderTest = new GatewayService(new List<IRestClient> { mockRestClient.Object }, mockLogger.Object);
        }

        [Fact()]
        public void GetApplicantInfoByCdn_UseRestClientWhenCdnIsNotEmpty()
        {
            // Arrange: setup the mock client to check if GetAsync<T> gets called
            var stubOfApiPath = "Applicant/stubOfCdnString";
            mockRestClient.Setup(client => client.GetAsync<TrimmedApplicantInformation>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath)).Verifiable();

            // Act
            var returnedObject = gatewayServiceUnderTest.GetApplicantInfoByCdn("stubOfCdnString");

            // Assert
            this.mockRestClient.Verify(prop => prop.GetAsync<TrimmedApplicantInformation>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath), Times.Once);
        }

        [Fact()]
        public void GetApplicantInfoByCdn_DoNotUseRestClientWhenCdnIsEmpty_ReturnsNull()
        {
            // Arrange: setup the mock client to check if GetAsync<T> gets called
            var stubOfApiPath = "Applicant/";
            mockRestClient.Setup(client => client.GetAsync<TrimmedApplicantInformation>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath)).Verifiable();

            // Act
            var returnedObject = gatewayServiceUnderTest.GetApplicantInfoByCdn("");

            // Assert
            this.mockRestClient.Verify(prop => prop.GetAsync<TrimmedApplicantInformation>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath), Times.Never);
            Assert.Null(returnedObject);
        }

        [Fact()]
        public void SearchForApplicants_UseRestClientWhenSearchCriteriaNotNullOrEmpty()
        {
            // Arrange: setup the mock client to check if PostAsync<T> gets called
            var stubOfApiPath = "search";
            ApplicantSearchCriteria applicantSearchCriteria = new ApplicantSearchCriteria { Cdn = "stubOfCdn" };
            mockRestClient.Setup(client => client.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath, applicantSearchCriteria)).Verifiable();

            // Act
            var returnedObject = gatewayServiceUnderTest.SearchForApplicants(applicantSearchCriteria);

            // Assert
            this.mockRestClient.Verify(prop => prop.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath, applicantSearchCriteria), Times.Once);
        }

        [Fact()]
        public void SearchForApplicants_DoNotUseRestClientWhenSearchCriteriaIsNull_ReturnsNonNullObject()
        {
            // Arrange: setup the mock client to check if PostAsync<T> gets called
            var stubOfApiPath = "search";
            ApplicantSearchCriteria applicantSearchCriteria = null;
            mockRestClient.Setup(client => client.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath, applicantSearchCriteria)).Verifiable();

            // Act
            var returnedObject = gatewayServiceUnderTest.SearchForApplicants(applicantSearchCriteria);

            // Assert
            this.mockRestClient.Verify(prop => prop.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath, applicantSearchCriteria), Times.Never);
            Assert.NotNull(returnedObject);
        }

        [Fact()]
        public void SearchForApplicants_DoNotUseRestClientWhenSearchCriteriaIsEmpty_ReturnsNonNullObject()
        {
            // Arrange: setup the mock client to check if PostAsync<T> gets called
            var stubOfApiPath = "search";
            ApplicantSearchCriteria applicantSearchCriteria = new ApplicantSearchCriteria();
            mockRestClient.Setup(client => client.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath, applicantSearchCriteria)).Verifiable();

            // Act
            var returnedObject = gatewayServiceUnderTest.SearchForApplicants(applicantSearchCriteria);

            // Assert
            this.mockRestClient.Verify(prop => prop.PostAsync<ApplicantSearchResult>(ServiceLocatorDomain.GatewayToMpdis, stubOfApiPath, applicantSearchCriteria), Times.Never);
            Assert.NotNull(returnedObject);
        }
    }
}