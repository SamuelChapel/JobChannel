using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace JobChannel.Domain.DTO
{
    public record JobOfferFindRequest(
        IEnumerable<int>? Id_Region,
        IEnumerable<int>? Id_Department,
        IEnumerable<int>? Id_City,
        IEnumerable<int>? Id_Job,
        IEnumerable<int>? Id_Contract,
        DateTime? StartDate,
        DateTime? EndDate,
        string? SearchString
    );

    public class JobOfferFindRequestValidator : AbstractValidator<JobOfferFindRequest>
    {
        public JobOfferFindRequestValidator()
        {
            RuleFor(x => x.StartDate).Must((a, b) => b is null && a.EndDate is null || b is not null && a.EndDate is not null && b < a.EndDate)
                                     .WithErrorCode("DateIntervalInvalid");

            RuleFor(x => x.Id_Region).Must(regions => regions is null || regions.All(r => r > 0)).WithMessage("IdRegionsInvalid");
            
            RuleFor(x => x.Id_Department).Must(departments => departments is null || departments.All(d => d > 0)).WithMessage("Les départements doivent avoir une Id supérieures à 0");
            
            RuleFor(x => x.Id_City).Must(cities => cities is null || cities.All(c => c > 0)).WithMessage("Les villes doivent avoir une Id supérieures à 0");
            
            RuleFor(x => x.Id_Job).Must(jobs => jobs is null || jobs.All(j => j > 0)).WithMessage("Les métiers doivent avoir une Id supérieures à 0");
            
            RuleFor(x => x.Id_Contract).Must(contracts => contracts is null || contracts.All(c => c > 0)).WithMessage("Les contrats doivent avoir une Id supérieures à 0");
        }
    }
}
