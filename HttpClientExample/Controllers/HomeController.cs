using HttpClientExample.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly MyHttpClient http;

        public HomeController(MyHttpClient http) 
        {
            this.http = http;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //Call another web api
            var result = await http.GetFromJson();
            return Ok(result);
        }
    }
}