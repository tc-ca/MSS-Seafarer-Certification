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
    using System.Threading.Tasks;
    using Xunit;

    public class PageSettingsMiddlewareTests
    {
        private readonly IConfiguration configuration;

        private readonly BreadcrumbOptions canadaBreadcrumbOptions = new BreadcrumbOptions();
        private readonly BreadcrumbOptions marineBreadcrumbOptions = new BreadcrumbOptions();
        private readonly BreadcrumbOptions csfBreadcrumbOptions = new BreadcrumbOptions();

        private ModelAccessor modelAccessor { get; init; }
        private PageSettingsMiddleware pageSettingsMiddleware { get; init; }

        public PageSettingsMiddlewareTests()
        {
            this.configuration = ConfigurationHelper.InitConfiguration();

            this.configuration?.GetSection(BreadcrumbOptions.CanadaBreadcrumb).Bind(this.canadaBreadcrumbOptions);
            this.configuration?.GetSection(BreadcrumbOptions.MarineBreadcrumb).Bind(this.marineBreadcrumbOptions);
            this.configuration?.GetSection(BreadcrumbOptions.CSFBreadcrumb).Bind(this.csfBreadcrumbOptions);

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
            Assert.Contains(this.configuration.GetSection("GoCWebTemplate")["HeaderTitle"], modelAccessor.Model.HeaderTitle);
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
                    Assert.Equal(marineBreadcrumbOptions.Href, item.Href);
                    Assert.Equal(marineBreadcrumbOptions.Title, item.Title);
                },
                item => {
                    Assert.Equal(csfBreadcrumbOptions.Href, item.Href);
                    Assert.Equal(csfBreadcrumbOptions.Title, item.Title);
                });
        }

        private ModelAccessor ArrangeModelAccessor()
        {
            var memoryCacheService = CreateMemoryCacheService();
            var mockEnvironment = Mock.Of<IHostingEnvironment>();
            return new ModelAccessor(memoryCacheService, (IHostingEnvironment)mockEnvironment);
        }

        private PageSettingsMiddleware ArrangePageSettingsMiddleware()
        {
            RequestDelegate next = (httpContext) => Task.CompletedTask;
            return new PageSettingsMiddleware(next, this.configuration);
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