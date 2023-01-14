using Application.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCampus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegistroController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userService.Registrar(user);
            return Ok(user);
        }
    }
}
