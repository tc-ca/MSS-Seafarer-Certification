namespace CSF.Web.Client.Tests.Unit.Services
{
    using CSF.Common.Library;
    using CSF.Common.Library.Rest;
    using CSF.Web.Client.Data.DTO.MTAPI;
    using CSF.Web.Client.Services;
    using CSF.Web.Client.Utilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using Xunit;

    public class MtoaServiceTests
    {
        private readonly IMtoaServices mtoaServices;
        private readonly Mock<IRestClient> mockRestClient;
        private readonly Mock<ILogger<MtoaServices>> mockLogger;

        public MtoaServiceTests()
        {
            var configuration = this.buildMockIConfiguration();
            this.mockRestClient = new Mock<IRestClient>();
            this.mockLogger = new Mock<ILogger<MtoaServices>>();

            this.mtoaServices = new MtoaServices(configuration, this.mockRestClient.Object, this.mockLogger.Object);
        }

        [Fact]
        public void PostSubmissionEmailNotificationTemplateAsync_Succeeds()
        {
            // Arrange
            mockRestClient.Setup(client => client.PostAsync<MtoaEmailNotificationTemplateDto>(ServiceLocatorDomain.Mtoa, "mockEmailPath", It.IsAny<MtoaEmailNotificationTemplateDto>())).Verifiable();

            // Act
            this.mtoaServices.PostSubmissionEmailNotificationTemplateAsync();

            // Assert
            this.mockRestClient.Verify(prop => prop.PostAsync<MtoaEmailNotificationTemplateDto>(ServiceLocatorDomain.Mtoa, "mockEmailPath", It.IsAny<MtoaEmailNotificationTemplateDto>()), Times.Once);
        }

        [Fact]
        public void PostSubmissionEmailNotificationTemplateAsync_ThowsException()
        {
            // Arrange
            this.mockRestClient.Setup(client => client.PostAsync<MtoaEmailNotificationTemplateDto>(ServiceLocatorDomain.Mtoa, "mockEmailPath", It.IsAny<MtoaEmailNotificationTemplateDto>())).Throws<Exception>();

            // Assert
            Assert.ThrowsAsync<Exception>(() => this.mtoaServices.PostSubmissionEmailNotificationTemplateAsync());
            this.mockLogger.Verify(logger => logger.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Fact]
        public void PostSendEmailNotificationAsync_Suceeds()
        {
            // Arrange
            mockRestClient.Setup(client => client.PostAsync<MtoaEmailNotificationDto>(ServiceLocatorDomain.Mtoa, "mockEmailNotificationPath", It.IsAny<MtoaEmailNotificationDto>())).Verifiable();

            // Act
            this.mtoaServices.PostSendEmailNotificationAsync(new MtoaEmailNotificationDto());

            // Assert
            this.mockRestClient.Verify(prop => prop.PostAsync<MtoaEmailNotificationDto>(ServiceLocatorDomain.Mtoa, "mockEmailNotificationPath", It.IsAny<MtoaEmailNotificationDto>()), Times.Once);
        }

        [Fact]
        public void PostSeafarersArtifactDtoAsync_Suceeds()
        {
            // Arrange
            var servicePath = "mockArtifact?artifactType=JsonDocument&version=1&serviceRequestId=0&userId=0";
            mockRestClient.Setup(client => client.PostAsync<object>(It.IsAny<RestClientRequestOptions>())).Verifiable();

            // Act
            this.mtoaServices.PostSeafarerArtifactInformationAsync(new SeafarersArtifactDto());

            // Assert
            this.mockRestClient.Verify(prop => prop.PostAsync<object>(It.IsAny<RestClientRequestOptions>()), Times.Once);
        }

        [Fact]
        public void PostSendEmailNotificationAsync_ThrowsException()
        {
            // Arrange
            mockRestClient.Setup(client => client.PostAsync<MtoaEmailNotificationDto>(ServiceLocatorDomain.Mtoa, "mockEmailNotificationPath", It.IsAny<MtoaEmailNotificationDto>())).Throws<Exception>();

            // Assert
            Assert.ThrowsAsync<Exception>(() => this.mtoaServices.PostSendEmailNotificationAsync(new MtoaEmailNotificationDto()));
            this.mockLogger.Verify(logger => logger.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Fact]
        public void PostServiceRequests_Suceeds()
        {
            // Arrange
            var servicePath = "api/v1/servicerequests?userId=0&serviceId=1337&englishName=MyService&frenchName=MonService&serviceRequestStatus=Status";
            mockRestClient.Setup(client => client.PostAsync<ServiceRequestCreationResult>(ServiceLocatorDomain.Mtoa, servicePath, It.IsAny<ServiceRequestCreationResult>())).Verifiable();

            // Act
            this.mtoaServices.PostServiceRequests();

            // Assert
            this.mockRestClient.Verify(prop => prop.PostAsync<ServiceRequestCreationResult>(ServiceLocatorDomain.Mtoa, servicePath, It.IsAny<ServiceRequestCreationResult>()), Times.Once);
        }

        [Fact]
        public void PostServiceRequests_ThrowsException()
        {
            // Arrange
            var servicePath = "api/v1/servicerequests?userId=0&serviceId=1337&englishName=MyService&frenchName=MonService&serviceRequestStatus=Status";
            mockRestClient.Setup(client => client.PostAsync<ServiceRequestCreationResult>(ServiceLocatorDomain.Mtoa, servicePath, It.IsAny<ServiceRequestCreationResult>())).Throws<Exception>();

            // Assert
            Assert.Throws<Exception>(() => this.mtoaServices.PostServiceRequests());
            this.mockLogger.Verify(logger => logger.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        private IConfiguration buildMockIConfiguration()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["EmailNotificationPath"]).Returns("mockEmailNotificationPath");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["EmailNotificationTemplatePath"]).Returns("mockEmailPath");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["EmailSubmissionTemplateName"]).Returns("mockEmailTemplate");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["SeafarerCertificationServiceName"]).Returns("mockCertificationServiceName");

            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["UserId"]).Returns("0");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["ServiceId"]).Returns("1337");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["ServiceNameInEnglish"]).Returns("MyService");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["ServiceNameInFrench"]).Returns("MonService");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["ProgressStatus"]).Returns("Status");

            return mockConfig.Object;
        }
    }
}
