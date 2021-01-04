namespace CSF.Web.Client.Tests.Unit.Middleware
{
    using CSF.Web.Client.Middleware;
    using CSF.Web.Client.Tests.Unit.TestHelpers;
    using CSF.Web.Client.Utilities;
    using GoC.WebTemplate.Components.Core.Services;
    using GoC.WebTemplate.Components.Entities;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class PageSettingsMiddlewareTests
    {
        private readonly Mock<IConfiguration> configuration;

        private readonly string headerTitle = "Seafarer Credentials Online Prototype";

        private readonly BreadcrumbOptions canadaBreadcrumbOptions = new BreadcrumbOptions { Href = "http://www.canada.ca/en/index.html", Title = "Canada.ca" };
        private readonly BreadcrumbOptions marineBreadcrumbOptions = new BreadcrumbOptions { Href = "https://www.tc.gc.ca/en/services/marine.html", Title = "Marine Transportation" };
        private readonly BreadcrumbOptions csfBreadcrumbOptions = new BreadcrumbOptions { Href = "/", Title = "Seafarer Credentials Online Prototype" };

        private ModelAccessor modelAccessor { get; init; }
        private PageSettingsMiddleware pageSettingsMiddleware { get; init; }

        public PageSettingsMiddlewareTests()
        {
            this.configuration = ArrangeConfigurationMock();
            this.modelAccessor = ArrangeModelAccessor();
            this.pageSettingsMiddleware = ArrangePageSettingsMiddleware();
        }

        [Fact]
        public void InvokeAsync_WhenModelAccessorIsNull_ThrowException()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>( () =>
                 this.pageSettingsMiddleware.InvokeAsync(httpContext, null));
        }

        [Fact]
        public void InvokeAsync_ValidRequest_SetsDateModifiedToToday()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            
            // Act
            var task = this.pageSettingsMiddleware.InvokeAsync(httpContext, this.modelAccessor);

            // Assert
            Assert.Equal(DateTime.Now.Date, modelAccessor.Model.DateModified);
        }

        [Fact]
        public void InvokeAsync_ValidRequest_SetsHeaderTitle()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            
            // Act
            var task = this.pageSettingsMiddleware.InvokeAsync(httpContext, this.modelAccessor);

            // Assert - assertion is set to Contains because the ModelAccessor class applies a postfix to the page's HeaderTitle.
            Assert.Contains(this.headerTitle, modelAccessor.Model.HeaderTitle);
        }

        [Fact]
        public void InvokeAsync_ValidRequest_SetsBreadcrumbs()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            
            var canadaBreadcrumb = new Breadcrumb { Href = this.canadaBreadcrumbOptions.Href, Title = this.canadaBreadcrumbOptions.Title };
            var marineBreadcrumb = new Breadcrumb { Href = this.marineBreadcrumbOptions.Href, Title = this.marineBreadcrumbOptions.Title };
            var csfBreadcrumb = new Breadcrumb { Href = this.csfBreadcrumbOptions.Href, Title = this.csfBreadcrumbOptions.Title };

            // Act
            var task = pageSettingsMiddleware.InvokeAsync(httpContext, modelAccessor);

            // Assert
            Assert.Collection<Breadcrumb>(modelAccessor.Model.Breadcrumbs,
                item => {
                    Assert.Equal(canadaBreadcrumb.Href, item.Href);
                    Assert.Equal(canadaBreadcrumb.Title, item.Title);
                },
                item => {
                    Assert.Equal(marineBreadcrumb.Href, item.Href);
                    Assert.Equal(marineBreadcrumb.Title, item.Title);
                },
                item => {
                    Assert.Equal(csfBreadcrumb.Href, item.Href);
                    Assert.Equal(csfBreadcrumb.Title, item.Title);
                });
        }

        private Mock<IConfiguration> ArrangeConfigurationMock()
        {
            var mockConfiguration = new Mock<IConfiguration>();

            mockConfiguration.Setup(x => x.GetSection("GoCWebTemplate")["HeaderTitle"]).Returns(this.headerTitle);
            
            // mock the 'canada' breadcrumb
            var canadaHrefSectionMock = new Mock<IConfigurationSection>();
            canadaHrefSectionMock.Setup(s => s.Value).Returns(canadaBreadcrumbOptions.Href);
            var canadaTitleSectionMock = new Mock<IConfigurationSection>();
            canadaTitleSectionMock.Setup(s => s.Value).Returns(canadaBreadcrumbOptions.Title);

            var canadaBreadcrumbSectionMock = new Mock<IConfigurationSection>();
            canadaBreadcrumbSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { canadaHrefSectionMock.Object, canadaTitleSectionMock.Object });
            
            mockConfiguration.Setup(c => c.GetSection(BreadcrumbOptions.CanadaBreadcrumb)).Returns(canadaBreadcrumbSectionMock.Object);

            // mock the 'marine' breadcrumb
            var marineHrefSectionMock = new Mock<IConfigurationSection>();
            marineHrefSectionMock.Setup(s => s.Value).Returns(marineBreadcrumbOptions.Href);
            var marineTitleSectionMock = new Mock<IConfigurationSection>();
            marineTitleSectionMock.Setup(s => s.Value).Returns(marineBreadcrumbOptions.Title);

            var marineBreadcrumbSectionMock = new Mock<IConfigurationSection>();
            marineBreadcrumbSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { marineHrefSectionMock.Object, marineTitleSectionMock.Object });
            mockConfiguration.Setup(c => c.GetSection(BreadcrumbOptions.MarineBreadcrumb)).Returns(marineBreadcrumbSectionMock.Object);

            // mock the 'csf' breadcrumb
            var csfHrefSectionMock = new Mock<IConfigurationSection>();
            csfHrefSectionMock.Setup(s => s.Value).Returns(csfBreadcrumbOptions.Href);
            var csfTitleSectionMock = new Mock<IConfigurationSection>();
            csfTitleSectionMock.Setup(s => s.Value).Returns(csfBreadcrumbOptions.Title);

            var csfBreadcrumbSectionMock = new Mock<IConfigurationSection>();
            csfBreadcrumbSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { csfHrefSectionMock.Object, csfTitleSectionMock.Object });
            mockConfiguration.Setup(c => c.GetSection(BreadcrumbOptions.CSFBreadcrumb)).Returns(csfBreadcrumbSectionMock.Object);

            return mockConfiguration;
        }

        private ModelAccessor ArrangeModelAccessor()
        {
            var memoryCacheService = CreateMemoryCacheService();
            var mockEnvironment = Mock.Of<IHostingEnvironment>();
            return new ModelAccessor(memoryCacheService, mockEnvironment);
        }

        private PageSettingsMiddleware ArrangePageSettingsMiddleware()
        {
            RequestDelegate next = (httpContext) => Task.CompletedTask;
            return new PageSettingsMiddleware(next, this.configuration.Object);
        }

        private IMemoryCache CreateMemoryCacheService()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<IMemoryCache>();
        }
    }
}