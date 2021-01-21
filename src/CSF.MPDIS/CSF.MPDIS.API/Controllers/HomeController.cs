namespace CSF.MPDIS.API.Controllers
{
    using CSF.MPDIS.API.Data.Entities;
    using CSF.MPDIS.API.Services.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ICertificateTypeRepository certifcateTypeRepository;
        public CertificateController(ICertificateTypeRepository certifcateTypeRepository)
        {
            this.certifcateTypeRepository = certifcateTypeRepository;
        }

        [HttpGet]
        [Route("certificate")]
        public IActionResult GetCertificates()
        {

            return Ok(certifcateTypeRepository.GetAll());
        }

    }
}
