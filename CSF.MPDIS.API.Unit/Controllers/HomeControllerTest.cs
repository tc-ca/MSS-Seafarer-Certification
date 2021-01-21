using CSF.MPDIS.API.Controllers;
using CSF.MPDIS.API.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace CSF.Web.Client.Tests.Unit.Controllers
{
    public class HomeControllerTest
    {

        [Fact]
        public void RestClient_GetCertificates_Succeeds()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var response = controller.GetCertificates();

            var res = response as OkObjectResult;

            var list = res.Value as List<CertificateType>;

            // Assert
            Assert.NotNull(res.Value);

            Assert.NotEmpty(list);

        }

        [Fact]
        public void RestClient_GetDocuments_Succeeds()
        {
            // Assert
            var controller = new HomeController();

            // Act

            var response = controller.GetDocuments();

            var res = response as OkObjectResult;

            var list = res.Value as List<DocumentType>;

            // Assert
            Assert.NotNull(res.Value);

            Assert.NotEmpty(list);

        }

    }
}
