using System.Data;
using System.Threading.Tasks;
using Dapper;
using JobChannel.DAL.UOW;
using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;
using Moq;
using Moq.Dapper;
using Xunit;

namespace JobChannel.Tests.Units.Repositories
{
    public class JobRepositoryTests
    {
        [Fact]
        public async void ShouldReturnJob_WhenGetById()
        {
            // Arrange
            var dbSessionMock = new Mock<IDbSession>();
            var dbConnection = new Mock<IDbConnection>();
            dbConnection.SetupDapper(c => c.QueryFirstOrDefaultAsync<Job>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)).ReturnsAsync(new Job());
            dbSessionMock.Setup(d => d.Connection).Returns(dbConnection.Object);
            var jobRepository = new JobRepository(dbSessionMock.Object);

            // Act
            var result = await jobRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldThrowJobNotFoundException_WhenGetById()
        {
            // Arrange
            var dbSessionMock = new Mock<IDbSession>();
            var dbConnection = new Mock<IDbConnection>();
            dbConnection.SetupDapper(c => c.QueryFirstOrDefaultAsync<Job>(It.IsAny<string>(), It.IsAny<object>(), null, null, null)).Returns<Job>(null);
            dbSessionMock.Setup(d => d.Connection).Returns(dbConnection.Object);
            var jobRepository = new JobRepository(dbSessionMock.Object);

            // Act
            async Task action() => await jobRepository.GetById(1);

            // Assert
            await Assert.ThrowsAsync<JobNotFoundException>(action);
        }
    }
}
