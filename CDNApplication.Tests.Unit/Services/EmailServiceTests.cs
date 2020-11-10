namespace CDNApplication.Tests.Unit.Services
{
    using Xunit;

    public class EmailServiceTests
    {
        public EmailServiceTests()
        {
        }

        [Fact]
        public void RequestEmailText_Englisg_ReturnsEnglishTextFirst()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void RequestEmailText_French_ReturnsFrenchTextFirst()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void RequestLanguage_ReturnsCurrentLanguage()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void RequestLanguage_EmptyString_ThrowsException()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void RequestLanguage_WhenProvinceIsQuebec_ReturnsFrench()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void RequestLanguage_WhenProvinceIsNotQuebec_ReturnsEnglish()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void RequestLanguage_WhenProvinceIsQuebecAndLanguageIsEnglish_ReturnsFrench()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
