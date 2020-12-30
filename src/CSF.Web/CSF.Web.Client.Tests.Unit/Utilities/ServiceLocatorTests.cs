namespace CSF.Web.Client.Tests.Unit.Utilities
{
    using CSF.Web.Client.Utilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using Xunit;

    public class ServiceLocatorTests
    {
        public ServiceLocatorTests()
        {

        }

        [Fact]
        public void GetServiceUri_ReturnsServiceCorrectly()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(config => config.GetSection("ServiceLocatorEndpoints")["Mtoa"]).Returns("https://localhost/mtoaapi");
            var loggerMock = new Mock<ILogger<ServiceLocator>>();
            var expectedResult = new Uri("https://localhost/mtoaapi");

            var serviceLocator = new ServiceLocator(configMock.Object, loggerMock.Object);

            // Act
            var serviceUri = serviceLocator.GetServiceUri(ServiceLocatorDomain.Mtoa);

            // Assert
            Assert.Equal(expectedResult, serviceUri);
        }

        [Fact]
        public void GetServiceUri_WhenKeyDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger<ServiceLocator>>();

            var serviceLocator = new ServiceLocator(configMock.Object, loggerMock.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => serviceLocator.GetServiceUri(ServiceLocatorDomain.Mtoa));
        }
    }
}
