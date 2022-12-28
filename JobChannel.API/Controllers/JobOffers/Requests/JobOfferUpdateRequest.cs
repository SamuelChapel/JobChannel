using System;
using FluentValidation;
using JobChannel.Domain.Contracts;

namespace JobChannel.API.Controllers.JobOffers.Requests
{
    public record JobOfferUpdateRequest(
        int Id,
        string Title,
        string Description,
        DateTime PublicationDate,
        DateTime ModificationDate,
        string Url,
        string Salary,
        string Experience,
        string Company,
        int JobId,
        int ContractId,
        int CityId
        ) : IRequest;

    public class JobOfferUpdateRequestValidator : AbstractValidator<JobOfferUpdateRequest>
    {
        public JobOfferUpdateRequestValidator()
        {
            RuleFor(j => j.Id).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(j => j.Title).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(j => j.Description).NotNull().NotEmpty().MaximumLength(8000);
            RuleFor(j => j.PublicationDate).ExclusiveBetween(DateTime.Now.AddMonths(-6), DateTime.Now);
            RuleFor(j => j.ModificationDate).GreaterThanOrEqualTo(j => j.PublicationDate);
            RuleFor(j => j.Url).NotNull().NotEmpty().Matches("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)");
            RuleFor(j => j.Salary).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(j => j.Experience).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(j => j.Company).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(j => j.JobId).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(j => j.ContractId).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(j => j.CityId).NotNull().NotEmpty().GreaterThan(-1);
        }
    }
}
