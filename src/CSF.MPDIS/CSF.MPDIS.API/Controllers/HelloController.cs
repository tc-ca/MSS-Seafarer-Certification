using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.MPDIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world. Holiday Greetings from Transport Canada :)");
        }

        [HttpGet]
        [Route("guid")]
        public IActionResult GetGuid()
        {

            return Ok("Your unique ID is: " + Guid.NewGuid().ToString());
        }

    }
}
