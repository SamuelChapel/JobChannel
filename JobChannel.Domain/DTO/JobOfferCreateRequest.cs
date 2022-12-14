using System;
using FluentValidation;
using static System.Net.WebRequestMethods;

namespace JobChannel.Domain.DTO
{
    public record JobOfferCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Url { get; set; }
        public string Salary { get; set; }
        public string Experience { get; set; }
        public string Company { get; set; }
        public int JobId { get; set; }
        public int ContractId { get; set; }
        public int CityId { get; set; }

        public JobOfferCreateRequest(
            string title,
            string description,
            DateTime publicationDate,
            DateTime modificationDate,
            string url,
            string salary,
            string experience,
            string company,
            int jobId,
            int contractId,
            int cityId)
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            ModificationDate = modificationDate;
            Url = url;
            Salary = salary;
            Experience = experience;
            Company = company;
            JobId = jobId;
            ContractId = contractId;
            CityId = cityId;
        }
    }

    public class JobOfferCreateRequestValidator : AbstractValidator<JobOfferCreateRequest>
    {
        public JobOfferCreateRequestValidator()
        {
            RuleFor(j => j.Title).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(j => j.Description).NotNull().NotEmpty().MaximumLength(8000);
            RuleFor(j => j.PublicationDate).ExclusiveBetween(DateTime.Now, DateTime.Now.AddMonths(-6));
            RuleFor(j => j.ModificationDate).GreaterThan(j => j.PublicationDate);
            RuleFor(j => j.Url).NotNull().NotEmpty().Matches(@"https ?:\/\/ (www\.)?[-a - zA - Z0 - 9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");
            RuleFor(j => j.Salary).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(j => j.Experience).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(j => j.Company).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(j => j.JobId).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(j => j.ContractId).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(j => j.CityId).NotNull().NotEmpty().GreaterThan(-1);
        }
    }
}
