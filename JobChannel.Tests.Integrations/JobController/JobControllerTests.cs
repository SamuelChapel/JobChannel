using JobChannel.Tests.Integrations.Fixtures;
using Xunit;
using JobChannel.Domain.BO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace JobChannel.Tests.Integrations.JobController
{
    public class JobControllerTests : AbstractIntegrationTest
    {
        public JobControllerTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Theory]
        [InlineData(71)]
        public async void GetJobByIdShouldReturnOk(int id)
        {
            // Arrange
            string uri = $"api/Job/{id}";
            var expectedJob = new Job()
            {
                Id = id,
                CodeRome = "M1805",
                Name = "Études et développement informatique"
            };

            // Act
            var response = await _client.GetAsync(uri);

            // Assert
            Assert.True(response.StatusCode is HttpStatusCode.OK);

            var actualBody = await response.Content.ReadFromJsonAsync<Job>();
            Assert.Equal(expectedJob, actualBody);
        }
    }
}
