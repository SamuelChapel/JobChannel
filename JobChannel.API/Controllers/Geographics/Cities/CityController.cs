using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobChannel.BLL.Services.CityServices;
using System.Collections.Generic;
using JobChannel.API.Controllers.Geographics.Cities.Responses;
using System.Linq;
using JobChannel.Domain.BO;

namespace JobChannel.API.Controllers.Geographics.Cities
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) => _cityService = cityService;

        [HttpGet]
        public async Task<IEnumerable<CityGetResponse>> GetAll()
        {
            return (await _cityService.GetAll()).Select(c => new CityGetResponse(c.Id, c.Name));
        }

        [HttpGet("Department/{id}")]
        public async Task<IEnumerable<CityGetResponse>> GetByDepartmentId(int id)
        {
            return (await _cityService.GetCitiesByDepartmentId(id)).Select(c => new CityGetResponse(c.Id, c.Name));
        }

        [HttpGet("{id}")]
        public async Task<City> GetById(int id)
        {
            return await _cityService.GetById(id);
        }
    }
}
