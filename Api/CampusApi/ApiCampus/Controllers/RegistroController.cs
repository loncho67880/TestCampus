using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCampus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {

            return Ok(user);
        }
    }
}
