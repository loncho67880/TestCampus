using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCampus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesServices _citiesServices;
        public CitiesController(ICitiesServices citiesServices)
        {
            _citiesServices = citiesServices;
        }

        [HttpGet]
        public IActionResult Get(string search)
        {
            return Ok(_citiesServices.getCities(search));
        }
    }
}
