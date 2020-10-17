using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuasarOperation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopSecretSplitController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendMessage()
        {
            return Ok();
        }
    }
}