using Microsoft.AspNetCore.Mvc;

namespace CorsExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Get");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Post");
        }

        [HttpPatch]
        public IActionResult Patch()
        {
            return Ok("Patch");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Put");
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}