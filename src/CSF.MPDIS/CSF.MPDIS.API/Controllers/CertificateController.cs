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
    public class CertificateController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public IActionResult GetCertificates()
        {

            return Ok(CertificateType.GetAllCertificateTypes());
        }

    }
}
