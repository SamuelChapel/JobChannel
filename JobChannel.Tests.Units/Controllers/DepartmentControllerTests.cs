using System.Collections.Generic;
using System.Linq;
using JobChannel.Domain.BO;
using Moq;
using Xunit;
using JobChannel.BLL.Services.DepartmentServices;
using JobChannel.API.Controllers.Geographics.Departments;
using JobChannel.API.Controllers.Geographics.Departments.Responses;

namespace JobChannel.Tests.Units.Controllers
{
    public class DepartmentControllerTests
    {
        [Fact]
        public async void ShouldReturnCities_WhenGetAll()
        {
            // Arrange
            var departmentServiceMock = new Mock<IDepartmentService>();
            var departments = new List<Department>()
            {
                new Department(){ Id = 1, Name = "Ile de France"},
                new Department(){ Id = 2, Name = "Hérault" }
            }.AsEnumerable();
            var departmentsResponse = departments.Select(d => new DepartmentGetResponse(d.Id, d.Name));

            departmentServiceMock.Setup(c => c.GetAll().Result)
                           .Returns(departments);
            var departmentController = new DepartmentController(departmentServiceMock.Object);

            // Act
            var result = await departmentController.GetAll();

            // Assert
            Assert.True(result.Count() == departmentsResponse.Count());
            Assert.Equal(departmentsResponse, result);
        }
    }
}
