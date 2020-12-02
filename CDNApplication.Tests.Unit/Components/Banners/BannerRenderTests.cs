namespace CDNApplication.Tests.Unit.Components.Banners
{
    using Bunit;
    using CDNApplication.Components.Banners;
    using Xunit;
    public class BanneRenderTests : TestContext
    {
        [Fact]
        public void BuildRenderTree_Correctly_BuildsBetaBanner()
        {
            // Arrange
            var bannerComponent = RenderComponent<Banner>(
                    (nameof(Banner.Message), "Hello world!"),
                    (nameof(Banner.State), BannerState.BETA),
                    (nameof(Banner.ShowBanner), false)
                );
            var expectedBannerPhase = "Beta";
            var expectedBannerContainerClass = "prototype";
            var expectedBannerMessage = "Hello world!";
            var expectBannerContainerHidden = true;
            // Act
            var bannerContainer = bannerComponent.Find(".banner-container");
            var bannerPhase = bannerComponent.Find(".banner-phase > span").InnerHtml;
            var bannerContainerClasses = bannerContainer.ClassName;
            var bannerContainerHidden = bannerContainer.HasAttribute("hidden");
            var bannerMessage = bannerComponent.Find(".banner-text").InnerHtml.Trim();
            // Assert
            Assert.Equal(expectedBannerPhase, bannerPhase);
            Assert.Contains(expectedBannerContainerClass, bannerContainerClasses);
            Assert.Equal(expectedBannerMessage, bannerMessage);
            Assert.Equal(expectBannerContainerHidden, bannerContainerHidden);
        }
        [Fact]
        public void BuildRenderTree_Correctly_BuildsAlphaBanner()
        {
            // Arrange
            var bannerComponent = RenderComponent<Banner>(
                (nameof(Banner.Message), "Hello world!"),
                (nameof(Banner.State), BannerState.ALPHA),
                (nameof(Banner.ShowBanner), false)
            );
            var expectedBannerPhase = "Alpha";
            // Act
            var bannerPhase = bannerComponent.Find(".banner-phase > span").InnerHtml;
            // Assert
            Assert.Equal(expectedBannerPhase, bannerPhase);
        }
        [Fact]
        public void BuildRenderTree_Correctly_BuildsWarningBanner()
        {
            // Arrange
            var bannerComponent = RenderComponent<Banner>(
                (nameof(Banner.Message), "Hello world!"),
                (nameof(Banner.State), BannerState.WARNING),
                (nameof(Banner.ShowBanner), false)
            );
            var expectedBannerPhase = @"<path fill-rule=""evenodd"" d=""M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5a.905.905 0 0 0-.9.995l.35 3.507a.552.552 0 0 0 1.1 0l.35-3.507A.905.905 0 0 0 8 5zm.002 6a1 1 0 1 0 0 2 1 1 0 0 0 0-2z""></path>";
            // Act
            var bannerPhase = bannerComponent.Find("svg").InnerHtml.Trim();
            // Assert
            Assert.Equal(expectedBannerPhase, bannerPhase);
        }
        [Fact]
        public void BuildRenderTree_Correctly_BuildErrorBanner()
        {
            // Arrange
            var bannerComponent = RenderComponent<Banner>(
                (nameof(Banner.Message), "Hello world!"),
                (nameof(Banner.State), BannerState.ERROR),
                (nameof(Banner.ShowBanner), false)
            );
            var expectedBannerPhase = @"<path fill-rule=""evenodd"" d=""M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8 4a.905.905 0 0 0-.9.995l.35 3.507a.552.552 0 0 0 1.1 0l.35-3.507A.905.905 0 0 0 8 4zm.002 6a1 1 0 1 0 0 2 1 1 0 0 0 0-2z""></path>";
            // Act
            var bannerPhase = bannerComponent.Find("svg").InnerHtml.Trim();
            // Assert
            Assert.Equal(expectedBannerPhase, bannerPhase);
        }
        [Fact]
        public void BuildRenderTree_Correctly_BuildSuccessBanner()
        {
            // Arrange
            var bannerComponent = RenderComponent<Banner>(
                (nameof(Banner.Message), "Hello world!"),
                (nameof(Banner.State), BannerState.SUCCESS),
                (nameof(Banner.ShowBanner), false)
            );
            var expectedBannerPhase = @"<path fill-rule=""evenodd"" d=""M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z""></path>";
            // Act
            var bannerPhase = bannerComponent.Find("svg").InnerHtml.Trim();
            // Assert
            Assert.Equal(expectedBannerPhase, bannerPhase);
        }
        [Fact]
        public void BuildRenderTree_CorrectlyHides_Banner()
        {
            // Arrange
            var bannerComponent = RenderComponent<Banner>(
                    (nameof(Banner.Message), "Hello world!"),
                    (nameof(Banner.State), BannerState.BETA),
                    (nameof(Banner.ShowBanner), false)
                );
            var expectBannerContainerHidden = true;
            // Act
            var bannerContainer = bannerComponent.Find(".banner-container");
            var bannerContainerHidden = bannerContainer.HasAttribute("hidden");
            // Assert
            Assert.Equal(expectBannerContainerHidden, bannerContainerHidden);
        }
        [Fact]
        public void BannerBase_ClassName_ReturnsEmptyIfStateIsInvalid()
        {
            // Arrange
            var bannerBase = new BannerBase()
            {
                State = (BannerState)int.MaxValue
            };
            var expectedBannerClassName = string.Empty;
            // Act
            var bannerClassName = bannerBase.ClassName;
            // Assert
            Assert.Equal(expectedBannerClassName, bannerClassName);
        }
    }
}