using System.Collections.Generic;
using System.Linq;
using JobChannel.API.Controllers.Geographics.Cities;
using JobChannel.API.Controllers.Geographics.Cities.Responses;
using JobChannel.BLL.Services.CityServices;
using JobChannel.Domain.BO;
using Moq;
using Xunit;

namespace JobChannel.Tests.Units.Controllers
{
    public class CityControllerTests
    {
        [Fact]
        public async void ShouldReturnCities_WhenGetAll()
        {
            // Arrange
            var cityServiceMock = new Mock<ICityService>();
            var cities = GetCitiesData();
            var citiesResponse = cities.Select(c => new CityGetResponse(c.Id, c.Name, c.Postcodes.First(), c.Department.Name));

            cityServiceMock.Setup(c => c.GetAll().Result)
                           .Returns(cities);
            var cityController = new CityController(cityServiceMock.Object);

            // Act
            var result = await cityController.GetAll(null);

            // Assert
            Assert.True(result.Count() == citiesResponse.Count());
            Assert.Equal(citiesResponse, result);
        }

        [Fact]
        public async void ShouldReturnCity_WhenGetById()
        {
            // Arrange
            int id = 2;
            var cityServiceMock = new Mock<ICityService>();
            var cities = GetCitiesData();
            var cityExpected = cities.First(c => c.Id == id);
            cityServiceMock.Setup(c => c.GetById(id).Result)
                           .Returns(cityExpected);
            var cityController = new CityController(cityServiceMock.Object);

            // Act
            var result = await cityController.GetById(id);

            // Assert
            Assert.Equal(cityExpected, result);
        }

        private static IEnumerable<City> GetCitiesData()
        {
            yield return new City() { Id = 1, Name = "Paris", Code = "123", Population = 600000, Postcodes = new List<string>() { "75000", "75001" }, Department = Mock.Of<Department>() };
            yield return new City() { Id = 2, Name = "Montpellier", Code = "456", Population = 300000, Postcodes = new List<string>() { "34000", "34070" }, Department = Mock.Of<Department>() };
        }
    }
}
