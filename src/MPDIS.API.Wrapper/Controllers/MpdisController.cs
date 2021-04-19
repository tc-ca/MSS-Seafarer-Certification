using Microsoft.AspNetCore.Mvc;
using MPDIS.API.Wrapper.Services.MPDIS;
using MPDIS.API.Wrapper.Services.MPDIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDIS.API.Wrapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MpdisController : Controller
    {
        private IMpdisService mpdisService;
        public MpdisController(IMpdisService mpdisService)
        {
            this.mpdisService = mpdisService;
        }

        [HttpGet]
        [Route("Applicant/{candidateDocumentNumber}")]
        public IActionResult GetApplicantInformation(string candidateDocumentNumber)
        {
            var applicantInformation = this.mpdisService.GetApplicantByCdn(candidateDocumentNumber);
            return Ok(applicantInformation);
        }

        [HttpPost]
        [Route("applicants/search")]
        public IActionResult Search(ApplicantSearchCriteria searchCriteria)
        {
            var applicantSearchResult = this.mpdisService.Search(searchCriteria);
            return Ok(applicantSearchResult);
        }
    }
}
