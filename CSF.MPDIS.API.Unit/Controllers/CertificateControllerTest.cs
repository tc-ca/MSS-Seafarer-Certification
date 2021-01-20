using CSF.MPDIS.API.Controllers;
using CSF.MPDIS.API.Data.Entity;
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
    public class CertificateControllerTest
    {

        [Fact]
        public void RestClient_GetCertificates_Succeeds()
        {
            
            var cr = new CertificateController();

            var res = cr.GetCertificates();

            var t = res as OkObjectResult;

            Assert.NotNull(t.Value);

        }


    }
}
