using FluentValidation;
using JobChannel.BLL.Extensions;
using JobChannel.DAL.UOW.Repositories.JobRepositories;

namespace JobChannel.API.Controllers.JobOffers.Requests
{
    public record GetPoleEmploiJobOffersRequest(
        int Start,
        int End,
        string? CodeRome,
        string? Commune,
        int? PublieeDepuis,
        bool? EntreprisesAdaptees
        );

    public class GetPoleEmploiJobOffersRequestValidator : AbstractValidator<GetPoleEmploiJobOffersRequest>
    {
        public GetPoleEmploiJobOffersRequestValidator(IJobRepository jobRepository)
        {
            RuleFor(j => j.Start).Must((request, start) => start >= 0 && request.End >= 0 && request.End >= start && request.End - start < 150).WithErrorCode("RangeInvalid");
            RuleFor(j => j.CodeRome).CodeRome(jobRepository);
        }
    }
}