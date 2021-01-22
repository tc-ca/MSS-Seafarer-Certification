namespace CSF.API.Tests.Unit.Controllers
{
    using CSF.API.Controllers;
    using CSF.API.Data.Entities;
    using CSF.API.Services.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using Xunit;

    public class DocumentControllerTests
    {
        private readonly IConfiguration configuration;

        public DocumentControllerTests()
        {
            this.configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }

        [Fact]
        public void RestClient_GetDocumentType_Succeeds()
        {
            // Arrange
            var documentTypeRepository = new DocumentTypeRepository(this.configuration);
            var controller = new DocumentController(documentTypeRepository);

            var expectedListCount = 2;

            // Act
            var response = controller.GetDocumentTypes();
            var res = response as OkObjectResult;
            var list = res.Value as List<DocumentType>;

            // Assert
            Assert.NotNull(res.Value);
            Assert.NotEmpty(list);
            Assert.Equal(expectedListCount, list.Count);
        }
    }
}
