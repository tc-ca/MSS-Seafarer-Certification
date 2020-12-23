namespace CDNApplication.Tests.Unit.Services.EmailNotification
{
    using CDNApplication.Models.PageModels;
    using CDNApplication.Services.EmailNotification;
    using CDNApplication.Shared;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Localization;
    using Moq;
    using System;
    using Xunit;

    public class SeafarersDocumentSubmissionEmailBuilderTests
    {
        public SeafarersDocumentSubmissionEmailBuilderTests()
        {

        }

        [Fact]
        public void BuildWhenPassingAValidUploadDocumentPageModel_Succeeeds()
        {
            // Arrange
            var stringLocalizer = this.buildMockStringLocalizer();
            var configuration = this.buildMockIConfiguration();
            var languageCode = "en";
            var seafarersDocumentSubmissionEmailBuilder = new SeafarersDocumentSubmissionEmailBuilder(stringLocalizer.Object, configuration.Object, languageCode);
            var uploadDocumentPageModel = this.buildUploadDocumentPageModel();

            var expectedFrom = "noreply@johnwick.ca";
            var expectedIsHtml = true;
            var expectedLanguage = "English";
            var expectedNotificationTemplate = "JohnWick_Warning";
            var expectedServiceRequestId = 1337;
            var expectedTo = "winston@continental.ca";
            var expectedUserId = 97633;
            var expectedUserName = "Nobody";

            // Act
            var mtoaEmailNotificationDTO = seafarersDocumentSubmissionEmailBuilder.Build(uploadDocumentPageModel);

            // Assert
            Assert.Equal(expectedFrom, mtoaEmailNotificationDTO.From);
            Assert.Equal(expectedIsHtml, mtoaEmailNotificationDTO.IsHtml);
            Assert.Equal(expectedLanguage, mtoaEmailNotificationDTO.Language);
            Assert.Equal(expectedNotificationTemplate, mtoaEmailNotificationDTO.NotificationTemplateName);
            Assert.Equal(expectedServiceRequestId, mtoaEmailNotificationDTO.ServiceRequestId);
            Assert.Equal(expectedTo, mtoaEmailNotificationDTO.To);
            Assert.Equal(expectedUserId, mtoaEmailNotificationDTO.UserId);
            Assert.Equal(expectedUserName, mtoaEmailNotificationDTO.UserName);
        }

        [Fact]
        public void BuildWhenPassingANullUploadDocumentPageModel_ThrowsArgumentNullException()
        {
            // Arrange
            var stringLocalizer = this.buildMockStringLocalizer();
            var configuration = this.buildMockIConfiguration();
            var languageCode = "en";
            var seafarersDocumentSubmissionEmailBuilder = new SeafarersDocumentSubmissionEmailBuilder(stringLocalizer.Object, configuration.Object, languageCode);

            // Assert
            Assert.Throws<ArgumentNullException>(() => seafarersDocumentSubmissionEmailBuilder.Build(null));
        }

        private UploadDocumentPageModel buildUploadDocumentPageModel()
        {
            var uploadDocumentPageModel = new UploadDocumentPageModel()
            {
                CdnNumber = "1234567",
                CertificateType = "Master Mariner",
                ConfirmationNumber = "7654321",
                EmailAddress = "winston@continental.ca",
                MtoaServiceRequestId = 1234,
                FileDescription = "My Files",
                PhoneNumber = "123-456-7890",
                SubmissionType = Data.SubmissionType.NEW
            };
            return uploadDocumentPageModel;
        }

        private Mock<IStringLocalizer<Common>> buildMockStringLocalizer()
        {
            var stringLocalizer = new Mock<IStringLocalizer<Common>>();
            stringLocalizer.Setup(localizer => localizer["NEW"]).Returns(new LocalizedString("NEW", "New certificate"));
            stringLocalizer.Setup(localizer => localizer["RENEWAL"]).Returns(new LocalizedString("RENEWAL", "Renew certificate"));
            return stringLocalizer;
        }

        private Mock<IConfiguration> buildMockIConfiguration()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["EmailSubmissionTemplateName"]).Returns("JohnWick_Warning");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["ServiceRequestId"]).Returns("1337");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["UserId"]).Returns("97633");
            mockConfig.Setup(config => config.GetSection("MtoaServiceSettings")["ReplyEmail"]).Returns("noreply@johnwick.ca");

            return mockConfig;
        }
    }
}
