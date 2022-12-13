using System.Threading.Tasks;
using JobChannel.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) => _departmentService = departmentService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllDepartments());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByRegionId(int id)
        {
            return Ok(await _departmentService.GetDepartmentsByRegionId(id));
        }
    }
}
