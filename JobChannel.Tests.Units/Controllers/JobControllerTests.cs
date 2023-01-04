using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobChannel.API.Controllers.Jobs;
using JobChannel.BLL.Services.JobServices;
using JobChannel.Domain.BO;
using Moq;
using Xunit;

namespace JobChannel.Tests.Units.Controllers
{
    public class JobControllerTests
    {
        [Fact]
        public async void ShouldReturnJobs_WhenGetAll()
        {
            //Arrange
            var jobServiceMock = new Mock<IJobService>();
            var jobs = new List<Job>()
            {
                new Job { Id = 1, Name = "Développeur", CodeRome = "M1805" }
            }.AsEnumerable();
            jobServiceMock.Setup(j => j.GetAll().Result)
                          .Returns(jobs);

            JobController jobController = new JobController(jobServiceMock.Object);

            //Act
            var result = await jobController.GetAll();

            //Assert
            Assert.Single(result);
            Assert.Collection(result, j => j.Equals(jobs));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async void ShouldReturnJob_WhenGetById(int id)
        {
            //Arrange
            var jobServiceMock = new Mock<IJobService>();
            var job = new Job { Id = id, Name = "Développeur", CodeRome = "M1805" };
            jobServiceMock.Setup(j => j.GetById(id).Result)
                          .Returns(job);

            JobController jobController = new JobController(jobServiceMock.Object);

            //Act
            var result = await jobController.GetById(id);

            //Assert
            Assert.Equal(job, result);
        }
    }
}
