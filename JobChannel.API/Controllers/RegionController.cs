using System.Threading.Tasks;
using JobChannel.BLL.Services.RegionServices;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService) => _regionService = regionService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _regionService.GetAllRegions());
        }
    }
}
