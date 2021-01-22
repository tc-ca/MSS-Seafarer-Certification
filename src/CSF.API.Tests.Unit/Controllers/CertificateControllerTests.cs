namespace CSF.API.Tests.Unit.Controllers
{
    using CSF.API.Controllers;
    using CSF.API.Data.Entities;
    using CSF.API.Services.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using Xunit;

    public class CertificateControllerTests
    {
        private readonly IConfiguration configuration;

        public CertificateControllerTests()
        {
            this.configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }

        [Fact]
        public void RestClient_GetCertificateType_Succeeds()
        {
            // Arrange
            var certificateTypeRepository = new CertificateTypeRepository(this.configuration);
            var controller = new CertificateController(certificateTypeRepository);

            var expectedListCount = 2;

            // Act
            var response = controller.GetCertificateTypes();
            var res = response as OkObjectResult;
            var list = res.Value as List<CertificateType>;

            // Assert
            Assert.NotNull(res.Value);
            Assert.NotEmpty(list);
            Assert.Equal(expectedListCount, list.Count);
        }
    }
}
