using System;
using FluentValidation;
using JobChannel.BLL.Extensions;
using JobChannel.Domain.Contracts;

namespace JobChannel.API.Controllers.JobOffers.Requests
{
    public record JobOfferCreateRequest(
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

    public class JobOfferCreateRequestValidator : AbstractValidator<JobOfferCreateRequest>
    {
        public JobOfferCreateRequestValidator()
        {
            RuleFor(j => j.Title).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(j => j.Description).NotNull().NotEmpty().MaximumLength(8000);
            RuleFor(j => j.PublicationDate).Date().ExclusiveBetween(DateTime.Now.AddMonths(-6), DateTime.Now);
            RuleFor(j => j.ModificationDate).Date().GreaterThanOrEqualTo(j => j.PublicationDate);
            RuleFor(j => j.Url).NotNull().NotEmpty().Url();
            RuleFor(j => j.Salary).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(j => j.Experience).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(j => j.Company).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(j => j.JobId).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(j => j.ContractId).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(j => j.CityId).NotNull().NotEmpty().GreaterThan(-1);
        }
    }
}
