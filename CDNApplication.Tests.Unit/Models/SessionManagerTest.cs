namespace CDNApplication.Tests.Unit.EmailTemplate
{
    using CDNApplication.Models;
    using Xunit;

    public class SessionManagerTest
    {
        [Fact]
        public void SessionManager_CorrectlySetsSessionStateModel()
        {
            // Arrange
            var sessionStateModel = new SessionStateModel()
            {
                CurrentLanguage = "en",
                LastViewedPage = string.Empty,
            };
            var sessionManager = new SessionManager(sessionStateModel);
            var expectedLanguage = "fr";
            var expectedLastViewedPage = "index";

            // Act
            sessionManager.UpdateSessionState(expectedLanguage, expectedLastViewedPage);

            // Assert
            Assert.Equal(expectedLanguage, sessionStateModel.CurrentLanguage);
            Assert.Equal(expectedLastViewedPage, sessionStateModel.LastViewedPage);
        }

        [Fact]
        public void SessionManager_CorrectlySetsSessionStateModel_WhenOnlyLastViewedPage()
        {
            // Arrange
            var expectedLanguage = "en";
            var sessionStateModel = new SessionStateModel()
            {
                CurrentLanguage = expectedLanguage,
                LastViewedPage = string.Empty,
            };
            var sessionManager = new SessionManager(sessionStateModel);
            var expectedLastViewedPage = "index";

            // Act
            sessionManager.UpdateSessionState(string.Empty, expectedLastViewedPage);

            // Assert
            Assert.Equal(expectedLanguage, sessionStateModel.CurrentLanguage);
            Assert.Equal(expectedLastViewedPage, sessionStateModel.LastViewedPage);
        }

        [Fact]
        public void SessionManager_CorrectlySetsSessionStateModel_WhenOnlyLanguage()
        {
            // Arrange
            var expectedLastViewedPage = "index";
            var sessionStateModel = new SessionStateModel()
            {
                CurrentLanguage = string.Empty,
                LastViewedPage = expectedLastViewedPage,
            };
            var expectedLanguage = "en";
            var sessionManager = new SessionManager(sessionStateModel);

            // Act
            sessionManager.UpdateSessionState(expectedLanguage, string.Empty);

            // Assert
            Assert.Equal(expectedLanguage, sessionStateModel.CurrentLanguage);
            Assert.Equal(expectedLastViewedPage, sessionStateModel.LastViewedPage);
        }
    }
}