using Bunit;
using Xunit;

using CDNApplication.Components.Banners;

namespace CDNApplication.Tests.Unit
{
    public class BannerTests : TestContext
    {
        [Fact]
        public void BuildRenderTree_BuildsBetaBanner()
        {
            // Arrange
            var bannerComponent = RenderComponent<Banner>(
                (nameof(Banner.Message), "Hello world!"),
                (nameof(Banner.Phase), "BETA"),
                (nameof(Banner.State), BannerState.BETA),
                (nameof(Banner.ShowBanner), false)
             );

            // Act
            var bannerPhaser = bannerComponent.Find(".banner-phase");

            // Assert

        }
    }
}