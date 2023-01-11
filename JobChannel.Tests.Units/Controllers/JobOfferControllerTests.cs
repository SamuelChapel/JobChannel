using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.API.Controllers.JobOffers;
using JobChannel.API.Controllers.JobOffers.Requests;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.Domain.BO;
using JobChannel.Tests.Commons.Seeds;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JobChannel.Tests.Units.Controllers
{
    public class JobOfferControllerTests
    {
        [Theory]
        [InlineData(1)]
        public async Task GetById_ReturnsCorrectJobOffer(int id)
        {
            // Arrange
            var mockJobOfferService = new Mock<IJobOfferService>();
            var jobOffers = JobOfferSeed.GetJobOfferData();
            var expectedJobOffer = jobOffers.First(jo => jo.Id == id);
            mockJobOfferService.Setup(service => service.GetById(id)).ReturnsAsync(expectedJobOffer);
            var jobOfferController = new JobOfferController(mockJobOfferService.Object);

            // Act
            var result = await jobOfferController.GetById(1);

            // Assert
            Assert.Equal(expectedJobOffer, result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult()
        {
            // Arrange
            var mockJobOfferService = new Mock<IJobOfferService>();
            mockJobOfferService.Setup(service => service.Update(It.IsAny<JobOffer>())).ReturnsAsync(1);
            var mockValidator = new Mock<IValidator<JobOfferUpdateRequest>>();
            var jobOfferController = new JobOfferController(mockJobOfferService.Object);
            var jobOfferUpdateRequest = new JobOfferUpdateRequest(
                1,
                "Software Developer",
                "We are seeking a skilled software developer to join our team.",
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 3),
                "https://example.com/jobs/software-developer",
                "75,000 - 100,000 per year",
                "3+ years",
                "Acme Inc.",
                1,
                11,
                1);

            // Act
            var result = await jobOfferController.Update(1, jobOfferUpdateRequest, mockValidator.Object) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task DeleteJobOffer_ReturnsNoContentResult()
        {
            // Arrange
            var mockJobOfferService = new Mock<IJobOfferService>();
            var jobOffer = new JobOffer() { Id = 1 };
            mockJobOfferService.Setup(service => service.Delete(It.IsAny<JobOffer>())).ReturnsAsync(1);
            var jobOfferController = new JobOfferController(mockJobOfferService.Object);

            // Act
            var result = await jobOfferController.Delete(jobOffer.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteJobOffer_ReturnsNotFoundResult()
        {
            // Arrange
            var mockJobOfferService = new Mock<IJobOfferService>();
            var jobOffer = new JobOffer() { Id = 1 };
            mockJobOfferService.Setup(service => service.Delete(It.IsAny<JobOffer>())).ReturnsAsync(0);
            var jobOfferController = new JobOfferController(mockJobOfferService.Object);

            // Act
            var result = await jobOfferController.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOkResult()
        {
            // Arrange
            var mockJobOfferService = new Mock<IJobOfferService>();
            mockJobOfferService.Setup(service => service.Create(It.IsAny<JobOffer>(), It.IsAny<IJobService>(), It.IsAny<ICityService>(), It.IsAny<IContractService>())).ReturnsAsync(1);
            var mockJobService = new Mock<IJobService>();
            var mockCityService = new Mock<ICityService>();
            var mockContractService = new Mock<IContractService>();
            var mockValidator = new Mock<IValidator<JobOfferCreateRequest>>();
            var jobOfferController = new JobOfferController(mockJobOfferService.Object);
            var jobOfferCreateRequest = new JobOfferCreateRequest("Software Developer",
                "We are seeking a skilled software developer to join our team.",
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 3),
                "https://example.com/jobs/software-developer",
                "75,000 - 100,000 per year",
                "3+ years",
                "Acme Inc.",
                1,
                11,
                1);

            // Act
            var result = await jobOfferController.Create(jobOfferCreateRequest, mockJobService.Object, mockCityService.Object, mockContractService.Object, mockValidator.Object) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Value);
        }
    }
}
