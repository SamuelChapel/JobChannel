using JobChannel.BLL.Services.DepartmentServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobChannel.BLL.Services.CityServices;

namespace JobChannel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) => _cityService = cityService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cityService.GetAllCities());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByDepartmentId(int id)
        {
            return Ok(await _cityService.GetCitiesByDepartmentId(id));
        }
    }
}
