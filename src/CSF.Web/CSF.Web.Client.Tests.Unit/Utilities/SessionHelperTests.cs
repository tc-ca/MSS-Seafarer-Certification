namespace CDNApplication.Tests.Unit.Utilities
{
    using CDNApplication.Utilities;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using System;
    using Xunit;
    public class SessionHelperTests
    {
        [Fact]
        public void GetLanguageFromContext_WhenContextNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => SessionHelper.GetLanguageFromContext(null));
        }

        [Fact]
        public void GetLanguageFromContext_WhenPathEmpty_ReturnsDefaultLanguage()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(httpContext => httpContext.Request.Path).Returns(new PathString("/"));
            var expectedLanguage = "en";

            // Act
            var result = SessionHelper.GetLanguageFromContext(mockHttpContext.Object);

            // Assert
            Assert.Equal(expectedLanguage, result);
        }

        [Fact]
        public void GetLanguageFromContext_WhenFrench_ReturnsFR()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(httpContext => httpContext.Request.Path).Returns(new PathString("/fr/index"));
            var expectedLanguage = "fr";

            // Act
            var result = SessionHelper.GetLanguageFromContext(mockHttpContext.Object);

            // Assert
            Assert.Equal(expectedLanguage, result);
        }

        [Fact]
        public void GetLanguageFromPath_WhenEmpty_ReturnsDefaultLanguage()
        {
            // Arrange
            var expectedResult = "en";

            // Act
            var result = SessionHelper.GetLanguageFromPath(string.Empty);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetLanguageFromPath_WhenOnlyOneSubPath_ReturnsDefaultLanguag()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(httpContext => httpContext.Request.Path).Returns(new PathString("/"));
            var expectedLanguage = "en";

            // Act
            var result = SessionHelper.GetLanguageFromPath(string.Empty);

            // Assert
            Assert.Equal(expectedLanguage, result);
        }

        [Fact]
        public void GetLanguageFromPath_WhenStartsWithEnWithLotsOfPaths_ReturnsEn()
        {
            // Arrange
            var expectedLanguage = "en";

            // Act
            var result = SessionHelper.GetLanguageFromPath("/en/index/path/subpath");

            // Assert
            Assert.Equal(expectedLanguage, result);
        }
    }
}
