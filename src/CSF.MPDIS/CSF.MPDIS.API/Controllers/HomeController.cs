using CSF.MPDIS.API.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.MPDIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        [Route("certificate")]
        public IActionResult GetCertificates()
        {

            return Ok(CertificateType.GetAllCertificateTypes());

        }

        [HttpGet]
        [Route("document")]
        public IActionResult GetDocuments()
        {

            return Ok(DocumentType.GetAllDocumentTypes());

        }

    }
}
