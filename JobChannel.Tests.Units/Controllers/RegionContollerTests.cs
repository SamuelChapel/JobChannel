using JobChannel.API.Controllers.Geographics.Regions;
using JobChannel.BLL.Services.RegionServices;
using JobChannel.Domain.BO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JobChannel.Tests.Units.Controllers
{
    public class RegionContollerTests
    {
        [Fact]
        public async void ShouldReturnRegions_WhenGetAll()
        {
            // Arrange
            var mockRegionService = new Mock<IRegionService>();
            var regions = new List<Region>
        {
            new Region { Id = 1, Name = "Region 1" },
            new Region { Id = 2, Name = "Region 2" }
        }.AsEnumerable();
            mockRegionService.Setup(service => service.GetAll()).ReturnsAsync(regions);
            var regionController = new RegionController(mockRegionService.Object);

            // Act
            var result = (await regionController.GetAll()) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(regions, result.Value);
        }
    }
}
