namespace CSF.API.Controllers
{
    using CSF.API.Services.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private ICertificateTypeRepository certifcateTypeRepository;
        public CertificateController(ICertificateTypeRepository certifcateTypeRepository)
        {
            this.certifcateTypeRepository = certifcateTypeRepository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetCertificateTypes()
        {

            return Ok(certifcateTypeRepository.GetAll());
        }

    }
}
