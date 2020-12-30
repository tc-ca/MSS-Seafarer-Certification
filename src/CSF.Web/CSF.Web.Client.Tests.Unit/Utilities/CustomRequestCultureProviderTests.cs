namespace CDNApplication.Tests.Unit.Utilities
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Moq;
    using System;
    using Xunit;

    public class CustomRequestCultureProviderTests
    {
        private readonly CDNApplication.Utilities.CustomRequestCultureProvider customRequestCultureProvider;

        public CustomRequestCultureProviderTests()
        {
            this.customRequestCultureProvider = new CDNApplication.Utilities.CustomRequestCultureProvider();
        }

        [Fact]
        public void DetermineProviderCultureResultWithEnPath_ReturnsENCAProviderCulture()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(httpContext => httpContext.Request.Path).Returns(new PathString("/en/index"));
            var expectedCulture = new ProviderCultureResult("en-CA");

            // Act
            ProviderCultureResult result = this.customRequestCultureProvider.DetermineProviderCultureResult(mockHttpContext.Object).Result;

            // Assert
            Assert.Equal(expectedCulture.Cultures[0].Value, result.Cultures[0].Value);
            Assert.Equal(expectedCulture.UICultures[0].Value, result.UICultures[0].Value);
        }

        [Fact]
        public void DetermineProviderCultureResultWithFrPath_ReturnsFRCAProviderCulture()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(httpContext => httpContext.Request.Path).Returns(new PathString("/fr/index"));
            var expectedCulture = new ProviderCultureResult("fr-CA");

            // Act
            ProviderCultureResult result = this.customRequestCultureProvider.DetermineProviderCultureResult(mockHttpContext.Object).Result;

            // Assert
            Assert.Equal(expectedCulture.Cultures[0].Value, result.Cultures[0].Value);
            Assert.Equal(expectedCulture.UICultures[0].Value, result.UICultures[0].Value);
        }

        [Fact]
        public void DetermineProviderCultureResultWithBlazorFrameworkPath_ReturnsENCAProviderCulture()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(httpContext => httpContext.Request.Path).Returns(new PathString("/index"));
            var expectedCulture = new ProviderCultureResult("en-CA");

            // Act
            ProviderCultureResult result = this.customRequestCultureProvider.DetermineProviderCultureResult(mockHttpContext.Object).Result;

            // Assert
            Assert.Equal(expectedCulture.Cultures[0].Value, result.Cultures[0].Value);
            Assert.Equal(expectedCulture.UICultures[0].Value, result.UICultures[0].Value);
        }

                [Fact]
        public void DetermineProviderCultureResultWithUnrecognizedCulture_ReturnsENCAProviderCulture()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(httpContext => httpContext.Request.Path).Returns(new PathString("/es/_blazor/index"));
            var expectedCulture = new ProviderCultureResult("en-CA");

            // Act
            ProviderCultureResult result = this.customRequestCultureProvider.DetermineProviderCultureResult(mockHttpContext.Object).Result;

            // Assert
            Assert.Equal(expectedCulture.Cultures[0].Value, result.Cultures[0].Value);
            Assert.Equal(expectedCulture.UICultures[0].Value, result.UICultures[0].Value);
        }

        [Fact]
        public void DetermineProviderCultureResultWithNullHttpContext_ThrowsArgumentNullException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => this.customRequestCultureProvider.DetermineProviderCultureResult(null));
        }
    }
}
