using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}