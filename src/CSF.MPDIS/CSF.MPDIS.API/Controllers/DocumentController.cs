namespace CSF.API.Controllers
{
    using CSF.API.Services.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentTypeRepository documentTypeRepository;
        public DocumentController(IDocumentTypeRepository documentTypeRepository)
        {
            this.documentTypeRepository = documentTypeRepository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetDocumentTypes()
        {

            return Ok(documentTypeRepository.GetAll());
        }

    }
}
