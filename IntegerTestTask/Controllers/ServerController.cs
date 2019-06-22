using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegerTestTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegerTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        // GET: api/Server/action
        [HttpGet("[action]")]
        public IActionResult ServerSettings()
        {
            return new JsonResult(new Settings());
        }
    }
}