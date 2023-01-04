using System;
using System.Collections.Generic;
using JobChannel.Domain.BO;
using Moq;

namespace JobChannel.Tests.Commons.Seeds
{
    public static class JobOfferSeed
    {
        public static IEnumerable<JobOffer> GetJobOfferData()
        {
            yield return new JobOffer
            {
                Id = 1,
                Title = "Software Developer",
                Description = "We are seeking a skilled software developer to join our team.",
                PublicationDate = new DateTime(2022, 1, 1),
                ModificationDate = new DateTime(2022, 2, 3),
                Url = "https://example.com/jobs/software-developer",
                Salary = "75,000 - 100,000 per year",
                Experience = "3+ years",
                Company = "Acme Inc.",
                Job = It.IsAny<Job>(),
                Contract = It.IsAny<Contract>(),
                City = It.IsAny<City>()
            };

            yield return new JobOffer
            {
                Id = 2,
                Title = "C# Developer",
                Description = "We are seeking a skilled C# developer to join our team.",
                PublicationDate = new DateTime(2021, 11, 15),
                ModificationDate = new DateTime(2022, 1, 2),
                Url = "https://example.com/jobs/c#-developer",
                Salary = "30000 - 800000 per year",
                Experience = "2+ years",
                Company = "Acme Inc.",
                Job = It.IsAny<Job>(),
                Contract = It.IsAny<Contract>(),
                City = It.IsAny<City>()
            };
        }
    }
}
