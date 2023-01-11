using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobChannel.Domain.BO;
using Xunit;

namespace JobChannel.Tests.Integrations.JobOfferController
{
    public class JobOfferIntegrationTests
    {
        [Theory]
        [InlineData(1)]
        public async void GetJobOfferByIdShouldReturnOk(int id)
        {
            // Arrange
            string uri = $"api/JobOffer/{id}";
            var expectedJoOffer = new JobOffer()
            {
                Id= id
            };
        }
    }
}
