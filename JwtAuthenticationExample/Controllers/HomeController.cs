using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationExample.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Everyone()
        {
            return Ok("Any user should see this");
        }

        [HttpGet]
        public IActionResult Members()
        {
            return Ok("Any user with a valid Jwt token should see this");
        }

        [HttpGet]
        [Authorize(Roles = "SuperUser,Admin")]
        public IActionResult Admins()
        {
            return Ok("Any user with a valid Jwt token and has an admin or a superuser role should see this");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Authorize(Roles = "Editor")]
        public IActionResult Change()
        {
            return Ok("Any user with a valid Jwt token and has an user and an editor role should see this");
        }

        [HttpGet]
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Employees()
        {
            return Ok("Any user with a valid Jwt token and has a employeenumber claim should see this");
        }

        [HttpGet]
        [Authorize(Policy = "Founders")]
        public IActionResult Founders()
        {
            return Ok("Any user with a valid Jwt token and has a employeenumber claim with the value 1, 2, 3, 4 or 5 should see this");
        }

        [HttpGet]
        [Authorize(Policy = "AtLeast21")]
        public IActionResult Grownups()
        {
            return Ok("Any user with a valid Jwt token and pass the AtLeast21 requirement should see this");
        }

        [HttpGet]
        [Authorize(Policy = "BadgeEntry")]
        public IActionResult Maindoor()
        {
            return Ok("Any user with a valid Jwt token and pass the BadgeEntry assertion should see this");
        }
    }
}