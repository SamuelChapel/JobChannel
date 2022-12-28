using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobChannel.API.Controllers.Geographics.Departments.Responses;
using JobChannel.BLL.Services.DepartmentServices;
using JobChannel.Domain.BO;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers.Geographics.Departments
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) => _departmentService = departmentService;

        [HttpGet]
        public async Task<IEnumerable<DepartmentGetResponse>> GetAll()
        {
            return (await _departmentService.GetAll()).Select(d => new DepartmentGetResponse(d.Id, d.Name));
        }

        [HttpGet("{id}")]
        public async Task<Department> GetById(int id)
        {
            return await _departmentService.GetById(id);
        }

        [HttpGet("Region/{id}")]
        public async Task<IEnumerable<DepartmentGetResponse>> GetByRegionId(int id)
        {
            return (await _departmentService.GetDepartmentsByRegionId(id)).Select(d => new DepartmentGetResponse(d.Id, d.Name));
        }
    }
}
