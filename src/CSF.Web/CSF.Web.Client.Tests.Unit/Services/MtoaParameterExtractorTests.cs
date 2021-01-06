namespace CSF.Web.Client.Tests.Unit.Services
{
    using System;
    using System.Linq;
    using CSF.Web.Client.Data.Attributes;
    using CSF.Web.Client.Services;
    using Xunit;

    public class MtoaParameterExtractorTests
    {
        readonly IMtoaParameterExtractor mtoaParameterExtractor;

        public MtoaParameterExtractorTests()
        {
            this.mtoaParameterExtractor = new MtoaParameterExtractor();
        }

        [Fact]
        public void ExtractParameters_WhenTemplateHasAttribute_ReturnsValues()
        {
            // Arrange
            var FirstName = "John";
            var LastName = "Wick";
            var template = new MtoaParameterTestWithAttribute
            {
                FirstName =  FirstName,
                LastName = LastName,
            };

            // Act
            var parameters = this.mtoaParameterExtractor.ExtractParameters(template).ToArray();
            var parameterFirstName = parameters[0];
            var parameterLastName = parameters[1];

            // Assert
            Assert.Equal("First_Name", parameterFirstName.Key);
            Assert.Equal(FirstName, parameterFirstName.Value);
            Assert.Equal("Last_Name", parameterLastName.Key);
            Assert.Equal(LastName, parameterLastName.Value);
        }

        [Fact]
        public void ExtractParameters_WhenOnlyOnePropertyIsSet_ReturnsOnlyOneValue()
        {
            // Arrange
            var FirstName = "John";
            var template = new MtoaParameterTestWithAttribute
            {
                FirstName = FirstName,
            };

            // Act
            var parameters = this.mtoaParameterExtractor.ExtractParameters(template).ToArray();

            // Assert
            Assert.Single(parameters);
        }

        [Fact]
        public void ExtractParameters_WhenTemplateDoesNotHaveAttribute_ReturnsEmpty()
        {
            // Arrange
            var FirstName = "John";
            var LastName = "Wick";
            var template = new MtoaParameterTestWithoutAttribute
            {
                FirstName = FirstName,
                LastName = LastName,
            };

            // Act
            var parameters = this.mtoaParameterExtractor.ExtractParameters(template).ToArray();

            // Assert
            Assert.Empty(parameters);
        }

        [Fact]
        public void ExtractParameter_WhenTemplateIsNull_ThrowsArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => this.mtoaParameterExtractor.ExtractParameters(null));
        }

        private class MtoaParameterTestWithAttribute
        {
            [MtoaNotificationParameterName("First_Name")]
            public string FirstName { get; set; }

            [MtoaNotificationParameterName("Last_Name")]
            public string LastName { get; set; }
        }

        private class MtoaParameterTestWithoutAttribute
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
