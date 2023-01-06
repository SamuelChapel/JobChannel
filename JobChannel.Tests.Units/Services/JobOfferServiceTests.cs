using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.DAL.UOW;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.Domain.BO;
using JobChannel.Tests.Commons.Seeds;
using Moq;
using Xunit;

namespace JobChannel.Tests.Units.Services
{
    public class JobOfferServiceTests
    {
        [Fact]
        public async void ShouldReturnJobOffer_WhenGetAll()
        {
            // Arrange
            var jobOffersExpected = JobOfferSeed.GetJobOfferData();
            var jobOfferRepositoryMock = new Mock<IJobOfferRepository>();
            var dbContextMock = new Mock<IUnitOfWork>();
            var searchFields = new Dictionary<string, dynamic>();
            dbContextMock.Setup(c =>c.JobOfferRepository.GetAll(searchFields)).ReturnsAsync(jobOffersExpected);
            var jobOfferService = new JobOfferService(dbContextMock.Object);

            // Act
            var result = await jobOfferService.GetAll(searchFields);

            // Assert
            Assert.Equal(jobOffersExpected, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public async void ShouldReturnJobOfferId_WhenCreate(int id)
        {
            // Arrange
            var jobOffer = new JobOffer();
            var jobServiceMock = Mock.Of<IJobService>();
            var cityServiceMock = Mock.Of<ICityService>();
            var contractServiceMock = Mock.Of<IContractService>();
            var jobOfferRepositoryMock = new Mock<IJobOfferRepository>();
            var dbContextMock = new Mock<IUnitOfWork>();
            dbContextMock.Setup(c => c.JobOfferRepository.Create(jobOffer)).ReturnsAsync(id);
            var jobOfferService = new JobOfferService(dbContextMock.Object);

            // Act
            var result = await jobOfferService.Create( jobOffer, jobServiceMock, cityServiceMock, contractServiceMock);

            // Assert
            Assert.Equal(id, result);
        }
    }
}
